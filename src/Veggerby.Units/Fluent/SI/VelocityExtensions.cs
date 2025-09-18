using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Velocity related numeric extensions (SI).</summary>
public static partial class VelocityExtensions
{
    /// <summary>Creates a measurement in metres per second (m/s).</summary>
    public static DoubleMeasurement MetersPerSecond(this double value) => new(value, QuantityKinds.Velocity.CanonicalUnit);
    /// <summary>Symbol style alias.</summary>
    public static DoubleMeasurement mps(this double value) => value.MetersPerSecond();
    /// <summary>Creates a decimal measurement in metres per second (m/s).</summary>
    public static DecimalMeasurement MetersPerSecond(this decimal value) => new(value, QuantityKinds.Velocity.CanonicalUnit);

    /// <summary>Semantic alias for metres per second.</summary>
    public static DoubleMeasurement Velocity(this double value) => value.MetersPerSecond();
    /// <summary>Semantic alias for metres per second.</summary>
    public static DecimalMeasurement Velocity(this decimal value) => value.MetersPerSecond();
}