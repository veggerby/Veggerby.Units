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
                QuantityKindTag.Get("Energy"),
                QuantityKindTag.Get("Energy.StateFunction")
        });

    /// <summary>Internal energy (U) shares energy dimensions.</summary>
    public static readonly QuantityKind InternalEnergy = new(
        "InternalEnergy",
        Energy.CanonicalUnit,
        "U",
        tags: new[]
        {
                QuantityKindTag.Get("Energy"),
                QuantityKindTag.Get("Energy.StateFunction"),
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Enthalpy (H) shares energy dimensions.</summary>
    public static readonly QuantityKind Enthalpy = new(
        "Enthalpy",
        Energy.CanonicalUnit,
        "H",
        tags: new[]
        {
                QuantityKindTag.Get("Energy"),
                QuantityKindTag.Get("Energy.StateFunction"),
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Gibbs free energy (G) shares energy dimensions.</summary>
    public static readonly QuantityKind GibbsFreeEnergy = new(
        "GibbsFreeEnergy",
        Energy.CanonicalUnit,
        "G",
        tags: new[]
        {
                QuantityKindTag.Get("Energy"),
                QuantityKindTag.Get("Energy.StateFunction"),
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Helmholtz free energy (A) shares energy dimensions.</summary>
    public static readonly QuantityKind HelmholtzFreeEnergy = new(
        "HelmholtzFreeEnergy",
        Energy.CanonicalUnit,
        "A",
        tags: new[]
        {
                QuantityKindTag.Get("Energy"),
                QuantityKindTag.Get("Energy.StateFunction"),
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Entropy (S) J/K.</summary>
    public static readonly QuantityKind Entropy = new(
        "Entropy",
        Energy.CanonicalUnit / Unit.SI.K,
        "S",
        tags: new[]
        {
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Torque (τ) shares energy dimension but denotes rotational effect.</summary>
    public static readonly QuantityKind Torque = new(
        "Torque",
        Energy.CanonicalUnit,
        "τ",
        tags: new[]
        {
                QuantityKindTag.Get("Domain.Mechanical")
        });

    /// <summary>Angle (θ) is dimensionless but semantically distinct; prohibits implicit dimensionless scalar fallback preserving kind when multiplied (treated as vector-like).</summary>
    public static readonly QuantityKind Angle = new(
        "Angle",
        Unit.None,
        "θ",
        tags: new[]
        {
                QuantityKindTag.Get("Form.Dimensionless"),
                QuantityKindTag.Get("Domain.Mechanical")
        });

    /// <summary>Heat capacity (C_p) J/K (semantic distinct from Entropy).</summary>
    public static readonly QuantityKind HeatCapacity = new(
        "HeatCapacity",
        Energy.CanonicalUnit / Unit.SI.K,
        "C_p",
        tags: new[]
        {
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Chemical potential (μ) J/mol.</summary>
    public static readonly QuantityKind ChemicalPotential = new(
        "ChemicalPotential",
        Energy.CanonicalUnit / Unit.SI.n,
        "μ",
        tags: new[]
        {
                QuantityKindTag.Get("Domain.Thermodynamic")
        });

    /// <summary>Work (W) path energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Work = new(
        "Work",
        Energy.CanonicalUnit,
        "Wk",
        tags: new[]
        {
                QuantityKindTag.Get("Energy.PathFunction"),
                QuantityKindTag.Get("Domain.Mechanical"),
                QuantityKindTag.Get("Energy")
        });

    /// <summary>Heat (Q) path thermal energy transfer; dimensionally energy.</summary>
    public static readonly QuantityKind Heat = new(
        "Heat",
        Energy.CanonicalUnit,
        "Q",
        tags: new[]
        {
                QuantityKindTag.Get("Energy.PathFunction"),
                QuantityKindTag.Get("Domain.Thermodynamic"),
                QuantityKindTag.Get("Energy")
        });
}