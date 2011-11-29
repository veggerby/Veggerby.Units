using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units
{
    public class PowerUnit : Unit, IPowerOperation
    {
        private readonly Unit _Base;
        private readonly int _Exponent;

        internal PowerUnit(Unit @base, int exponent)
        {
            this._Base = @base;
            this._Exponent = exponent;
        }

        public override string Symbol
        {
            get { return string.Format("{0}^{1}", this._Base.Symbol, this._Exponent); }
        }

        public override string Name
        {
            get { return string.Format("{0} ^ {1}", this._Base.Name, this._Exponent); }
        }

        public override UnitSystem System
        {
            get { return this._Base.System; }
        }

        public override Dimension Dimension
        {
            get { return this._Base.Dimension ^ this._Exponent; }
        }

        IOperand IPowerOperation.Base
        {
            get { return this._Base; }
        }

        int IPowerOperation.Exponent
        {
            get { return this._Exponent; }
        }
    }
}