using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>Imperial engineering force/pressure/energy numeric extensions (pound-force, foot-pound, psi).</summary>
public static class ForceExtensions
{
    /// <summary>Creates a force measurement in pound-force (lbf).</summary>
    public static DoubleMeasurement PoundForce(this double value) => new(value, Unit.Imperial.lbf);
    /// <summary>Symbol alias for <see cref="PoundForce(double)"/>.</summary>
    public static DoubleMeasurement lbf(this double value) => value.PoundForce();
    /// <summary>Creates a decimal force measurement in pound-force (lbf).</summary>
    public static DecimalMeasurement PoundForce(this decimal value) => new(value, Unit.Imperial.lbf);
    /// <summary>Symbol alias for <see cref="PoundForce(decimal)"/>.</summary>
    public static DecimalMeasurement lbf(this decimal value) => value.PoundForce();

    /// <summary>Creates an energy (work) measurement in foot-pounds (ft·lb). Shares dimension with Joule.</summary>
    public static DoubleMeasurement FootPounds(this double value) => new(value, Unit.Imperial.ft_lb);
    /// <summary>Alias for <see cref="FootPounds(double)"/>.</summary>
    public static DoubleMeasurement FootPound(this double value) => value.FootPounds();
    /// <summary>Creates a decimal energy measurement in foot-pounds (ft·lb).</summary>
    public static DecimalMeasurement FootPounds(this decimal value) => new(value, Unit.Imperial.ft_lb);
    /// <summary>Alias for <see cref="FootPounds(decimal)"/>.</summary>
    public static DecimalMeasurement FootPound(this decimal value) => value.FootPounds();

    /// <summary>Creates a pressure measurement in pounds per square inch (psi).</summary>
    public static DoubleMeasurement PoundsPerSquareInch(this double value) => new(value, Unit.Imperial.psi);
    /// <summary>Symbol alias for <see cref="PoundsPerSquareInch(double)"/>.</summary>
    public static DoubleMeasurement psi(this double value) => value.PoundsPerSquareInch();
    /// <summary>Creates a decimal pressure measurement in pounds per square inch (psi).</summary>
    public static DecimalMeasurement PoundsPerSquareInch(this decimal value) => new(value, Unit.Imperial.psi);
    /// <summary>Symbol alias for <see cref="PoundsPerSquareInch(decimal)"/>.</summary>
    public static DecimalMeasurement psi(this decimal value) => value.PoundsPerSquareInch();
}