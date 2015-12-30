using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units
{
    public class ProductUnit : Unit, IProductOperation
    {
        private readonly IList<Unit> _Operands;

        public ProductUnit(Unit[] operands)
        {
            _Operands = new ReadOnlyCollection<Unit>(OperationUtility.LinearizeMultiplication(operands).ToList());
        }

        public override string Symbol
        {
            get { return string.Join(string.Empty, this._Operands.Select(x => x.Symbol)); }
        }

        public override string Name
        {
            get { return string.Join(" * ", this._Operands.Select(x => x.Name)); }
        }

        public override UnitSystem System
        {
            get { return this._Operands.Any() ? this._Operands.First().System : UnitSystem.None; }
        }

        public override Dimension Dimension
        {
            get { return this._Operands.Select(x => x.Dimension).Multiply((x, y) => x * y, Dimension.None); }
        }

        public override bool Equals(object obj)
        {
            return OperationUtility.Equals(this, obj as IProductOperation);
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        IEnumerable<IOperand> IProductOperation.Operands
        {
            get { return this._Operands; }
        }
    }
}