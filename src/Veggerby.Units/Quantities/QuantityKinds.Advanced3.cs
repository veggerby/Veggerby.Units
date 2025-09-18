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
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics"), QuantityKindTag.Get("Domain.Material") });

    /// <summary>Shear stress (Pa) semantic anchor.</summary>
    public static readonly QuantityKind ShearStress = new(
        "ShearStress",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "τ_s",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics"), QuantityKindTag.Get("Domain.Material") });

    /// <summary>Second moment of area (area moment of inertia) (m^4).</summary>
    public static readonly QuantityKind SecondMomentOfArea = new(
        "SecondMomentOfArea",
        Unit.SI.m ^ 4,
        "I_A",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Section modulus (m^3).</summary>
    public static readonly QuantityKind SectionModulus = new(
        "SectionModulus",
        Unit.SI.m ^ 3,
        "Z_sec",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Stiffness (N/m) force per displacement.</summary>
    public static readonly QuantityKind Stiffness = new(
        "Stiffness",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / Unit.SI.m,
        "k_st",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Compliance (m/N) displacement per force.</summary>
    public static readonly QuantityKind Compliance = new(
        "Compliance",
        Unit.SI.m / (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)),
        "C_comp",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Damping coefficient (N·s/m).</summary>
    public static readonly QuantityKind DampingCoefficient = new(
        "DampingCoefficient",
        ((Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) * Unit.SI.s) / Unit.SI.m,
        "c_d",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Coefficient of friction (dimensionless).</summary>
    public static readonly QuantityKind CoefficientOfFriction = new(
        "CoefficientOfFriction",
        Unit.None,
        "μ_f",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Poisson ratio (dimensionless).</summary>
    public static readonly QuantityKind PoissonRatio = new(
        "PoissonRatio",
        Unit.None,
        "ν_p",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Material") });

    // Kinematics & Fluids dimensionless / circulation
    /// <summary>Vorticity (1/s) local rotation rate.</summary>
    public static readonly QuantityKind Vorticity = new(
        "Vorticity",
        1 / Unit.SI.s,
        "ω_v",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Domain.Kinematics") });

    /// <summary>Circulation (m^2/s).</summary>
    public static readonly QuantityKind Circulation = new(
        "Circulation",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "Γ",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Domain.Kinematics") });

    // Fluid / flow dimensionless groups
    /// <summary>Mach number (dimensionless) ratio of flow speed to speed of sound.</summary>
    public static readonly QuantityKind MachNumber = new(
        "MachNumber",
        Unit.None,
        "Ma",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Froude number (dimensionless) inertia vs gravity.</summary>
    public static readonly QuantityKind FroudeNumber = new(
        "FroudeNumber",
        Unit.None,
        "Fr",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Weber number (dimensionless) inertia vs surface tension.</summary>
    public static readonly QuantityKind WeberNumber = new(
        "WeberNumber",
        Unit.None,
        "We",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Péclet number (dimensionless) advection vs diffusion.</summary>
    public static readonly QuantityKind PecletNumber = new(
        "PecletNumber",
        Unit.None,
        "Pe",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Stanton number (dimensionless) heat transferred into a fluid.</summary>
    public static readonly QuantityKind StantonNumber = new(
        "StantonNumber",
        Unit.None,
        "St",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Strouhal number (dimensionless) oscillating flow mechanisms.</summary>
    public static readonly QuantityKind StrouhalNumber = new(
        "StrouhalNumber",
        Unit.None,
        "St_r",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Euler number (dimensionless) pressure forces vs inertial forces.</summary>
    public static readonly QuantityKind EulerNumber = new(
        "EulerNumber",
        Unit.None,
        "Eu",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Knudsen number (dimensionless) molecular mean free path scale.</summary>
    public static readonly QuantityKind KnudsenNumber = new(
        "KnudsenNumber",
        Unit.None,
        "Kn",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    /// <summary>Damköhler number (dimensionless) reaction rate vs transport rate.</summary>
    public static readonly QuantityKind DamkohlerNumber = new(
        "DamkohlerNumber",
        Unit.None,
        "Da",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Domain.Chemistry") });

    /// <summary>Richardson number (dimensionless) buoyancy vs shear.</summary>
    public static readonly QuantityKind RichardsonNumber = new(
        "RichardsonNumber",
        Unit.None,
        "Ri",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Transport") });

    // Thermal
    /// <summary>Coefficient of thermal expansion (1/K).</summary>
    public static readonly QuantityKind CoefficientOfThermalExpansion = new(
        "CoefficientOfThermalExpansion",
        1 / Unit.SI.K,
        "α_th",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic") });

    /// <summary>Specific latent heat (J/kg) = m^2/s^2.</summary>
    public static readonly QuantityKind SpecificLatentHeat = new(
        "SpecificLatentHeat",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "L_spec",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic") });

    /// <summary>Molar latent heat (J/mol).</summary>
    public static readonly QuantityKind MolarLatentHeat = new(
        "MolarLatentHeat",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.n,
        "L_mol",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic"), QuantityKindTag.Get("Domain.Chemistry") });

    /// <summary>Isentropic exponent (dimensionless) (γ).</summary>
    public static readonly QuantityKind IsentropicExponent = new(
        "IsentropicExponent",
        Unit.None,
        "γ_iso",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Thermodynamic") });

    // Optics / Radiometry
    /// <summary>Radiant exposure (J/m^2).</summary>
    public static readonly QuantityKind RadiantExposure = new(
        "RadiantExposure",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 2),
        "H_e",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Luminous exposure (lx·s = cd·s/m^2).</summary>
    public static readonly QuantityKind LuminousExposure = new(
        "LuminousExposure",
        (Unit.SI.cd / (Unit.SI.m ^ 2)) * Unit.SI.s,
        "H_v",
        tags: new[] { QuantityKindTag.Get("Domain.Optics") });

    /// <summary>Linear attenuation coefficient (1/m).</summary>
    public static readonly QuantityKind LinearAttenuationCoefficient = new(
        "LinearAttenuationCoefficient",
        1 / Unit.SI.m,
        "μ_lin",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation"), QuantityKindTag.Get("Domain.Material") });

    /// <summary>Optical depth (dimensionless) path integral of attenuation.</summary>
    public static readonly QuantityKind OpticalDepth = new(
        "OpticalDepth",
        Unit.None,
        "τ_opt",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Optics") });

    // Electromagnetics / Materials
    /// <summary>Hall coefficient (m^3/C) = m^3/(A·s).</summary>
    public static readonly QuantityKind HallCoefficient = new(
        "HallCoefficient",
        (Unit.SI.m ^ 3) / (Unit.SI.A * Unit.SI.s),
        "R_H",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Seebeck coefficient (V/K).</summary>
    public static readonly QuantityKind SeebeckCoefficient = new(
        "SeebeckCoefficient",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.s * Unit.SI.K),
        "S_seeb",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic"), QuantityKindTag.Get("Domain.Thermodynamic") });

    /// <summary>Magnetic susceptibility (dimensionless volume susceptibility).</summary>
    public static readonly QuantityKind MagneticSusceptibility = new(
        "MagneticSusceptibility",
        Unit.None,
        "χ_m",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Electric susceptibility (dimensionless).</summary>
    public static readonly QuantityKind ElectricSusceptibility = new(
        "ElectricSusceptibility",
        Unit.None,
        "χ_e",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Relative permittivity (dimensionless).</summary>
    public static readonly QuantityKind RelativePermittivity = new(
        "RelativePermittivity",
        Unit.None,
        "ε_r",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Relative permeability (dimensionless).</summary>
    public static readonly QuantityKind RelativePermeability = new(
        "RelativePermeability",
        Unit.None,
        "μ_r",
        tags: new[] { QuantityKindTag.Get("Form.Dimensionless"), QuantityKindTag.Get("Domain.Electromagnetic") });

    // Chemistry / Nuclear
    /// <summary>Activity concentration (Bq/m^3) = s^-1·m^-3.</summary>
    public static readonly QuantityKind ActivityConcentration = new(
        "ActivityConcentration",
        (1 / Unit.SI.s) / (Unit.SI.m ^ 3),
        "A_c",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation"), QuantityKindTag.Get("Domain.Chemistry") });

    /// <summary>Absorbed dose rate (Gy/s) = m^2/s^3.</summary>
    public static readonly QuantityKind AbsorbedDoseRate = new(
        "AbsorbedDoseRate",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "Ḋ_abs",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Equivalent dose rate (Sv/s) = m^2/s^3.</summary>
    public static readonly QuantityKind EquivalentDoseRate = new(
        "EquivalentDoseRate",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "Ḋ_eq",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Fluence (1/m^2).</summary>
    public static readonly QuantityKind Fluence = new(
        "Fluence",
        1 / (Unit.SI.m ^ 2),
        "Φ_flu",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Fluence rate (1/(m^2·s)).</summary>
    public static readonly QuantityKind FluenceRate = new(
        "FluenceRate",
        (1 / (Unit.SI.m ^ 2)) / Unit.SI.s,
        "Φ̇_flu",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });
}