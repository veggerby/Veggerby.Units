using System;

namespace Veggerby.Units.Conversion;

/// <summary>
/// Thrown when attempting a measurement conversion between incompatible dimensions or unsupported numeric types.
/// </summary>
public class MeasurementConversionException(string message) : Exception(message)
{
}