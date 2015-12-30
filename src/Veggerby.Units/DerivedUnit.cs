using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class DerivedUnit : Unit
    {
        private readonly string _symbol;
        private readonly string _same;
        private readonly Unit _expression;

        public DerivedUnit(string symbol, string name, Unit expression)
        {
            _symbol = symbol;
            _same = name;
            _expression = expression;
        }

        public override string Symbol
        {
            get { return _symbol; }
        }

        public override string Name
        {
            get { return _same; }
        }

        public override UnitSystem System
        {
            get { return _expression.System; }
        }

        public override Dimension Dimension
        {
            get { return _expression.Dimension; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}