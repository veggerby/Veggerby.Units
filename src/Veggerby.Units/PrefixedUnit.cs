using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Represents a unit with a metric prefix applied (e.g. kilo + metre = kilometre).
/// Scale factor is prefix factor times the underlying base unit scale.
/// </summary>
public class PrefixedUnit : Unit
{
    internal PrefixedUnit(Prefix prefix, Unit baseUnit)
    {
        Prefix = prefix;
        BaseUnit = baseUnit;
    }

    /// <summary>The applied prefix.</summary>
    public Prefix Prefix { get; }
    /// <summary>The underlying unit.</summary>
    public Unit BaseUnit { get; }

    /// <inheritdoc />
    public override string Name => string.Format("{0}{1}", Prefix.Name, BaseUnit.Name);
    /// <inheritdoc />
    public override string Symbol => string.Format("{0}{1}", Prefix.Symbol, BaseUnit.Symbol);
    /// <inheritdoc />
    public override Dimension Dimension => BaseUnit.Dimension;
    /// <inheritdoc />
    public override UnitSystem System => BaseUnit.System;

    internal override double GetScaleFactor() => Prefix.Factor * BaseUnit.GetScaleFactor();
}