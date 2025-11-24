namespace Veggerby.Units.Fluent.CGS;

/// <summary>
/// CGS mass numeric extensions (gram). Values are converted to the corresponding
/// <see cref="Unit.CGS"/> units.
/// </summary>
public static class MassExtensions
{
    /// <summary>Creates a mass measurement in grams (g).</summary>
    public static DoubleMeasurement Grams(this double value) => new(value, Unit.CGS.g);
    /// <summary>Alias for <see cref="Grams(double)"/>.</summary>
    public static DoubleMeasurement Gram(this double value) => value.Grams();
    /// <summary>Symbol alias for <see cref="Grams(double)"/>.</summary>
    public static DoubleMeasurement g(this double value) => value.Grams();
    /// <summary>Creates a decimal mass measurement in grams (g).</summary>
    public static DecimalMeasurement Grams(this decimal value) => new(value, Unit.CGS.g);
    /// <summary>Alias for <see cref="Grams(decimal)"/>.</summary>
    public static DecimalMeasurement Gram(this decimal value) => value.Grams();
}
