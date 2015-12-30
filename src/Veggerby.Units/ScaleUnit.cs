using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class ScaleUnit : Unit
    {
        private readonly string _symbol;
        private readonly string _name;
        private readonly double _scale;        
        private readonly Unit _base;
        private readonly UnitSystem _system;

        public ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null)
        {
            _symbol = symbol;
            _name = name;
            _scale = scale;
            _base = @base;
            _system = system;
        }

        public override string Symbol
        {
            get { return _symbol; }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override UnitSystem System
        {
            get { return _system ?? _base.System; }
        }

        public override Dimension Dimension
        {
            get { return _base.Dimension; }
        }

        public double Scale
        {
            get { return _scale; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
