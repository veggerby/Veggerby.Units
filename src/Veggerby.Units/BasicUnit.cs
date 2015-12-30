using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class BasicUnit : Unit
    {
        private readonly string _symbol;
        private readonly string _same;
        private readonly UnitSystem _system;
        private readonly Dimension _dimension;

        public BasicUnit(string symbol, string name, UnitSystem system, Dimension dimension)
        {
            _symbol = symbol;
            _same = name;
            _system = system;
            _dimension = dimension;
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
            get { return _system; }
        }

        public override Dimension Dimension
        {
            get { return _dimension; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}