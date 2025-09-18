namespace Veggerby.Units.Quantities;

/// <summary>Additional high-value semantic kinds (structures, advanced fluids, thermal coefficients, optical exposures, susceptibilities, nuclear/chemistry rates).</summary>
public static partial class QuantityKinds
{
    // Mechanics & Structures
    /// <summary>Stress (Pa) synonym semantic anchor distinct from Pressure.</summary>
    public static readonly QuantityKind Stress = new(
        "Stress",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "σ",
        tags: new[] { QuantityKindTags.DomainMechanics, QuantityKindTags.DomainMaterial });

    /// <summary>Shear stress (Pa) semantic anchor.</summary>
    public static readonly QuantityKind ShearStress = new(
        "ShearStress",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "τ_s",
        tags: new[] { QuantityKindTags.DomainMechanics, QuantityKindTags.DomainMaterial });

    /// <summary>Second moment of area (area moment of inertia) (m^4).</summary>
    public static readonly QuantityKind SecondMomentOfArea = new(
        "SecondMomentOfArea",
        Unit.SI.m ^ 4,
        "I_A",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Section modulus (m^3).</summary>
    public static readonly QuantityKind SectionModulus = new(
        "SectionModulus",
        Unit.SI.m ^ 3,
        "Z_sec",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Stiffness (N/m) force per displacement.</summary>
    public static readonly QuantityKind Stiffness = new(
        "Stiffness",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / Unit.SI.m,
        "k_st",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Compliance (m/N) displacement per force.</summary>
    public static readonly QuantityKind Compliance = new(
        "Compliance",
        Unit.SI.m / (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)),
        "C_comp",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Damping coefficient (N·s/m).</summary>
    public static readonly QuantityKind DampingCoefficient = new(
        "DampingCoefficient",
        ((Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) * Unit.SI.s) / Unit.SI.m,
        "c_d",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Coefficient of friction (dimensionless).</summary>
    public static readonly QuantityKind CoefficientOfFriction = new(
        "CoefficientOfFriction",
        Unit.None,
        "μ_f",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainMechanics });

    /// <summary>Poisson ratio (dimensionless).</summary>
    public static readonly QuantityKind PoissonRatio = new(
        "PoissonRatio",
        Unit.None,
        "ν_p",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainMaterial });

    // Kinematics & Fluids dimensionless / circulation
    /// <summary>Vorticity (1/s) local rotation rate.</summary>
    public static readonly QuantityKind Vorticity = new(
        "Vorticity",
        1 / Unit.SI.s,
        "ω_v",
        tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainKinematics });

    /// <summary>Circulation (m^2/s).</summary>
    public static readonly QuantityKind Circulation = new(
        "Circulation",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "Γ",
        tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainKinematics });

