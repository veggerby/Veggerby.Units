# Veggerby.Units

Build status:

[![Build Status](https://travis-ci.org/veggerby/Veggerby.Units.svg?branch=master)](https://travis-ci.org/veggerby/Veggerby.Units)

Veggerby Units is a C# class library for algebraic unit expressions, dimensional analysis, and runtime-safe measurement arithmetic.

* Conversion between unit systems (e.g. SI ↔ Imperial) with dimensional safety.
* Numeric operations (multiplication, division, power) reflect on units, e.g. 4 km / 2 min = 2 km/min.
* Scaling across units, e.g. 2 km/min = 120 km/h (60 min/h).
* Dimensions are validated; converting from SI m/s (L/T) to Imperial in/lb (L/M) raises an exception.
* Equality across unit systems, e.g. 1 cm == 0.393700787 in.
* Mixed systems auto-reduce; density = 0.5 kg / 2 gal converts internally to SI: 0.0660430131 kg/L (1 gal = 3.78541178 L).
* (Planned) String interpretation/parsing (currently only formatting via ToString()).
* SI units are always base representations; all other systems scale relative to SI (e.g. 1 gal = 3.78541178 L).

## Quick Start

```csharp
// distance: 5 kilometers
var distance = new DoubleMeasurement(5, Prefix.k * Unit.SI.m);

// time: 30 seconds
var time = new DoubleMeasurement(30, Unit.SI.s);

// speed: km/s
var speed = distance / time; // value: 0.166666..., unit: km/s

// Convert to m/s
var speedMS = speed.ConvertTo(Unit.SI.m / Unit.SI.s); // ~166.666 m/s
```

For an overview of all capabilities see `docs/CAPABILITIES.md`.

Let's explain this top-down:

Values have Units, eg. 1 kg

Units are related to a unit system eg. SI units or Imperial Units

Units are composable, eg. we can calculate with units
    "kg * m / s^2" = Newton (N)

Numerical operations on values w. units are reflected in units, e.g.
    4 km/2 min = 2 km/min

Units are related to Dimensions (there are 8 basic dimensions), eg.
    m / s = Length / Time.

Dimensions are the "cornerstone" for property validation

So 1 J is composed of:

```text
Unit value
+- value = 1
+- unit = J
|  +- derived unit
|  +- dimension = M*L^2/T^2
|  +- system = SI
|  +- composition = N * m
|  |  +- unit = N
|  |  |  +- derived unit
|  |  |  +- composition = kg*m/s^2
|  |  |  |  +- ...
|  |  |  +- dimension = M*L/T^2
|  |  |  +- system = SI
|  |  +- unit = m
|  |  |  +- base unit
|  |  |  +- dimension = L
|  |  |  +- system = SI
```

Properties (e.g. does a value with unit of J represent Energy, Heat or Work)
are left out initially, since they are fairly complex; future implementation
should leave concepts intact. Besides added complexity, having a complete set
of properties is also not viable for initial version(s).

References:
<https://en.wikipedia.org/wiki/Units_of_measurement>

--> Dimensional Analysis must be further investigated (reduction, etc., i.e.
is T*(L/T) ALWAYS the same as L?
<https://en.wikipedia.org/wiki/Dimensional_analysis>
<https://en.wikipedia.org/wiki/Nondimensionalization>
