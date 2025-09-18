namespace Veggerby.Units.Quantities;

/// <summary>Additional semantic quantity kinds (geometry, kinematics, flow, material, thermal, electromagnetic, radiometry/photometry, chemistry, ratios).</summary>
public static partial class QuantityKinds
{
    // Geometry & Kinematics
    /// <summary>Plane angle (radian) – existing Angle already dimensionless; solid angle adds steradian semantic.</summary>
    public static readonly QuantityKind SolidAngle = new(
        "SolidAngle",
        Unit.SI.sr,
        "Ω_s",
        tags: new[] { QuantityKindTags.DomainGeometry });

    /// <summary>Angular velocity (rad/s).</summary>
    public static readonly QuantityKind AngularVelocity = new(
        "AngularVelocity",
        Unit.SI.rad / Unit.SI.s,
        "ω",
        tags: new[] { QuantityKindTags.DomainKinematics });

    /// <summary>Angular acceleration (rad/s²).</summary>
    public static readonly QuantityKind AngularAcceleration = new(
        "AngularAcceleration",
        Unit.SI.rad / (Unit.SI.s ^ 2),
        "α_ang",
        tags: new[] { QuantityKindTags.DomainKinematics });

    /// <summary>Angular momentum (kg·m²/s).</summary>
    public static readonly QuantityKind AngularMomentum = new(
        "AngularMomentum",
        Unit.SI.kg * (Unit.SI.m ^ 2) / Unit.SI.s,
        "L_ang",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Moment of inertia (kg·m²).</summary>
    public static readonly QuantityKind MomentOfInertia = new(
        "MomentOfInertia",
        Unit.SI.kg * (Unit.SI.m ^ 2),
        "I_m",
        tags: new[] { QuantityKindTags.DomainMechanics });

    /// <summary>Jerk (time derivative of acceleration) (m/s³).</summary>
    public static readonly QuantityKind Jerk = new(
        "Jerk",
        Unit.SI.m / (Unit.SI.s ^ 3),
        "j",
        tags: new[] { QuantityKindTags.DomainKinematics });

    /// <summary>Wavenumber / spatial frequency (1/m).</summary>
    public static readonly QuantityKind Wavenumber = new(
        "Wavenumber",
        1 / Unit.SI.m,
        "k",
        tags: new[] { QuantityKindTags.DomainGeometry });

    /// <summary>Curvature (1/m).</summary>
    public static readonly QuantityKind Curvature = new(
        "Curvature",
        1 / Unit.SI.m,
        "κ",
        tags: new[] { QuantityKindTags.DomainGeometry });

    /// <summary>Strain (dimensionless ratio).</summary>
    public static readonly QuantityKind Strain = new(
        "Strain",
        Unit.None,
        "ε",
        tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>Strain rate / shear rate (1/s).</summary>
    public static readonly QuantityKind StrainRate = new(
        "StrainRate",
        1 / Unit.SI.s,
        "ε_dot",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainTransport });

    // Flow & Rates
    /// <summary>Volumetric flow rate (m³/s).</summary>
    public static readonly QuantityKind VolumetricFlowRate = new(
        "VolumetricFlowRate",
        (Unit.SI.m ^ 3) / Unit.SI.s,
        "Q_v",
        tags: new[] { QuantityKindTags.DomainTransport });

    /// <summary>Mass flow rate (kg/s).</summary>
    public static readonly QuantityKind MassFlowRate = new(
        "MassFlowRate",
        Unit.SI.kg / Unit.SI.s,
        "ṁ",
        tags: new[] { QuantityKindTags.DomainTransport });

    /// <summary>Molar flow rate (mol/s).</summary>
    public static readonly QuantityKind MolarFlowRate = new(
        "MolarFlowRate",
        Unit.SI.n / Unit.SI.s,
        "n_dot",
        tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainChemistry });

