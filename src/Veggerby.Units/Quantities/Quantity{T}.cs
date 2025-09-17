using System;
using Veggerby.Units.Conversion;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Wraps a <see cref="Measurement{T}"/> with an associated <see cref="QuantityKind"/> providing semantic disambiguation
/// for identical dimensions (e.g. Joule as Energy vs Torque). Arithmetic rules are opt-in via static helpers.
/// </summary>
public sealed class Quantity<T> where T : IComparable
{
    /// <summary>The underlying measurement.</summary>
    public Measurement<T> Measurement { get; }
    /// <summary>The associated semantic kind.</summary>
    public QuantityKind Kind { get; }

    /// <summary>
    /// Creates a quantity wrapping a measurement and kind. When <paramref name="strictDimensionCheck"/> is true and the
    /// measurement dimension differs from the kind's canonical dimension a <see cref="UnitException"/> is thrown.
    /// </summary>
    public Quantity(Measurement<T> measurement, QuantityKind kind, bool strictDimensionCheck = true)
    {
        if (measurement == null) { throw new ArgumentNullException(nameof(measurement)); }
        if (kind == null) { throw new ArgumentNullException(nameof(kind)); }
        if (strictDimensionCheck && !kind.Matches(measurement.Unit))
        {
            throw new UnitException(measurement.Unit, kind.CanonicalUnit);
        }
        Measurement = measurement;
        Kind = kind;
    }

    /// <summary>Returns a new quantity converted to a target unit (semantic kind preserved).</summary>
    public Quantity<T> ConvertTo(Unit targetUnit) => new(Measurement.ConvertTo(targetUnit), Kind, strictDimensionCheck: true);

    /// <inheritdoc />
    public override string ToString() => $"{Measurement} [{Kind}]";

    /// <summary>Adds two quantities. When <paramref name="requireSameKind"/> is true different kinds raise an exception.</summary>
    public static Quantity<T> Add(Quantity<T> a, Quantity<T> b, bool requireSameKind = false)
    {
        if (a == null || b == null) { return a ?? b; }
        if (requireSameKind && !ReferenceEquals(a.Kind, b.Kind))
        {
            throw new InvalidOperationException($"Cannot add {a.Kind.Name} to {b.Kind.Name}.");
        }
        var bAligned = b.Measurement.ConvertTo(a.Measurement.Unit);
        return new Quantity<T>(a.Measurement + bAligned, a.Kind, strictDimensionCheck: false);
    }

    /// <summary>Subtracts two quantities with optional semantic enforcement.</summary>
    public static Quantity<T> Sub(Quantity<T> a, Quantity<T> b, bool requireSameKind = false)
    {
        if (a == null || b == null) { return a ?? b; }
        if (requireSameKind && !ReferenceEquals(a.Kind, b.Kind))
        {
            throw new InvalidOperationException($"Cannot subtract {b.Kind.Name} from {a.Kind.Name}.");
        }
        var bAligned = b.Measurement.ConvertTo(a.Measurement.Unit);
        return new Quantity<T>(a.Measurement - bAligned, a.Kind, strictDimensionCheck: false);
    }
}
