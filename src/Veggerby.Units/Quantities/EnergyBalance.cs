using System;

using Veggerby.Units.Conversion;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Helper for aggregating energy transfer quantities (Work, Heat) distinctly from stored energy kinds.
/// This is intentionally minimal: it does not attempt first-law bookkeeping beyond summation.
/// </summary>
public sealed class EnergyBalance
{
    private double _work; // Joules
    private double _heat; // Joules

    /// <summary>Add a work transfer (signed). Positive typically denotes work done on the system.</summary>
    public void AddWork(Quantity<double> work)
    {
        if (work is null || work.Kind != QuantityKinds.Work)
        {
            throw new InvalidOperationException("Expected Work quantity.");
        }
        _work += work.Measurement.ConvertTo(QuantityKinds.Energy.CanonicalUnit).Value;
    }

    /// <summary>Add a heat transfer (signed). Positive typically denotes heat added to the system.</summary>
    public void AddHeat(Quantity<double> heat)
    {
        if (heat is null || heat.Kind != QuantityKinds.Heat)
        {
            throw new InvalidOperationException("Expected Heat quantity.");
        }
        _heat += heat.Measurement.ConvertTo(QuantityKinds.Energy.CanonicalUnit).Value;
    }

    /// <summary>Total work (J).</summary>
    public Quantity<double> TotalWork() => Quantity.Work(_work);
    /// <summary>Total heat (J).</summary>
    public Quantity<double> TotalHeat() => Quantity.Heat(_heat);
    /// <summary>Net energy transfer (Q + W).</summary>
    public Quantity<double> NetTransfer() => Quantity.Energy(_heat + _work);
}