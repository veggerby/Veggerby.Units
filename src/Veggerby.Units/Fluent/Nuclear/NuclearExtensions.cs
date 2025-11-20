using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Nuclear;

/// <summary>Nuclear and radiation-related quantity numeric extensions (radioactivity, absorbed dose, etc.).</summary>
public static class NuclearExtensions
{
    /// <summary>Creates a measurement in becquerels (Bq) for radioactivity.</summary>
    public static DoubleMeasurement Becquerels(this double value) => new(value, QuantityKinds.Radioactivity.CanonicalUnit);
    /// <summary>Alias for <see cref="Becquerels(double)"/>.</summary>
    public static DoubleMeasurement Becquerel(this double value) => value.Becquerels();
    /// <summary>Symbol alias for <see cref="Becquerels(double)"/>.</summary>
    public static DoubleMeasurement Bq(this double value) => value.Becquerels();
    /// <summary>Creates a decimal measurement in becquerels (Bq).</summary>
    public static DecimalMeasurement Becquerels(this decimal value) => new(value, QuantityKinds.Radioactivity.CanonicalUnit);
    /// <summary>Alias for <see cref="Becquerels(decimal)"/>.</summary>
    public static DecimalMeasurement Becquerel(this decimal value) => value.Becquerels();

    /// <summary>Creates a measurement in grays (Gy) for absorbed dose.</summary>
    public static DoubleMeasurement Grays(this double value) => new(value, QuantityKinds.AbsorbedDose.CanonicalUnit);
    /// <summary>Alias for <see cref="Grays(double)"/>.</summary>
    public static DoubleMeasurement Gray(this double value) => value.Grays();
    /// <summary>Symbol alias for <see cref="Grays(double)"/>.</summary>
    public static DoubleMeasurement Gy(this double value) => value.Grays();
    /// <summary>Creates a decimal measurement in grays (Gy).</summary>
    public static DecimalMeasurement Grays(this decimal value) => new(value, QuantityKinds.AbsorbedDose.CanonicalUnit);
    /// <summary>Alias for <see cref="Grays(decimal)"/>.</summary>
    public static DecimalMeasurement Gray(this decimal value) => value.Grays();

    /// <summary>Creates a measurement in sieverts (Sv) for dose equivalent.</summary>
    public static DoubleMeasurement Sieverts(this double value) => new(value, QuantityKinds.DoseEquivalent.CanonicalUnit);
    /// <summary>Alias for <see cref="Sieverts(double)"/>.</summary>
    public static DoubleMeasurement Sievert(this double value) => value.Sieverts();
    /// <summary>Symbol alias for <see cref="Sieverts(double)"/>.</summary>
    public static DoubleMeasurement Sv(this double value) => value.Sieverts();
    /// <summary>Creates a decimal measurement in sieverts (Sv).</summary>
    public static DecimalMeasurement Sieverts(this decimal value) => new(value, QuantityKinds.DoseEquivalent.CanonicalUnit);
    /// <summary>Alias for <see cref="Sieverts(decimal)"/>.</summary>
    public static DecimalMeasurement Sievert(this decimal value) => value.Sieverts();

    /// <summary>Creates a measurement representing radiation exposure (C/kg).</summary>
    public static DoubleMeasurement RadiationExposure(this double value) => new(value, QuantityKinds.RadiationExposure.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing radiation exposure (C/kg).</summary>
    public static DecimalMeasurement RadiationExposure(this decimal value) => new(value, QuantityKinds.RadiationExposure.CanonicalUnit);

    /// <summary>Creates a measurement representing absorbed dose rate (Gy/s).</summary>
    public static DoubleMeasurement AbsorbedDoseRate(this double value) => new(value, QuantityKinds.AbsorbedDoseRate.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing absorbed dose rate (Gy/s).</summary>
    public static DecimalMeasurement AbsorbedDoseRate(this decimal value) => new(value, QuantityKinds.AbsorbedDoseRate.CanonicalUnit);

    /// <summary>Creates a measurement representing equivalent dose rate (Sv/s).</summary>
    public static DoubleMeasurement EquivalentDoseRate(this double value) => new(value, QuantityKinds.EquivalentDoseRate.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing equivalent dose rate (Sv/s).</summary>
    public static DecimalMeasurement EquivalentDoseRate(this decimal value) => new(value, QuantityKinds.EquivalentDoseRate.CanonicalUnit);
}
