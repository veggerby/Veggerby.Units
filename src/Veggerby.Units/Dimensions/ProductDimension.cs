using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

/// <summary>Composite dimension representing a commutative product of operand dimensions.</summary>
public class ProductDimension : Dimension, IProductOperation, ICanonicalFactorsProvider
{
    private readonly IList<Dimension> _operands;

    internal ProductDimension(params Dimension[] operands)
    {
        _operands = new ReadOnlyCollection<Dimension>(OperationUtility.LinearizeMultiplication(operands).ToList());
    }

    /// <inheritdoc />
    public override string Symbol => string.Join(string.Empty, _operands.Select(x => x.Symbol));
    /// <inheritdoc />
    public override string Name => string.Join(" * ", _operands.Select(x => x.Name));

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var h in _operands.Select(o => o.GetHashCode()).OrderBy(x => x))
            {
                hash = hash * 31 + h;
            }
            return hash ^ 0x5555AAAA;
        }
    }
    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IProductOperation);

    IEnumerable<IOperand> IProductOperation.Operands => _operands;
    private FactorVector<IOperand>? _cachedFactors;
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
            .ThenBy(t => (t.Base as Dimension)?.Symbol ?? string.Empty)
            .Select(t => (t.Base, t.Exponent))
            .ToArray();
        _cachedFactors = new FactorVector<IOperand>(arr);
        return _cachedFactors;
    }
}