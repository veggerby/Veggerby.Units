namespace Veggerby.Units.Quantities;

/// <summary>
/// Registry of built-in quantity kinds. All are additive to the core dimensional model; they impose no changes
/// to reduction or unit equality.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Energy (J) = kg·m²/s².</summary>
    public static readonly QuantityKind Energy = new("Energy", Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), "E");

    /// <summary>Internal energy (U) shares energy dimensions.</summary>
    public static readonly QuantityKind InternalEnergy = new("InternalEnergy", Energy.CanonicalUnit, "U");

    /// <summary>Enthalpy (H) shares energy dimensions.</summary>
    public static readonly QuantityKind Enthalpy = new("Enthalpy", Energy.CanonicalUnit, "H");

    /// <summary>Gibbs free energy (G) shares energy dimensions.</summary>
    public static readonly QuantityKind GibbsFreeEnergy = new("GibbsFreeEnergy", Energy.CanonicalUnit, "G");

    /// <summary>Helmholtz free energy (A) shares energy dimensions.</summary>
    public static readonly QuantityKind HelmholtzFreeEnergy = new("HelmholtzFreeEnergy", Energy.CanonicalUnit, "A");

    /// <summary>Entropy (S) J/K.</summary>
    public static readonly QuantityKind Entropy = new("Entropy", Energy.CanonicalUnit / Unit.SI.K, "S");

    /// <summary>Torque (τ) shares energy dimension but denotes rotational effect.</summary>
    public static readonly QuantityKind Torque = new("Torque", Energy.CanonicalUnit, "τ");

    /// <summary>Angle (θ) is dimensionless but semantically distinct; prohibits implicit dimensionless scalar fallback preserving kind when multiplied (treated as vector-like).</summary>
    public static readonly QuantityKind Angle = new("Angle", Unit.None, "θ");

    /// <summary>Heat capacity (C_p) J/K (semantic distinct from Entropy).</summary>
    public static readonly QuantityKind HeatCapacity = new("HeatCapacity", Energy.CanonicalUnit / Unit.SI.K, "C_p");

    /// <summary>Chemical potential (μ) J/mol.</summary>
    public static readonly QuantityKind ChemicalPotential = new("ChemicalPotential", Energy.CanonicalUnit / Unit.SI.n, "μ");
}