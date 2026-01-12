# Mixed Formatting & Ambiguity Policy

This document specifies the deterministic Mixed formatting algorithm and the ambiguity qualification policy underlying `UnitFormat.Mixed` and `UnitFormat.Qualified`.

## 1. Goals

- Minimize token count while preserving readability.
- Prefer domain‑salient derived symbols (mechanical, electrical) before photometric / optical.
- Preserve semantic torque vs energy intent when caller supplies `QuantityKinds.Torque` (render `N·m` even though dimension equals Joule).
- Avoid misleading conflations for ambiguous symbols (J, Pa, W, H) unless a quantity kind is explicitly or implicitly presented.
- Deterministic results: same dimension vector always yields the same Mixed string for a given `QuantityKind`.

## 2. Algorithm Summary

1. Reduce the unit dimension to an exponent vector (kg, m, s, A, K, mol, cd).
2. If the vector matches a canonical derived symbol and decomposition is not forced (e.g. Weber), short‑circuit.
3. Otherwise enumerate subsets of a priority‑ordered candidate list of derived symbols. Each symbol is used at most once.
4. Score each subset:
   - Base cost = count(derived symbols) + count(leftover base factor tokens)
   - Penalties/bonuses applied for heuristics (prefer N with leftover m, reward W when Ampere is present, penalize J when a power context is clearer via W, force decomposing Wb, discourage ambiguous C·Ω pair, etc.).
5. Select the minimal cost subset (first winner on tie due to enumeration order). Build output by concatenating derived tokens (in priority order) then leftover base factors, splitting numerator/denominator.
6. If a `QuantityKind` was supplied and any derived symbol in the result is ambiguous, append `(KindName)`.
7. For torque preference: if caller kind is Torque and dimension == Joule, return `N·m` directly.

Complexity: O(2^N) where N = number of candidate derived symbols (currently bounded small). A size guard bypasses enumeration for very large exponent magnitudes (fallback to base-factor string).

## 3. Derived Symbol Priority (Excerpt)

`J, N, Pa, W, C, V, Ω, S, F, H, T, Wb, lm, lx`

Priority biases reflect mechanical/electrical prevalence and ergonomic readability. Symbols representing less common combinations (e.g., Weber) are penalized or decomposed for clarity.

## 4. Ambiguity Registry

Central mapping in `AmbiguityRegistry` lists symbols that collide across quantity kinds:

```text
J  -> Energy, Work, Heat, Torque
Pa -> Pressure, Stress
W  -> Power, RadiantFlux
H  -> Inductance, MagneticFieldStrength
```

The registry is authoritative—do not infer ambiguity structurally. When `UnitFormat.Qualified` or Mixed with a provided `QuantityKind` encounters one of these symbols, the formatter appends the explicit kind name.

### Rationale for Included Symbols

**J (Joule):** Energy and Work are conceptually equivalent (both measure capacity to do work), but Heat represents thermal energy transfer, and Torque (N·m) is dimensionally equivalent but semantically distinct (rotational vs translational). The formatter provides explicit torque handling via `N·m` when the Torque quantity kind is supplied.

**Pa (Pascal):** Pressure and Stress share the same dimensions (force per unit area) but represent different physical contexts—fluid/gas pressure vs. material deformation stress.

**W (Watt):** Power (mechanical or electrical work per unit time) and RadiantFlux (electromagnetic energy per unit time) share dimensions but differ in domain.

**H (Henry):** Inductance (electrical property) uses the derived symbol "H", while MagneticFieldStrength (A/m) also uses "H" as its quantity kind symbol in literature, though it formats as "A/m" in SI units. The ambiguity exists at the semantic level when discussing these quantities.

### Excluded Potential Ambiguities

**V:** While Voltage and Volume both use "V" as a quantity kind symbol, only Voltage has a derived SI unit symbol "V" (volt). Volume formats as "m³", so no unit formatting ambiguity exists in practice.

**Other symbols:** After comprehensive review of SI derived units and common quantity kinds, J, Pa, W, and H represent the complete set of genuine unit formatting ambiguities in this library's scope.

## 5. Mixed vs DerivedSymbols vs Qualified

| Format | Substitution | Partial Decomposition | Ambiguity Annotation | Torque Preference |
|--------|--------------|-----------------------|----------------------|-------------------|
| BaseFactors | None | N/A | None | No |
| DerivedSymbols | Full (exact only) | No | Only if caller chooses Qualified | No |
| Mixed | Greedy minimal tokens | Yes | If kind provided & ambiguous symbol present | Yes (N·m) |
| Qualified | Same as DerivedSymbols (or Mixed fallback when strict) | Only on strict fallback | Always for ambiguous symbols when kind known | Torque if Mixed fallback invoked |

## 6. Determinism & Caching

- Vector → token subset selection is deterministic due to fixed priority ordering and stable tie-breaks.
- A small bounded cache stores unqualified Mixed results to reduce repeated enumeration overhead.
- Qualification (kind annotation) is not cached (depends on the supplied kind argument).

