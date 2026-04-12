using System;

namespace Veggerby.Units;

/// <summary>
/// Thrown when attempting to add or subtract units/measurements with incompatible dimensions (no coercion permitted).
/// </summary>
public class UnitException : Exception
{
    /// <summary>The first (left) operand unit involved in the failing operation.</summary>
    public Unit Unit1 { get; }

    /// <summary>The second (right) operand unit involved in the failing operation.</summary>
    public Unit Unit2 { get; }

    /// <summary>
    /// Initialises a new instance with both operand units. The exception message includes their symbols and dimensions.
    /// </summary>
    /// <param name="u1">First (left) operand.</param>
    /// <param name="u2">Second (right) operand.</param>
    public UnitException(Unit u1, Unit u2)
        : base(string.Format("Cannot operate on {0} and {1}; incompatible dimensions ({2} vs {3})", u1?.Symbol, u2?.Symbol, u1?.Dimension, u2?.Dimension))
    {
        Unit1 = u1;
        Unit2 = u2;
    }
}

/// <summary>
/// Thrown when a numeric scaling factor cannot be mapped to a known <see cref="Prefix"/> during implicit prefix
/// application.
/// </summary>
public class PrefixException(double factor) : Exception(string.Format("Invalid unit prefix {0}", factor))
{
}