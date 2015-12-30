using System;

namespace Veggerby.Units.Dimensions
{
    public class DimensionException : Exception
    {
        public DimensionException(Dimension d1, Dimension d2)
            : base(string.Format("Dimensions are incompatible ({0} and {1})", d1.Symbol, d2.Symbol))
        {
        }
    }
}