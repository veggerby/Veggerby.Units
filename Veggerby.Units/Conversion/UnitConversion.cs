using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Units.Conversion
{
    public abstract class UnitConversion
    {
        public abstract Measurement ConvertTo(Measurement value, Unit unit);
    }
}