## 7. Guardrails

- Enumeration skipped when total absolute exponent sum > 12 (cost/benefit tradeoff). Falls back to base-symbol expression.
- Wb (Weber) always decomposed in Mixed for better compositional visibility.
- No attempt is made to multiply ambiguous tokens to create new composite symbols.

## 8. Analyzer Interactions

- `VUNITS002` flags formatting of ambiguous symbols via `ToString()` / `Format()` calls lacking an explicit `UnitFormat`. Remedy: specify `UnitFormat.Qualified` (or `Mixed` if partial decomposition desired) to surface semantic intent.

## 9. Extension Guidelines

1. Update `_derived` map and `_mixedPriority` ordering.
2. If ambiguous, register it in `AmbiguityRegistry` with all legitimate quantity kinds.
3. Adjust scoring heuristics only with explicit rationale; keep penalties minimal and additive.
4. Update tests to include new ambiguity / substitution cases.

## 10. Examples

| Dimension (Simplified) | Mixed (Energy) | Mixed (Torque) | DerivedSymbols | BaseFactors |
|------------------------|----------------|----------------|----------------|-------------|
| kg·m²/s² (Joule) | J (Energy) | N·m (Torque) | J | kg·m²/s² |
| kg·m²/(s³·A) (Volt) | V | V | V | kg·m²/(s³·A) |
| kg·m²/(s²·A²) (Henry) | H (Inductance) | H (Inductance) | H | kg·m²/(s²·A²) |
| kg·m/(s²) (Newton) | N | N | N | kg·m/s² |

## 11. Future Work

- Optional strict Mixed mode enforcing maximum token length.
- Parsing pipeline to reconstruct dimensions from formatted strings (out of scope for current release).

## 12. Usage Guidelines: When to Use Qualified Formatting

### Recommended Scenarios

**Use `UnitFormat.Qualified` when:**

1. **Logging or diagnostics** where semantic intent must be explicit:
   ```csharp
   var energy = Quantity.Energy(100); // 100 J
   var torque = Quantity.Torque(100); // 100 N·m (dimensionally equivalent to J)
   logger.Info($"Energy: {energy.Format(UnitFormat.Qualified)}"); // "100 J (Energy)"
   logger.Info($"Torque: {torque.Format(UnitFormat.Qualified)}"); // "100 N·m (Torque)" or "100 J (Torque)" depending on mode
   ```

2. **API responses or data export** where consumers may not have context:
   ```csharp
   var pressure = Quantity.Pressure(101325); // Pa
   var stress = Quantity.YoungsModulus(200e9); // Pa (stress-like)
   return new
   {
       Pressure = pressure.Format(UnitFormat.Qualified), // "101325 Pa (Pressure)"
       Stress = stress.Format(UnitFormat.Qualified)      // "200000000000 Pa (Stress)"
   };
   ```

3. **Documentation examples** to teach users about quantity kind distinctions.

4. **Mixed calculations** involving ambiguous symbols where reader needs hints:
   ```csharp
   var power = voltage * current; // W (Power)
   var radiantFlux = luminosity.ConvertTo(QuantityKinds.RadiantFlux.CanonicalUnit); // W (RadiantFlux)
   Console.WriteLine($"Power: {power.Format(UnitFormat.Qualified)}");
   Console.WriteLine($"Radiant flux: {radiantFlux.Format(UnitFormat.Qualified)}");
   ```

**Use `UnitFormat.Mixed` (with quantity kind) when:**

1. You want **readable decomposition** plus qualification:
   ```csharp
   var torque = Quantity.Torque(50);
   torque.Format(UnitFormat.Mixed, QuantityKinds.Torque); // "N·m" (not "J (Torque)")
   ```

2. **Complex units** benefit from partial substitution:
   ```csharp
   var complexUnit = Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 3) * Unit.SI.A);
   UnitFormatter.Format(complexUnit, UnitFormat.Mixed, QuantityKinds.Voltage); // "V (Voltage)"
   ```

**Use `UnitFormat.DerivedSymbols` or `UnitFormat.BaseFactors` when:**

1. **No ambiguity exists** in the current context (e.g., internal calculations).
2. **Space is limited** (UI labels, compact displays).
3. **Target audience** understands the domain (engineer-to-engineer documentation).

### Performance Considerations

- **BaseFactors**: Fastest (direct symbol access, no lookups)
- **DerivedSymbols**: Fast (single dictionary lookup)
- **Mixed**: Moderate (subset enumeration + scoring, cached for common patterns)
- **Qualified**: Same as DerivedSymbols/Mixed + string concatenation when ambiguous

For hot paths, prefer `DerivedSymbols` or cache formatted strings. Qualification overhead is typically < 100 ns per call.

---

This specification is authoritative; Mixed formatting changes must stay within these constraints and update this document.
