using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class DerivedUnit(string symbol, string name, Unit expression) : Unit
{
    private readonly Unit _expression = expression;

    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;

    public override UnitSystem System => _expression.System;
    public override Dimension Dimension => _expression.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => _expression.GetScaleFactor();
}