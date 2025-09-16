using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

public class ProductUnit(Unit[] operands) : Unit, IProductOperation
{
    private readonly IList<Unit> _operands = new ReadOnlyCollection<Unit>(OperationUtility.LinearizeMultiplication(operands).ToList());

    public override string Symbol => string.Join(string.Empty, _operands.Select(x => x.Symbol));
    public override string Name => string.Join(" * ", _operands.Select(x => x.Name));
    public override UnitSystem System => _operands.Any() ? _operands.First().System : UnitSystem.None;
    public override Dimension Dimension => _operands.Select(x => x.Dimension).Multiply((x, y) => x * y, Dimension.None);

    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IProductOperation);

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

    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);

    IEnumerable<IOperand> IProductOperation.Operands => _operands;

    internal override double GetScaleFactor() => _operands.Select(x => x.GetScaleFactor()).Aggregate(1d, (a, b) => a * b);
}