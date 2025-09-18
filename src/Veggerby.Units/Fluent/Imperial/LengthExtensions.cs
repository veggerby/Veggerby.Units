using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>
/// Imperial length numeric extensions (foot, inch, mile). Values are converted to the corresponding
/// <see cref="Unit.Imperial"/> units.
/// </summary>
public static class LengthExtensions
{
    /// <summary>Creates a length measurement in feet (ft).</summary>
    public static DoubleMeasurement Feet(this double value) => new(value, Unit.Imperial.ft);
    /// <summary>Alias for <see cref="Feet(double)"/>.</summary>
    public static DoubleMeasurement Foot(this double value) => value.Feet();
    /// <summary>Symbol alias for <see cref="Feet(double)"/>.</summary>
    public static DoubleMeasurement ft(this double value) => value.Feet();
    /// <summary>Creates a decimal length measurement in feet (ft).</summary>
    public static DecimalMeasurement Feet(this decimal value) => new(value, Unit.Imperial.ft);
    /// <summary>Alias for <see cref="Feet(decimal)"/>.</summary>
    public static DecimalMeasurement Foot(this decimal value) => value.Feet();

    /// <summary>Creates a length measurement in inches (in).</summary>
    public static DoubleMeasurement Inches(this double value) => new(value, Unit.Imperial.@in);
    /// <summary>Alias for <see cref="Inches(double)"/>.</summary>
    public static DoubleMeasurement Inch(this double value) => value.Inches();
    /// <summary>Symbol alias for <see cref="Inches(double)"/>.</summary>
    public static DoubleMeasurement @in(this double value) => value.Inches();
    /// <summary>Creates a decimal length measurement in inches (in).</summary>
    public static DecimalMeasurement Inches(this decimal value) => new(value, Unit.Imperial.@in);
    /// <summary>Alias for <see cref="Inches(decimal)"/>.</summary>
    public static DecimalMeasurement Inch(this decimal value) => value.Inches();

    /// <summary>Creates a length measurement in miles (mi).</summary>
    public static DoubleMeasurement Miles(this double value) => new(value, Unit.Imperial.mi);
    /// <summary>Alias for <see cref="Miles(double)"/>.</summary>
    public static DoubleMeasurement Mile(this double value) => value.Miles();
    /// <summary>Symbol alias for <see cref="Miles(double)"/>.</summary>
    public static DoubleMeasurement mi(this double value) => value.Miles();
    /// <summary>Creates a decimal length measurement in miles (mi).</summary>
    public static DecimalMeasurement Miles(this decimal value) => new(value, Unit.Imperial.mi);
    /// <summary>Alias for <see cref="Miles(decimal)"/>.</summary>
    public static DecimalMeasurement Mile(this decimal value) => value.Miles();
}