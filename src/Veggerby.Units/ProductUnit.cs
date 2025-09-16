using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

/// <summary>
/// Composite unit representing the commutative product of one or more operand units.
/// Internally flattens nested product structures to maintain a linear operand list for reduction and equality.
/// </summary>
/// <param name="operands">Units to multiply (Unit.None is ignored).</param>
public class ProductUnit(Unit[] operands) : Unit, IProductOperation, ICanonicalFactorsProvider
{
    private readonly IList<Unit> _operands = new ReadOnlyCollection<Unit>(OperationUtility.LinearizeMultiplication(operands).ToList());
    private FactorVector<IOperand>? _cachedFactors;

    /// <inheritdoc />
    public override string Symbol => string.Join(string.Empty, _operands.Select(x => x.Symbol));
    /// <inheritdoc />
    public override string Name => string.Join(" * ", _operands.Select(x => x.Name));
    /// <inheritdoc />
    public override UnitSystem System => _operands.Any() ? _operands.First().System : UnitSystem.None;
    /// <inheritdoc />
    public override Dimension Dimension => _operands.Select(x => x.Dimension).Multiply((x, y) => x * y, Dimension.None);

    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IProductOperation);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // Order-insensitive hash: sort operand hash codes then combine
        unchecked
        {
            int hash = 17;
            foreach (var h in _operands.Select(o => o.GetHashCode()).OrderBy(x => x))
            {
                hash = hash * 31 + h;
            }
            // Distinguish product from other composite forms minimally
            return hash ^ 0x55555555;
        }
    }

    /// <inheritdoc />
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    IEnumerable<IOperand> IProductOperation.Operands => _operands;
    FactorVector<IOperand>? ICanonicalFactorsProvider.GetCanonicalFactors()
    {
        if (!ReductionSettings.UseFactorVector)
        {
            return null;
        }
        if (_cachedFactors.HasValue)
        {
            return _cachedFactors;
        }
        var arr = _operands
            .GroupBy(o => o, (k, g) => (Base: (IOperand)k, Exponent: g.Count()))
            .OrderBy(t => t.Base.GetType().FullName)
            .ThenBy(t => (t.Base as Unit)?.Symbol ?? string.Empty)
            .Select(t => (t.Base, t.Exponent))
            .ToArray();
        _cachedFactors = new FactorVector<IOperand>(arr);
        return _cachedFactors;
    }

    internal override double GetScaleFactor() => _operands.Select(x => x.GetScaleFactor()).Aggregate(1d, (a, b) => a * b);
}