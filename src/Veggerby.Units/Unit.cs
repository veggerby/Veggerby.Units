using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;
using Veggerby.Units.Visitors;

namespace Veggerby.Units;

/// <summary>
/// Abstract base for all unit varieties (basic, derived, composite, prefixed, scaled).
/// Provides algebraic composition through operator overloads while enforcing dimensional correctness
/// for additive operators (+ and -). Multiplicative operators (*, /, ^) delegate to <see cref="OperationUtility"/>
/// to perform:
/// <list type="bullet">
/// <item><description>Flattening of nested product / division graphs</description></item>
/// <item><description>Cancellation of reciprocal factors</description></item>
/// <item><description>Aggregation of identical factors into powers</description></item>
/// <item><description>Expansion / distribution of powers across composite operands</description></item>
/// </list>
/// This guarantees that structurally equivalent expressions reduce to the same canonical form which underpins
/// equality and hash code semantics.
/// </summary>
public abstract class Unit : IOperand
{
    /// <summary>
    /// Dimensionless identity unit (multiplicative identity). Acts as neutral element for * and / and as the
    /// result of raising any unit to the power 0. Represented internally by <see cref="NullUnit"/>.
    /// </summary>
    public static Unit None = new NullUnit();

    /// <summary>Singleton instance of the International (SI) unit system.</summary>
    public static InternationalUnitSystem SI = new();
    /// <summary>Singleton instance of the (subset) Imperial unit system.</summary>
    public static ImperialUnitSystem Imperial = new();

    /// <summary>Short symbolic representation (e.g. m, kg, N·m). Must be stable and pure.</summary>
    public abstract string Symbol { get; }
    /// <summary>Human readable name used for diagnostic and descriptive output.</summary>
    public abstract string Name { get; }
    /// <summary>Owning <see cref="UnitSystem"/> or <see cref="UnitSystem.None"/> when the unit is synthetic.</summary>
    public abstract UnitSystem System { get; }
    /// <summary>Physical <see cref="Dimensions.Dimension"/> associated with this unit (never null).</summary>
    public abstract Dimension Dimension { get; }

    /// <summary>
    /// Creates a product of the supplied operands without performing reduction.
    /// Prefer using the * operator which applies canonicalisation first.
    /// </summary>
    public static Unit Multiply(params Unit[] operands)
    {
        return new ProductUnit(operands);
    }

    /// <summary>
    /// Creates a division unit (dividend/divisor) without performing cancellation.
    /// Prefer using the / operator which applies rearrangement and reduction.
    /// </summary>
    public static Unit Divide(Unit dividend, Unit divisor)
    {
        return new DivisionUnit(dividend, divisor);
    }

    /// <summary>
    /// Creates a power unit (base^exponent) without expansion or negative exponent handling.
    /// Prefer using the ^ operator which converts negative exponents into reciprocal structures and expands
    /// composite operands when beneficial.
    /// </summary>
    public static Unit Power(Unit @base, int exponent)
    {
        return new PowerUnit(@base, exponent);
    }

    /// <summary>
    /// Adds two units ensuring structural equality (dimension + structure). Addition never performs implicit
    /// conversion; operands must be identical by <see cref="operator ==(Unit, Unit)"/>.
    /// Returns the left operand to avoid allocation.
    /// </summary>
    public static Unit operator +(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

    /// <summary>
    /// Subtracts two units ensuring structural equality. Subtraction never performs implicit
    /// conversion; operands must be identical. Returns the left operand to avoid allocation.
    /// </summary>
    public static Unit operator -(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

    /// <summary>
    /// Multiplies two units applying canonical reduction (factor cancellation, power aggregation, flattening)
    /// producing a minimal structural representation.
    /// </summary>
    public static Unit operator *(Unit u1, Unit u2)
    {
        if (u1 == None) // and if u2 == None, return u2 (=None)
        {
            return u2;
        }

        if (u2 == None)
        {
            return u1;
        }

        // Affine guard: disallow composite multiplicative algebra with affine units (ill-defined with offsets)
        if (IsAffine(u1) || IsAffine(u2))
        {
            throw new UnitException(u1, u2);
        }

        // where to put OperationUtility.ReduceMultiplication
        return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, u1, u2) ??
            OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x ^ y, u1, u2) ??
            Multiply(u1, u2);
    }

