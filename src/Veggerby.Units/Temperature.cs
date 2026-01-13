namespace Veggerby.Units;

/// <summary>
/// Factory helpers for constructing temperature measurements with common numeric types.
/// </summary>
/// <remarks>
/// For advanced temperature calculations (dew point, heat index, humidex), see 
/// <see cref="Calculations.TemperatureDomainHelpers"/>.
/// </remarks>
public static class Temperature
{
    /// <summary>Create a double precision Celsius measurement.</summary>
    public static DoubleMeasurement Celsius(double value) => new(value, Unit.SI.C);
    /// <summary>Create a double precision Fahrenheit measurement.</summary>
    public static DoubleMeasurement Fahrenheit(double value) => new(value, Unit.Imperial.F);
    /// <summary>Create a double precision Kelvin measurement.</summary>
    public static DoubleMeasurement Kelvin(double value) => new(value, Unit.SI.K);
}