namespace Veggerby.Units.Quantities;

/// <summary>
/// Energy, thermodynamic state/path functions, and related heat/temperature transfer coefficients.
/// </summary>
public static partial class QuantityKinds
{
    // Energy core
    /// <summary>Energy (J) = kg·m²/s².</summary>
    public static readonly QuantityKind Energy = new(
        "Energy",
        Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "E",
        tags:
        [
            QuantityKindTags.Energy,
            QuantityKindTags.EnergyStateFunction
        ]);

    /// <summary>Internal energy (U) shares energy dimensions.</summary>
    public static readonly QuantityKind InternalEnergy = new(
        "InternalEnergy",
        Energy.CanonicalUnit,
        "U",
        tags:
        [
            QuantityKindTags.Energy,
            QuantityKindTags.EnergyStateFunction,
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Enthalpy (H) shares energy dimensions.</summary>
    public static readonly QuantityKind Enthalpy = new(
        "Enthalpy",
        Energy.CanonicalUnit,
        "H",
        tags:
        [
            QuantityKindTags.Energy,
            QuantityKindTags.EnergyStateFunction,
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Gibbs free energy (G) shares energy dimensions.</summary>
    public static readonly QuantityKind GibbsFreeEnergy = new(
        "GibbsFreeEnergy",
        Energy.CanonicalUnit,
        "G",
        tags:
        [
            QuantityKindTags.Energy,
            QuantityKindTags.EnergyStateFunction,
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Helmholtz free energy (A) shares energy dimensions.</summary>
    public static readonly QuantityKind HelmholtzFreeEnergy = new(
        "HelmholtzFreeEnergy",
        Energy.CanonicalUnit,
        "A",
        tags:
        [
            QuantityKindTags.Energy,
            QuantityKindTags.EnergyStateFunction,
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Entropy (S) J/K.</summary>
    public static readonly QuantityKind Entropy = new(
        "Entropy",
        Energy.CanonicalUnit / Unit.SI.K,
        "S",
        tags:
        [
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Heat capacity (C_p) J/K (semantic distinct from Entropy).</summary>
    public static readonly QuantityKind HeatCapacity = new(
        "HeatCapacity",
        Energy.CanonicalUnit / Unit.SI.K,
        "C_p",
        tags:
        [
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Chemical potential (μ) J/mol.</summary>
    public static readonly QuantityKind ChemicalPotential = new(
        "ChemicalPotential",
        Energy.CanonicalUnit / Unit.SI.n,
        "μ",
        tags:
        [
            QuantityKindTags.DomainThermodynamic
        ]);

    /// <summary>Work (W) path energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Work = new(
        "Work",
        Energy.CanonicalUnit,
        "Wk",
        tags:
        [
            QuantityKindTags.EnergyPathFunction,
            QuantityKindTags.DomainMechanics,
            QuantityKindTags.Energy
        ]);

    /// <summary>Heat (Q) path thermal energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Heat = new(
        "Heat",
        Energy.CanonicalUnit,
        "Q",
        tags:
        [
            QuantityKindTags.EnergyPathFunction,
            QuantityKindTags.DomainThermodynamic,
            QuantityKindTags.Energy
        ]);

    // Thermal capacities & related
    /// <summary>Volumetric heat capacity (J/(m^3·K)).</summary>
    public static readonly QuantityKind VolumetricHeatCapacity = new(
        "VolumetricHeatCapacity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / ((Unit.SI.m ^ 3) * Unit.SI.K),
        "C_vol",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Molar heat capacity (J/(mol·K)).</summary>
    public static readonly QuantityKind MolarHeatCapacity = new(
        "MolarHeatCapacity",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.n * Unit.SI.K),
        "C_m",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainChemistry]);

    /// <summary>Specific enthalpy (J/kg).</summary>
    public static readonly QuantityKind SpecificEnthalpy = new(
        "SpecificEnthalpy",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "h_spec",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Specific latent heat (J/kg).</summary>
    public static readonly QuantityKind SpecificLatentHeat = new(
        "SpecificLatentHeat",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "L_spec",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Molar latent heat (J/mol).</summary>
    public static readonly QuantityKind MolarLatentHeat = new(
        "MolarLatentHeat",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.n,
        "L_mol",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainChemistry]);

    // Thermal transfer / flux coefficients
    /// <summary>Heat transfer coefficient (W/(m²·K)).</summary>
    public static readonly QuantityKind HeatTransferCoefficient = new(
        "HeatTransferCoefficient",
        ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.s) / ((Unit.SI.m ^ 2) * Unit.SI.K),
        "h_c",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.DomainTransport]);

    /// <summary>Thermal conductivity – heat flow rate per length and temperature gradient (W·m^-1·K^-1).</summary>
    public static readonly QuantityKind ThermalConductivity = new(
        "ThermalConductivity",
        (Energy.CanonicalUnit / Unit.SI.s) / (Unit.SI.m * Unit.SI.K),
        "k_th",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainThermodynamic]);

    /// <summary>Thermal diffusivity (m²/s).</summary>
    public static readonly QuantityKind ThermalDiffusivity = new(
        "ThermalDiffusivity",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "α",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainThermodynamic]);

    /// <summary>Heat flux (W/m²).</summary>
    public static readonly QuantityKind HeatFlux = new(
        "HeatFlux",
        (Energy.CanonicalUnit / Unit.SI.s) / (Unit.SI.m ^ 2),
        "q_dot",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.EnergyPathFunction]);

    /// <summary>Thermal resistance (K/W).</summary>
    public static readonly QuantityKind ThermalResistance = new(
        "ThermalResistance",
        Unit.SI.K * Unit.SI.s / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
        "R_th",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Thermal conductance (W/K).</summary>
    public static readonly QuantityKind ThermalConductance = new(
        "ThermalConductance",
        (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.K * Unit.SI.s),
        "G_th",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Coefficient of thermal expansion (1/K).</summary>
    public static readonly QuantityKind CoefficientOfThermalExpansion = new(
        "CoefficientOfThermalExpansion",
        1 / Unit.SI.K,
        "α_th",
        tags: [QuantityKindTags.DomainThermodynamic]);

    /// <summary>Emissivity (dimensionless).</summary>
    public static readonly QuantityKind Emissivity = new(
        "Emissivity",
        Unit.None,
        "ε_emis",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless]);

    /// <summary>Absorptivity (dimensionless).</summary>
    public static readonly QuantityKind Absorptivity = new(
        "Absorptivity",
        Unit.None,
        "α_abs",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless]);

    /// <summary>Reflectivity (dimensionless).</summary>
    public static readonly QuantityKind Reflectivity = new(
        "Reflectivity",
        Unit.None,
        "ρ_refl",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless]);

    /// <summary>Transmissivity (dimensionless).</summary>
    public static readonly QuantityKind Transmissivity = new(
        "Transmissivity",
        Unit.None,
        "τ_trans",
        tags: [QuantityKindTags.DomainThermodynamic, QuantityKindTags.FormDimensionless]);

    /// <summary>Isentropic exponent (dimensionless).</summary>
    public static readonly QuantityKind IsentropicExponent = new(
        "IsentropicExponent",
        Unit.None,
        "γ_iso",
        tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainThermodynamic]);
}