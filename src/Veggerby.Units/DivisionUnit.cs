using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

public class DivisionUnit : Unit, IDivisionOperation
{
    private readonly Unit _dividend;
    private readonly Unit _divisor;

    internal DivisionUnit(Unit dividend, Unit divisor)
    {
        _dividend = dividend;
        _divisor = divisor;
    }

    public override string Symbol => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Symbol)}/{_divisor.Symbol}";
    public override string Name => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Name)} / {_divisor.Name}";
    public override UnitSystem System => _dividend != Unit.None ? _dividend.System : _divisor.System;
    public override Dimension Dimension => _dividend.Dimension / _divisor.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
    IOperand IDivisionOperation.Dividend => _dividend;
    IOperand IDivisionOperation.Divisor => _divisor;

    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IDivisionOperation);
    public override int GetHashCode() => Symbol.GetHashCode();

    internal override double GetScaleFactor() => _dividend.GetScaleFactor() / _divisor.GetScaleFactor();
}