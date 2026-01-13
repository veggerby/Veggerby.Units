using System;

using Veggerby.Units.Conversion;

namespace Veggerby.Units.Calculations;

/// <summary>
/// Domain-specific temperature calculations that correctly handle absolute vs delta temperature semantics.
/// All methods accept absolute temperature measurements and return appropriate absolute or delta results.
/// </summary>
/// <remarks>
/// These helpers implement well-established meteorological formulas for:
/// <list type="bullet">
/// <item><description>Dew point calculation using Magnus-Tetens approximation</description></item>
/// <item><description>Heat index (apparent temperature) using NWS/Rothfusz regression</description></item>
/// <item><description>Humidex (Canadian humidity index)</description></item>
/// </list>
/// All formulas are validated against authoritative meteorological sources and tested across
/// temperature scales (°C, °F, K).
/// </remarks>
public static class TemperatureDomainHelpers
{
    /// <summary>
    /// Calculates the dew point temperature using the Magnus-Tetens approximation.
    /// </summary>
    /// <param name="temperature">Absolute ambient temperature.</param>
    /// <param name="relativeHumidity">Relative humidity as a percentage (0-100).</param>
    /// <returns>Absolute dew point temperature in the same unit as input temperature.</returns>
    /// <exception cref="ArgumentException">When relative humidity is outside valid range [0, 100].</exception>
    /// <remarks>
    /// Formula source: Magnus-Tetens approximation, commonly used in meteorology.
    /// Valid for typical atmospheric conditions (-40°C to 50°C, 1% to 100% RH).
    /// The formula uses:
    /// <code>
    /// γ(T,RH) = ln(RH/100) + (b·T)/(c+T)
    /// Td = (c·γ)/(b-γ)
    /// where b ≈ 17.27, c ≈ 237.7°C for temperatures above 0°C
    /// </code>
    /// Reference: Alduchov, O. A., and R. E. Eskridge, 1996: Improved Magnus form approximation of saturation vapor pressure.
    /// </remarks>
    public static DoubleMeasurement DewPoint(DoubleMeasurement temperature, double relativeHumidity)
    {
        if (relativeHumidity < 0 || relativeHumidity > 100)
        {
            throw new ArgumentException("Relative humidity must be between 0 and 100.", nameof(relativeHumidity));
        }

        // Convert to Celsius for calculation (Magnus-Tetens formula uses Celsius)
        var tempC = temperature.ConvertTo(Unit.SI.C);
        var T = tempC.Value;

        // Magnus-Tetens constants
        const double b = 17.27;
        const double c = 237.7;

        // Calculate gamma
        var gamma = Math.Log(relativeHumidity / 100.0) + ((b * T) / (c + T));

        // Calculate dew point in Celsius
        var dewPointC = (c * gamma) / (b - gamma);

        // Return in original unit
        var resultC = new DoubleMeasurement(dewPointC, Unit.SI.C);
        var converted = resultC.ConvertTo(temperature.Unit);
        return new DoubleMeasurement(converted.Value, temperature.Unit);
    }

