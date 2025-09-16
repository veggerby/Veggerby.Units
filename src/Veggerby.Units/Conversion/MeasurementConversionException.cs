using System;

namespace Veggerby.Units.Conversion;

/// <summary>
/// Exception thrown when attempting a measurement conversion between incompatible dimensions.
/// </summary>
/// <remarks>
/// This exception is not raised for unsupported numeric types; those raise <see cref="NotSupportedException"/>.
/// </remarks>
public class MeasurementConversionException(string message) : Exception(message)
{
}