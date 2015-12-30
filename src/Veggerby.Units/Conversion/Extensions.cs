using System;

namespace Veggerby.Units.Conversion
{
    public static class Extensions
    {
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

            // should convert Measurement to specified unit
            return null;
        }

        public static Measurement<T> AlignUnits<T>(this Measurement<T> v1, Measurement<T> v2) where T : IComparable
        {
            if (v1.Unit == v2.Unit)
            {
                return v1;
            }

            return v1.ConvertTo(v2.Unit);
        }
    }
}
