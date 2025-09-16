using System;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

/// <summary>
/// Composite unit representing an integer exponent applied to a base unit.
/// Negative exponents are represented as division outside this type (handled by operator ^).
/// </summary>
public class PowerUnit : Unit, IPowerOperation
{
    private readonly Unit _base;
    private readonly int _exponent;

    internal PowerUnit(Unit @base, int exponent)
    {
        _base = @base;
        _exponent = exponent;
    }

    /// <inheritdoc />
    public override string Symbol => $"{_base.Symbol}^{_exponent}";
    /// <inheritdoc />
    public override string Name => $"{_base.Name} ^ {_exponent}";
    /// <inheritdoc />
    public override UnitSystem System => _base.System;
    /// <inheritdoc />
    public override Dimension Dimension => _base.Dimension ^ _exponent;
    IOperand IPowerOperation.Base => _base;
    int IPowerOperation.Exponent => _exponent;

    /// <inheritdoc />
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    internal override double GetScaleFactor() => Math.Pow(_base.GetScaleFactor(), _exponent);

    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IPowerOperation);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 29;
            hash = hash * 37 + _base.GetHashCode();
            hash = hash * 37 + _exponent.GetHashCode();
            return hash ^ 0x77777777;
        }
    }
}