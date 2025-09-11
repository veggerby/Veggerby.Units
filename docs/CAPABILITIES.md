# Veggerby.Units Capabilities

This document summarizes the current features of the Veggerby.Units library.

## Core Concepts

* Unit: Abstract representation of a measurement unit (basic, derived, product, division, power, prefixed, scaled, or null).
* Dimension: Physical dimension (Length L, Mass M, Time T, Electric Current I, Thermodynamic Temperature Θ, Luminous Intensity J, Amount of Substance N) with full algebra support.
* Unit System: Currently implemented systems are International (SI) and Imperial; all non‑SI units scale to SI base units for conversion.
* Measurement&lt;T&gt;: Couples a numeric value with a Unit and a Calculator&lt;T&gt; enabling generic arithmetic (int and double implemented).

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
* Supported numeric types: int (rounded) and double.

## Equality & Comparison

* Structural equality for all unit expression trees (order independent for multiplication, identical structure for division/power).
* Comparison operators (&lt;, &lt;=, &gt;, &gt;=, ==, !=) on Measurement&lt;T&gt; automatically align units via conversion.

## Error Handling

* UnitException for invalid operations between incompatible units.
* MeasurementConversionException for dimension mismatches in conversion requests.
* PrefixException for invalid numeric prefix factors.

## Extensibility

* Add new basic units or systems by composing existing units and/or adding ScaleUnit / DerivedUnit definitions.
* Generic calculation engine can be extended by implementing Calculator&lt;T&gt; for new numeric types.
* Internal reduction utilities (OperationUtility) centralize algebraic simplification logic.

## Not Implemented / Future Ideas

* Rich physical property taxonomy (e.g. tagging J as energy/work/heat) is intentionally omitted.
* Additional unit systems (CGS, US customary variations) could be added.
* Support for decimals / BigInteger / arbitrary precision via additional Calculator&lt;T&gt;.
* Parsing of unit strings into expression trees (currently only simple ToString formatting is implemented; parsing referenced in README but not yet coded).
* Serialization helpers and culture-aware formatting.

## Example

```csharp
var distance = new DoubleMeasurement(5, Prefix.k * Unit.SI.m); // 5 km
var time = new DoubleMeasurement(30, Unit.SI.s);               // 30 s
var speed = distance / time;                                   // units: km/s
var speedInMS = (speed).ConvertTo(Unit.SI.m / Unit.SI.s);      // numeric value ~ 5000 m/s
```

---
Generated: 2025-09-11
