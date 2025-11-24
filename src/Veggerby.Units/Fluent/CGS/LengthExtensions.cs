namespace Veggerby.Units.Fluent.CGS;

/// <summary>
/// CGS length numeric extensions (centimeter). Values are converted to the corresponding
/// <see cref="Unit.CGS"/> units.
/// </summary>
public static class LengthExtensions
{
    /// <summary>Creates a length measurement in centimeters (cm).</summary>
    public static DoubleMeasurement Centimeters(this double value) => new(value, Unit.CGS.cm);
    /// <summary>Alias for <see cref="Centimeters(double)"/>.</summary>
    public static DoubleMeasurement Centimeter(this double value) => value.Centimeters();
    /// <summary>Symbol alias for <see cref="Centimeters(double)"/>.</summary>
    public static DoubleMeasurement cm(this double value) => value.Centimeters();
    /// <summary>Creates a decimal length measurement in centimeters (cm).</summary>
    public static DecimalMeasurement Centimeters(this decimal value) => new(value, Unit.CGS.cm);
    /// <summary>Alias for <see cref="Centimeters(decimal)"/>.</summary>
    public static DecimalMeasurement Centimeter(this decimal value) => value.Centimeters();
}
