using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Identity (dimensionless) unit used as neutral element for multiplication/division and result of zero exponentiation.
/// </summary>
public class NullUnit : Unit
{
    /// <inheritdoc />
    public override string Symbol => string.Empty;
    /// <inheritdoc />
    public override string Name => string.Empty;
    /// <inheritdoc />
    public override UnitSystem System => UnitSystem.None;
    /// <inheritdoc />
    public override Dimension Dimension => Dimension.None;

    /// <inheritdoc />
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
}