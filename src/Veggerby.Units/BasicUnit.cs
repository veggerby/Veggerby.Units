using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class BasicUnit(string symbol, string name, UnitSystem system, Dimension dimension) : Unit
{
    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;
    public override UnitSystem System { get; } = system;
    public override Dimension Dimension { get; } = dimension;

    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
}