using System;

using Veggerby.Units.Conversion;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Helpers for safe averaging of absolute temperatures by converting to base (K), averaging as linear values, and returning an absolute.
/// </summary>
public static class TemperatureMean
{
    /// <summary>
    /// Computes the arithmetic mean of absolute temperature quantities (any supported unit). Uses Kelvin conversion,
    /// averages in linear space, and returns an absolute temperature in the unit of the first sample.
    /// </summary>
    public static Quantity<double> Mean(params Quantity<double>[] absolutes)
    {
        if (absolutes == null || absolutes.Length == 0)
        {
            return null;
        }

        // Validate all are TemperatureAbsolute
        double sumK = 0.0;
        foreach (var q in absolutes)
        {
            if (q == null)
            {
                continue;
            }
            if (!ReferenceEquals(q.Kind, QuantityKinds.TemperatureAbsolute))
            {
                throw new InvalidOperationException("Mean requires only absolute temperature quantities.");
            }
            var k = q.Measurement.ConvertTo(QuantityKinds.TemperatureAbsolute.CanonicalUnit);
            sumK += (double)k.Value;
        }

        var meanK = sumK / absolutes.Length;
        var firstUnit = absolutes[0].Measurement.Unit; // preserve caller's preferred display
        var meanBase = new DoubleMeasurement(meanK, QuantityKinds.TemperatureAbsolute.CanonicalUnit);
        var meanDisplay = meanBase.ConvertTo(firstUnit);
        return new Quantity<double>(meanDisplay, QuantityKinds.TemperatureAbsolute, strictDimensionCheck: true);
    }
}