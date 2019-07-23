using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class DerivedUnit : Unit
    {
        private readonly Unit _expression;
        public DerivedUnit(string symbol, string name, Unit expression)
        {
            Symbol = symbol;
            Name = name;
            _expression = expression;
        }

        public override string Symbol { get; }
        public override string Name { get; }

        public override UnitSystem System => _expression.System;
        public override Dimension Dimension => _expression.Dimension;
        internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
    }
}