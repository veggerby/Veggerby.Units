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

        public override string Symbol
        {
            get { return string.Format("{0}/{1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Symbol, _divisor.Symbol); }
        }

        public override string Name
        {
            get { return string.Format("{0} / {1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Name, _divisor.Name); }
        }

        public override bool Equals(object obj)
        {
            return OperationUtility.Equals(this, obj as IDivisionOperation);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        IOperand IDivisionOperation.Dividend
        {
            get { return _dividend; }
        }

        IOperand IDivisionOperation.Divisor
        {
            get { return _divisor; }
        }
    }
}