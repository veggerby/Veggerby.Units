# Built-in Quantity Kinds

This page lists the built-in `QuantityKind` instances shipped with the library. Kinds disambiguate identical dimensions by semantic meaning (Energy vs Torque both J, HeatCapacity vs Entropy both J/K).

> The list is intentionally explicit (maintained alongside code) to provide stable documentation without relying on reflection at runtime.

| Name | Symbol | Canonical Unit | Dimension (formatted) | Direct + | Direct - | Difference Result |
|------|--------|----------------|-----------------------|----------|----------|-------------------|
| Energy | E | J | kg·m^2/s^2 | Yes | Yes | — |
| InternalEnergy | U | J | kg·m^2/s^2 | Yes | Yes | — |
| Enthalpy | H | J | kg·m^2/s^2 | Yes | Yes | — |
| GibbsFreeEnergy | G | J | kg·m^2/s^2 | Yes | Yes | — |
| HelmholtzFreeEnergy | A | J | kg·m^2/s^2 | Yes | Yes | — |
| Entropy | S | J/K | kg·m^2/(s^2·K) | Yes | Yes | — |
| HeatCapacity | C_p | J/K | kg·m^2/(s^2·K) | Yes | Yes | — |
| ChemicalPotential | μ | J/mol | kg·m^2/(s^2·mol) | Yes | Yes | — |
| Torque | τ | J | kg·m^2/s^2 | Yes | Yes | — |
| Angle | θ | (dimensionless) | 1 | Yes | Yes | — |
| TemperatureDelta | ΔT | K | K | Yes | Yes | — |
| TemperatureAbsolute | T_abs | K | K | No | No | TemperatureDelta |

Legend:

* Direct + / - indicate whether the kind allows same-kind addition / subtraction producing the same kind.
* Difference Result indicates a Point − Point → Vector mapping (absolute to delta) where applicable.

## Programmatic Enumeration (Optional)

You can enumerate available kinds via reflection if you dynamically load extensions:

```csharp
var kinds = typeof(Veggerby.Units.Quantities.QuantityKinds)
    .Assembly
    .GetTypes()
    .Where(t => t.IsClass && t.IsAbstract && t.IsSealed && t.Name == "QuantityKinds") // partial static class
    .SelectMany(t => t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
    .Where(f => f.FieldType == typeof(Veggerby.Units.Quantities.QuantityKind))
    .Select(f => (QuantityKind)f.GetValue(null));
```

> Note: The library ships with a curated set; adding domain-specific kinds is as simple as defining new static readonly `QuantityKind` instances inside a partial `QuantityKinds` class in your own assembly.

## Adding Your Own

1. Define a canonical unit expression (ensuring dimensional correctness).
2. Decide whether direct + / - should be allowed.
3. If representing an absolute (point) value whose differences are meaningful (like temperature), supply a `differenceResultKind`.
4. (Optional) Register inference rules if it participates in multiplicative/divisive semantics.
5. Provide factory helpers for ergonomics.

Keep rules minimal and explicit—avoid overloading the registry with speculative mappings.
