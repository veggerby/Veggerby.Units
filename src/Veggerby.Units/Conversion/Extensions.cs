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
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (unit is null)
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
        // Convert via affine-aware base methods.
        double numeric = Convert.ToDouble(value.Value);

        double converted;
        if (value.Unit is AffineUnit auFrom && unit is AffineUnit auTo && ReferenceEquals(auFrom.BaseUnit, auTo.BaseUnit))
        {
            // Direct affine -> affine conversion without intermediate rounding: ((x*sf + of) - ot)/st
            var baseVal = (numeric * auFrom.Scale) + auFrom.Offset; // to base
            converted = (baseVal - auTo.Offset) / auTo.Scale;
        }
        else
        {
            double baseValue = value.Unit.ToBase(numeric);
            converted = unit.FromBase(baseValue);
        }

        object newValue;
        if (typeof(T) == typeof(int))
        {
            newValue = (T)(object)Convert.ToInt32(Math.Round(converted));
        }
        else if (typeof(T) == typeof(double))
        {
            newValue = (T)(object)converted;
        }
        else if (typeof(T) == typeof(decimal))
        {
            newValue = (T)(object)Convert.ToDecimal(converted);
        }
        else
        {
            throw new NotSupportedException($"Conversion for calculator type {typeof(T).Name} is not supported.");
        }

        return new Measurement<T>((T)newValue, unit, value.Calculator);
    }

    /// <summary>
    /// Attempts to convert a measurement to an equivalent value expressed in the target unit.
    /// Unlike <see cref="ConvertTo{T}(Measurement{T}, Unit)"/> this method never throws for dimension
    /// mismatch or unsupported numeric type; instead it returns <c>false</c> and sets <paramref name="result"/>
    /// to <c>null</c>. Argument <c>null</c> checks still throw to surface programmer errors early.
    /// </summary>
    /// <typeparam name="T">Underlying numeric type (currently int or double supported).</typeparam>
    /// <param name="value">Source measurement (must not be null).</param>
    /// <param name="unit">Target unit (must not be null).</param>
    /// <param name="result">Converted measurement when the operation succeeds; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> when conversion succeeded; otherwise <c>false</c>.</returns>
    public static bool TryConvertTo<T>(this Measurement<T> value, Unit unit, out Measurement<T> result) where T : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (unit is null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        if (value.Unit.Dimension != unit.Dimension)
        {
            result = null;
            return false;
        }

        if (value.Unit == unit)
        {
            result = value; // already in correct unit
            return true;
        }

        double numeric = Convert.ToDouble(value.Value);
        double converted;
        if (value.Unit is AffineUnit auFrom && unit is AffineUnit auTo && ReferenceEquals(auFrom.BaseUnit, auTo.BaseUnit))
        {
            var baseVal = (numeric * auFrom.Scale) + auFrom.Offset;
            converted = (baseVal - auTo.Offset) / auTo.Scale;
        }
        else
        {
            double baseValue = value.Unit.ToBase(numeric);
            converted = unit.FromBase(baseValue);
        }

        object newValue;
        if (typeof(T) == typeof(int))
        {
            newValue = (T)(object)Convert.ToInt32(Math.Round(converted));
        }
        else if (typeof(T) == typeof(double))
        {
            newValue = (T)(object)converted;
        }
        else if (typeof(T) == typeof(decimal))
        {
            newValue = (T)(object)Convert.ToDecimal(converted);
        }
        else
        {
            result = null;
            return false;
        }

        result = new Measurement<T>((T)newValue, unit, value.Calculator);
        return true;
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