using System;

namespace Veggerby.Units.Dimensions;

/// <summary>
/// Thrown when attempting additive/subtractive operations on incompatible dimensions.
/// </summary>
public class DimensionException(Dimension d1, Dimension d2) : Exception(string.Format("Dimensions are incompatible ({0} and {1})", d1.Symbol, d2.Symbol))
{
}