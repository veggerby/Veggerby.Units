using Veggerby.Units.Reduction;
namespace Veggerby.Units.Dimensions
{
    public class PowerDimension : Dimension, IPowerOperation
    {
        private readonly Dimension _base;
        private readonly int _rxponent;

        internal PowerDimension(Dimension @base, int exponent)
        {
            _base = @base;
            _rxponent = exponent;
        }

        public override string Symbol
        {
            get { return string.Format("{0}^{1}", _base.Symbol, _rxponent); }
        }

        public override string Name
        {
            get { return string.Format("{0} ^ {1}", _base.Name, _rxponent); }
        }

        IOperand IPowerOperation.Base
        {
            get { return _base; }
        }

        int IPowerOperation.Exponent
        {
            get { return _rxponent; }
        }
    }
}