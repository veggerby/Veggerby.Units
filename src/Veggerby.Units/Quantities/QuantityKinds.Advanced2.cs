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
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Action (J·s) classical action dimension.</summary>
    public static readonly QuantityKind Action = new(
        "Action",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.s, // J·s
        "S_act",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics") });

    /// <summary>Specific angular momentum (m^2/s).</summary>
    public static readonly QuantityKind SpecificAngularMomentum = new(
        "SpecificAngularMomentum",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "h_ang",
        tags: new[] { QuantityKindTag.Get("Domain.Mechanics"), QuantityKindTag.Get("Domain.Kinematics") });

    /// <summary>Torsion (1/m) geometric curve torsion.</summary>
    public static readonly QuantityKind Torsion = new(
        "Torsion",
        1 / Unit.SI.m,
        "τ_tor",
        tags: new[] { QuantityKindTag.Get("Domain.Geometry") });

    // Thermal
    /// <summary>Volumetric heat capacity (J/(m^3·K)).</summary>
    public static readonly QuantityKind VolumetricHeatCapacity = new(
        "VolumetricHeatCapacity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / ((Unit.SI.m ^ 3) * Unit.SI.K), // J/(m^3·K)
        "C_vol",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic") });

    /// <summary>Emissivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Emissivity = new(
        "Emissivity",
        Unit.None,
        "ε_emis",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Absorptivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Absorptivity = new(
        "Absorptivity",
        Unit.None,
        "α_abs",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Reflectivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Reflectivity = new(
        "Reflectivity",
        Unit.None,
        "ρ_refl",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Transmissivity (dimensionless 0..1).</summary>
    public static readonly QuantityKind Transmissivity = new(
        "Transmissivity",
        Unit.None,
        "τ_trans",
        tags: new[] { QuantityKindTag.Get("Domain.Thermodynamic"), QuantityKindTag.Get("Form.Dimensionless") });

    // Fluid / Transport dimensionless groups & properties
    /// <summary>Reynolds number (dimensionless).</summary>
    public static readonly QuantityKind Reynolds = new(
        "Reynolds",
        Unit.None,
        "Re",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Prandtl number (dimensionless).</summary>
    public static readonly QuantityKind Prandtl = new(
        "Prandtl",
        Unit.None,
        "Pr",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Nusselt number (dimensionless).</summary>
    public static readonly QuantityKind Nusselt = new(
        "Nusselt",
        Unit.None,
        "Nu",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Schmidt number (dimensionless).</summary>
    public static readonly QuantityKind Schmidt = new(
        "Schmidt",
        Unit.None,
        "Sc",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Sherwood number (dimensionless).</summary>
    public static readonly QuantityKind Sherwood = new(
        "Sherwood",
        Unit.None,
        "Sh",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Biot number (dimensionless).</summary>
    public static readonly QuantityKind Biot = new(
        "Biot",
        Unit.None,
        "Bi",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Specific weight (N/m^3).</summary>
    public static readonly QuantityKind SpecificWeight = new(
        "SpecificWeight",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 3), // N/m^3
        "γ_spec",
        tags: new[] { QuantityKindTag.Get("Domain.Transport"), QuantityKindTag.Get("Domain.Material") });

    // Radiometry spectral
    /// <summary>Spectral radiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralRadiance = new(
        "SpectralRadiance",
        // Radiance / m => (kg·m^2/s^3)/(m^3)
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3),
        "L_eλ",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Spectral irradiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralIrradiance = new(
        "SpectralIrradiance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3), // W/(m^3)
        "E_eλ",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Spectral radiant flux (W/m).</summary>
    public static readonly QuantityKind SpectralFlux = new(
        "SpectralFlux",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / Unit.SI.m, // W/m
        "Φ_eλ",
        tags: new[] { QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Refractive index (dimensionless).</summary>
    public static readonly QuantityKind RefractiveIndex = new(
        "RefractiveIndex",
        Unit.None,
        "n_refr",
        tags: new[] { QuantityKindTag.Get("Domain.Optics"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Optical path length (m).</summary>
    public static readonly QuantityKind OpticalPathLength = new(
        "OpticalPathLength",
        Unit.SI.m,
        "L_opt",
        tags: new[] { QuantityKindTag.Get("Domain.Optics") });

    // Chemistry / Material
    /// <summary>Partial pressure (Pa).</summary>
    public static readonly QuantityKind PartialPressure = new(
        "PartialPressure",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "p_i",
        tags: new[] { QuantityKindTag.Get("Domain.Chemistry"), QuantityKindTag.Get("Domain.Thermodynamic") });

    /// <summary>Activity (dimensionless).</summary>
    public static readonly QuantityKind Activity = new(
        "Activity",
        Unit.None,
        "a",
        tags: new[] { QuantityKindTag.Get("Domain.Chemistry"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Activity coefficient (dimensionless).</summary>
    public static readonly QuantityKind ActivityCoefficient = new(
        "ActivityCoefficient",
        Unit.None,
        "γ_act",
        tags: new[] { QuantityKindTag.Get("Domain.Chemistry"), QuantityKindTag.Get("Form.Dimensionless") });

    /// <summary>Mass attenuation coefficient (m^2/kg).</summary>
    public static readonly QuantityKind MassAttenuationCoefficient = new(
        "MassAttenuationCoefficient",
        (Unit.SI.m ^ 2) / Unit.SI.kg,
        "μ_mass",
        tags: new[] { QuantityKindTag.Get("Domain.Material"), QuantityKindTag.Get("Domain.Radiation") });

    /// <summary>Henry's constant (Pa·m^3/mol).</summary>
    public static readonly QuantityKind HenrysConstant = new(
        "HenrysConstant",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * (Unit.SI.m ^ 3) / Unit.SI.n, // Pa·m^3/mol
        "H_c",
        tags: new[] { QuantityKindTag.Get("Domain.Chemistry"), QuantityKindTag.Get("Domain.Thermodynamic") });
}