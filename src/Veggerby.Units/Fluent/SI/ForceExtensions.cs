using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Force related numeric extensions (SI).</summary>
public static partial class ForceExtensions
{
    /// <summary>Creates a measurement in newtons (N).</summary>
    public static DoubleMeasurement Newtons(this double value) => new(value, QuantityKinds.Force.CanonicalUnit);
    /// <summary>Alias for <see cref="Newtons(double)"/>.</summary>
    public static DoubleMeasurement Newton(this double value) => value.Newtons();
    /// <summary>Symbol alias for <see cref="Newtons(double)"/>.</summary>
    public static DoubleMeasurement N(this double value) => value.Newtons();
    /// <summary>Creates a decimal measurement in newtons (N).</summary>
    public static DecimalMeasurement Newtons(this decimal value) => new(value, QuantityKinds.Force.CanonicalUnit);
    /// <summary>Alias for <see cref="Newtons(decimal)"/>.</summary>
    public static DecimalMeasurement Newton(this decimal value) => value.Newtons();

    /// <summary>Semantic alias for <see cref="Newtons(double)"/>.</summary>
    public static DoubleMeasurement Force(this double value) => value.Newtons();
    /// <summary>Semantic alias for <see cref="Newtons(decimal)"/>.</summary>
    public static DecimalMeasurement Force(this decimal value) => value.Newtons();
}