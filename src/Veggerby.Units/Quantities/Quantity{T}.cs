using System;

using Veggerby.Units.Conversion;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Wraps a <see cref="Measurement{T}"/> with an associated <see cref="QuantityKind"/> providing semantic disambiguation
/// for identical dimensions (e.g. Joule as Energy vs Torque). Arithmetic rules are opt-in via static helpers.
/// </summary>
/// <remarks>
/// Arithmetic semantics (see docs/quantities.md for full matrix):
/// <list type="bullet">
/// <item><description>Same-kind + / - allowed only when kind flags permit.</description></item>
/// <item><description>Point - Point (same kind) -> DifferenceResultKind (e.g. AbsoluteTemperature - AbsoluteTemperature -> TemperatureDelta).</description></item>
/// <item><description>Point ± Delta -> Point; Delta ± Delta -> Delta.</description></item>
/// <item><description>Cross-kind + / - always throws (even if dimensions align).</description></item>
/// <item><description>Multiply / Divide consult <see cref="QuantityKindInferenceRegistry"/>; if no rule and one operand dimensionless, preserves the other non point-like kind.</description></item>
/// <item><description>Absolute (point-like) kinds never survive scalar fallback; explicit inference required.</description></item>
/// <item><description>Scaling by dimensionless measurement allowed only for vector-like kinds.</description></item>
/// <item><description>No direct power operator at quantity level (apply to underlying measurement/unit explicitly).</description></item>
/// </list>
/// Rationale: keep dimensional reducer pure, surface ambiguous semantics early, and require explicit opt-in for domain meaning.
/// </remarks>
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
        if (measurement == null)
        {
            throw new ArgumentNullException(nameof(measurement));
        }

        if (kind == null)
        {
            throw new ArgumentNullException(nameof(kind));
        }

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

    /// <summary>
    /// Determines semantic equality: requires identical <see cref="Kind"/> and equality of numeric value after aligning the
    /// right operand to this quantity's unit. Cross-kind quantities are never equal even if dimensions &amp; units match.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns><c>true</c> when both quantities share the same kind and aligned values; otherwise <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
        if (obj is not Quantity<T> other)
        {
            return false;
        }
        if (!ReferenceEquals(Kind, other.Kind))
        {
            return false; // cross-kind never equal
        }
        var otherAligned = other.Measurement.ConvertTo(Measurement.Unit);
        return Measurement.Value.CompareTo(otherAligned.Value) == 0;
    }

    /// <summary>
    /// Computes a stable hash code by normalizing the underlying measurement to the kind's canonical unit and combining
    /// it with the semantic kind reference.
    /// </summary>
    /// <returns>Hash code consistent with <see cref="Equals(object)"/>.</returns>
    public override int GetHashCode()
    {
        var canonical = Measurement.ConvertTo(Kind.CanonicalUnit);
        return HashCode.Combine(Kind, canonical.Value);
    }

    /// <summary>Compares two quantities of the same semantic kind (after unit alignment). Cross-kind comparisons throw.</summary>
    public int CompareTo(Quantity<T> other)
    {
        if (other == null)
        {
            return 1;
        }

        if (!ReferenceEquals(Kind, other.Kind))
        {
            throw new InvalidOperationException($"Cannot compare {Kind.Name} with {other.Kind.Name}.");
        }

        var otherAligned = other.Measurement.ConvertTo(Measurement.Unit);
        return Measurement.Value.CompareTo(otherAligned.Value);
    }

    /// <summary>
    /// Equality operator enforcing same-kind comparison and unit-aligned numeric equality. Throws when kinds differ to
    /// surface semantic mismatches eagerly rather than silently returning false.
    /// </summary>
    public static bool operator ==(Quantity<T> a, Quantity<T> b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }
        if (a is null || b is null)
        {
            return false;
        }
        if (!ReferenceEquals(a.Kind, b.Kind))
        {
            throw new InvalidOperationException($"Cannot compare {a.Kind.Name} with {b.Kind.Name}.");
        }
        var bAligned = b.Measurement.ConvertTo(a.Measurement.Unit);
        return a.Measurement.Value.CompareTo(bAligned.Value) == 0;
    }

    /// <summary>
    /// Inequality operator inverse of <see cref="operator ==(Quantity{T}, Quantity{T})"/>.
    /// </summary>
    public static bool operator !=(Quantity<T> a, Quantity<T> b) => !(a == b);

    /// <summary>
    /// Less-than relational operator. Requires both operands share the same semantic kind; values are compared after
    /// aligning <paramref name="b"/>'s unit to <paramref name="a"/>'s unit. Throws on cross-kind comparison.
    /// </summary>
    public static bool operator <(Quantity<T> a, Quantity<T> b)
    {
        if (a == null || b == null)
        {
            throw new InvalidOperationException("Cannot compare null quantities.");
        }
        return a.CompareTo(b) < 0;
    }

    /// <summary>
    /// Greater-than relational operator. Requires same kind and aligns units prior to comparison.
    /// </summary>
    public static bool operator >(Quantity<T> a, Quantity<T> b)
    {
        if (a == null || b == null)
        {
            throw new InvalidOperationException("Cannot compare null quantities.");
        }
        return a.CompareTo(b) > 0;
    }

    /// <summary>
    /// Less-than-or-equal relational operator. Requires same kind and aligns units prior to comparison.
    /// </summary>
    public static bool operator <=(Quantity<T> a, Quantity<T> b)
    {
        if (a == null || b == null)
        {
            throw new InvalidOperationException("Cannot compare null quantities.");
        }
        return a.CompareTo(b) <= 0;
    }

    /// <summary>
    /// Greater-than-or-equal relational operator. Requires same kind and aligns units prior to comparison.
    /// </summary>
    public static bool operator >=(Quantity<T> a, Quantity<T> b)
    {
        if (a == null || b == null)
        {
            throw new InvalidOperationException("Cannot compare null quantities.");
        }
        return a.CompareTo(b) >= 0;
    }

    /// <summary>Attempts to multiply two quantities. Returns false (without throwing) when no semantic rule or fallback applies.</summary>
    public static bool TryMultiply(Quantity<T> left, Quantity<T> right, out Quantity<T> result)
    {
        result = null;
        if (left == null || right == null)
        {
            result = left ?? right;
            return true;
        }
        try
        {
            result = left * right; // reuse operator semantics
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>Attempts to divide two quantities. Returns false when no semantic rule or fallback applies.</summary>
    public static bool TryDivide(Quantity<T> left, Quantity<T> right, out Quantity<T> result)
    {
        result = null;
        if (left == null || right == null)
        {
            result = left ?? right;
            return true;
        }
        try
        {
            result = left / right; // reuse operator semantics
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>Attempts to add two quantities respecting semantic rules.</summary>
    public static bool TryAdd(Quantity<T> left, Quantity<T> right, out Quantity<T> result)
    {
        result = null;
        if (left == null || right == null)
        {
            result = left ?? right;
            return true;
        }
        try
        {
            result = left + right;
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>Attempts to subtract two quantities respecting semantic rules.</summary>
    public static bool TrySubtract(Quantity<T> left, Quantity<T> right, out Quantity<T> result)
    {
        result = null;
        if (left == null || right == null)
        {
            result = left ?? right;
            return true;
        }
        try
        {
            result = left - right;
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Adds two quantities enforcing the same semantic rules as the '+' operator. Mixed point±delta handled; cross-kind without a permitted
    /// rule throws. This method previously allowed unsafe cross-kind addition when <c>requireSameKind</c> was false; that behavior is removed.
    /// </summary>
    public static Quantity<T> Add(Quantity<T> a, Quantity<T> b, bool requireSameKind = false)
    {
        if (a == null || b == null)
        {
            return a ?? b;
        }

        // Same kind
        if (ReferenceEquals(a.Kind, b.Kind))
        {
            if (!a.Kind.AllowDirectAddition)
            {
                throw new InvalidOperationException($"Addition of quantities of kind '{a.Kind.Name}' is not semantically supported.");
            }

            var bAligned = b.Measurement.ConvertTo(a.Measurement.Unit);
            return new Quantity<T>(a.Measurement + bAligned, a.Kind, strictDimensionCheck: false);
        }

        // Point ± Delta pattern (a is point)
        if (ReferenceEquals(a.Kind.DifferenceResultKind, b.Kind))
        {
            var basePoint = a.Measurement.ConvertTo(a.Kind.CanonicalUnit);
            var delta = b.Measurement.ConvertTo(a.Kind.DifferenceResultKind.CanonicalUnit);
            var sum = basePoint + delta;
            var back = sum.ConvertTo(a.Measurement.Unit);
            return new Quantity<T>(back, a.Kind, strictDimensionCheck: true);
        }

        // Delta ± Point pattern (b is point)
        if (ReferenceEquals(b.Kind.DifferenceResultKind, a.Kind))
        {
            var basePoint = b.Measurement.ConvertTo(b.Kind.CanonicalUnit);
            var delta = a.Measurement.ConvertTo(b.Kind.DifferenceResultKind.CanonicalUnit);
            var sum = basePoint + delta;
            var back = sum.ConvertTo(b.Measurement.Unit);
            return new Quantity<T>(back, b.Kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"Cannot add {a.Kind.Name} and {b.Kind.Name}.");
    }

    /// <summary>
    /// Subtracts two quantities enforcing the same semantic rules as the '-' operator. Produces delta kinds for point-point subtraction.
    /// </summary>
    public static Quantity<T> Sub(Quantity<T> a, Quantity<T> b, bool requireSameKind = false)
    {
        if (a == null || b == null)
        {
            return a ?? b;
        }

        if (ReferenceEquals(a.Kind, b.Kind))
        {
            if (a.Kind.AllowDirectSubtraction)
            {
                var bAligned = b.Measurement.ConvertTo(a.Measurement.Unit);
                return new Quantity<T>(a.Measurement - bAligned, a.Kind, strictDimensionCheck: false);
            }

            if (a.Kind.DifferenceResultKind != null)
            {
                var aBase = a.Measurement.ConvertTo(a.Kind.DifferenceResultKind.CanonicalUnit);
                var bBase = b.Measurement.ConvertTo(a.Kind.DifferenceResultKind.CanonicalUnit);
                var diff = aBase - bBase;
                return new Quantity<T>(diff, a.Kind.DifferenceResultKind, strictDimensionCheck: true);
            }

            throw new InvalidOperationException($"Direct subtraction producing '{a.Kind.Name}' is not semantically supported.");
        }

        // Point - Delta -> Point
        if (ReferenceEquals(a.Kind.DifferenceResultKind, b.Kind))
        {
            var basePoint = a.Measurement.ConvertTo(a.Kind.CanonicalUnit);
            var delta = b.Measurement.ConvertTo(a.Kind.DifferenceResultKind.CanonicalUnit);
            var res = basePoint - delta;
            var back = res.ConvertTo(a.Measurement.Unit);
            return new Quantity<T>(back, a.Kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"Cannot subtract {b.Kind.Name} from {a.Kind.Name}.");
    }

    /// <summary>
    /// Adds two quantities enforcing identical semantic kinds. This is equivalent to <see cref="Add(Quantity{T}, Quantity{T}, bool)"/>
    /// with <c>requireSameKind</c> = true.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when kinds differ even if dimensions match.</exception>
    public static Quantity<T> operator +(Quantity<T> left, Quantity<T> right)
    {
        if (left == null)
        {
            return right;
        }

        if (right == null)
        {
            return left;
        }

        // Same kind path
        if (ReferenceEquals(left.Kind, right.Kind))
        {
            if (!left.Kind.AllowDirectAddition)
            {
                throw new InvalidOperationException($"Addition of quantities of kind '{left.Kind.Name}' is not semantically supported.");
            }

            var rAligned = right.Measurement.ConvertTo(left.Measurement.Unit);
            return new Quantity<T>(left.Measurement + rAligned, left.Kind, strictDimensionCheck: false);
        }

        // Mixed: Point ± Vector -> Point (support both orders for +)
        if (ReferenceEquals(left.Kind.DifferenceResultKind, right.Kind))
        {
            // left = point, right = vector
            var basePoint = left.Measurement.ConvertTo(left.Kind.CanonicalUnit);
            var delta = right.Measurement.ConvertTo(left.Kind.DifferenceResultKind.CanonicalUnit);
            var sum = basePoint + delta;
            var back = sum.ConvertTo(left.Measurement.Unit);
            return new Quantity<T>(back, left.Kind, strictDimensionCheck: true);
        }

        if (ReferenceEquals(right.Kind.DifferenceResultKind, left.Kind))
        {
            // right = point, left = vector
            var basePoint = right.Measurement.ConvertTo(right.Kind.CanonicalUnit);
            var delta = left.Measurement.ConvertTo(right.Kind.DifferenceResultKind.CanonicalUnit);
            var sum = basePoint + delta;
            var back = sum.ConvertTo(right.Measurement.Unit);
            return new Quantity<T>(back, right.Kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"Cannot add {left.Kind.Name} and {right.Kind.Name}.");
    }

    /// <summary>
    /// Subtracts two quantities enforcing identical semantic kinds. This is equivalent to <see cref="Sub(Quantity{T}, Quantity{T}, bool)"/>
    /// with <c>requireSameKind</c> = true.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when kinds differ even if dimensions match.</exception>
    public static Quantity<T> operator -(Quantity<T> left, Quantity<T> right)
    {
        if (left == null)
        {
            return right == null ? null : new Quantity<T>(right.Measurement, right.Kind);
        }

        if (right == null)
        {
            return left;
        }

        if (ReferenceEquals(left.Kind, right.Kind))
        {
            if (left.Kind.AllowDirectSubtraction)
            {
                var rAligned = right.Measurement.ConvertTo(left.Measurement.Unit);
                return new Quantity<T>(left.Measurement - rAligned, left.Kind, strictDimensionCheck: false);
            }

            if (left.Kind.DifferenceResultKind != null)
            {
                // Point - Point -> Vector
                var aBase = left.Measurement.ConvertTo(left.Kind.DifferenceResultKind.CanonicalUnit);
                var bBase = right.Measurement.ConvertTo(left.Kind.DifferenceResultKind.CanonicalUnit);
                var diff = aBase - bBase;
                return new Quantity<T>(diff, left.Kind.DifferenceResultKind, strictDimensionCheck: true);
            }

            throw new InvalidOperationException($"Direct subtraction producing '{left.Kind.Name}' is not semantically supported.");
        }

        // Mixed: Point - Vector -> Point (vector kind must equal DifferenceResultKind of point)
        if (ReferenceEquals(left.Kind.DifferenceResultKind, right.Kind))
        {
            var basePoint = left.Measurement.ConvertTo(left.Kind.CanonicalUnit);
            var delta = right.Measurement.ConvertTo(left.Kind.DifferenceResultKind.CanonicalUnit);
            var result = basePoint - delta; // subtract delta
            var back = result.ConvertTo(left.Measurement.Unit);

            return new Quantity<T>(back, left.Kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"Cannot subtract {right.Kind.Name} from {left.Kind.Name}.");
    }

    private static bool IsDimensionless(Unit u) => ReferenceEquals(u, Unit.None) || ReferenceEquals(u.Dimension, Veggerby.Units.Dimensions.Dimension.None);

    /// <summary>Scale by a dimensionless measurement (vector semantics only; point-like kinds forbidden).</summary>
    /// <summary>
    /// Multiplies a quantity by a dimensionless scalar measurement preserving semantic kind for vector-like kinds.
    /// Throws when the scalar has a dimension or when attempting to scale a point-like kind (e.g. absolute temperature).
    /// </summary>
    public static Quantity<T> operator *(Quantity<T> quantity, Measurement<T> scalar)
    {
        if (!IsDimensionless(scalar.Unit))
        {
            throw new InvalidOperationException("Scaling requires a dimensionless scalar.");
        }

        if (quantity.Kind.DifferenceResultKind != null && !quantity.Kind.AllowDirectAddition && !quantity.Kind.AllowDirectSubtraction)
        {
            throw new InvalidOperationException($"Cannot scale point-like kind {quantity.Kind.Name}.");
        }

        return new Quantity<T>(quantity.Measurement * scalar, quantity.Kind, strictDimensionCheck: false);
    }

    /// <summary>
    /// Multiplies a dimensionless scalar measurement by a quantity (commutative convenience). See
    /// <see cref="operator *(Quantity{T}, Measurement{T})"/> for rules.
    /// </summary>
    public static Quantity<T> operator *(Measurement<T> scalar, Quantity<T> quantity) => quantity * scalar;

    /// <summary>Divide by a dimensionless scalar (vector semantics only; point-like kinds forbidden).</summary>
    /// <summary>
    /// Divides a quantity by a dimensionless scalar measurement preserving semantic kind for vector-like kinds.
    /// Throws when the scalar has a dimension or when attempting to scale a point-like kind.
    /// </summary>
    public static Quantity<T> operator /(Quantity<T> quantity, Measurement<T> scalar)
    {
        if (!IsDimensionless(scalar.Unit))
        {
            throw new InvalidOperationException("Scaling requires a dimensionless scalar.");
        }

        if (quantity.Kind.DifferenceResultKind != null && !quantity.Kind.AllowDirectAddition && !quantity.Kind.AllowDirectSubtraction)
        {
            throw new InvalidOperationException($"Cannot scale point-like kind {quantity.Kind.Name}.");
        }

        return new Quantity<T>(quantity.Measurement / scalar, quantity.Kind, strictDimensionCheck: false);
    }

    /// <summary>
    /// Multiplies two quantities attempting semantic inference. If an inference rule exists the result kind is applied; otherwise
    /// if one side is dimensionless (unitless) the other side's kind is preserved. When neither condition holds an <see cref="InvalidOperationException"/> is thrown.
    /// Absolute (point-like) kinds may participate only via inference (e.g. T_abs * Entropy -> Energy) and never preserve point semantics by dimensionless fallback.
    /// </summary>
    public static Quantity<T> operator *(Quantity<T> left, Quantity<T> right)
    {
        if (left == null || right == null)
        {
            return left ?? right;
        }

        var inferred = QuantityKindInferenceRegistry.ResolveOrNull(left.Kind, QuantityKindBinaryOperator.Multiply, right.Kind);

        if (inferred != null)
        {
            // Numeric multiplication (unit algebra already handled by Measurement operators)
            var product = left.Measurement * right.Measurement;
            return new Quantity<T>(product, inferred, strictDimensionCheck: true);
        }

        // Dimensionless fallback: preserve other kind
        bool leftDimless = ReferenceEquals(left.Measurement.Unit, Unit.None) || ReferenceEquals(left.Measurement.Unit.Dimension, Veggerby.Units.Dimensions.Dimension.None);
        bool rightDimless = ReferenceEquals(right.Measurement.Unit, Unit.None) || ReferenceEquals(right.Measurement.Unit.Dimension, Veggerby.Units.Dimensions.Dimension.None);

        if (leftDimless ^ rightDimless)
        {
            var product = left.Measurement * right.Measurement;
            var kind = leftDimless ? right.Kind : left.Kind;

            // Disallow preserving a point-like absolute via scalar multiply
            if (kind.DifferenceResultKind != null && !kind.AllowDirectAddition && !kind.AllowDirectSubtraction)
            {
                throw new InvalidOperationException($"Cannot scale point-like kind {kind.Name} in multiplication without inference.");
            }

            // Angle guard: Angle is not a passive scalar; require explicit inference (Torque*Angle -> Energy, etc.)
            if (ReferenceEquals(left.Kind, QuantityKinds.Angle) || ReferenceEquals(right.Kind, QuantityKinds.Angle))
            {
                throw new InvalidOperationException("Angle requires explicit inference; it does not act as a generic scalar.");
            }

            return new Quantity<T>(product, kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"No semantic inference rule for {left.Kind.Name} * {right.Kind.Name}.");
    }

    /// <summary>
    /// Divides two quantities with semantic inference (e.g. Energy / TemperatureAbsolute -> Entropy). If no rule exists and the divisor is dimensionless
    /// the left kind is preserved (unless point-like). Otherwise throws.
    /// </summary>
    public static Quantity<T> operator /(Quantity<T> left, Quantity<T> right)
    {
        if (left == null || right == null)
        {
            return left ?? right;
        }

        var inferred = QuantityKindInferenceRegistry.ResolveOrNull(left.Kind, QuantityKindBinaryOperator.Divide, right.Kind);
        if (inferred != null)
        {
            var quotient = left.Measurement / right.Measurement;
            return new Quantity<T>(quotient, inferred, strictDimensionCheck: true);
        }

        bool rightDimless = ReferenceEquals(right.Measurement.Unit, Unit.None) || ReferenceEquals(right.Measurement.Unit.Dimension, Veggerby.Units.Dimensions.Dimension.None);

        if (rightDimless)
        {
            if (left.Kind.DifferenceResultKind != null && !left.Kind.AllowDirectAddition && !left.Kind.AllowDirectSubtraction)
            {
                throw new InvalidOperationException($"Cannot scale point-like kind {left.Kind.Name} in division without inference.");
            }

            if (ReferenceEquals(right.Kind, QuantityKinds.Angle))
            {
                throw new InvalidOperationException("Angle requires explicit inference; it does not act as a generic scalar.");
            }

            var quotient = left.Measurement / right.Measurement;
            return new Quantity<T>(quotient, left.Kind, strictDimensionCheck: true);
        }

        throw new InvalidOperationException($"No semantic inference rule for {left.Kind.Name} / {right.Kind.Name}.");
    }
}