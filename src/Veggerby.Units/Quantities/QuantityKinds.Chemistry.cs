namespace Veggerby.Units.Quantities;

/// <summary>
/// Chemistry and substance-related quantities: concentrations, potentials, fractions, rates, thermochemical specifics.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Molar mass (kg/mol).</summary>
    public static readonly QuantityKind MolarMass = new("MolarMass", Unit.SI.kg / Unit.SI.n, "M", tags: [QuantityKindTags.DomainChemistry]);
    /// <summary>Molar volume (m^3/mol).</summary>
    public static readonly QuantityKind MolarVolume = new("MolarVolume", (Unit.SI.m ^ 3) / Unit.SI.n, "V_m", tags: [QuantityKindTags.DomainChemistry]);
    /// <summary>Molar concentration (mol/m^3).</summary>
    public static readonly QuantityKind MolarConcentration = new("MolarConcentration", Unit.SI.n / (Unit.SI.m ^ 3), "c", tags: [QuantityKindTags.DomainChemistry]);
    /// <summary>Number density (1/m^3).</summary>
    public static readonly QuantityKind NumberDensity = new("NumberDensity", 1 / (Unit.SI.m ^ 3), "n_d", tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainChemistry]);
    /// <summary>Areal number density (1/m^2).</summary>
    public static readonly QuantityKind ArealNumberDensity = new("ArealNumberDensity", 1 / (Unit.SI.m ^ 2), "n_dA", tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainChemistry]);
    /// <summary>Mole fraction (dimensionless).</summary>
    public static readonly QuantityKind MoleFraction = new("MoleFraction", Unit.None, "x", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless]);
    /// <summary>Mass fraction (dimensionless).</summary>
    public static readonly QuantityKind MassFraction = new("MassFraction", Unit.None, "w", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless]);
    /// <summary>Osmotic pressure (Pa).</summary>
    public static readonly QuantityKind OsmoticPressure = new("OsmoticPressure", Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)), "π_osm", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic]);
    /// <summary>Partial pressure (Pa).</summary>
    public static readonly QuantityKind PartialPressure = new("PartialPressure", Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)), "p_i", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic]);
    /// <summary>Activity (dimensionless).</summary>
    public static readonly QuantityKind Activity = new("Activity", Unit.None, "a", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless]);
    /// <summary>Activity coefficient (dimensionless).</summary>
    public static readonly QuantityKind ActivityCoefficient = new("ActivityCoefficient", Unit.None, "γ_act", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless]);
    /// <summary>Activity concentration (1/(m^3·s)).</summary>
    public static readonly QuantityKind ActivityConcentration = new("ActivityConcentration", (1 / Unit.SI.s) / (Unit.SI.m ^ 3), "A_c", tags: [QuantityKindTags.DomainChemistry]);
    /// <summary>Reaction rate (mol/(m^3·s)).</summary>
    public static readonly QuantityKind ReactionRate = new("ReactionRate", Unit.SI.n / ((Unit.SI.m ^ 3) * Unit.SI.s), "r_rxn", tags: [QuantityKindTags.DomainChemistry]);
    /// <summary>Diffusion coefficient (m^2/s).</summary>
    public static readonly QuantityKind DiffusionCoefficient = new("DiffusionCoefficient", (Unit.SI.m ^ 2) / Unit.SI.s, "D_diff", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainChemistry]);
    /// <summary>Mass attenuation coefficient (m^2/kg).</summary>
    public static readonly QuantityKind MassAttenuationCoefficient = new("MassAttenuationCoefficient", (Unit.SI.m ^ 2) / Unit.SI.kg, "μ_mass", tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainRadiation]);
    /// <summary>Henry's constant (Pa·m^3/mol).</summary>
    public static readonly QuantityKind HenrysConstant = new("HenrysConstant", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * (Unit.SI.m ^ 3) / Unit.SI.n, "H_c", tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic]);
    /// <summary>Catalytic activity (kat = mol/s).</summary>
    public static readonly QuantityKind CatalyticActivity = new("CatalyticActivity", Unit.SI.n / Unit.SI.s, "kat", tags: [QuantityKindTags.DomainChemistry]);
}