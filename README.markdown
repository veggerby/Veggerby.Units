# Veggerby.Units

Build status: 

![Build status](https://travis-ci.org/veggerby/Veggerby.Units.svg?branch=master)

Veggerby Units is a class library to assist in handling units of measurement.

It is written in C#.

*  Conversion between unit systems (eg. SI units and Imperial Units).
*  Numeric operations (multiplication, division, etc.) will be reflected on units, eg. 4 km/2 min = 2 km/min.
*  Scaling in unit systems, eg. 2 km/min = 120 km/h (60 min/h).
*  Dimensionsare validated, i.e. it makes no sense to convert from SI "m/s" (length/time) to Imperial "in/lbs" (length/mass).
*  Equality across unit systems, e.g. 1 cm == 0.393700787 in.
*  It is valid to mix systems, e.g. density = mass / volume = 0.5 kg / 2 gal will be automatically converted to the "left-most" system, i.e kg, so we get  0.0660430131 kg / L (1 gal = 3.78541178 L).
*  String interpretation, eg. "20.00 kg/L" will be interpreted as the numerical value "20" and unit "kg/L" (M/L^3).
*  SI units are ALWAYS used as base representations, ie. all other unit systems are related to SI units. "1 gal" is represented as "3.78541178 L".

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

Properties (e.g. does a value with unit of J represent Energy, Heat or Work) 
are left out initially, since they are fairly complex, however future 
implementation of properties should leave concepts intact. Besides added 
complexity, having a complete set of properties is also not viable for 
initial version(s).

References:
http://en.wikipedia.org/wiki/Units_of_measurement

--> Dimensional Analysis must be further investigated (reduction, etc., i.e. 
is T*(L/T) ALWAYS the same as L?
http://en.wikipedia.org/wiki/Dimensional_analysis
http://en.wikipedia.org/wiki/Nondimensionalization
