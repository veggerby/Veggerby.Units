# Veggerby.Units

[![NuGet](https://img.shields.io/nuget/v/Veggerby.Units.svg)](https://www.nuget.org/packages/Veggerby.Units)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Veggerby.Units.svg)](https://www.nuget.org/packages/Veggerby.Units)

Strongly-typed units and measurements for .NET with deterministic dimensional algebra, reduction, and safe conversions across SI ↔ Imperial.

- Structural unit algebra: products, divisions, powers, and decimal prefixes
- Deterministic reduction: cancellation, exponent aggregation, and canonical equality
- Measurement arithmetic: safe math over `Measurement<T>` with conversion helpers
- Affine-aware temperatures (K, °C, °F) with explicit semantics and guards

Targets .NET 9.0.

## Install

```bash
dotnet add package Veggerby.Units
```

## Quick Start

```csharp
using Veggerby.Units;
using Veggerby.Units.Conversion;

// Units are canonical and order-independent
var u1 = (Unit.SI.m * Unit.SI.s) ^ 2;        // (m*s)^2
var u2 = (Unit.SI.m ^ 2) * (Unit.SI.s ^ 2);
bool equal = (u1 == u2);                      // true

// Measurements retain their unit and enforce dimension integrity
var a = new DoubleMeasurement(2.0, Unit.SI.m);
var b = new DoubleMeasurement(3.0, Unit.SI.m);
var sum = a + b;                              // 5 m

// Conversions are explicit
var oneMeter = new DoubleMeasurement(1.0, Unit.SI.m);
var inFeet = oneMeter.ConvertTo(Unit.Imperial.ft); // ~3.28084 ft

// Illegal operations throw
// var bad = a + new DoubleMeasurement(1.0, Unit.SI.s); // throws UnitException
```

## Core Concepts

- Equality is canonical: `A*B == B*A`, division is negative exponents, and powers distribute where beneficial.
- Reduction is centralized: operators delegate to internal reduction to normalize shape and merge exponents.
- Add/subtract require identical units (no silent coercion). Convert measurements first if needed.
- Temperatures are affine: convert and compare absolute temperatures, but do not multiply/divide/prefix them.

## Units: Algebra and Reduction

```csharp
var m = Unit.SI.m;         // metre
var s = Unit.SI.s;         // second
var ft = Unit.Imperial.ft; // foot

var speed = m / s;         // m/s
var accel = m / (s ^ 2);   // m/s^2
var area  = m ^ 2;         // m^2

// Decimal prefixes (exact powers of ten only)
var km = 1000 * m;         // k·m (kilometre)
var mm = Prefix.m * m;     // milli·metre

// Canonical equality
(Unit.Multiply(m, s) == (s * m));        // true
((m * s) ^ 2 == (m ^ 2) * (s ^ 2));      // true
```

Notes:

- Prefix multiplication supports exact decimal factors only; unknown factors throw `PrefixException`.
- Affine temperatures (K/°C/°F) cannot be multiplied, divided, or prefixed; such operations throw `UnitException`.

## Measurements: Safe Arithmetic

```csharp
var d1 = new DoubleMeasurement(2.5, Unit.SI.m);
var d2 = new DoubleMeasurement(1.5, Unit.SI.m);
var d3 = d1 + d2;          // 4.0 m

// Different units must be converted before +/-
var m1 = new DoubleMeasurement(1.0, Unit.SI.m);
var f1 = new DoubleMeasurement(1.0, Unit.Imperial.ft);
var f1InMeters = f1.ConvertTo(Unit.SI.m);
var d4 = m1 + f1InMeters;  // ok

// Relational comparisons align units on the right to the left
bool less = m1 < f1;       // compares as m1 < f1.ConvertTo(m1.Unit)

// Unit composition via measurements
var t = new DoubleMeasurement(10.0, Unit.SI.s);
var distance = new DoubleMeasurement(20.0, Unit.SI.m);
var speed = distance / t;  // 2 m/s
```

## Conversion Helpers

Use `ConvertTo` for explicit conversions or `TryConvertTo` to avoid exceptions.

```csharp
using Veggerby.Units.Conversion;

var tempC = Temperature.Celsius(25.0);         // 25 °C
var tempF = tempC.ConvertTo(Unit.Imperial.F);  // 77 °F

DoubleMeasurement result;
bool ok = tempF.TryConvertTo(Unit.SI.C, out result); // true
```

- Dimension mismatches raise `MeasurementConversionException` in `ConvertTo` and return `false` in `TryConvertTo`.
- Conversions proceed via each unit’s base-space representation; affine pairs (°C↔K, °F↔K) use offset-aware math.

## Temperatures (Affine Units)

- Absolute temperatures (K, °C, °F) support conversions and comparisons only.
- Not allowed: multiply, divide, prefix, or raise to powers > 1.

```csharp
var c = Temperature.Celsius(20);
var f = c.ConvertTo(Unit.Imperial.F); // 68 °F

// Illegal – throws UnitException:
// var x = Unit.SI.C * Unit.SI.m;    // ❌
// var y = 1000 * Unit.SI.C;         // ❌
```

## More Information

- Reduction architecture: [docs/reduction-architecture.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/reduction-architecture.md)
- Reduction pipeline: [docs/reduction-pipeline.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/reduction-pipeline.md)
- Capabilities: [docs/capabilities.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/capabilities.md)
- Quantities overview: [docs/quantities.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/quantities.md)
- Quantity kinds: [docs/quantity-kinds.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/quantity-kinds.md)
- Performance notes: [docs/performance.md](https://github.com/veggerby/Veggerby.Units/blob/master/docs/performance.md)

## License

MIT License — see the repository `LICENSE` file.

---

Questions or ideas? Open an issue at [veggerby/Veggerby.Units](https://github.com/veggerby/Veggerby.Units).
