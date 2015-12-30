using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units
{
    public class DivisionUnit : Unit, IDivisionOperation
    {
        private readonly Unit _dividend;
        private readonly Unit _divisor;

        internal DivisionUnit(Unit dividend, Unit divisor)
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

        public override UnitSystem System
        {
            get { return _dividend != Unit.None ? _dividend.System : _divisor.System; }
        }

        public override Dimension Dimension
        {
            get { return _dividend.Dimension / _divisor.Dimension; }
        }

        public override bool Equals(object obj)
        {
            return OperationUtility.Equals(this, obj as IDivisionOperation);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
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