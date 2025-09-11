using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class NullUnit : Unit
{
    public override string Symbol => string.Empty;
    public override string Name => string.Empty;
    public override UnitSystem System => UnitSystem.None;
    public override Dimension Dimension => Dimension.None;

    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
}