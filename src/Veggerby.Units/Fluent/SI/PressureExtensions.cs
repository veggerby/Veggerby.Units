using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Pressure related numeric extensions (SI).</summary>
public static partial class PressureExtensions
{
    /// <summary>Creates a measurement in pascals (Pa).</summary>
    public static DoubleMeasurement Pascals(this double value) => new(value, QuantityKinds.Pressure.CanonicalUnit);
    /// <summary>Alias for <see cref="Pascals(double)"/>.</summary>
    public static DoubleMeasurement Pascal(this double value) => value.Pascals();
    /// <summary>Symbol alias for <see cref="Pascals(double)"/>.</summary>
    public static DoubleMeasurement Pa(this double value) => value.Pascals();
    /// <summary>Creates a decimal measurement in pascals (Pa).</summary>
    public static DecimalMeasurement Pascals(this decimal value) => new(value, QuantityKinds.Pressure.CanonicalUnit);
    /// <summary>Alias for <see cref="Pascals(decimal)"/>.</summary>
    public static DecimalMeasurement Pascal(this decimal value) => value.Pascals();

    /// <summary>Semantic alias for pascals.</summary>
    public static DoubleMeasurement Pressure(this double value) => value.Pascals();
    /// <summary>Semantic alias for pascals.</summary>
    public static DecimalMeasurement Pressure(this decimal value) => value.Pascals();
}