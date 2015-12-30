using System;

namespace Veggerby.Units.Conversion
{
    public class MeasurementConversionException : Exception
    {
        public MeasurementConversionException(string message):
            base(message)
        {
        }
    }
}