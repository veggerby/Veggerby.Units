namespace Veggerby.Units.Quantities;

/// <summary>
/// Describes a semantic quantity kind (e.g. Energy, Torque, Entropy) that may share dimensions with other kinds
/// but represents a distinct physical concept. Provides an optional dimensional guard.
/// </summary>
/// <param name="Name">Human readable name.</param>
/// <param name="CanonicalUnit">Representative unit for dimensional validation.</param>
/// <param name="Symbol">Optional symbolic abbreviation.</param>
/// <param name="allowDirectAddition">If false, the + operator for this kind will throw to prevent semantic misuse.</param>
/// <param name="allowDirectSubtraction">If false, the - operator (same-kind result) will throw; domain-specific delta op expected.</param>
public sealed record QuantityKind(string Name, Unit CanonicalUnit, string Symbol, bool allowDirectAddition = true, bool allowDirectSubtraction = true)
{
    /// <summary>Returns true when the supplied unit has the same dimension as the canonical unit.</summary>
    public bool Matches(Unit unit) => unit.Dimension == CanonicalUnit.Dimension;

    /// <inheritdoc />
    /// <summary>
    /// Indicates whether the <c>+</c> operator between two quantities of this kind should be permitted directly.
    /// For affine absolutes (e.g. absolute temperature) this is typically <c>false</c>.
    /// </summary>
    /// <summary>True if '+' is permitted directly for this kind.</summary>
    public bool AllowDirectAddition { get; } = allowDirectAddition;

    /// <summary>
    /// Indicates whether the <c>-</c> operator producing a same-kind quantity is permitted directly. For affine
    /// absolutes where subtraction should yield a delta kind, this is typically <c>false</c>.
    /// </summary>
    /// <summary>True if '-' (same-kind result) is permitted directly for this kind.</summary>
    public bool AllowDirectSubtraction { get; } = allowDirectSubtraction;

    /// <summary>Returns a readable representation including symbol when present.</summary>
    public override string ToString() => string.IsNullOrEmpty(Symbol) ? Name : $"{Name} ({Symbol})";
}