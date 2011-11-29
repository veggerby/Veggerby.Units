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
            get { return string.Format("{0}{1}", this._Prefix.Name, this._BaseUnit.Name); }
        }

        public override string Symbol
        {
            get { return string.Format("{0}{1}", this._Prefix.Symbol, this._BaseUnit.Symbol); }
        }

        public override Dimension Dimension
        {
            get { return this.BaseUnit.Dimension; }
        }

        public override UnitSystem System
        {
            get { return this.BaseUnit.System; }
        }
    }
}
