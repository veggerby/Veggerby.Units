namespace Veggerby.Units.Fluent.SI;

/// <summary>Length related numeric extensions (SI).</summary>
public static partial class LengthExtensions
{
    /// <summary>Creates a length measurement in metres (m).</summary>
    public static DoubleMeasurement Meters(this double value) => new(value, Unit.SI.m);
    /// <summary>Alias for <see cref="Meters(double)"/>.</summary>
    public static DoubleMeasurement Meter(this double value) => value.Meters();
    /// <summary>Symbol alias for <see cref="Meters(double)"/>.</summary>
    public static DoubleMeasurement m(this double value) => value.Meters();
    /// <summary>Creates a decimal length measurement in metres (m).</summary>
    public static DecimalMeasurement Meters(this decimal value) => new(value, Unit.SI.m);
    /// <summary>Alias for <see cref="Meters(decimal)"/>.</summary>
    public static DecimalMeasurement Meter(this decimal value) => value.Meters();

    /// <summary>Creates a length measurement in kilometres (km) expressed as metres internally.</summary>
    /// <remarks>Implemented as a scale factor (value * 1000) since prefixed kilometre unit is not yet exposed as a distinct <see cref="Unit"/>.</remarks>
    public static DoubleMeasurement Kilometers(this double value) => new(value * 1000d, Unit.SI.m);
    /// <summary>Symbol alias for <see cref="Kilometers(double)"/>.</summary>
    public static DoubleMeasurement km(this double value) => value.Kilometers();
    /// <summary>Creates a decimal length measurement in kilometres (km) expressed as metres internally.</summary>
    /// <remarks>Implemented as a scale factor (value * 1000) since prefixed kilometre unit is not yet exposed as a distinct <see cref="Unit"/>.</remarks>
    public static DecimalMeasurement Kilometers(this decimal value) => new(value * 1000m, Unit.SI.m);
}