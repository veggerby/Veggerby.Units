namespace Veggerby.Units.Quantities;

/// <summary>Additional high-value semantic kinds (mechanics, thermal, fluid dimensionless groups, radiometry spectral, chemistry/material).</summary>
public static partial class QuantityKinds
{
    // Mechanics / Dynamics
    /// <summary>Impulse (N·s) force integrated over time.</summary>
    public static readonly QuantityKind Impulse = new(
        "Impulse",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) * Unit.SI.s, // N·s
        "J_imp",
    tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Action (J·s) classical action dimension.</summary>
    public static readonly QuantityKind Action = new(
        "Action",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.s, // J·s
        "S_act",
    tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Specific angular momentum (m^2/s).</summary>
    public static readonly QuantityKind SpecificAngularMomentum = new(
        "SpecificAngularMomentum",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "h_ang",
    tags: new[] { QuantityKindTags.DomainMechanics, QuantityKindTags.DomainKinematics });

    /// <summary>Torsion (1/m) geometric curve torsion.</summary>
    public static readonly QuantityKind Torsion = new(
        "Torsion",
        1 / Unit.SI.m,
        "τ_tor",
    tags: new[] { QuantityKindTags.DomainGeometry });

    // Thermal
    /// <summary>Volumetric heat capacity (J/(m^3·K)).</summary>
    public static readonly QuantityKind VolumetricHeatCapacity = new(
        "VolumetricHeatCapacity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / ((Unit.SI.m ^ 3) * Unit.SI.K), // J/(m^3·K)
        "C_vol",
    tags: new[] { QuantityKindTags.DomainThermodynamic });

    /// <summary>Emissivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Emissivity = new(
        "Emissivity",
        Unit.None,
        "ε_emis",
    tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless });

    /// <summary>Absorptivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Absorptivity = new(
        "Absorptivity",
        Unit.None,
        "α_abs",
    tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless });

    /// <summary>Reflectivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Reflectivity = new(
        "Reflectivity",
        Unit.None,
        "ρ_refl",
    tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless });

    /// <summary>Transmissivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Transmissivity = new(
        "Transmissivity",
        Unit.None,
        "τ_trans",
    tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless });

    // Fluid / Transport dimensionless groups & properties
    /// <summary>Reynolds number (dimensionless).</summary>
    public static readonly QuantityKind Reynolds = new(
        "Reynolds",
        Unit.None,
        "Re",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Prandtl number (dimensionless).</summary>
    public static readonly QuantityKind Prandtl = new(
        "Prandtl",
        Unit.None,
        "Pr",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Nusselt number (dimensionless).</summary>
    public static readonly QuantityKind Nusselt = new(
        "Nusselt",
        Unit.None,
        "Nu",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Schmidt number (dimensionless).</summary>
    public static readonly QuantityKind Schmidt = new(
        "Schmidt",
        Unit.None,
        "Sc",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Sherwood number (dimensionless).</summary>
    public static readonly QuantityKind Sherwood = new(
        "Sherwood",
        Unit.None,
        "Sh",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Biot number (dimensionless).</summary>
    public static readonly QuantityKind Biot = new(
        "Biot",
        Unit.None,
        "Bi",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless });

    /// <summary>Specific weight (N/m^3).</summary>
    public static readonly QuantityKind SpecificWeight = new(
        "SpecificWeight",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 3), // N/m^3
        "γ_spec",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainMaterial });

    // Radiometry spectral
    /// <summary>Spectral radiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralRadiance = new(
        "SpectralRadiance",
        // Radiance / m => (kg·m^2/s^3)/(m^3)
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3),
        "L_eλ",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Spectral irradiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralIrradiance = new(
        "SpectralIrradiance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3), // W/(m^3)
        "E_eλ",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Spectral radiant flux (W/m).</summary>
    public static readonly QuantityKind SpectralFlux = new(
        "SpectralFlux",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / Unit.SI.m, // W/m
        "Φ_eλ",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Refractive index (dimensionless).</summary>
    public static readonly QuantityKind RefractiveIndex = new(
        "RefractiveIndex",
        Unit.None,
        "n_refr",
    tags: new[] { QuantityKindTags.DomainOptics, QuantityKindTags.FormDimensionless });

    /// <summary>Optical path length (m).</summary>
    public static readonly QuantityKind OpticalPathLength = new(
        "OpticalPathLength",
        Unit.SI.m,
        "L_opt",
    tags: new[] { QuantityKindTags.DomainOptics });

    // Chemistry / Material
    /// <summary>Partial pressure (Pa).</summary>
    public static readonly QuantityKind PartialPressure = new(
        "PartialPressure",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "p_i",
    tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic });

    /// <summary>Activity (dimensionless).</summary>
    public static readonly QuantityKind Activity = new(
        "Activity",
        Unit.None,
        "a",
    tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless });

    /// <summary>Activity coefficient (dimensionless).</summary>
    public static readonly QuantityKind ActivityCoefficient = new(
        "ActivityCoefficient",
        Unit.None,
        "γ_act",
    tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.FormDimensionless });

    /// <summary>Mass attenuation coefficient (m^2/kg).</summary>
    public static readonly QuantityKind MassAttenuationCoefficient = new(
        "MassAttenuationCoefficient",
        (Unit.SI.m ^ 2) / Unit.SI.kg,
        "μ_mass",
    tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainRadiation });

    /// <summary>Henry's constant (Pa·m^3/mol).</summary>
    public static readonly QuantityKind HenrysConstant = new(
        "HenrysConstant",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * (Unit.SI.m ^ 3) / Unit.SI.n, // Pa·m^3/mol
        "H_c",
    tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic });
}