using System;

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
    /// Converts the measurement to the specified <paramref name="target"/> unit (delegates to existing conversion logic) for fluent style.
    /// </summary>
    public static Measurement<T> In<T>(this Measurement<T> measurement, Unit target) where T : IComparable
    {
        if (measurement.Unit == target)
        {
            return measurement;
        }

        // Perform conversion through double base space for now using internal scale factors.
        // For generic T we restrict to double/decimal/int supported calculators by attempting double path first.
        if (measurement is DoubleMeasurement dm)
        {
            var baseVal = dm.Unit.ToBase(dm.Value);
            var converted = target.FromBase(baseVal);
            return (Measurement<T>)(object)new DoubleMeasurement(converted, target);
        }

        if (measurement is DecimalMeasurement decm)
        {
            // Convert via double to reuse scaling (acceptable minor precision loss for derived units if scale factors are power of 10).
            var baseVal = (decimal)decm.Unit.ToBase((double)decm.Value);
            var converted = (decimal)target.FromBase((double)baseVal);
            return (Measurement<T>)(object)new DecimalMeasurement(converted, target);
        }

        if (measurement is Int32Measurement im)
        {
            var baseVal = im.Unit.ToBase(im.Value);
            var converted = target.FromBase(baseVal);
            return (Measurement<T>)(object)new Int32Measurement((int)converted, target);
        }

        throw new System.NotSupportedException("Generic conversion for supplied numeric type not implemented.");
    }
}