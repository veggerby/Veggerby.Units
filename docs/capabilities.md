# Veggerby.Units Capabilities

This document summarizes the current features of the Veggerby.Units library.

## Core Concepts

* Unit: Abstract representation of a measurement unit (basic, derived, product, division, power, prefixed, scaled, or null).
* Dimension: Physical dimension (Length L, Mass M, Time T, Electric Current I, Thermodynamic Temperature Θ, Luminous Intensity J, Amount of Substance N) with full algebra support.
* Unit System: Currently implemented systems are International (SI), Imperial, and CGS; all non‑SI units scale to SI base units for conversion.
* Measurement&lt;T&gt;: Couples a numeric value with a Unit and a Calculator&lt;T&gt; enabling generic arithmetic (int, double and decimal implemented).

## Arithmetic & Algebra

* Multiplication, division, exponentiation (^), and reduction of composite units/dimensions (e.g. m*m -> m^2, m^2/m -> m).
* Commutative & associative product handling with internal normalization so A*B == B*A.
* Automatic reduction of nested division ( (A/B)/(C/D) -> AD/BC ).
* Power expansion for composite units ( (m*s)^2 -> m^2*s^2 ).

## Prefixes & Scaling

* Metric prefixes from yocto (1E-24) to yotta (1E24).
* Custom scale units (e.g. ft relative to m, lb relative to kg) and derived units (e.g. acre, pint) defined through compositions.
* Prefix application via operator overloading: `Prefix.k * Unit.SI.m` produces kilometer.

## Conversion

* Dimension checked conversions: attempting to convert between incompatible dimensions throws a MeasurementConversionException.
* Conversion implemented by computing relative scale factors to SI base representations (e.g. 1 ft = 0.3048 m; 1 km = 1000 m).
* Supported numeric types: int (rounded), double, and decimal (added for higher precision scenarios).
* Affine (offset) conversions: temperature support via °C (Celsius) defined as an affine unit relative to Kelvin.
  * Additional Fahrenheit affine unit (°F) defined relative to Kelvin in Imperial system.
  * Guard rails: affine units cannot be multiplied, divided, prefixed or raised to exponents > 1; only direct conversions and equality/ordering comparisons are supported.

## Equality & Comparison

* Deterministic canonical equality for composite expressions via factor multiset normalization (order & lazy power distribution independent).
* Comparison operators (&lt;, &lt;=, &gt;, &gt;=, ==, !=) on Measurement&lt;T&gt; automatically align units via conversion.
* Non-throwing `TryConvertTo` for safe conversion attempts without exceptions.

## Quantity Semantics (Kinds)

The semantic layer (`QuantityKind` + `Quantity<T>`) disambiguates identical dimensions:

* Examples of distinct kinds sharing dimensions: Energy vs Torque (J), Entropy vs HeatCapacity vs SpecificHeatCapacity (J/K).
* Addition / subtraction: same-kind only and only if the kind allows direct addition/subtraction.
* Point vs Delta (TemperatureAbsolute vs TemperatureDelta) prevents accidental affine misuse.
* Explicit inference registry maps certain multiplicative/divisive relationships (e.g. Power × Time → Energy, Pressure × Volume → Energy). No transitive chaining.
* Angle is treated as a distinct kind despite being dimensionless to prevent generic scalar leakage (Torque × Angle ↔ Energy rule only).

### Quantity Comparisons

`<`, `<=`, `>`, `>=` on `Quantity<T>` require identical kinds. Cross-kind comparisons throw even if units & dimensions match. Equality also requires same kind.

### Try* Semantic Arithmetic

`TryAdd`, `TrySubtract`, `TryMultiply`, `TryDivide` return `false` (instead of throwing) when a semantic rule is missing or disallowed (e.g. Energy + Torque, Force × Velocity without registered mapping). This enables exploratory or batch processing scenarios without exception control flow.

### Registry Sealing

Call `QuantityKindInferenceRegistry.Seal()` after all custom rule registrations to lock the semantic environment. Further `Register` calls throw, ensuring determinism and preventing late dynamic alteration.

### Temperature Mean Helper

`TemperatureMean.Mean(params Quantity<double>[] absolutes)` converts each absolute temperature to Kelvin, averages, and returns an absolute in the first sample’s unit. Rejects non-absolute kinds and empty input (returns null for empty array).

### Example (Semantic Work Inference)

```csharp
using Veggerby.Units.Quantities;

var pressure = Quantity.Pressure(101325.0);  // Pa
var volume   = Quantity.Volume(0.01);        // m^3
Quantity<double>.TryMultiply(pressure, volume, out var energy).Should().BeTrue();
Console.WriteLine(energy.Kind.Name); // Energy
```

### Example (Safe Temperature Mean)

```csharp
var t1 = TemperatureQuantity.Absolute(25.0, Unit.SI.C);
var t2 = TemperatureQuantity.Absolute(77.0, Unit.Imperial.F);
var mean = TemperatureMean.Mean(t1, t2); // expressed in °C
```

## Error Handling

* UnitException for invalid operations between incompatible units.
* MeasurementConversionException for dimension mismatches in conversion requests.
* PrefixException for invalid numeric prefix factors.

## Extensibility

* Add new basic units or systems by composing existing units and/or adding ScaleUnit / DerivedUnit definitions.
* Generic calculation engine can be extended by implementing Calculator&lt;T&gt; for new numeric types.
* Internal reduction utilities (OperationUtility) centralize algebraic simplification logic.

## Not Implemented / Future Ideas

* Rich compile-time kind generics (current layer is runtime semantic). Existing semantic disambiguation is opt-in.
* Additional unit systems (US customary variations) could be added.
* Additional numeric types (BigInteger / arbitrary precision) via additional Calculator&lt;T&gt;.
* Potential future: richer temperature domain helpers (dew point, heat index) building on affine units.
* Parsing of formatted/qualified strings back into semantic kinds and dimensions (UnitParser exists for raw unit expressions only).
* Serialization helpers and culture-aware formatting.

## Example

```csharp
var distance = new DoubleMeasurement(5, Prefix.k * Unit.SI.m); // 5 km
var time = new DoubleMeasurement(30, Unit.SI.s);               // 30 s
var speed = distance / time;                                   // units: km/s
var speedInMS = (speed).ConvertTo(Unit.SI.m / Unit.SI.s);      // numeric value ~ 5000 m/s
```

---

### Runtime Feature Flags (Advanced)

For experimentation / benchmarking (all on `ReductionSettings`):

| Flag | Default | Effect |
|------|---------|--------|
| `EqualityNormalizationEnabled` | true | Enables canonical normalized factor multiset equality (recommended). |
| `LazyPowerExpansion` | false | Leaves `(Product)^n` unexpanded until required (still equal to distributed form). |
| `UseFactorVector` | false | Enables cached factor vectors on some composite nodes (allocation reduction). |
| `UseExponentMapForReduction` | false | Switches multiplication/division reduction to pooled exponent map accumulator. |
| (legacy flag removed) | - | Previous hash-bucket product equality removed in favour of canonical path. |

Generated: 2025-09-17
