using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class ScaleUnit : Unit
    {
        private readonly Unit _base;
        private readonly UnitSystem _system;

        public ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null)
        {
            Symbol = symbol;
            Name = name;
            Scale = scale;
            _base = @base;
            _system = system;
        }

        public override string Symbol { get; }
        public override string Name { get; }
        public override Dimension Dimension { get; }
        public double Scale { get; }

        public override UnitSystem System => _system ?? _base.System;
        internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
    }
}
