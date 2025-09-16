using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

/// <summary>
/// Composite unit representing a division between a dividend and a divisor unit.
/// Reduction logic cancels shared factors during construction via operator helpers.
/// </summary>
public class DivisionUnit : Unit, IDivisionOperation
{
    private readonly Unit _dividend;
    private readonly Unit _divisor;

    internal DivisionUnit(Unit dividend, Unit divisor)
    {
        _dividend = dividend;
        _divisor = divisor;
    }

    /// <inheritdoc />
    public override string Symbol => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Symbol)}/{_divisor.Symbol}";
    /// <inheritdoc />
    public override string Name => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Name)} / {_divisor.Name}";
    /// <inheritdoc />
    public override UnitSystem System => _dividend != Unit.None ? _dividend.System : _divisor.System;
    /// <inheritdoc />
    public override Dimension Dimension => _dividend.Dimension / _divisor.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
    IOperand IDivisionOperation.Dividend => _dividend;
    IOperand IDivisionOperation.Divisor => _divisor;

    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IDivisionOperation);
    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            // Order matters for division; incorporate a prime and tag
            int hash = 23;
            hash = hash * 31 + _dividend.GetHashCode();
            hash = hash * 31 + (_divisor.GetHashCode() ^ unchecked((int)0xAAAAAAAA));
            return hash ^ 0x33333333;
        }
    }

    internal override double GetScaleFactor() => _dividend.GetScaleFactor() / _divisor.GetScaleFactor();
}