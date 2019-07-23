using Veggerby.Units.Reduction;
namespace Veggerby.Units.Dimensions
{
    public class PowerDimension : Dimension, IPowerOperation
    {
        private readonly Dimension _base;
        private readonly int _exponent;

        internal PowerDimension(Dimension @base, int exponent)
        {
            _base = @base;
            _exponent = exponent;
        }

        public override string Symbol => $"{_base.Symbol}^{_exponent}";
        public override string Name => $"{_base.Name} ^ {_exponent}";

        IOperand IPowerOperation.Base => _base;
        int IPowerOperation.Exponent => _exponent;
    }
}