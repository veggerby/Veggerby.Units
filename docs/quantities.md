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

* Inference map (e.g. Energy / Temperature -> Entropy, Energy / Amount -> ChemicalPotential).
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

