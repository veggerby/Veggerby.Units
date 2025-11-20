using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Optics;

/// <summary>Optical and radiometric quantity numeric extensions (luminous flux, irradiance, etc.).</summary>
public static class OpticsExtensions
{
    /// <summary>Creates a measurement in candelas (cd) for luminous intensity.</summary>
    public static DoubleMeasurement Candelas(this double value) => new(value, QuantityKinds.LuminousIntensity.CanonicalUnit);
    /// <summary>Alias for <see cref="Candelas(double)"/>.</summary>
    public static DoubleMeasurement Candela(this double value) => value.Candelas();
    /// <summary>Symbol alias for <see cref="Candelas(double)"/>.</summary>
    public static DoubleMeasurement cd(this double value) => value.Candelas();
    /// <summary>Creates a decimal measurement in candelas (cd).</summary>
    public static DecimalMeasurement Candelas(this decimal value) => new(value, QuantityKinds.LuminousIntensity.CanonicalUnit);
    /// <summary>Alias for <see cref="Candelas(decimal)"/>.</summary>
    public static DecimalMeasurement Candela(this decimal value) => value.Candelas();

    /// <summary>Creates a measurement in lumens (lm) for luminous flux.</summary>
    public static DoubleMeasurement Lumens(this double value) => new(value, QuantityKinds.LuminousFlux.CanonicalUnit);
    /// <summary>Alias for <see cref="Lumens(double)"/>.</summary>
    public static DoubleMeasurement Lumen(this double value) => value.Lumens();
    /// <summary>Symbol alias for <see cref="Lumens(double)"/>.</summary>
    public static DoubleMeasurement lm(this double value) => value.Lumens();
    /// <summary>Creates a decimal measurement in lumens (lm).</summary>
    public static DecimalMeasurement Lumens(this decimal value) => new(value, QuantityKinds.LuminousFlux.CanonicalUnit);
    /// <summary>Alias for <see cref="Lumens(decimal)"/>.</summary>
    public static DecimalMeasurement Lumen(this decimal value) => value.Lumens();

    /// <summary>Creates a measurement in lux (lx) for illuminance.</summary>
    public static DoubleMeasurement Lux(this double value) => new(value, QuantityKinds.Illuminance.CanonicalUnit);
    /// <summary>Symbol alias for <see cref="Lux(double)"/>.</summary>
    public static DoubleMeasurement lx(this double value) => value.Lux();
    /// <summary>Creates a decimal measurement in lux (lx).</summary>
    public static DecimalMeasurement Lux(this decimal value) => new(value, QuantityKinds.Illuminance.CanonicalUnit);

    /// <summary>Creates a measurement representing luminance (cd/m²).</summary>
    public static DoubleMeasurement Luminance(this double value) => new(value, QuantityKinds.Luminance.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing luminance (cd/m²).</summary>
    public static DecimalMeasurement Luminance(this decimal value) => new(value, QuantityKinds.Luminance.CanonicalUnit);

    /// <summary>Creates a measurement representing luminous efficacy (lm/W).</summary>
    public static DoubleMeasurement LuminousEfficacy(this double value) => new(value, QuantityKinds.LuminousEfficacy.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing luminous efficacy (lm/W).</summary>
    public static DecimalMeasurement LuminousEfficacy(this decimal value) => new(value, QuantityKinds.LuminousEfficacy.CanonicalUnit);

    /// <summary>Creates a measurement in radians (rad) for plane angle.</summary>
    public static DoubleMeasurement Radians(this double value) => new(value, Unit.SI.rad);
    /// <summary>Alias for <see cref="Radians(double)"/>.</summary>
    public static DoubleMeasurement Radian(this double value) => value.Radians();
    /// <summary>Symbol alias for <see cref="Radians(double)"/>.</summary>
    public static DoubleMeasurement rad(this double value) => value.Radians();
    /// <summary>Creates a decimal measurement in radians (rad).</summary>
    public static DecimalMeasurement Radians(this decimal value) => new(value, Unit.SI.rad);
    /// <summary>Alias for <see cref="Radians(decimal)"/>.</summary>
    public static DecimalMeasurement Radian(this decimal value) => value.Radians();

    /// <summary>Creates a measurement in steradians (sr) for solid angle.</summary>
    public static DoubleMeasurement Steradians(this double value) => new(value, Unit.SI.sr);
    /// <summary>Alias for <see cref="Steradians(double)"/>.</summary>
    public static DoubleMeasurement Steradian(this double value) => value.Steradians();
    /// <summary>Symbol alias for <see cref="Steradians(double)"/>.</summary>
    public static DoubleMeasurement sr(this double value) => value.Steradians();
    /// <summary>Creates a decimal measurement in steradians (sr).</summary>
    public static DecimalMeasurement Steradians(this decimal value) => new(value, Unit.SI.sr);
    /// <summary>Alias for <see cref="Steradians(decimal)"/>.</summary>
    public static DecimalMeasurement Steradian(this decimal value) => value.Steradians();

    /// <summary>Creates a measurement representing irradiance (W/m²).</summary>
    public static DoubleMeasurement WattsPerSquareMeter(this double value) => new(value, QuantityKinds.Irradiance.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing irradiance (W/m²).</summary>
    public static DecimalMeasurement WattsPerSquareMeter(this decimal value) => new(value, QuantityKinds.Irradiance.CanonicalUnit);

    /// <summary>Creates a measurement representing radiance (W/(m²·sr)).</summary>
    public static DoubleMeasurement Radiance(this double value) => new(value, QuantityKinds.Radiance.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing radiance (W/(m²·sr)).</summary>
    public static DecimalMeasurement Radiance(this decimal value) => new(value, QuantityKinds.Radiance.CanonicalUnit);

    /// <summary>Creates a measurement representing radiant intensity (W/sr).</summary>
    public static DoubleMeasurement RadiantIntensity(this double value) => new(value, QuantityKinds.RadiantIntensity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing radiant intensity (W/sr).</summary>
    public static DecimalMeasurement RadiantIntensity(this decimal value) => new(value, QuantityKinds.RadiantIntensity.CanonicalUnit);

    /// <summary>Creates a measurement representing radiant exposure (J/m²).</summary>
    public static DoubleMeasurement RadiantExposure(this double value) => new(value, QuantityKinds.RadiantExposure.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing radiant exposure (J/m²).</summary>
    public static DecimalMeasurement RadiantExposure(this decimal value) => new(value, QuantityKinds.RadiantExposure.CanonicalUnit);
}
