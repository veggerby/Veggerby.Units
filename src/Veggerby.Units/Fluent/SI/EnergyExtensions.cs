using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Energy related numeric extensions (SI).</summary>
public static partial class EnergyExtensions
{
    /// <summary>Creates a measurement in joules (J).</summary>
    public static DoubleMeasurement Joules(this double value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    /// <summary>Alias for <see cref="Joules(double)"/>.</summary>
    public static DoubleMeasurement Joule(this double value) => value.Joules();
    /// <summary>Symbol alias for <see cref="Joules(double)"/>.</summary>
    public static DoubleMeasurement J(this double value) => value.Joules();
    /// <summary>Creates a decimal measurement in joules (J).</summary>
    public static DecimalMeasurement Joules(this decimal value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    /// <summary>Alias for <see cref="Joules(decimal)"/>.</summary>
    public static DecimalMeasurement Joule(this decimal value) => value.Joules();

    /// <summary>Semantic alias for <see cref="Joules(double)"/>.</summary>
    public static DoubleMeasurement Energy(this double value) => value.Joules();
    /// <summary>Semantic alias for <see cref="Joules(decimal)"/>.</summary>
    public static DecimalMeasurement Energy(this decimal value) => value.Joules();
}