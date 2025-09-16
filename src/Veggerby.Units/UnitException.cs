using System;

namespace Veggerby.Units;

/// <summary>
/// Thrown when attempting to add or subtract units/measurements with incompatible dimensions (no coercion permitted).
/// </summary>
public class UnitException(Unit u1, Unit u2) : Exception(string.Format("Cannot operate on {0} and {1}; incompatible dimensions ({2} vs {3})", u1?.Symbol, u2?.Symbol, u1?.Dimension, u2?.Dimension))
{
}

/// <summary>
/// Thrown when a numeric scaling factor cannot be mapped to a known <see cref="Prefix"/> during implicit prefix
/// application.
/// </summary>
public class PrefixException(double factor) : Exception(string.Format("Invalid unit prefix {0}", factor))
{
}