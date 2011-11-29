using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class PrefixedUnit : Unit
    {
        internal PrefixedUnit(Prefix prefix, Unit baseUnit)
        {
            this._Prefix = prefix;
            this._BaseUnit = baseUnit;
        }

        private readonly Prefix _Prefix;
        private readonly Unit _BaseUnit;

        public Prefix Prefix
        {
            get { return this._Prefix; }
        }

        public Unit BaseUnit
        {
            get { return this._BaseUnit; }
        }

        public override string Name
        {
            get { return string.Format("{0}{1}", this.Prefix.Name, this.BaseUnit.Name); }
        }

        public override string Symbol
        {
            get { return string.Format("{0}{1}", this.Prefix.Symbol, this.BaseUnit.Symbol); }
        }

        public override Dimension Dimension
        {
            get { return this.BaseUnit.Dimension; }
        }

        public override UnitSystem System
        {
            get { return this.BaseUnit.System; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
