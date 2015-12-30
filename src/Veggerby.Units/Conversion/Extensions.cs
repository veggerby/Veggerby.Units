using System;

namespace Veggerby.Units.Conversion
{
    public static class Extensions
    {
        public static Measurement ConvertTo(this Measurement value, Unit unit)
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

            return null;
        }

        public static Measurement AlignUnits(this Measurement v1, Measurement v2)
        {
            if (v1.Unit == v2.Unit)
            {
                return v1;
            }

            return v1.ConvertTo(v2.Unit);
        }
    }
}
