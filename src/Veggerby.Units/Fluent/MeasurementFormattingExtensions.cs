using System;

using Veggerby.Units.Conversion;
using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent;

/// <summary>
/// Fluent formatting helpers for <see cref="Measurement{T}"/> providing derived symbol and qualified output.
/// </summary>
public static class MeasurementFormattingExtensions
{
    /// <summary>
    /// Formats the measurement using the specified <paramref name="format"/>. When <paramref name="format"/>
    /// is <see cref="UnitFormat.Qualified"/> and the underlying unit symbol is ambiguous a quantity kind name
    /// is appended if supplied.
    /// </summary>
    public static string Format<T>(this Measurement<T> measurement, UnitFormat format, QuantityKind kind = null, bool strict = false) where T : IComparable
    {
        return UnitFormatter.Format(measurement, format, kind, strict);
    }

    /// <summary>
    /// Converts the measurement to the specified <paramref name="target"/> unit using the standard
    /// <see cref="Extensions.ConvertTo{T}(Measurement{T}, Unit)"/> conversion path.
    /// </summary>
    /// <typeparam name="T">Underlying numeric type.</typeparam>
    /// <param name="measurement">The source measurement (must not be null).</param>
    /// <param name="target">The target unit (must have the same dimension).</param>
    /// <returns>A measurement expressed in <paramref name="target"/> (or the same instance when units already match).</returns>
    public static Measurement<T> In<T>(this Measurement<T> measurement, Unit target) where T : IComparable
    {
        return measurement.ConvertTo(target);
    }
}