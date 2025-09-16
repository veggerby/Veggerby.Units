using System;

namespace Veggerby.Units.Conversion;

/// <summary>
/// Measurement conversion helpers. Conversions proceed via canonical scale factors relative to SI base units.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts a measurement to an equivalent value expressed in the target unit. Throws when dimensions are
    /// incompatible or calculator type unsupported.
    /// </summary>
    public static Measurement<T> ConvertTo<T>(this Measurement<T> value, Unit unit) where T : IComparable
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        if (unit == null)
        {
            throw new ArgumentNullException("unit");
        }

        if (value.Unit.Dimension != unit.Dimension)
        {
            throw new MeasurementConversionException(string.Format("Cannot convert {0} to {1}, dimensions are incompatible ({2} and {2})", value, unit, value.Unit.Dimension, unit.Dimension));
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
        double baseValue = System.Convert.ToDouble(value.Value) * sourceFactor;
        double converted = baseValue / targetFactor;

        object newValue;
        if (typeof(T) == typeof(int))
        {
            newValue = (T)(object)System.Convert.ToInt32(System.Math.Round(converted));
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
    /// Ensures v1 is expressed in the same unit as v2 (converting when necessary). Used internally for
    /// relational comparisons.
    /// </summary>
    public static Measurement<T> AlignUnits<T>(this Measurement<T> v1, Measurement<T> v2) where T : IComparable
    {
        if (v1.Unit == v2.Unit)
        {
            return v1;
        }

        return v1.ConvertTo(v2.Unit);
    }
}