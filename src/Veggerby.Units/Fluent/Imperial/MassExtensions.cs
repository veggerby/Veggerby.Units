using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>Imperial mass/weight numeric extensions (pound, ounce, stone).</summary>
public static class MassExtensions
{
    /// <summary>Creates a mass measurement in pounds (lb).</summary>
    public static DoubleMeasurement Pounds(this double value) => new(value, Unit.Imperial.lb);
    /// <summary>Alias for <see cref="Pounds(double)"/>.</summary>
    public static DoubleMeasurement Pound(this double value) => value.Pounds();
    /// <summary>Symbol alias for <see cref="Pounds(double)"/>.</summary>
    public static DoubleMeasurement lb(this double value) => value.Pounds();
    /// <summary>Creates a decimal mass measurement in pounds (lb).</summary>
    public static DecimalMeasurement Pounds(this decimal value) => new(value, Unit.Imperial.lb);
    /// <summary>Alias for <see cref="Pounds(decimal)"/>.</summary>
    public static DecimalMeasurement Pound(this decimal value) => value.Pounds();

    /// <summary>Creates a mass measurement in ounces (oz).</summary>
    public static DoubleMeasurement Ounces(this double value) => new(value, Unit.Imperial.oz);
    /// <summary>Alias for <see cref="Ounces(double)"/>.</summary>
    public static DoubleMeasurement Ounce(this double value) => value.Ounces();
    /// <summary>Symbol alias for <see cref="Ounces(double)"/>.</summary>
    public static DoubleMeasurement oz(this double value) => value.Ounces();
    /// <summary>Creates a decimal mass measurement in ounces (oz).</summary>
    public static DecimalMeasurement Ounces(this decimal value) => new(value, Unit.Imperial.oz);
    /// <summary>Alias for <see cref="Ounces(decimal)"/>.</summary>
    public static DecimalMeasurement Ounce(this decimal value) => value.Ounces();

    /// <summary>Creates a mass measurement in stones (st).</summary>
    public static DoubleMeasurement Stones(this double value) => new(value, Unit.Imperial.st);
    /// <summary>Alias for <see cref="Stones(double)"/>.</summary>
    public static DoubleMeasurement Stone(this double value) => value.Stones();
    /// <summary>Symbol alias for <see cref="Stones(double)"/>.</summary>
    public static DoubleMeasurement st(this double value) => value.Stones();
    /// <summary>Creates a decimal mass measurement in stones (st).</summary>
    public static DecimalMeasurement Stones(this decimal value) => new(value, Unit.Imperial.st);
    /// <summary>Alias for <see cref="Stones(decimal)"/>.</summary>
    public static DecimalMeasurement Stone(this decimal value) => value.Stones();
}