    // Materials & Mechanics
    /// <summary>Young's modulus (Pa).</summary>
    public static readonly QuantityKind YoungsModulus = new(
        "YoungsModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "E",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics });

    /// <summary>Shear modulus (Pa).</summary>
    public static readonly QuantityKind ShearModulus = new(
        "ShearModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "G_mod",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics });

    /// <summary>Bulk modulus (Pa).</summary>
    public static readonly QuantityKind BulkModulus = new(
        "BulkModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "K_mod",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics });

    /// <summary>Compressibility (1/Pa).</summary>
    public static readonly QuantityKind Compressibility = new(
        "Compressibility",
        (Unit.SI.m * (Unit.SI.s ^ 2)) / Unit.SI.kg,
        "β_T",
        tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>Linear mass density (kg/m).</summary>
    public static readonly QuantityKind LinearMassDensity = new(
        "LinearMassDensity",
        Unit.SI.kg / Unit.SI.m,
        "μ_lin",
        tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>Areal mass density (kg/m²).</summary>
    public static readonly QuantityKind ArealMassDensity = new(
        "ArealMassDensity",
        Unit.SI.kg / (Unit.SI.m ^ 2),
        "σ_areal",
        tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>Number density (1/m³).</summary>
    public static readonly QuantityKind NumberDensity = new(
        "NumberDensity",
        1 / (Unit.SI.m ^ 3),
        "n_d",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainChemistry });

    /// <summary>Areal number density (1/m²).</summary>
    public static readonly QuantityKind ArealNumberDensity = new(
        "ArealNumberDensity",
        1 / (Unit.SI.m ^ 2),
        "n_dA",
        tags: new[] { QuantityKindTags.DomainMaterial, QuantityKindTags.DomainChemistry });

    /// <summary>Porosity (dimensionless ratio 0..1).</summary>
    public static readonly QuantityKind Porosity = new(
        "Porosity",
        Unit.None,
        "φ",
        tags: new[] { QuantityKindTags.DomainMaterial });

    // Thermal
    /// <summary>Thermal resistance (K/W).</summary>
    public static readonly QuantityKind ThermalResistance = new(
        "ThermalResistance",
        Unit.SI.K * Unit.SI.s / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
        "R_th",
        tags: new[] { QuantityKindTags.DomainThermodynamic });

    /// <summary>Thermal conductance (W/K) reciprocal of resistance.</summary>
    public static readonly QuantityKind ThermalConductance = new(
        "ThermalConductance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.K * Unit.SI.s),
        "G_th",
        tags: new[] { QuantityKindTags.DomainThermodynamic });

    /// <summary>Heat transfer coefficient (W/(m²·K)).</summary>
    public static readonly QuantityKind HeatTransferCoefficient = new(
        "HeatTransferCoefficient",
        ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.s) / ((Unit.SI.m ^ 2) * Unit.SI.K),
        "h_c",
        tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainTransport });

    /// <summary>Molar heat capacity (J/(mol·K)).</summary>
    public static readonly QuantityKind MolarHeatCapacity = new(
        "MolarHeatCapacity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.n * Unit.SI.K),
        "C_m",
        tags: new[] { QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainChemistry });

    /// <summary>Specific enthalpy (J/kg).</summary>
    public static readonly QuantityKind SpecificEnthalpy = new(
        "SpecificEnthalpy",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "h_spec",
        tags: new[] { QuantityKindTags.DomainThermodynamic });

    // Electromagnetics (field additions)
    /// <summary>Magnetic field strength H (A/m).</summary>
    public static readonly QuantityKind MagneticFieldStrength = new(
        "MagneticFieldStrength",
        Unit.SI.A / Unit.SI.m,
        "H",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Magnetization M (A/m).</summary>
    public static readonly QuantityKind Magnetization = new(
        "Magnetization",
        Unit.SI.A / Unit.SI.m,
        "M_mag",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Electric displacement field D (C/m²).</summary>
    public static readonly QuantityKind ElectricDisplacement = new(
        "ElectricDisplacement",
        (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2),
        "D",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Magnetic vector potential (Wb/m).</summary>
    public static readonly QuantityKind MagneticVectorPotential = new(
        "MagneticVectorPotential",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.m),
        "A_vec",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Permittivity ε (F/m).</summary>
    public static readonly QuantityKind Permittivity = new(
        "Permittivity",
        ((Unit.SI.A * Unit.SI.s) ^ 2) / ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.m),
        "ε",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Permeability μ (H/m).</summary>
    public static readonly QuantityKind Permeability = new(
        "Permeability",
        // μ = Energy / (A^2 · m)
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A * Unit.SI.m),
        "μ",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Electrical impedance / reactance (Ω).</summary>
    public static readonly QuantityKind Impedance = new(
        "Impedance",
        // Ω = Energy / (A^2 · s)
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A * Unit.SI.s),
        "Z",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Electrical admittance / susceptance (S).</summary>
    public static readonly QuantityKind Admittance = new(
        "Admittance",
        // S = A^2·s / Energy
        (Unit.SI.A * Unit.SI.A * Unit.SI.s) / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
        "Y",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>Charge mobility (m²/(V·s)).</summary>
    public static readonly QuantityKind ChargeMobility = new(
        "ChargeMobility",
        (Unit.SI.m ^ 2) * (Unit.SI.A * Unit.SI.s) / ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.s),
        "μ_e",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    // Radiation / Photometry additions
    /// <summary>Radiant flux (W).</summary>
    public static readonly QuantityKind RadiantFlux = new(
        "RadiantFlux",
        // W = kg·m^2/s^3
        Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3),
        "Φ_e",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Radiant intensity (W/sr).</summary>
    public static readonly QuantityKind RadiantIntensity = new(
        "RadiantIntensity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / Unit.SI.sr,
        "I_e",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Irradiance (W/m²).</summary>
    public static readonly QuantityKind Irradiance = new(
        "Irradiance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 2),
        "E_e",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Radiance (W/(m²·sr)).</summary>
    public static readonly QuantityKind Radiance = new(
        "Radiance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / ((Unit.SI.m ^ 2) * Unit.SI.sr),
        "L_e",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Radiation exposure (C/kg).</summary>
    public static readonly QuantityKind RadiationExposure = new(
        "RadiationExposure",
        (Unit.SI.A * Unit.SI.s) / Unit.SI.kg,
        "X",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>Luminance (cd/m²).</summary>
    public static readonly QuantityKind Luminance = new(
        "Luminance",
        Unit.SI.cd / (Unit.SI.m ^ 2),
        "L_v",
    tags: new[] { QuantityKindTags.DomainOptics });

    /// <summary>Luminous intensity (cd).</summary>
    public static readonly QuantityKind LuminousIntensity = new(
        "LuminousIntensity",
        Unit.SI.cd,
        "I_v",
    tags: new[] { QuantityKindTags.DomainOptics });

    /// <summary>Luminous efficacy (lm/W).</summary>
    public static readonly QuantityKind LuminousEfficacy = new(
        "LuminousEfficacy",
        Unit.SI.cd / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)),
        "K_cd",
    tags: new[] { QuantityKindTags.DomainOptics });

    // Chemistry additions
    /// <summary>Diffusion coefficient (m²/s).</summary>
    public static readonly QuantityKind DiffusionCoefficient = new(
        "DiffusionCoefficient",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "D_diff",
        tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainChemistry });

    /// <summary>Reaction rate (mol/(m³·s)).</summary>
    public static readonly QuantityKind ReactionRate = new(
        "ReactionRate",
        Unit.SI.n / ((Unit.SI.m ^ 3) * Unit.SI.s),
        "r_rxn",
        tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>Mole fraction (dimensionless).</summary>
    public static readonly QuantityKind MoleFraction = new(
        "MoleFraction",
        Unit.None,
        "x",
        tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>Mass fraction (dimensionless).</summary>
    public static readonly QuantityKind MassFraction = new(
        "MassFraction",
        Unit.None,
        "w",
        tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>Osmotic pressure (Pa).</summary>
    public static readonly QuantityKind OsmoticPressure = new(
        "OsmoticPressure",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "π_osm",
        tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.DomainThermodynamic });

    // Odds & Ends
    /// <summary>Generic dimensionless ratio (unitless) distinct semantic anchor.</summary>
    public static readonly QuantityKind DimensionlessRatio = new(
        "DimensionlessRatio",
        Unit.None,
        "R_dim",
        tags: new[] { QuantityKindTags.FormDimensionless });
}