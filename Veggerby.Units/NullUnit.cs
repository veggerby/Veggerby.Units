using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class NullUnit : Unit
    {
        public override string Symbol
        {
            get { return string.Empty; }
        }

        public override string Name
        {
            get { return string.Empty; }
        }

        public override UnitSystem System
        {
            get { return UnitSystem.None; }
        }

        public override Dimension Dimension
        {
            get { return Dimension.None; }
        }
    }
}
