using System;
using Veggerby.Units.Resources;

namespace Veggerby.Units
{
    public class UnitException : Exception
    {
        public UnitException(Unit d1, Unit d2)
            : base(string.Format(Strings.IncompatibleUnits, d1.Symbol, d2.Symbol))
        {
        }
    }

    public class PrefixException : Exception
    {
        public PrefixException(double factor)
            : base(string.Format(Strings.InvalidPrefix, factor))
        {
        }
    }
}