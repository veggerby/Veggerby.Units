using System;

namespace Veggerby.Units
{
    public class UnitException : Exception
    {
        public UnitException(Unit d1, Unit d2)
            : base(string.Format("Cannot convert {0} to {1}, dimensions are incompatible ({2} and {2})", d1.Symbol, d2.Symbol))
        {
        }
    }

    public class PrefixException : Exception
    {
        public PrefixException(double factor)
            : base(string.Format("Invalid unit prefix {0}", factor))
        {
        }
    }
}