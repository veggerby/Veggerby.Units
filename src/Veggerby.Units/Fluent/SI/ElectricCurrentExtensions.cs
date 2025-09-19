using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Electric current numeric extensions (SI).</summary>
public static partial class ElectricCurrentExtensions
{
    /// <summary>Creates a measurement in amperes (A).</summary>
    public static DoubleMeasurement Amperes(this double value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Alias for <see cref="Amperes(double)"/>.</summary>
    public static DoubleMeasurement Ampere(this double value) => value.Amperes();
    /// <summary>Symbol alias for <see cref="Amperes(double)"/>.</summary>
    public static DoubleMeasurement A(this double value) => value.Amperes();
    /// <summary>Creates a decimal measurement in amperes (A).</summary>
    public static DecimalMeasurement Amperes(this decimal value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Alias for <see cref="Amperes(decimal)"/>.</summary>
    public static DecimalMeasurement Ampere(this decimal value) => value.Amperes();

    /// <summary>Semantic alias for amperes.</summary>
    public static DoubleMeasurement Current(this double value) => value.Amperes();
    /// <summary>Semantic alias for amperes.</summary>
    public static DecimalMeasurement Current(this decimal value) => value.Amperes();
}