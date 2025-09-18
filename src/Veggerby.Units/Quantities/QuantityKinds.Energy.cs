namespace Veggerby.Units.Quantities;

/// <summary>
/// Registry of built-in quantity kinds. All are additive to the core dimensional model; they impose no changes
/// to reduction or unit equality.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Energy (J) = kg·m²/s².</summary>
    public static readonly QuantityKind Energy = new(
        "Energy",
        Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2),
        "E",
        tags: new[]
        {
                QuantityKindTags.Energy,
                QuantityKindTags.EnergyStateFunction
        });

    /// <summary>Internal energy (U) shares energy dimensions.</summary>
    public static readonly QuantityKind InternalEnergy = new(
        "InternalEnergy",
        Energy.CanonicalUnit,
        "U",
        tags: new[]
        {
                QuantityKindTags.Energy,
                QuantityKindTags.EnergyStateFunction,
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Enthalpy (H) shares energy dimensions.</summary>
    public static readonly QuantityKind Enthalpy = new(
        "Enthalpy",
        Energy.CanonicalUnit,
        "H",
        tags: new[]
        {
                QuantityKindTags.Energy,
                QuantityKindTags.EnergyStateFunction,
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Gibbs free energy (G) shares energy dimensions.</summary>
    public static readonly QuantityKind GibbsFreeEnergy = new(
        "GibbsFreeEnergy",
        Energy.CanonicalUnit,
        "G",
        tags: new[]
        {
                QuantityKindTags.Energy,
                QuantityKindTags.EnergyStateFunction,
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Helmholtz free energy (A) shares energy dimensions.</summary>
    public static readonly QuantityKind HelmholtzFreeEnergy = new(
        "HelmholtzFreeEnergy",
        Energy.CanonicalUnit,
        "A",
        tags: new[]
        {
                QuantityKindTags.Energy,
                QuantityKindTags.EnergyStateFunction,
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Entropy (S) J/K.</summary>
    public static readonly QuantityKind Entropy = new(
        "Entropy",
        Energy.CanonicalUnit / Unit.SI.K,
        "S",
        tags: new[]
        {
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Torque (τ) shares energy dimension but denotes rotational effect.</summary>
    public static readonly QuantityKind Torque = new(
        "Torque",
        Energy.CanonicalUnit,
        "τ",
        tags: new[]
        {
                QuantityKindTags.DomainMechanics
        });

    /// <summary>Angle (θ) is dimensionless but semantically distinct; prohibits implicit dimensionless scalar fallback preserving kind when multiplied (treated as vector-like).</summary>
    public static readonly QuantityKind Angle = new(
        "Angle",
        Unit.None,
        "θ",
        tags: new[]
        {
                QuantityKindTags.FormDimensionless,
                QuantityKindTags.DomainMechanics
        });

    /// <summary>Heat capacity (C_p) J/K (semantic distinct from Entropy).</summary>
    public static readonly QuantityKind HeatCapacity = new(
        "HeatCapacity",
        Energy.CanonicalUnit / Unit.SI.K,
        "C_p",
        tags: new[]
        {
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Chemical potential (μ) J/mol.</summary>
    public static readonly QuantityKind ChemicalPotential = new(
        "ChemicalPotential",
        Energy.CanonicalUnit / Unit.SI.n,
        "μ",
        tags: new[]
        {
                QuantityKindTags.DomainThermodynamic
        });

    /// <summary>Work (W) path energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Work = new(
        "Work",
        Energy.CanonicalUnit,
        "Wk",
        tags: new[]
        {
                QuantityKindTags.EnergyPathFunction,
                QuantityKindTags.DomainMechanics,
                QuantityKindTags.Energy
        });

    /// <summary>Heat (Q) path thermal energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Heat = new(
        "Heat",
        Energy.CanonicalUnit,
        "Q",
        tags: new[]
        {
                QuantityKindTags.EnergyPathFunction,
                QuantityKindTags.DomainThermodynamic,
                QuantityKindTags.Energy
        });
}