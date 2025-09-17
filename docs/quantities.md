# Quantity Kinds (Semantic Layer)

Dimensions guarantee algebraic correctness but not meaning. "Energy" and "Torque" share the same dimension (J or N·m) while representing distinct physical concepts. This semantic layer allows you to tag measurements with an explicit *QuantityKind* to disambiguate identical dimensions.

## Goals

* Preserve existing unit & dimension reduction logic (no changes to core).
* Provide optional runtime semantic validation (strict dimension check).
* Enable opt-in enforcement for additive operations (require same kind).
* Remain additive and backward compatible.

### Core Types

| Type | Purpose |
|------|---------|
| `QuantityKind` | Describes a semantic classification (Energy, Torque, Entropy). |
| `QuantityKinds` | Registry of common built-in kinds. |
| `Quantity<T>` | Wraps a `Measurement<T>` with semantic kind metadata. |
| `Quantity` (factories) | Intent-first helpers (Energy(…), Gibbs(…), Entropy(…)). |

### Example

```csharp
var H = Quantity.Enthalpy(1250.0);          // 1250 J [Enthalpy (H)]
var S = Quantity.Entropy(12.5);             // 12.5 J/K [Entropy (S)]
var T = new DoubleMeasurement(298.15, Unit.SI.K);
var TS = new DoubleMeasurement(T.Value * S.Measurement.Value, Unit.SI.J);
var G  = Quantity.Gibbs(H.Measurement.Value - TS.Value); // G = H – T*S
```

### Addition Rules

```csharp
var e1 = Quantity.Energy(10);
var e2 = Quantity.Energy(5);
var sum = Quantity<double>.Add(e1, e2, requireSameKind: true); // ok
var torque = Quantity.Torque(3);
// Throws: different kinds when enforcement enabled
Quantity<double>.Add(e1, torque, requireSameKind: true);
```

### Strict Dimension Check

```csharp
// Throws (entropy expects J/K dimension)
Quantity.Of(1.0, Unit.SI.J, QuantityKinds.Entropy, strict: true);

// Succeeds (dimensions match; both J/K)
Quantity.Entropy(2.5);
```

### Future Extensions

* Compile-time generics (marker interfaces per kind) for stronger static safety.
* Pluggable registry for domain-specific kinds (biomedical, astrophysics, etc.).

### Non-Goals

* Replacing core dimensional analysis.
* Implicit semantic coercion (always explicit, opt-in).

---
Generated: 2025-09-17

## Temperature Semantics

Absolute temperatures are affine (offset + scale) while temperature differences are purely linear. To avoid accidental misuse (e.g. averaging or directly summing absolute temperatures) the semantic layer models two distinct kinds:

* `QuantityKinds.TemperatureAbsolute` (symbol `T_abs`) – Absolute temperatures (K, °C, °F). Direct + / - is disallowed.
* `QuantityKinds.TemperatureDelta` (symbol `ΔT`) – Temperature differences (canonical K). Linear and freely addable/subtractable within the delta kind.

### Creating Temperature Quantities

```csharp
var tC = TemperatureQuantity.Absolute(25.0, Unit.SI.C);   // 25 °C (absolute)
var tF = TemperatureQuantity.Absolute(77.0, Unit.Imperial.F); // 77 °F (absolute)
var d5 = TemperatureQuantity.Delta(5.0);                  // 5 K delta (ΔT)
```

### Computing a Delta

Use `TemperatureOps.Delta(a, b)` which returns `a - b` as a `ΔT` in Kelvin scale (convert if needed):

```csharp
var t70F = TemperatureQuantity.Absolute(70.0, Unit.Imperial.F);
var t60F = TemperatureQuantity.Absolute(60.0, Unit.Imperial.F);
var dF = TemperatureOps.Delta(t70F, t60F); // 10 °F difference
// Convert to K (10 °F -> 5.555... K)
var dK = dF.Measurement.ConvertTo(Unit.SI.K);
```

### Applying a Delta to an Absolute

