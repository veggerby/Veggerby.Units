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

    /// <summary>Creates a length measurement in yards (yd).</summary>
    public static DoubleMeasurement Yards(this double value) => new(value, Unit.Imperial.ya);
    /// <summary>Alias for <see cref="Yards(double)"/>.</summary>
    public static DoubleMeasurement Yard(this double value) => value.Yards();
    /// <summary>Symbol alias for <see cref="Yards(double)"/>.</summary>
    public static DoubleMeasurement yd(this double value) => value.Yards();
    /// <summary>Creates a decimal length measurement in yards (yd).</summary>
    public static DecimalMeasurement Yards(this decimal value) => new(value, Unit.Imperial.ya);
    /// <summary>Alias for <see cref="Yards(decimal)"/>.</summary>
    public static DecimalMeasurement Yard(this decimal value) => value.Yards();

    /// <summary>Creates a length measurement in fathoms.</summary>
    public static DoubleMeasurement Fathoms(this double value) => new(value, Unit.Imperial.fathom);
    /// <summary>Alias for <see cref="Fathoms(double)"/>.</summary>
    public static DoubleMeasurement Fathom(this double value) => value.Fathoms();
    /// <summary>Creates a decimal length measurement in fathoms.</summary>
    public static DecimalMeasurement Fathoms(this decimal value) => new(value, Unit.Imperial.fathom);
    /// <summary>Alias for <see cref="Fathoms(decimal)"/>.</summary>
    public static DecimalMeasurement Fathom(this decimal value) => value.Fathoms();

    /// <summary>Creates a length measurement in nautical miles (nmi).</summary>
    public static DoubleMeasurement NauticalMiles(this double value) => new(value, Unit.Imperial.nmi);
    /// <summary>Alias for <see cref="NauticalMiles(double)"/>.</summary>
    public static DoubleMeasurement NauticalMile(this double value) => value.NauticalMiles();
    /// <summary>Symbol alias for <see cref="NauticalMiles(double)"/>.</summary>
    public static DoubleMeasurement nmi(this double value) => value.NauticalMiles();
    /// <summary>Creates a decimal length measurement in nautical miles (nmi).</summary>
    public static DecimalMeasurement NauticalMiles(this decimal value) => new(value, Unit.Imperial.nmi);
    /// <summary>Alias for <see cref="NauticalMiles(decimal)"/>.</summary>
    public static DecimalMeasurement NauticalMile(this decimal value) => value.NauticalMiles();
}