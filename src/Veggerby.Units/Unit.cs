using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;
using Veggerby.Units.Visitors;

namespace Veggerby.Units;

/// <summary>
/// Abstract base for all unit types (basic, derived, composite, prefixed, scaled). Supports algebraic composition via
/// operator overloading while enforcing dimensional correctness for additive operations. Multiplicative operators use
/// reduction utilities to minimise intermediate allocations and normalise structural equality.
/// </summary>
public abstract class Unit : IOperand
{
    /// <summary>Dimensionless identity unit (neutral element for multiplication/division).</summary>
    public static Unit None = new NullUnit();

    /// <summary>Singleton instance of the SI unit system.</summary>
    public static InternationalUnitSystem SI = new();
    /// <summary>Singleton subset of the Imperial unit system.</summary>
    public static ImperialUnitSystem Imperial = new();

    /// <summary>Short symbolic representation (e.g. m, kg, N·m).</summary>
    public abstract string Symbol { get; }
    /// <summary>Human readable name.</summary>
    public abstract string Name { get; }
    /// <summary>Owning <see cref="UnitSystem"/> or <see cref="UnitSystem.None"/> when not applicable.</summary>
    public abstract UnitSystem System { get; }
    /// <summary>Physical <see cref="Dimensions.Dimension"/> associated with this unit.</summary>
    public abstract Dimension Dimension { get; }

    /// <summary>Creates a product of the supplied operands without reduction (internal helpers prefer reduction first).</summary>
    public static Unit Multiply(params Unit[] operands)
    {
        return new ProductUnit(operands);
    }

    /// <summary>Creates a division unit (dividend/divisor) without performing cancellation.</summary>
    public static Unit Divide(Unit dividend, Unit divisor)
    {
        return new DivisionUnit(dividend, divisor);
    }

    /// <summary>Creates a power unit (@base^exponent) without expansion.</summary>
    public static Unit Power(Unit @base, int exponent)
    {
        return new PowerUnit(@base, exponent);
    }

    /// <summary>Adds two units ensuring dimensional equality; returns left operand when compatible.</summary>
    public static Unit operator +(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

    /// <summary>Subtracts two units ensuring dimensional equality; returns left operand when compatible.</summary>
    public static Unit operator -(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

    /// <summary>Multiplies two units with reduction (cancellation / power aggregation) applied when possible.</summary>
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

        // where to put OperationUtility.ReduceMultiplication
        return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, u1, u2) ??
            OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x ^ y, u1, u2) ??
            Multiply(u1, u2);
    }

    /// <summary>Applies an integer scaling factor via prefix lookup.</summary>
    public static Unit operator *(int factor, Unit unit)
    {
        return ((double)factor) * unit;
    }

    /// <summary>Applies a floating point scaling factor via prefix lookup (throws when no exact prefix exists).</summary>
    public static Unit operator *(double factor, Unit unit)
    {
        Prefix pre = factor;

        if (pre == null)
        {
            throw new PrefixException(factor);
        }

        return new PrefixedUnit(
            pre,
            unit);
    }

    /// <summary>Applies an explicit prefix instance to a unit.</summary>
    public static Unit operator *(Prefix pre, Unit unit)
    {
        return new PrefixedUnit(pre, unit);
    }

    /// <summary>Creates a reciprocal unit (1/divisor) with reduction.</summary>
    public static Unit operator /(int dividend, Unit divisor)
    {
        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, None, divisor) ??
            Divide(None, divisor);
    }

    /// <summary>Divides two units applying reduction (cancellation / power collapsing) when possible.</summary>
    public static Unit operator /(Unit dividend, Unit divisor)
    {
        if (divisor == None)
        {
            return dividend;
        }

        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
            OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
            Divide(dividend, divisor);
    }

    /// <summary>Raises a unit to an integer exponent. Negative exponents yield the reciprocal of the positive power.</summary>
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

        return OperationUtility.ExpandPower(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, (x, y) => x ^ y, @base, exponent) ??
               Power(@base, exponent);
    }

    /// <summary>Structural equality (via reduction) supporting commutative product comparison.</summary>
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

    /// <summary>Negation of structural equality.</summary>
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

    /// <summary>
    /// Scale factor relative to underlying SI base units (e.g. km => 1000, ft => 0.3048, composite units multiply/divide factors).
    /// NullUnit returns 1.
    /// </summary>
    internal virtual double GetScaleFactor() => 1d;
}