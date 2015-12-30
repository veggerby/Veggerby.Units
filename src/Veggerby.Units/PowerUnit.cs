using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units
{
    public class PowerUnit : Unit, IPowerOperation
    {
        private readonly Unit _base;
        private readonly int _exponent;

        internal PowerUnit(Unit @base, int exponent)
        {
            _base = @base;
            _exponent = exponent;
        }

        public override string Symbol
        {
            get { return string.Format("{0}^{1}", _base.Symbol, _exponent); }
        }

        public override string Name
        {
            get { return string.Format("{0} ^ {1}", _base.Name, _exponent); }
        }

        public override UnitSystem System
        {
            get { return _base.System; }
        }

        public override Dimension Dimension
        {
            get { return _base.Dimension ^ _exponent; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        IOperand IPowerOperation.Base
        {
            get { return _base; }
        }

        int IPowerOperation.Exponent
        {
            get { return _exponent; }
        }
    }
}