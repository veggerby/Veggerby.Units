using System;

using Veggerby.Units.Conversion;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Operations bridging absolute (affine) and delta (linear) temperature semantics.
/// </summary>
public static class TemperatureOps
{
    /// <summary>ΔT = a - b producing a linear temperature difference in canonical Kelvin.</summary>
    public static Quantity<double> Delta(Quantity<double> aAbsolute, Quantity<double> bAbsolute)
    {
        RequireKind(aAbsolute, QuantityKinds.TemperatureAbsolute);
        RequireKind(bAbsolute, QuantityKinds.TemperatureAbsolute);

        var aK = aAbsolute.Measurement.ConvertTo(Unit.SI.K);
        var bK = bAbsolute.Measurement.ConvertTo(Unit.SI.K);
        var diff = new DoubleMeasurement(aK.Value - bK.Value, Unit.SI.K);
        return new Quantity<double>(diff, QuantityKinds.TemperatureDelta, strictDimensionCheck: true);
    }

    /// <summary>T' = T_abs + ΔT (delta treated as linear, result expressed in absolute's unit).</summary>
    public static Quantity<double> AddDelta(Quantity<double> absolute, Quantity<double> delta)
    {
        RequireKind(absolute, QuantityKinds.TemperatureAbsolute);
        RequireKind(delta, QuantityKinds.TemperatureDelta);

        var absK = absolute.Measurement.ConvertTo(Unit.SI.K);
        var dK = delta.Measurement.ConvertTo(Unit.SI.K);
        var sumK = new DoubleMeasurement(absK.Value + dK.Value, Unit.SI.K);
        var back = sumK.ConvertTo(absolute.Measurement.Unit);
        return new Quantity<double>(back, QuantityKinds.TemperatureAbsolute, strictDimensionCheck: true);
    }

    private static void RequireKind(Quantity<double> q, QuantityKind expected)
    {
        if (!ReferenceEquals(q.Kind, expected))
        {
            throw new InvalidOperationException($"Expected '{expected.Name}', got '{q.Kind.Name}'.");
        }
    }
}