using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

public class PrefixedUnit : Unit
{
    internal PrefixedUnit(Prefix prefix, Unit baseUnit)
    {
        Prefix = prefix;
        BaseUnit = baseUnit;
    }

    public Prefix Prefix { get; }
    public Unit BaseUnit { get; }

    public override string Name => string.Format("{0}{1}", Prefix.Name, BaseUnit.Name);
    public override string Symbol => string.Format("{0}{1}", Prefix.Symbol, BaseUnit.Symbol);
    public override Dimension Dimension => BaseUnit.Dimension;
    public override UnitSystem System => BaseUnit.System;

    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => Prefix.Factor * BaseUnit.GetScaleFactor();
}