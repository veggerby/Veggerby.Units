using System;

namespace Veggerby.Units;

public class UnitException(Unit u1, Unit u2) : Exception(string.Format("Cannot operate on {0} and {1}; incompatible dimensions ({2} vs {3})", u1?.Symbol, u2?.Symbol, u1?.Dimension, u2?.Dimension))
{
}

public class PrefixException(double factor) : Exception(string.Format("Invalid unit prefix {0}", factor))
{
}