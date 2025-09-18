using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>Imperial power and speed numeric extensions (horsepower, ft/s).</summary>
public static class PowerAndSpeedExtensions
{
    /// <summary>Creates a power measurement in mechanical horsepower (hp).</summary>
    public static DoubleMeasurement Horsepower(this double value) => new(value, Unit.Imperial.hp);
    /// <summary>Symbol alias for <see cref="Horsepower(double)"/>.</summary>
    public static DoubleMeasurement hp(this double value) => value.Horsepower();
    /// <summary>Creates a decimal power measurement in mechanical horsepower (hp).</summary>
    public static DecimalMeasurement Horsepower(this decimal value) => new(value, Unit.Imperial.hp);
    /// <summary>Symbol alias for <see cref="Horsepower(decimal)"/>.</summary>
    public static DecimalMeasurement hp(this decimal value) => value.Horsepower();

    /// <summary>
    /// Creates a speed measurement in feet per second (ft/s). Constructed as composite ft / s using existing
    /// imperial foot and shared second unit.
    /// </summary>
    public static DoubleMeasurement FeetPerSecond(this double value) => new(value, Unit.Imperial.ft / Unit.Imperial.s);
    /// <summary>Symbol style alias for <see cref="FeetPerSecond(double)"/>.</summary>
    public static DoubleMeasurement ftps(this double value) => value.FeetPerSecond();
    /// <summary>Creates a decimal speed measurement in feet per second (ft/s).</summary>
    public static DecimalMeasurement FeetPerSecond(this decimal value) => new(value, Unit.Imperial.ft / Unit.Imperial.s);
    /// <summary>Symbol style alias for <see cref="FeetPerSecond(decimal)"/>.</summary>
    public static DecimalMeasurement ftps(this decimal value) => value.FeetPerSecond();
}