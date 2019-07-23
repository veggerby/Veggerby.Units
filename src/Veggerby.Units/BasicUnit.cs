using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class BasicUnit : Unit
    {
        public BasicUnit(string symbol, string name, UnitSystem system, Dimension dimension)
        {
            Symbol = symbol;
            Name = name;
            System = system;
            Dimension = dimension;
        }

        public override string Symbol { get; }
        public override string Name { get; }
        public override UnitSystem System { get; }
        public override Dimension Dimension { get; }

        internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
    }
}