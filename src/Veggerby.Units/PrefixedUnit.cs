using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class PrefixedUnit : Unit
    {
        internal PrefixedUnit(Prefix prefix, Unit baseUnit)
        {
            _prefix = prefix;
            _baseUnit = baseUnit;
        }

        private readonly Prefix _prefix;
        private readonly Unit _baseUnit;

        public Prefix Prefix
        {
            get { return _prefix; }
        }

        public Unit BaseUnit
        {
            get { return _baseUnit; }
        }

        public override string Name
        {
            get { return string.Format("{0}{1}", Prefix.Name, BaseUnit.Name); }
        }

        public override string Symbol
        {
            get { return string.Format("{0}{1}", Prefix.Symbol, BaseUnit.Symbol); }
        }

        public override Dimension Dimension
        {
            get { return BaseUnit.Dimension; }
        }

        public override UnitSystem System
        {
            get { return BaseUnit.System; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
