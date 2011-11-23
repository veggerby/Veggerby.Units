namespace Veggerby.Units.Dimensions
{
    public class PowerDimension : Dimension
    {
        private readonly Dimension _Base;
        private readonly int _Exponent;

        internal PowerDimension(Dimension @base, int exponent)
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
    }
}