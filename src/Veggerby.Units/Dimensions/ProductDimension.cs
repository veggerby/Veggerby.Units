using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

public class ProductDimension : Dimension, IProductOperation
{
    private readonly IList<Dimension> _operands;

    internal ProductDimension(params Dimension[] operands)
    {
        _operands = new ReadOnlyCollection<Dimension>(OperationUtility.LinearizeMultiplication(operands).ToList());
    }

    public override string Symbol => string.Join(string.Empty, _operands.Select(x => x.Symbol));
    public override string Name => string.Join(" * ", _operands.Select(x => x.Name));

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
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IProductOperation);

    IEnumerable<IOperand> IProductOperation.Operands => _operands;
}