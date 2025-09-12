using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null) : Unit
{
    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;
    public override Dimension Dimension => @base.Dimension;
    public double Scale { get; } = scale;

    public override UnitSystem System => system ?? @base.System;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => Scale * @base.GetScaleFactor();
}