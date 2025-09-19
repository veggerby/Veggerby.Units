namespace Veggerby.Units.Quantities;

/// <summary>
/// Classical dimensionless flow/transport groups and ratios (Re, Pr, Nu, Ma, Fr, We, Pe, etc.).
/// All must include <see cref="QuantityKindTags.FormDimensionless"/>.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Reynolds number (dimensionless).</summary>
    public static readonly QuantityKind Reynolds = new("Reynolds", Unit.None, "Re", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Prandtl number (dimensionless).</summary>
    public static readonly QuantityKind Prandtl = new("Prandtl", Unit.None, "Pr", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Nusselt number (dimensionless).</summary>
    public static readonly QuantityKind Nusselt = new("Nusselt", Unit.None, "Nu", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Schmidt number (dimensionless).</summary>
    public static readonly QuantityKind Schmidt = new("Schmidt", Unit.None, "Sc", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Sherwood number (dimensionless).</summary>
    public static readonly QuantityKind Sherwood = new("Sherwood", Unit.None, "Sh", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Biot number (dimensionless).</summary>
    public static readonly QuantityKind Biot = new("Biot", Unit.None, "Bi", tags: [QuantityKindTags.DomainTransport, QuantityKindTags.FormDimensionless]);
    /// <summary>Mach number (dimensionless).</summary>
    public static readonly QuantityKind MachNumber = new("MachNumber", Unit.None, "Ma", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Froude number (dimensionless).</summary>
    public static readonly QuantityKind FroudeNumber = new("FroudeNumber", Unit.None, "Fr", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Weber number (dimensionless).</summary>
    public static readonly QuantityKind WeberNumber = new("WeberNumber", Unit.None, "We", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Peclet number (dimensionless).</summary>
    public static readonly QuantityKind PecletNumber = new("PecletNumber", Unit.None, "Pe", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Stanton number (dimensionless).</summary>
    public static readonly QuantityKind StantonNumber = new("StantonNumber", Unit.None, "St", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Strouhal number (dimensionless).</summary>
    public static readonly QuantityKind StrouhalNumber = new("StrouhalNumber", Unit.None, "St_r", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Euler number (dimensionless).</summary>
    public static readonly QuantityKind EulerNumber = new("EulerNumber", Unit.None, "Eu", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Knudsen number (dimensionless).</summary>
    public static readonly QuantityKind KnudsenNumber = new("KnudsenNumber", Unit.None, "Kn", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Damkohler number (dimensionless).</summary>
    public static readonly QuantityKind DamkohlerNumber = new("DamkohlerNumber", Unit.None, "Da", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>Richardson number (dimensionless).</summary>
    public static readonly QuantityKind RichardsonNumber = new("RichardsonNumber", Unit.None, "Ri", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainTransport]);
    /// <summary>General dimensionless ratio placeholder.</summary>
    public static readonly QuantityKind DimensionlessRatio = new("DimensionlessRatio", Unit.None, "R_dim", tags: [QuantityKindTags.FormDimensionless]);
}