using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions
{
    public class ProductDimension : Dimension, IProductOperation
    {
        private readonly IList<Dimension> _Operands;

        internal ProductDimension(params Dimension[] operands)
        {
            _Operands = new ReadOnlyCollection<Dimension>(OperationUtility.LinearizeMultiplication(operands).ToList());
        }

        public override string Symbol
        {
            get { return string.Join(string.Empty, this._Operands.Select(x => x.Symbol)); }
        }

        public override string Name
        {
            get { return string.Join(" * ", this._Operands.Select(x => x.Name)); }
        }

        public override bool Equals(object obj)
        {
            return OperationUtility.Equals(this, obj as IProductOperation);
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }

        IEnumerable<IOperand> IProductOperation.Operands
        {
            get { return this._Operands; }
        }
    }
}