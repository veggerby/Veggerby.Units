namespace Veggerby.Units.Fluent.SI;

/// <summary>Mass related numeric extensions (SI).</summary>
public static partial class MassExtensions
{
    /// <summary>Creates a measurement in kilograms (kg).</summary>
    public static DoubleMeasurement Kilograms(this double value) => new(value, Unit.SI.kg);
    /// <summary>Alias for <see cref="Kilograms(double)"/>.</summary>
    public static DoubleMeasurement Kilogram(this double value) => value.Kilograms();
    /// <summary>Symbol alias for <see cref="Kilograms(double)"/>.</summary>
    public static DoubleMeasurement kg(this double value) => value.Kilograms();
    /// <summary>Creates a decimal measurement in kilograms (kg).</summary>
    public static DecimalMeasurement Kilograms(this decimal value) => new(value, Unit.SI.kg);
    /// <summary>Alias for <see cref="Kilograms(decimal)"/>.</summary>
    public static DecimalMeasurement Kilogram(this decimal value) => value.Kilograms();
}