Use `TemperatureOps.AddDelta(absolute, delta)` – the result is expressed in the original absolute unit:

```csharp
var t20C = TemperatureQuantity.Absolute(20.0, Unit.SI.C);
var d5K = TemperatureQuantity.Delta(5.0); // 5 K
var t25C = TemperatureOps.AddDelta(t20C, d5K); // 25 °C
```

### Operator Guardrails

`Quantity<T>.operator +` and `operator -` enforce identical kinds and then consult the kind's `AllowDirectAddition` / `AllowDirectSubtraction` flags. For `TemperatureAbsolute` both are false, so `t1 + t2` (two absolutes) throws, guiding users to compute a delta (`ΔT = t1 - t2` conceptually) via `TemperatureOps.Delta` and then intentionally apply it with `AddDelta`.

This separation prevents subtle mistakes (e.g. summing °C values) while keeping the core dimensional reducer free of domain-specific rules.

### Rationale

* Affine units cannot be naively added/subtracted for semantic meaning (offset distortion).
* Deltas are dimensionally identical but semantically distinct; modeling them separately preserves intent.
* The approach is extensible (additional affine semantics like dates, times, energies with reference baselines, etc.).

## Semantic Inference (Multiply / Divide)

Certain quantity relationships are so standard that preserving pure dimensional reduction alone leaves ambiguity (e.g. Energy vs Torque both J). The inference registry supplies *explicit*, opt-in semantic mappings of the form:

```text
LeftKind <op> RightKind => ResultKind
```

Currently seeded rules:

| Rule | Description |
|------|-------------|
| Entropy * TemperatureAbsolute => Energy | T*S work/heat term (commutative) |
| Energy / TemperatureAbsolute => Entropy | Inverse of above |
| Energy / Entropy => TemperatureAbsolute | Rearranged form |
| Torque * Angle => Energy | Rotational work τ·θ |
| Energy / Angle => Torque | Inverse |
| Energy / Torque => Angle | Rearranged |
| Power * Time => Energy | P·t work (commutative) |
| Energy / Time => Power | Inverse definition |
| Energy / Power => Time | Rearranged |
| Force * Length => Energy | Work W = F·d (commutative) |
| Energy / Length => Force | Inverse |
| Energy / Force => Length | Rearranged |
| Pressure * Volume => Energy | p·V work (commutative) |
| Energy / Volume => Pressure | Inverse |
| Energy / Pressure => Volume | Rearranged |
| Pressure * Area => Force | F = p·A (commutative) |
| Force / Area => Pressure | Inverse |
| Force / Pressure => Area | Rearranged |

All multiplicative rules marked commutative automatically install the symmetric mapping. Division is non‑commutative.

### Fallback Behavior

If no rule exists:

* Multiply / divide by a dimensionless scalar `Measurement<T>` uses scalar operators (preserving vector-like kinds only).
* Quantity × Quantity: if one operand is dimensionless (unit or dimension) and the other is *not* a point-like kind, the non-dimensionless kind is preserved. Point-like (absolute) kinds require an explicit inference rule.
* Otherwise an `InvalidOperationException` is thrown to surface ambiguous semantics early.

### Extending

Call `QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, op, right, result, Commutative:true/false))` during application startup. Conflicts throw by default (`StrictConflictDetection = true`). Set `StrictConflictDetection = false` prior to registering to allow benign duplicate mappings (mapping to same result) or intentional overwrites.

Enumerate active rules with `QuantityKindInferenceRegistry.EnumerateRules()` for diagnostics or documentation.

### Design Intent

* Keeps semantic algebra explicit and reviewable (no hidden heuristics).
* Maintains core purity: unit reduction and equality are untouched.
* Scales incrementally; unknown composites fail fast instead of guessing.

> Single-step only: the registry resolves only *direct* mappings. It never chains results transitively (e.g. Pressure × Length × Area will not infer via intermediate Force). If a composite is meaningful, register an explicit rule or decompose intentionally.

## Quantity Arithmetic: Supported vs Rejected

