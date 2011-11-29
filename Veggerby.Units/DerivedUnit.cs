using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class DerivedUnit : Unit
    {
        private readonly string _Symbol;
        private readonly string _Name;
        private readonly Unit _Expression;

        public DerivedUnit(string symbol, string name, Unit expression)
        {
            this._Symbol = symbol;
            this._Name = name;
            this._Expression = expression;
        }

        public override string Symbol
        {
            get { return this._Symbol; }
        }

        public override string Name
        {
            get { return this._Name; }
        }

        public override UnitSystem System
        {
            get { return this._Expression.System; }
        }

        public override Dimension Dimension
        {
            get { return this._Expression.Dimension; }
        }
    }
}