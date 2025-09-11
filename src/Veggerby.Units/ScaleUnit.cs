using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null) : Unit
{
    private readonly Unit _base = @base;
    private readonly UnitSystem _system = system;

    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;
    public override Dimension Dimension => _base.Dimension;
    public double Scale { get; } = scale;

    public override UnitSystem System => _system ?? _base.System;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => Scale * _base.GetScaleFactor();
}