| Operation | Supported? | Result / Behavior | Notes |
|-----------|------------|-------------------|-------|
| SameKind + (direct) | Yes (if `AllowDirectAddition`) | Same kind | Energy + Energy; TemperatureDelta + TemperatureDelta |
| SameKind - (direct) | Yes (if `AllowDirectSubtraction`) | Same kind | Entropy - Entropy |
| Point - Point (same) | Yes | DifferenceResultKind | AbsoluteTemperature - AbsoluteTemperature → TemperatureDelta |
| Point ± Vector (its delta) | Yes | Point kind | AbsoluteTemperature ± TemperatureDelta → AbsoluteTemperature |
| Vector ± Vector (same) | Yes | Vector kind | ΔT + ΔT |
| Cross-kind + / - | No | Throws | Even if dimensions match (Energy + Torque) |
| Quantity * Quantity | Conditional | Inferred kind or preserved other via dimensionless fallback | Absolute kinds require rule |
| Quantity / Quantity | Conditional | Inferred kind or preserved left via dimensionless divisor | |
| Scalar (dimensionless Measurement) * VectorKind | Yes | Same vector kind | Prevents scaling absolutes |
| Scalar (dimensionless Measurement) * AbsoluteKind | No | Throws | Must convert to delta first |
| Power (Quantity ^ n) | No (by design) | N/A | Apply power to underlying measurement then wrap manually |
| Implicit chain inference | No | Throws if missing direct rule | No multi-step deduction |
| Angle acting as pure scalar | Limited | Only via explicit rule or dimensionless fallback of other | Angle preserved as distinct kind |

### Why Not Power / Generic Function Lifting?

#### Angle Is Not A Generic Scalar

Although angle is dimensionless in SI, it is **not** treated as an interchangeable scalar here. Angles carry affine/periodic semantics (wrapping at 2π, directional context) and appear in explicit physical work relationships (Torque × Angle → Energy). Allowing Angle to silently behave like an unlabelled scalar in arbitrary multiplications would erase this meaning and permit accidental semantic leakage (e.g. scaling unrelated kinds by a radian value). Therefore Angle only participates via explicit inference rules (e.g. torque work) and is rejected as a passive fallback scalar.

Raising a semantic quantity to a power frequently changes its meaning (Area vs Length^2, Energy^0.5 → sqrt(E) with no standard semantic alias). Absent universally accepted semantics, the library defers to explicit wrapping by user code.

### Why Block Automatic Mixed Addition?

Energy + Torque is dimensionally fine (both J) but typically meaningless; forcing an explicit decision prevents subtle domain errors (e.g. accidentally summing rotational and translational contributions labelled differently).

### Extending Safely

1. Introduce a new `QuantityKind` with canonical unit.
2. (Optional) Provide factories for ergonomic creation.
3. If it participates in point↔vector semantics, supply `differenceResultKind` and set direct add/sub flags accordingly.
4. Register inference rules explicitly; keep them minimal and domain-reviewed.
5. Add tests: happy path, absence (throws), and conflict detection if adding overlapping rules.

### Conflict Detection Strategy

`QuantityKindInferenceRegistry.StrictConflictDetection` defaults to true: registering a different result for an existing (Left, Op, Right) triple throws. Duplicate idempotent registrations (same result) pass silently. Set to false only in controlled initialization scenarios where later modules intentionally override a default mapping.

### Debugging Inference

Enumerate active canonical rules:

```csharp
foreach (var rule in QuantityKindInferenceRegistry.EnumerateRules())
{
    Console.WriteLine($"{rule.Left.Name} {rule.Operator} {rule.Right.Name} => {rule.Result.Name}");
}
```

If an unexpected exception occurs for multiplication/division:

1. Confirm both units reduce as expected (dimensionless fallback requires truly unitless operand).
2. Check for point-like kinds: absolutes require explicit inference.
3. Verify rule registration ordering (ensure your custom rule executes before first arithmetic call).
4. Inspect conflicts: if overriding a seed rule set `StrictConflictDetection = false` before registration.

---

---
