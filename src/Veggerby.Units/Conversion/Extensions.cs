using System;

namespace Veggerby.Units.Conversion;

/// <summary>
/// Measurement conversion helpers. Conversions proceed via canonical scale factors relative to SI base units.
/// Only integral (int) and double calculators are supported; other generic numeric types will raise
/// <see cref="NotSupportedException"/>.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts a measurement to an equivalent value expressed in the target unit.
    /// </summary>
    /// <typeparam name="T">Underlying numeric type (currently int or double supported).</typeparam>
    /// <param name="value">Source measurement (must not be null).</param>
    /// <param name="unit">Target unit (must not be null and must have identical dimension).</param>
    /// <returns>A new measurement expressed in the target unit (or the original instance if already expressed in that unit).</returns>
    /// <exception cref="ArgumentNullException">value or unit is null.</exception>
    /// <exception cref="MeasurementConversionException">Dimensions are incompatible.</exception>
    /// <exception cref="NotSupportedException">Underlying numeric type unsupported.</exception>
    public static Measurement<T> ConvertTo<T>(this Measurement<T> value, Unit unit) where T : IComparable
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        if (value.Unit.Dimension != unit.Dimension)
        {
            throw new MeasurementConversionException($"Cannot convert {value} to {unit}, dimensions are incompatible ({value.Unit.Dimension} and {unit.Dimension})");
        }

        if (value.Unit == unit)
        {
            return value; // no conversion necessary
        }

        // Convert via scale factor to underlying base (SI) then to target unit.
        // value_in_base = value * factor(source)
        // value_in_target = value_in_base / factor(target)
        // Works because both factors are relative to same base representation.
        var sourceFactor = value.Unit.GetScaleFactor();
        var targetFactor = unit.GetScaleFactor();

        // We only support numeric Calculator types that can handle double intermediates (int, double).
        // For int calculators we round the final result.
        double baseValue = Convert.ToDouble(value.Value) * sourceFactor;
        double converted = baseValue / targetFactor;

        object newValue;
        if (typeof(T) == typeof(int))
        {
            newValue = (T)(object)Convert.ToInt32(Math.Round(converted));
        }
        else if (typeof(T) == typeof(double))
        {
            newValue = (T)(object)converted;
        }
        else
        {
            throw new NotSupportedException($"Conversion for calculator type {typeof(T).Name} is not supported.");
        }

        return new Measurement<T>((T)newValue, unit, value.Calculator);
    }

    /// <summary>
    /// Ensures <paramref name="v1"/> is expressed in the same unit as <paramref name="v2"/> (converting when necessary).
    /// Used internally for relational comparisons; no dimension mismatch can occur because both measurements
    /// must already be comparable for operators to be valid.
    /// </summary>
    /// <typeparam name="T">Underlying numeric type.</typeparam>
    /// <param name="v1">Left measurement to align.</param>
    /// <param name="v2">Reference measurement whose unit will be matched.</param>
    /// <returns>Either the original left measurement (if units already match) or a converted measurement.</returns>
    public static Measurement<T> AlignUnits<T>(this Measurement<T> v1, Measurement<T> v2) where T : IComparable
    {
        if (v1.Unit == v2.Unit)
        {
            return v1;
        }

        return v1.ConvertTo(v2.Unit);
    }
}