    /// <summary>
    /// Calculates the heat index (apparent temperature) using the NWS/Rothfusz regression equation.
    /// </summary>
    /// <param name="temperature">Absolute ambient temperature.</param>
    /// <param name="relativeHumidity">Relative humidity as a percentage (0-100).</param>
    /// <returns>Absolute heat index temperature in the same unit as input temperature.</returns>
    /// <exception cref="ArgumentException">When relative humidity is outside valid range [0, 100].</exception>
    /// <remarks>
    /// Formula source: National Weather Service (Rothfusz regression, 1990).
    /// Valid for temperatures above 80°F (27°C) and relative humidity above 40%.
    /// For conditions outside this range, returns the input temperature.
    /// The full Rothfusz regression includes multiple correction terms for accuracy.
    /// <para>
    /// Reference: Rothfusz, L.P., 1990: The Heat Index "Equation" (or, More Than You Ever Wanted to Know About Heat Index).
    /// National Weather Service Technical Attachment SR 90-23.
    /// </para>
    /// </remarks>
    public static DoubleMeasurement HeatIndex(DoubleMeasurement temperature, double relativeHumidity)
    {
        if (relativeHumidity < 0 || relativeHumidity > 100)
        {
            throw new ArgumentException("Relative humidity must be between 0 and 100.", nameof(relativeHumidity));
        }

        // Convert to Fahrenheit for calculation (NWS formula uses Fahrenheit)
        var tempF = temperature.ConvertTo(Unit.Imperial.F);
        var T = tempF.Value;
        var RH = relativeHumidity;

        // Heat index is only meaningful for high temperatures and humidity
        if (T < 80.0 || RH < 40.0)
        {
            // Return original temperature for conditions outside valid range
            return temperature;
        }

        // Rothfusz regression coefficients
        const double c1 = -42.379;
        const double c2 = 2.04901523;
        const double c3 = 10.14333127;
        const double c4 = -0.22475541;
        const double c5 = -0.00683783;
        const double c6 = -0.05481717;
        const double c7 = 0.00122874;
        const double c8 = 0.00085282;
        const double c9 = -0.00000199;

        // Calculate heat index using full regression
        var HI = c1
            + (c2 * T)
            + (c3 * RH)
            + (c4 * T * RH)
            + (c5 * T * T)
            + (c6 * RH * RH)
            + (c7 * T * T * RH)
            + (c8 * T * RH * RH)
            + (c9 * T * T * RH * RH);

        // Return in original unit
        var resultF = new DoubleMeasurement(HI, Unit.Imperial.F);
        var converted = resultF.ConvertTo(temperature.Unit);
        return new DoubleMeasurement(converted.Value, temperature.Unit);
    }

    /// <summary>
    /// Calculates the humidex (Canadian humidity index) combining temperature and humidity.
    /// </summary>
    /// <param name="temperature">Absolute ambient temperature.</param>
    /// <param name="relativeHumidity">Relative humidity as a percentage (0-100).</param>
    /// <returns>Humidex value (dimensionless, but expressed in same unit as temperature for interpretation).</returns>
    /// <exception cref="ArgumentException">When relative humidity is outside valid range [0, 100].</exception>
    /// <remarks>
    /// Formula source: Environment and Climate Change Canada.
    /// Humidex = T + 0.5555 × (e - 10.0)
    /// where e is the vapor pressure in hPa, calculated from temperature and relative humidity.
    /// <para>
    /// The humidex is a dimensionless index but is expressed in the same unit as temperature
    /// for ease of interpretation (e.g., a humidex of 40 means it "feels like" 40°C).
    /// </para>
    /// <para>
    /// Reference: Masterson, J. and F. Richardson, 1979: Humidex, A Method of Quantifying Human Discomfort
    /// Due to Excessive Heat and Humidity. Environment Canada, Atmospheric Environment Service.
    /// </para>
    /// </remarks>
    public static DoubleMeasurement Humidex(DoubleMeasurement temperature, double relativeHumidity)
    {
        if (relativeHumidity < 0 || relativeHumidity > 100)
        {
            throw new ArgumentException("Relative humidity must be between 0 and 100.", nameof(relativeHumidity));
        }

        // Convert to Celsius for calculation (Humidex formula uses Celsius)
        var tempC = temperature.ConvertTo(Unit.SI.C);
        var T = tempC.Value;
        var RH = relativeHumidity;

        // Calculate vapor pressure (e) in hPa using simplified formula
        // e = 6.112 × exp((17.67 × T)/(T + 243.5)) × (RH/100)
        var e = 6.112 * Math.Exp((17.67 * T) / (T + 243.5)) * (RH / 100.0);

        // Calculate humidex
        var humidex = T + (0.5555 * (e - 10.0));

        // Return in original unit
        var resultC = new DoubleMeasurement(humidex, Unit.SI.C);
        var converted = resultC.ConvertTo(temperature.Unit);
        return new DoubleMeasurement(converted.Value, temperature.Unit);
    }
}
