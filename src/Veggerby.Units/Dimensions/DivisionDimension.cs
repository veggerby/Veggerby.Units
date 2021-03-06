using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions
{
    public class DivisionDimension : Dimension, IDivisionOperation
    {
        private readonly Dimension _dividend;
        private readonly Dimension _divisor;

        internal DivisionDimension(Dimension dividend, Dimension divisor)
        {
            _dividend = dividend;
            _divisor = divisor;
        }

        public override string Symbol => string.Format("{0}/{1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Symbol, _divisor.Symbol);
        public override string Name => string.Format("{0} / {1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Name, _divisor.Name);

        public override int GetHashCode() => Symbol.GetHashCode();
        public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IDivisionOperation);

        IOperand IDivisionOperation.Dividend => _dividend;
        IOperand IDivisionOperation.Divisor => _divisor;
    }
}