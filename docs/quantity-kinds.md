# Built-in Quantity Kinds

This page lists the built-in `QuantityKind` instances shipped with the library. Kinds disambiguate identical dimensions by semantic meaning (Energy vs Torque both J, HeatCapacity vs Entropy both J/K).

> The list is intentionally explicit (maintained alongside code) to provide stable documentation without relying on reflection at runtime.

| Name | Symbol | Canonical Unit | Dimension (formatted) | Direct + | Direct - | Difference Result | Tags (subset) |
|------|--------|----------------|-----------------------|----------|----------|-------------------|--------------|
| Energy | E | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction |
| InternalEnergy | U | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| Enthalpy | H | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| GibbsFreeEnergy | G | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| HelmholtzFreeEnergy | A | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| Entropy | S | J/K | kg·m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| HeatCapacity | C_p | J/K | kg·m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| ChemicalPotential | μ | J/mol | kg·m^2/(s^2·mol) | Yes | Yes | — | Domain.Thermodynamic |
| Torque | τ | J | kg·m^2/s^2 | Yes | Yes | — | Domain.Mechanical |
| Angle | θ | (dimensionless) | 1 | Yes | Yes | — | Form.Dimensionless, Domain.Mechanical |
| TemperatureDelta | ΔT | K | K | Yes | Yes | — | Domain.Thermodynamic |
| TemperatureAbsolute | T_abs | K | K | No | No | TemperatureDelta | Domain.Thermodynamic |
| Power | P | W (J/s) | kg·m^2/s^3 | Yes | Yes | — | — |
| Force | F | N (kg·m/s^2) | kg·m/s^2 | Yes | Yes | — | — |
| Pressure | p | Pa (N/m^2) | kg/(m·s^2) | Yes | Yes | — | — |
| Volume | V | m^3 | m^3 | Yes | Yes | — | — |
| Area | A_r | m^2 | m^2 | Yes | Yes | — | — |
| Velocity | v | m/s | m/s | Yes | Yes | — | — |
| Acceleration | a | m/s^2 | m/s^2 | Yes | Yes | — | — |
| Momentum | p_m | kg·m/s | kg·m/s | Yes | Yes | — | — |
| EnergyDensity | u | J/m^3 | kg/(m·s^2) | Yes | Yes | — | — |
| SpecificHeatCapacity | c_p | J/(kg·K) | m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| SpecificEntropy | s | J/(kg·K) | m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| ElectricCharge | Q | C (A·s) | A·s | Yes | Yes | — | Domain.Electromagnetic |
| ElectricCurrent | I | A | A | Yes | Yes | — | Domain.Electromagnetic |
| Voltage | V | V (J/C) | kg·m^2/(s^3·A) | Yes | Yes | — | Domain.Electromagnetic |
| Resistance | R | Ω (V/A) | kg·m^2/(s^3·A^2) | Yes | Yes | — | Domain.Electromagnetic |
| Conductance | G | S (1/Ω) | s^3·A^2/(kg·m^2) | Yes | Yes | — | Domain.Electromagnetic |
| Capacitance | C | F (C/V) | s^4·A^2/(kg·m^2) | Yes | Yes | — | Domain.Electromagnetic |
| Inductance | L | H (V·s/A) | kg·m^2/(s^2·A^2) | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFlux | Φ | Wb (V·s) | kg·m^2/(s^2·A) | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFluxDensity | B | T (Wb/m^2) | kg/(s^2·A) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricChargeDensity | ρ_q | C/m^3 | A·s/m^3 | Yes | Yes | — | Domain.Electromagnetic |
| ElectricFieldStrength | E_f | V/m | kg·m/(s^3·A) | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFieldStrength | H_f | A/m | A/m | Yes | Yes | — | Domain.Electromagnetic |
| Permittivity | ε | F/m | s^4·A^2/(kg·m^3) | Yes | Yes | — | Domain.Electromagnetic |
| Permeability | μ_0 | H/m | kg·m/(s^2·A^2) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricDipoleMoment | p_e | C·m | A·s·m | Yes | Yes | — | Domain.Electromagnetic |
| MagneticDipoleMoment | m_e | A·m^2 | A·m^2 | Yes | Yes | — | Domain.Electromagnetic |
| ElectricPotentialEnergy | U_e | J | kg·m^2/s^2 | Yes | Yes | — | Energy, Domain.Electromagnetic |
| PowerSpectralDensity | S_p | W/Hz | kg·m^2/s^2 | Yes | Yes | — | Domain.Electromagnetic |

Legend:

* Direct + / - indicate whether the kind allows same-kind addition / subtraction producing the same kind.
* Difference Result indicates a Point − Point → Vector mapping (absolute to delta) where applicable.
* Tags column lists a representative subset (kinds may carry more tags over time; absence of a tag does not imply prohibition—only semantic labeling).

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

## Dimensional Match ≠ Same Kind

Multiple kinds intentionally share identical dimensions (Energy vs Torque, Energy vs Work-like terms, Entropy vs HeatCapacity vs SpecificHeatCapacity). This prevents silent cross-domain aggregation and preserves domain intent. Always choose the kind that matches the semantic role; adding different kinds (even with identical units) throws to surface potential modeling mistakes.

## Electromagnetic Canonical Unit Corrections

The electromagnetic kinds above reflect corrected canonical unit formulations (September 2025) resolving prior exponent inaccuracies:

* Resistance (R): Energy / (A^2·s) → kg·m^2/(s^3·A^2)
* Conductance (G): inverse of Resistance → s^3·A^2/(kg·m^2)
* Capacitance (C): (A·s)^2 / Energy → s^4·A^2/(kg·m^2)
* Inductance (L): Energy / A^2 → kg·m^2/(s^2·A^2)

Derivations leverage: V = J/C, C = A·s, Ω = V/A, F = C/V, H = V·s/A. Prior docs used inconsistent time exponents; code and tests now align with SI base analysis. No breaking API changes were introduced.

## Inference Registry Sealing & Test Reset

`QuantityKindInferenceRegistry.Seal()` freezes rule registration for deterministic semantics. Production code should register custom rules during startup then call `Seal()` once. The internal method `ResetForTests()` (not public API) is used only within the test suite to unseal and clear rules between tests requiring isolated registration scenarios (e.g. conflict detection). Avoid relying on reset semantics in application code.

Lifecycle pattern:

```csharp
QuantityKindInferenceRegistry.Register(/* custom rule */);
// ... additional registrations
QuantityKindInferenceRegistry.Seal();
```

After sealing, further `Register` calls throw. This defends against late plugin or module load altering previously validated semantic algebra.
