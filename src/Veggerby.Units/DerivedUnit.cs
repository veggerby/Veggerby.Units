using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class DerivedUnit(string symbol, string name, Unit expression) : Unit
{
    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;

    public override UnitSystem System => expression.System;
    public override Dimension Dimension => expression.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => expression.GetScaleFactor();
}