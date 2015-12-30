using System;
using Veggerby.Units.Resources;

namespace Veggerby.Units.Dimensions
{
    public class DimensionException : Exception
    {
        public DimensionException(Dimension d1, Dimension d2)
            : base(string.Format(Strings.IncompatibleDimensions, d1.Symbol, d2.Symbol))
        {
        }
    }
}