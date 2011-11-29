using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class BasicUnit : Unit
    {
        private readonly string _Symbol;
        private readonly string _Name;
        private readonly UnitSystem _System;
        private readonly Dimension _Dimension;

        public BasicUnit(string symbol, string name, UnitSystem system, Dimension dimension)
        {
            this._Symbol = symbol;
            this._Name = name;
            this._System = system;
            this._Dimension = dimension;
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
            get { return this._System; }
        }

        public override Dimension Dimension
        {
            get { return this._Dimension; }
        }

        internal override T Accept<T>(Visitors.Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}