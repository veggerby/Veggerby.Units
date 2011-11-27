using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units
{
    public class DivisionUnit : Unit, IDivisionOperation
    {
        private readonly Unit _Dividend;
        private readonly Unit _Divisor;

        internal DivisionUnit(Unit dividend, Unit divisor)
        {
            this._Dividend = dividend;
            this._Divisor = divisor;
        }

        public override string Symbol
        {
            get { return string.Format("{0}/{1}", this._Dividend.Symbol == string.Empty ? "1" : this._Dividend.Symbol, this._Divisor.Symbol); }
        }

        public override string Name
        {
            get { return string.Format("{0} / {1}", this._Dividend.Symbol == string.Empty ? "1" : this._Dividend.Name, this._Divisor.Name); }
        }

        public override Dimension Dimension
        {
            get { return this._Dividend.Dimension / this._Divisor.Dimension; }
        }

        public override bool Equals(object obj)
        {
            return OperationUtility.Equals(this, obj as IDivisionOperation);
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }

        IOperand IDivisionOperation.Dividend
        {
            get { return this._Dividend; }
        }

        IOperand IDivisionOperation.Divisor
        {
            get { return this._Divisor; }
        }
    }
}