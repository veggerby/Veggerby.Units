using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class ScaleUnit : Unit
    {
        private readonly string _Symbol;
        private readonly string _Name;
        private readonly double _Scale;        
        private readonly Unit _Base;
        private readonly UnitSystem _System;

        public ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null)
        {
            this._Symbol = symbol;
            this._Name = name;
            this._Scale = scale;
            this._Base = @base;
            this._System = system;
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
            get { return this._System ?? this._Base.System; }
        }

        public override Dimension Dimension
        {
            get { return this._Base.Dimension; }
        }

        public double Scale
        {
            get { return _Scale; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
