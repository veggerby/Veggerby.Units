# Temperature Domain Helpers

The `TemperatureDomainHelpers` class provides meteorological calculations that correctly handle absolute vs delta temperature semantics.

## Overview

These helpers implement well-established formulas for:
- **Dew Point**: Temperature at which air becomes saturated with moisture
- **Heat Index**: Apparent temperature combining heat and humidity effects
- **Humidex**: Canadian humidity index for perceived temperature

All helpers:
- Accept absolute temperature measurements in any scale (°C, °F, K)
- Return results in the same unit as the input
- Validate relative humidity range (0-100%)
- Are tested across all temperature scales

## Usage Examples

### Dew Point Calculation

Calculate the dew point using the Magnus-Tetens approximation:

```csharp
using Veggerby.Units;
using Veggerby.Units.Calculations;

// Create temperature measurement (can use any scale)
var temp = new DoubleMeasurement(20.0, Unit.SI.C);      // 20°C
var relativeHumidity = 50.0;                             // 50% RH

// Calculate dew point
var dewPoint = TemperatureDomainHelpers.DewPoint(temp, relativeHumidity);
// Result: ~9.3°C (in same unit as input)

// Works with Fahrenheit too
var tempF = new DoubleMeasurement(68.0, Unit.Imperial.F);
var dewPointF = TemperatureDomainHelpers.DewPoint(tempF, 50.0);
// Result: ~48.7°F
```

**Valid Range**: Accurate for typical atmospheric conditions (-40°C to 50°C, 1% to 100% RH)

**Formula Source**: Magnus-Tetens approximation (Alduchov & Eskridge, 1996)

### Heat Index Calculation

Calculate apparent temperature (feels-like temperature) using the NWS/Rothfusz regression:

```csharp
// Heat index is meaningful for hot, humid conditions
var hotTemp = new DoubleMeasurement(95.0, Unit.Imperial.F);  // Hot day
var humidity = 60.0;                                          // Moderate humidity

var heatIndex = TemperatureDomainHelpers.HeatIndex(hotTemp, humidity);
// Result: ~114°F (feels significantly hotter than actual)

// Below threshold conditions return original temperature
var mildTemp = new DoubleMeasurement(75.0, Unit.Imperial.F);
var heatIndexMild = TemperatureDomainHelpers.HeatIndex(mildTemp, 50.0);
// Result: 75°F (outside valid range, returns input)
```

**Valid Range**: Temperatures above 80°F (27°C) and relative humidity above 40%

**Formula Source**: National Weather Service (Rothfusz, 1990)

### Humidex Calculation

Calculate the Canadian humidity index:

```csharp
var temp = new DoubleMeasurement(30.0, Unit.SI.C);      // Warm day
var humidity = 70.0;                                     // High humidity

var humidex = TemperatureDomainHelpers.Humidex(temp, humidity);
// Result: ~41°C (feels much warmer than actual)
```

**Interpretation**: Humidex values indicate perceived temperature:
- 20-29: Comfortable
- 30-39: Some discomfort
- 40-45: Great discomfort
- 46+: Dangerous

**Formula Source**: Environment and Climate Change Canada (Masterson & Richardson, 1979)

## Error Handling

All helpers validate the relative humidity input:

```csharp
var temp = new DoubleMeasurement(25.0, Unit.SI.C);

// These will throw ArgumentException
try
{
    var dp1 = TemperatureDomainHelpers.DewPoint(temp, -10.0);  // Negative
    var dp2 = TemperatureDomainHelpers.DewPoint(temp, 105.0);  // > 100%
}
catch (ArgumentException ex)
{
    // "Relative humidity must be between 0 and 100."
}
```

## Temperature Scale Consistency

Results are always returned in the same unit as the input:

```csharp
var tempC = new DoubleMeasurement(20.0, Unit.SI.C);
var tempF = new DoubleMeasurement(68.0, Unit.Imperial.F);
var tempK = new DoubleMeasurement(293.15, Unit.SI.K);

var dewPointC = TemperatureDomainHelpers.DewPoint(tempC, 50.0);  // Returns °C
var dewPointF = TemperatureDomainHelpers.DewPoint(tempF, 50.0);  // Returns °F
var dewPointK = TemperatureDomainHelpers.DewPoint(tempK, 50.0);  // Returns K

// All represent the same dew point, just in different scales
```

## Absolute vs Delta Semantics

These helpers use **absolute temperature** measurements (actual temperatures, not differences):

```csharp
// ✓ Correct: Use absolute temperature measurements
var temp = new DoubleMeasurement(25.0, Unit.SI.C);
var dewPoint = TemperatureDomainHelpers.DewPoint(temp, 60.0);

// ✗ Incorrect: Don't use temperature deltas
// These helpers expect actual temperatures, not differences
```

The helpers correctly handle affine temperature conversions internally, converting between scales as needed for calculation while preserving the input scale for the result.

## See Also

- `Temperature` class for simple temperature measurement factory methods
- `TemperatureQuantity` and `TemperatureOps` for semantic temperature operations
- `AffineUnit` for understanding temperature scale conversions
