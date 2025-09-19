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

---

This specification is authoritative; Mixed formatting changes must stay within these constraints and update this document.
