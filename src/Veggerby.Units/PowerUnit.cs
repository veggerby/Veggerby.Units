using System;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

public class PowerUnit : Unit, IPowerOperation
{
    private readonly Unit _base;
    private readonly int _exponent;

    internal PowerUnit(Unit @base, int exponent)
    {
        _base = @base;
        _exponent = exponent;
    }

    public override string Symbol => $"{_base.Symbol}^{_exponent}";
    public override string Name => $"{_base.Name} ^ {_exponent}";
    public override UnitSystem System => _base.System;
    public override Dimension Dimension => _base.Dimension ^ _exponent;
    IOperand IPowerOperation.Base => _base;
    int IPowerOperation.Exponent => _exponent;

    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => Math.Pow(_base.GetScaleFactor(), _exponent);
}