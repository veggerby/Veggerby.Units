namespace Veggerby.Units.Quantities;

/// <summary>
/// Ergonomic factories for temperature semantic quantities.
/// </summary>
public static class TemperatureQuantity
{
    /// <summary>Create absolute temperature quantity (affine).</summary>
    public static Quantity<double> Absolute(double value, Unit unit)
        => new(new DoubleMeasurement(value, unit), QuantityKinds.TemperatureAbsolute, strictDimensionCheck: true);

    /// <summary>Create temperature difference (linear) quantity; unit defaults to Kelvin.</summary>
    public static Quantity<double> Delta(double value, Unit unit = null)
    {
        var u = unit ?? Unit.SI.K; // must be dimensionally K
        return new Quantity<double>(new DoubleMeasurement(value, u), QuantityKinds.TemperatureDelta, strictDimensionCheck: true);
    }

    /// <summary>
    /// Create a temperature delta specified in degrees Celsius. A difference of Δ°C is identical in scale to Kelvin,
    /// so the internal canonical representation uses Kelvin.
    /// </summary>
    /// <param name="value">Difference in degrees Celsius.</param>
    public static Quantity<double> DeltaC(double value)
        => Delta(value, Unit.SI.K);

    /// <summary>
    /// Create a temperature delta specified in degrees Fahrenheit. Fahrenheit deltas are scaled (Δ°F * 5/9) relative
    /// to Kelvin, so the value is converted to canonical Kelvin difference.
    /// </summary>
    /// <param name="value">Difference in degrees Fahrenheit.</param>
    public static Quantity<double> DeltaF(double value)
        => Delta(value * 5.0 / 9.0, Unit.SI.K);
}