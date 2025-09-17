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
