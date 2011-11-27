using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class BasicUnit : Unit
    {
        private readonly string _Symbol;
        private readonly string _Name;
        private readonly Dimension _Dimension;

        public BasicUnit(string symbol, string name, Dimension dimension)
        {
            this._Symbol = symbol;
            this._Name = name;
            this._Dimension = dimension;
        }

        public override string Symbol
        {
            get { return this._Symbol; }
        }

        public override string Name
        {
            get { return this._Name; }
        }

        public override Dimension Dimension
        {
            get { return this._Dimension; }
        }
    }
}