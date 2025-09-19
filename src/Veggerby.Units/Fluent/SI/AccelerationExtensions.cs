using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Acceleration related numeric extensions (SI).</summary>
public static partial class AccelerationExtensions
{
    /// <summary>Creates a measurement in metres per second squared (m/s^2).</summary>
    public static DoubleMeasurement MetersPerSecondSquared(this double value) => new(value, QuantityKinds.Acceleration.CanonicalUnit);
    /// <summary>Symbol style alias.</summary>
    public static DoubleMeasurement mps2(this double value) => value.MetersPerSecondSquared();
    /// <summary>Creates a decimal measurement in metres per second squared (m/s^2).</summary>
    public static DecimalMeasurement MetersPerSecondSquared(this decimal value) => new(value, QuantityKinds.Acceleration.CanonicalUnit);

    /// <summary>Semantic alias for metres per second squared.</summary>
    public static DoubleMeasurement Acceleration(this double value) => value.MetersPerSecondSquared();
    /// <summary>Semantic alias for metres per second squared.</summary>
    public static DecimalMeasurement Acceleration(this decimal value) => value.MetersPerSecondSquared();
}