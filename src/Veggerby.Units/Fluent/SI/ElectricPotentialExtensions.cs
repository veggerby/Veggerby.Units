using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.SI;

/// <summary>Electric potential (voltage) numeric extensions (SI).</summary>
public static partial class ElectricPotentialExtensions
{
    /// <summary>Creates a measurement in volts (V).</summary>
    public static DoubleMeasurement Volts(this double value) => new(value, QuantityKinds.Voltage.CanonicalUnit);
    /// <summary>Alias for <see cref="Volts(double)"/>.</summary>
    public static DoubleMeasurement Volt(this double value) => value.Volts();
    /// <summary>Symbol alias for <see cref="Volts(double)"/>.</summary>
    public static DoubleMeasurement V(this double value) => value.Volts();
    /// <summary>Creates a decimal measurement in volts (V).</summary>
    public static DecimalMeasurement Volts(this decimal value) => new(value, QuantityKinds.Voltage.CanonicalUnit);
    /// <summary>Alias for <see cref="Volts(decimal)"/>.</summary>
    public static DecimalMeasurement Volt(this decimal value) => value.Volts();

    /// <summary>Semantic alias for volts.</summary>
    public static DoubleMeasurement Voltage(this double value) => value.Volts();
    /// <summary>Semantic alias for volts.</summary>
    public static DecimalMeasurement Voltage(this decimal value) => value.Volts();
}