    // Fluid / flow dimensionless groups
    /// <summary>Mach number (dimensionless) ratio of flow speed to speed of sound.</summary>
    public static readonly QuantityKind MachNumber = new(
        "MachNumber",
        Unit.None,
        "Ma",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Froude number (dimensionless) inertia vs gravity.</summary>
    public static readonly QuantityKind FroudeNumber = new(
        "FroudeNumber",
        Unit.None,
        "Fr",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Weber number (dimensionless) inertia vs surface tension.</summary>
    public static readonly QuantityKind WeberNumber = new(
        "WeberNumber",
        Unit.None,
        "We",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Péclet number (dimensionless) advection vs diffusion.</summary>
    public static readonly QuantityKind PecletNumber = new(
        "PecletNumber",
        Unit.None,
        "Pe",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Stanton number (dimensionless) heat transferred into a fluid.</summary>
    public static readonly QuantityKind StantonNumber = new(
        "StantonNumber",
        Unit.None,
        "St",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Strouhal number (dimensionless) oscillating flow mechanisms.</summary>
    public static readonly QuantityKind StrouhalNumber = new(
        "StrouhalNumber",
        Unit.None,
        "St_r",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Euler number (dimensionless) pressure forces vs inertial forces.</summary>
    public static readonly QuantityKind EulerNumber = new(
        "EulerNumber",
        Unit.None,
        "Eu",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Knudsen number (dimensionless) molecular mean free path scale.</summary>
    public static readonly QuantityKind KnudsenNumber = new(
        "KnudsenNumber",
        Unit.None,
        "Kn",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    /// <summary>Damköhler number (dimensionless) reaction rate vs transport rate.</summary>
    public static readonly QuantityKind DamkohlerNumber = new(
        "DamkohlerNumber",
        Unit.None,
        "Da",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport, QuantityKindTags.DomainChemistry });

    /// <summary>Richardson number (dimensionless) buoyancy vs shear.</summary>
    public static readonly QuantityKind RichardsonNumber = new(
        "RichardsonNumber",
        Unit.None,
        "Ri",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport });

    // Thermal
    /// <summary>Coefficient of thermal expansion (1/K).</summary>
    public static readonly QuantityKind CoefficientOfThermalExpansion = new(
        "CoefficientOfThermalExpansion",
        1 / Unit.SI.K,
        "α_th",
        tags: new[] { QuantityKindTags.DomainThermodynamic });

    /// <summary>Specific latent heat (J/kg) = m^2/s^2.</summary>
    public static readonly QuantityKind SpecificLatentHeat = new(
        "SpecificLatentHeat",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "L_spec",
        tags: new[] { QuantityKindTags.DomainThermodynamic });

    /// <summary>Molar latent heat (J/mol).</summary>
    public static readonly QuantityKind MolarLatentHeat = new(
        "MolarLatentHeat",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.n,
        "L_mol",
        tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainChemistry });

    /// <summary>Isentropic exponent (dimensionless) (γ).</summary>
    public static readonly QuantityKind IsentropicExponent = new(
        "IsentropicExponent",
        Unit.None,
        "γ_iso",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainThermodynamic });

    // Optics / Radiometry
    /// <summary>Radiant exposure (J/m^2).</summary>
    public static readonly QuantityKind RadiantExposure = new(
        "RadiantExposure",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 2),
        "H_e",
        tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Luminous exposure (lx·s = cd·s/m^2).</summary>
    public static readonly QuantityKind LuminousExposure = new(
        "LuminousExposure",
        (Unit.SI.cd / (Unit.SI.m ^ 2)) * Unit.SI.s,
        "H_v",
        tags: new[] { QuantityKindTags.DomainOptics });

    /// <summary>Linear attenuation coefficient (1/m).</summary>
    public static readonly QuantityKind LinearAttenuationCoefficient = new(
        "LinearAttenuationCoefficient",
        1 / Unit.SI.m,
        "μ_lin",
        tags: new[] { QuantityKindTags.DomainRadiation, QuantityKindTags.DomainMaterial });

    /// <summary>Optical depth (dimensionless) path integral of attenuation.</summary>
    public static readonly QuantityKind OpticalDepth = new(
        "OpticalDepth",
        Unit.None,
        "τ_opt",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainOptics });

    // Electromagnetics / Materials
    /// <summary>Hall coefficient (m^3/C) = m^3/(A·s).</summary>
    public static readonly QuantityKind HallCoefficient = new(
        "HallCoefficient",
        (Unit.SI.m ^ 3) / (Unit.SI.A * Unit.SI.s),
        "R_H",
        tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Seebeck coefficient (V/K).</summary>
    public static readonly QuantityKind SeebeckCoefficient = new(
        "SeebeckCoefficient",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.s * Unit.SI.K),
        "S_seeb",
        tags: new[] { QuantityKindTags.DomainElectromagnetic, QuantityKindTags.DomainThermodynamic });

    /// <summary>Magnetic susceptibility (dimensionless volume susceptibility).</summary>
    public static readonly QuantityKind MagneticSusceptibility = new(
        "MagneticSusceptibility",
        Unit.None,
        "χ_m",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic });

    /// <summary>Electric susceptibility (dimensionless).</summary>
    public static readonly QuantityKind ElectricSusceptibility = new(
        "ElectricSusceptibility",
        Unit.None,
        "χ_e",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic });

    /// <summary>Relative permittivity (dimensionless).</summary>
    public static readonly QuantityKind RelativePermittivity = new(
        "RelativePermittivity",
        Unit.None,
        "ε_r",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic });

    /// <summary>Relative permeability (dimensionless).</summary>
    public static readonly QuantityKind RelativePermeability = new(
        "RelativePermeability",
        Unit.None,
        "μ_r",
        tags: new[] { QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic });

    // Chemistry / Nuclear
    /// <summary>Activity concentration (Bq/m^3) = s^-1·m^-3.</summary>
    public static readonly QuantityKind ActivityConcentration = new(
        "ActivityConcentration",
        (1 / Unit.SI.s) / (Unit.SI.m ^ 3),
        "A_c",
        tags: new[] { QuantityKindTags.DomainRadiation, QuantityKindTags.DomainChemistry });

    /// <summary>Absorbed dose rate (Gy/s) = m^2/s^3.</summary>
    public static readonly QuantityKind AbsorbedDoseRate = new(
        "AbsorbedDoseRate",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "Ḋ_abs",
        tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Equivalent dose rate (Sv/s) = m^2/s^3.</summary>
    public static readonly QuantityKind EquivalentDoseRate = new(
        "EquivalentDoseRate",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "Ḋ_eq",
        tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Fluence (1/m^2).</summary>
    public static readonly QuantityKind Fluence = new(
        "Fluence",
        1 / (Unit.SI.m ^ 2),
        "Φ_flu",
        tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Fluence rate (1/(m^2·s)).</summary>
    public static readonly QuantityKind FluenceRate = new(
        "FluenceRate",
        (1 / (Unit.SI.m ^ 2)) / Unit.SI.s,
        "Φ̇_flu",
        tags: new[] { QuantityKindTags.DomainRadiation });
}