    /// <summary>
    /// Applies an integer scaling factor via prefix lookup.
    /// Throws <see cref="PrefixException"/> if no matching decimal prefix exists.
    /// </summary>
    public static Unit operator *(int factor, Unit unit)
    {
        return ((double)factor) * unit;
    }

    /// <summary>
    /// Applies a floating point scaling factor via prefix lookup.
    /// Throws <see cref="PrefixException"/> if the factor does not match a known prefix exactly.
    /// </summary>
    public static Unit operator *(double factor, Unit unit)
    {
        if (IsAffine(unit))
        {
            throw new UnitException(unit, unit); // reuse UnitException; dimensions identical but operation invalid
        }
        Prefix pre = factor;

        if (pre == null)
        {
            throw new PrefixException(factor);
        }

        return new PrefixedUnit(
            pre,
            unit);
    }

    /// <summary>Applies an explicit prefix instance to a unit (no validation of prefix uniqueness performed).</summary>
    public static Unit operator *(Prefix pre, Unit unit)
    {
        if (IsAffine(unit))
        {
            throw new UnitException(unit, unit);
        }
        return new PrefixedUnit(pre, unit);
    }

    /// <summary>Creates a reciprocal unit (1/divisor) with reduction and cancellation applied.</summary>
    public static Unit operator /(int dividend, Unit divisor)
    {
        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, None, divisor) ??
            Divide(None, divisor);
    }

    /// <summary>Divides two units applying rearrangement, cancellation and power collapsing.</summary>
    public static Unit operator /(Unit dividend, Unit divisor)
    {
        if (divisor == None)
        {
            return dividend;
        }

        if (IsAffine(dividend) || IsAffine(divisor))
        {
            throw new UnitException(dividend, divisor);
        }

        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
            OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
            Divide(dividend, divisor);
    }

    /// <summary>
    /// Raises a unit to an integer exponent. Negative exponents yield the reciprocal of the positive power.
    /// Exponentiation distributes across composite products and divisions where that produces a simpler
    /// canonical structure.
    /// </summary>
    public static Unit operator ^(Unit @base, int exponent)
    {
        if (exponent < 0)
        {
            return 1 / (@base ^ (-exponent));
        }

        if (exponent == 0)
        {
            return None;
        }

        if (exponent == 1)
        {
            return @base;
        }

        if (IsAffine(@base))
        {
            throw new UnitException(@base, @base);
        }

        return OperationUtility.ExpandPower(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, (x, y) => x ^ y, @base, exponent) ??
               Power(@base, exponent);
    }

    /// <summary>Structural equality (reduction + commutative normalisation for products).</summary>
    public static bool operator ==(Unit u1, Unit u2)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(u1, u2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)u1 == null) || ((object)u2 == null))
        {
            return false;
        }

        return u1.Equals(u2);
    }

    /// <summary>Negation of <see cref="operator ==(Unit, Unit)"/>.</summary>
    public static bool operator !=(Unit u1, Unit u2)
    {
        // If both are null, or both are same instance, they are equal => inequality false
        if (ReferenceEquals(u1, u2))
        {
            return false;
        }

        // If one is null (but not both) => not equal => inequality true
        if ((object)u1 == null || (object)u2 == null)
        {
            return true;
        }

        return !u1.Equals(u2);
    }

    /// <summary>Implicitly returns the unit symbol.</summary>
    public static implicit operator string(Unit u)
    {
        return u.Symbol;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Symbol.GetHashCode();
    /// <inheritdoc />
    public override string ToString() => Symbol;

    /// <summary>Returns multiplicative scale factor relative to SI base representation (override in composites).</summary>
    internal virtual double GetScaleFactor() => 1d;
    /// <summary>
    /// Converts a numeric value in this unit to its base (affine-aware) SI-space value. Default assumes purely
    /// multiplicative scaling (no offset).
    /// </summary>
    internal virtual double ToBase(double value) => value * GetScaleFactor();
    /// <summary>
    /// Converts a numeric value expressed in SI base space back to this unit. Default assumes purely
    /// multiplicative scaling.
    /// </summary>
    internal virtual double FromBase(double baseValue) => baseValue / GetScaleFactor();

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is Unit)
        {
            return OperationUtility.Equals(this, (obj as Unit));
        }

        return false;
    }

    internal abstract T Accept<T>(Visitor<T> visitor);

    private static bool IsAffine(Unit unit) => unit is AffineUnit;

    // (legacy duplicate GetScaleFactor removed – single definition above)
}