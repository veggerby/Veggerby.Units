using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Represents a named unit derived from an expression of other units. Acts as an identity wrapper that preserves the
/// composite's dimension and scale factor while providing a stable symbol/name for display and equality.
/// </summary>
public class DerivedUnit(string symbol, string name, Unit expression) : Unit
{
    /// <inheritdoc />
    public override string Symbol { get; } = symbol;
    /// <inheritdoc />
    public override string Name { get; } = name;

    /// <inheritdoc />
    public override UnitSystem System => expression.System;
    /// <inheritdoc />
    public override Dimension Dimension => expression.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => expression.GetScaleFactor();
}