namespace Veggerby.Units.Quantities;

/// <summary>
/// Describes a semantic quantity kind (e.g. Energy, Torque, Entropy) that may share dimensions with other kinds
/// but represents a distinct physical concept. Provides an optional dimensional guard.
/// </summary>
/// <param name="Name">Human readable name.</param>
/// <param name="CanonicalUnit">Representative unit for dimensional validation.</param>
/// <param name="Symbol">Optional symbolic abbreviation.</param>
public sealed record QuantityKind(string Name, Unit CanonicalUnit, string Symbol)
{
    /// <summary>Returns true when the supplied unit has the same dimension as the canonical unit.</summary>
    public bool Matches(Unit unit) => unit.Dimension == CanonicalUnit.Dimension;

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Symbol) ? Name : $"{Name} ({Symbol})";
}
