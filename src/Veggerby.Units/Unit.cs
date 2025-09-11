using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;
using Veggerby.Units.Visitors;

namespace Veggerby.Units;

public abstract class Unit : IOperand
{
    public static Unit None = new NullUnit();

    public static InternationalUnitSystem SI = new();
    public static ImperialUnitSystem Imperial = new();

    public abstract string Symbol { get; }
    public abstract string Name { get; }
    public abstract UnitSystem System { get; }
    public abstract Dimension Dimension { get; }

    public static Unit Multiply(params Unit[] operands)
    {
        return new ProductUnit(operands);
    }

    public static Unit Divide(Unit dividend, Unit divisor)
    {
        return new DivisionUnit(dividend, divisor);
    }

    public static Unit Power(Unit @base, int exponent)
    {
        return new PowerUnit(@base, exponent);
    }

    public static Unit operator +(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

    public static Unit operator -(Unit u1, Unit u2)
    {
        if (u1 != u2)
        {
            throw new UnitException(u1, u2);
        }

        return u1;
    }

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

    public static Unit operator *(int factor, Unit unit)
    {
        return ((double)factor) * unit;
    }

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

    public static Unit operator *(Prefix pre, Unit unit)
    {
        return new PrefixedUnit(pre, unit);
    }

    public static Unit operator /(int dividend, Unit divisor)
    {
        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, None, divisor) ??
            Divide(None, divisor);
    }

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

    public static implicit operator string(Unit u)
    {
        return u.Symbol;
    }

    public override int GetHashCode() => Symbol.GetHashCode();
    public override string ToString() => Symbol;

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