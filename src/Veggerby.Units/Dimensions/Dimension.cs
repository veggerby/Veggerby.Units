using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

/// <summary>
/// Base abstraction for physical dimensions (L, T, etc.) supporting algebraic composition mirroring unit
/// operations. Structural equality is reduction based to allow commutative product comparisons.
/// </summary>
public abstract class Dimension : IOperand
{
    /// <summary>
    /// Dimensionless property, eg. a constant (pi, e, etc.)
    /// </summary>
    public static readonly Dimension None = new NullDimension();

    /// <summary>Length (L)</summary>
    public static readonly Dimension Length = new BasicDimension("L", "length");
    /// <summary>Mass (M)</summary>
    public static readonly Dimension Mass = new BasicDimension("M", "mass");
    /// <summary>Time (T)</summary>
    public static readonly Dimension Time = new BasicDimension("T", "time");
    /// <summary>Electric current (I)</summary>
    public static readonly Dimension ElectricCurrent = new BasicDimension("I", "electric current");
    /// <summary>Thermodynamic temperature (Θ)</summary>
    public static readonly Dimension ThermodynamicTemperature = new BasicDimension("Θ", "thermodynamic temperature");
    /// <summary>Luminous intensity (J)</summary>
    public static readonly Dimension LuminousIntensity = new BasicDimension("J", "luminous intensity");
    /// <summary>Amount of substance (N)</summary>
    public static readonly Dimension AmountOfSubstance = new BasicDimension("N", "amount of substance");

    internal Dimension()
    {
    }

    /// <summary>Short symbolic representation (e.g. L, T).</summary>
    public abstract string Symbol { get; }
    /// <summary>Human readable name.</summary>
    public abstract string Name { get; }

    /// <summary>Creates a product dimension without applying reduction.</summary>
    public static Dimension Multiply(params Dimension[] operands)
    {
        return new ProductDimension(operands);
    }

    /// <summary>Creates a division dimension without reduction.</summary>
    public static Dimension Divide(Dimension dividend, Dimension divisor)
    {
        return new DivisionDimension(dividend, divisor);
    }

    /// <summary>Creates a power dimension without expansion.</summary>
    public static Dimension Power(Dimension @base, int exponent)
    {
        return new PowerDimension(@base, exponent);
    }

    /// <summary>Addition requiring identical dimensions (identity returned).</summary>
    public static Dimension operator +(Dimension d1, Dimension d2)
    {
        if (d1 != d2)
        {
            throw new DimensionException(d1, d2);
        }

        return d1;
    }

    /// <summary>Subtraction requiring identical dimensions (identity returned).</summary>
    public static Dimension operator -(Dimension d1, Dimension d2)
    {
        if (d1 != d2)
        {
            throw new DimensionException(d1, d2);
        }

        return d1;
    }

    /// <summary>Multiplicative composition with reduction/cancellation when possible.</summary>
    public static Dimension operator *(Dimension d1, Dimension d2)
    {
        if (d1 == Dimension.None) // and if d2 == None, return d2 (=None)
        {
            return d2;
        }

        if (d2 == Dimension.None)
        {
            return d1;
        }

        // where to put OperationUtility.ReduceMultiplication
        return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, Dimension.None), (x, y) => x / y, d1, d2) ??
            OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, Dimension.None), (x, y) => x ^ y, d1, d2) ??
            Multiply(d1, d2);
    }

    /// <summary>Reciprocal (1/divisor).</summary>
    public static Dimension operator /(int dividend, Dimension divisor)
    {
        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, Dimension.None, divisor) ??
            Divide(Dimension.None, divisor);
    }

    /// <summary>Division with reduction when possible.</summary>
    public static Dimension operator /(Dimension dividend, Dimension divisor)
    {
        if (divisor == Dimension.None)
        {
            return dividend;
        }

        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
            OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, Dimension.None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
            Divide(dividend, divisor);
    }

    /// <summary>Exponentiation (negative exponents produce reciprocal).</summary>
    public static Dimension operator ^(Dimension @base, int exponent)
    {
        if (exponent < 0)
        {
            return 1 / (@base ^ (-exponent));
        }

        if (exponent == 0)
        {
            return Dimension.None;
        }

        if (exponent == 1)
        {
            return @base;
        }

        return OperationUtility.ExpandPower(x => x.Multiply((a, b) => a * b, Dimension.None), (x, y) => x / y, (x, y) => x ^ y, @base, exponent) ??
               Power(@base, exponent);
    }

    /// <summary>Structural equality (reduction aware).</summary>
    public static bool operator ==(Dimension d1, Dimension d2)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(d1, d2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)d1 == null) || ((object)d2 == null))
        {
            return false;
        }

        return d1.Equals(d2);
    }

    /// <summary>Inequality inverse of structural equality.</summary>
    public static bool operator !=(Dimension d1, Dimension d2)
    {
        return !d1.Equals(d2);
    }

    /// <summary>Implicitly returns the symbol.</summary>
    public static implicit operator string(Dimension d)
    {
        return d.Symbol;
    }

    /// <inheritdoc />
    public override string ToString() => Symbol;

    /// <inheritdoc />
    public override int GetHashCode() => Symbol.GetHashCode();

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is Dimension)
        {
            return OperationUtility.Equals(this, obj as Dimension);
        }

        return false;
    }
}