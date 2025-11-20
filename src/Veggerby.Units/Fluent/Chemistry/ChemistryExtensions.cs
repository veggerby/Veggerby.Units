using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Chemistry;

/// <summary>Chemistry-related quantity numeric extensions (moles, concentrations, catalytic activity, etc.).</summary>
public static class ChemistryExtensions
{
    /// <summary>Creates a measurement in moles (mol) for amount of substance.</summary>
    public static DoubleMeasurement Moles(this double value) => new(value, Unit.SI.n);
    /// <summary>Alias for <see cref="Moles(double)"/>.</summary>
    public static DoubleMeasurement Mole(this double value) => value.Moles();
    /// <summary>Symbol alias for <see cref="Moles(double)"/>.</summary>
    public static DoubleMeasurement mol(this double value) => value.Moles();
    /// <summary>Creates a decimal measurement in moles (mol).</summary>
    public static DecimalMeasurement Moles(this decimal value) => new(value, Unit.SI.n);
    /// <summary>Alias for <see cref="Moles(decimal)"/>.</summary>
    public static DecimalMeasurement Mole(this decimal value) => value.Moles();

    /// <summary>Creates a measurement representing molar mass (kg/mol).</summary>
    public static DoubleMeasurement KilogramsPerMole(this double value) => new(value, QuantityKinds.MolarMass.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing molar mass (kg/mol).</summary>
    public static DecimalMeasurement KilogramsPerMole(this decimal value) => new(value, QuantityKinds.MolarMass.CanonicalUnit);

    /// <summary>Creates a measurement representing molar volume (m³/mol).</summary>
    public static DoubleMeasurement MolarVolume(this double value) => new(value, QuantityKinds.MolarVolume.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing molar volume (m³/mol).</summary>
    public static DecimalMeasurement MolarVolume(this decimal value) => new(value, QuantityKinds.MolarVolume.CanonicalUnit);

    /// <summary>Creates a measurement representing molar concentration (mol/m³).</summary>
    public static DoubleMeasurement MolesPerCubicMeter(this double value) => new(value, QuantityKinds.MolarConcentration.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing molar concentration (mol/m³).</summary>
    public static DecimalMeasurement MolesPerCubicMeter(this decimal value) => new(value, QuantityKinds.MolarConcentration.CanonicalUnit);

    /// <summary>Creates a measurement representing molar concentration in mol/L (commonly used in chemistry).</summary>
    public static DoubleMeasurement MolesPerLiter(this double value) => new(value, Unit.SI.n / (Prefix.d * (Unit.SI.m ^ 3)));
    /// <summary>Creates a decimal measurement representing molar concentration in mol/L.</summary>
    public static DecimalMeasurement MolesPerLiter(this decimal value) => new(value, Unit.SI.n / (Prefix.d * (Unit.SI.m ^ 3)));

    /// <summary>Creates a measurement in katals (kat) for catalytic activity.</summary>
    public static DoubleMeasurement Katals(this double value) => new(value, QuantityKinds.CatalyticActivity.CanonicalUnit);
    /// <summary>Alias for <see cref="Katals(double)"/>.</summary>
    public static DoubleMeasurement Katal(this double value) => value.Katals();
    /// <summary>Symbol alias for <see cref="Katals(double)"/>.</summary>
    public static DoubleMeasurement kat(this double value) => value.Katals();
    /// <summary>Creates a decimal measurement in katals (kat).</summary>
    public static DecimalMeasurement Katals(this decimal value) => new(value, QuantityKinds.CatalyticActivity.CanonicalUnit);
    /// <summary>Alias for <see cref="Katals(decimal)"/>.</summary>
    public static DecimalMeasurement Katal(this decimal value) => value.Katals();

    /// <summary>Creates a measurement representing reaction rate (mol/(m³·s)).</summary>
    public static DoubleMeasurement ReactionRate(this double value) => new(value, QuantityKinds.ReactionRate.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing reaction rate (mol/(m³·s)).</summary>
    public static DecimalMeasurement ReactionRate(this decimal value) => new(value, QuantityKinds.ReactionRate.CanonicalUnit);

    /// <summary>Creates a measurement representing diffusion coefficient (m²/s).</summary>
    public static DoubleMeasurement DiffusionCoefficient(this double value) => new(value, QuantityKinds.DiffusionCoefficient.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing diffusion coefficient (m²/s).</summary>
    public static DecimalMeasurement DiffusionCoefficient(this decimal value) => new(value, QuantityKinds.DiffusionCoefficient.CanonicalUnit);

    /// <summary>Creates a measurement representing number density (1/m³).</summary>
    public static DoubleMeasurement NumberDensity(this double value) => new(value, QuantityKinds.NumberDensity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing number density (1/m³).</summary>
    public static DecimalMeasurement NumberDensity(this decimal value) => new(value, QuantityKinds.NumberDensity.CanonicalUnit);
}
