namespace Veggerby.Units.Fluent.SI;

/// <summary>Time related numeric extensions (SI base + common derived subset).</summary>
public static partial class TimeExtensions
{
    /// <summary>Creates a measurement in seconds (s).</summary>
    public static DoubleMeasurement Seconds(this double value) => new(value, Unit.SI.s);
    /// <summary>Alias for <see cref="Seconds(double)"/>.</summary>
    public static DoubleMeasurement Second(this double value) => value.Seconds();
    /// <summary>Symbol alias for <see cref="Seconds(double)"/>.</summary>
    public static DoubleMeasurement s(this double value) => value.Seconds();
    /// <summary>Creates a decimal measurement in seconds (s).</summary>
    public static DecimalMeasurement Seconds(this decimal value) => new(value, Unit.SI.s);
    /// <summary>Alias for <see cref="Seconds(decimal)"/>.</summary>
    public static DecimalMeasurement Second(this decimal value) => value.Seconds();
}