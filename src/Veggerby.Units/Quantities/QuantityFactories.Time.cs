using System;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Time quantity factory helpers.
/// </summary>
public static partial class Quantity
{
    /// <summary>
    /// Creates a time quantity (seconds) with the canonical second unit.
    /// </summary>
    /// <param name="seconds">Time in seconds.</param>
    /// <returns>A <see cref="Quantity{Double}"/> representing elapsed time.</returns>
    public static Quantity<double> Time(double seconds)
        => Of(seconds, QuantityKinds.Time.CanonicalUnit, QuantityKinds.Time);

    /// <summary>
    /// Creates a time quantity from a <see cref="TimeSpan"/> using total seconds.
    /// </summary>
    /// <param name="span">The span to convert.</param>
    /// <returns>A <see cref="Quantity{Double}"/> whose value is <paramref name="span"/> total seconds.</returns>
    public static Quantity<double> Time(TimeSpan span)
        => Time(span.TotalSeconds);
}