namespace Veggerby.Units.Quantities;

/// <summary>
/// Mechanics (forces, motion-derived) and material response (moduli, densities, stresses, structural geometry).
/// </summary>
/// <remarks>
/// Quantity kind definitions align with QUDT ontology specifications for mechanics and materials.
/// See <c>docs/qudt-mapping-table.md</c> for detailed QUDT URI mappings.
/// <para>
/// QUDT Reference: http://qudt.org/doc/DOC_VOCAB-QUANTITY-KINDS.html
/// </para>
/// </remarks>
public static partial class QuantityKinds
{
    // Mechanics & Materials core (structural, stress, moduli, densities)
    /// <summary>Torque (τ) shares energy dimension but denotes rotational effect.</summary>
    public static readonly QuantityKind Torque = new(
        "Torque",
        QuantityKinds.Energy.CanonicalUnit,
        "τ",
        tags: [QuantityKindTags.DomainMechanics]);


    /// <summary>Shear modulus (Pa).</summary>
    public static readonly QuantityKind ShearModulus = new(
        "ShearModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "G_mod",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics]);

    /// <summary>Bulk modulus (Pa).</summary>
    public static readonly QuantityKind BulkModulus = new(
        "BulkModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "K_mod",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics]);

    /// <summary>Stress (Pa).</summary>
    public static readonly QuantityKind Stress = new(
        "Stress",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "σ",
        tags: [QuantityKindTags.DomainMechanics, QuantityKindTags.DomainMaterial]);

    /// <summary>Shear stress (Pa).</summary>
    public static readonly QuantityKind ShearStress = new(
        "ShearStress",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "τ_s",
        tags: [QuantityKindTags.DomainMechanics, QuantityKindTags.DomainMaterial]);

    /// <summary>Strain (dimensionless).</summary>
    public static readonly QuantityKind Strain = new(
        "Strain",
        Unit.None,
        "ε",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.FormDimensionless]);

    /// <summary>Strain rate (1/s).</summary>
    public static readonly QuantityKind StrainRate = new(
        "StrainRate",
        1 / Unit.SI.s,
        "ε_dot",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainTransport]);

    /// <summary>Second moment of area (m^4).</summary>
    public static readonly QuantityKind SecondMomentOfArea = new(
        "SecondMomentOfArea",
        Unit.SI.m ^ 4,
        "I_A",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Section modulus (m^3).</summary>
    public static readonly QuantityKind SectionModulus = new(
        "SectionModulus",
        Unit.SI.m ^ 3,
        "Z_sec",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Linear mass density (kg/m).</summary>
    public static readonly QuantityKind LinearMassDensity = new(
        "LinearMassDensity",
        Unit.SI.kg / Unit.SI.m,
        "μ_lin",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Areal mass density (kg/m²).</summary>
    public static readonly QuantityKind ArealMassDensity = new(
        "ArealMassDensity",
        Unit.SI.kg / (Unit.SI.m ^ 2),
        "σ_areal",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Mass density (kg/m³).</summary>
    public static readonly QuantityKind MassDensity = new(
        "MassDensity",
        Unit.SI.kg / (Unit.SI.m ^ 3),
        "ρ",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Mass concentration (kg/m³) semantic distinct from mass density.</summary>
    public static readonly QuantityKind MassConcentration = new(
        "MassConcentration",
        Unit.SI.kg / (Unit.SI.m ^ 3),
        "ρ_m",
        tags: [QuantityKindTags.DomainChemistry, QuantityKindTags.DomainMaterial]);

    /// <summary>Specific energy (J/kg).</summary>
    public static readonly QuantityKind SpecificEnergy = new(
        "SpecificEnergy",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "e_spec",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Specific power (W/kg).</summary>
    public static readonly QuantityKind SpecificPower = new(
        "SpecificPower",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "p_spec",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Specific volume (m^3/kg).</summary>
    public static readonly QuantityKind SpecificVolume = new(
        "SpecificVolume",
        (Unit.SI.m ^ 3) / Unit.SI.kg,
        "v_spec",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Surface tension (N/m).</summary>
    public static readonly QuantityKind SurfaceTension = new(
        "SurfaceTension",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / Unit.SI.m,
        "γ",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Porosity (dimensionless).</summary>
    public static readonly QuantityKind Porosity = new(
        "Porosity",
        Unit.None,
        "φ",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.FormDimensionless]);

    /// <summary>Compressibility (1/Pa).</summary>
    public static readonly QuantityKind Compressibility = new(
        "Compressibility",
        (Unit.SI.m * (Unit.SI.s ^ 2)) / Unit.SI.kg,
        "β_T",
        tags: [QuantityKindTags.DomainMaterial]);

    /// <summary>Poisson ratio (dimensionless).</summary>
    public static readonly QuantityKind PoissonRatio = new(
        "PoissonRatio",
        Unit.None,
        "ν_p",
        tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainMaterial]);

    /// <summary>Coefficient of friction (dimensionless).</summary>
    public static readonly QuantityKind CoefficientOfFriction = new(
        "CoefficientOfFriction",
        Unit.None,
        "μ_f",
        tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainMechanics]);

    /// <summary>Stiffness (N/m).</summary>
    public static readonly QuantityKind Stiffness = new(
        "Stiffness",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / Unit.SI.m,
        "k_st",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Compliance (m/N).</summary>
    public static readonly QuantityKind Compliance = new(
        "Compliance",
        Unit.SI.m / (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)),
        "C_comp",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Damping coefficient (N·s/m).</summary>
    public static readonly QuantityKind DampingCoefficient = new(
        "DampingCoefficient",
        ((Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) * Unit.SI.s) / Unit.SI.m,
        "c_d",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Impulse (N·s) force integrated over time.</summary>
    public static readonly QuantityKind Impulse = new(
        "Impulse",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) * Unit.SI.s,
        "J_imp",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Action (J·s) classical action dimension.</summary>
    public static readonly QuantityKind Action = new(
        "Action",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.s,
        "S_act",
        tags: [QuantityKindTags.DomainMechanics]);
}