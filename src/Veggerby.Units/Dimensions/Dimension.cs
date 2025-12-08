using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

/// <summary>
/// Base abstraction for physical dimensions (L, T, etc.) supporting algebraic composition mirroring unit
/// operations. Structural equality is reduction based so dimension graphs that are algebraically equivalent
/// (e.g. L * T and T * L) compare equal. Dimensions intentionally mirror unit operator semantics but omit
/// scaling concerns (dimensions are qualitative only).
/// </summary>
/// <remarks>
/// Dimensional vectors align with the QUDT ontology's dimensional analysis framework based on
/// the seven SI base dimensions (L, M, T, I, Θ, N, J). QUDT represents dimensions as exponent
/// vectors enabling dimensional algebra validation.
/// <para>
/// QUDT uses symbols: L (length), M (mass), T (time), I (electric current), Θ (temperature),
/// N (amount of substance), J (luminous intensity).
/// </para>
/// <para>
/// Example QUDT dimensional vector for Force: L¹ M¹ T⁻²
/// </para>
/// <para>
/// See <c>docs/qudt-alignment.md</c> for dimensional exponent verification.
/// </para>
/// </remarks>
public abstract class Dimension : IOperand
{
    /// <summary>Dimensionless identity (used for constants or fully cancelled expressions).</summary>
    public static readonly Dimension None = new NullDimension();

    /// <summary>Length (L)</summary>
    /// <remarks>QUDT dimension symbol: <c>L</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L1-I0-M0-H0-T0-D0)</remarks>
    public static readonly Dimension Length = new BasicDimension("L", "length");
    /// <summary>Mass (M)</summary>
    /// <remarks>QUDT dimension symbol: <c>M</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L0-I0-M1-H0-T0-D0)</remarks>
    public static readonly Dimension Mass = new BasicDimension("M", "mass");
    /// <summary>Time (T)</summary>
    /// <remarks>QUDT dimension symbol: <c>T</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L0-I0-M0-H0-T1-D0)</remarks>
    public static readonly Dimension Time = new BasicDimension("T", "time");
    /// <summary>Electric current (I)</summary>
    /// <remarks>QUDT dimension symbol: <c>I</c> (http://qudt.org/vocab/dimensionvector/A0-E1-L0-I0-M0-H0-T0-D0)</remarks>
    public static readonly Dimension ElectricCurrent = new BasicDimension("I", "electric current");
    /// <summary>Thermodynamic temperature (Θ)</summary>
    /// <remarks>QUDT dimension symbol: <c>Θ</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L0-I0-M0-H1-T0-D0)</remarks>
    public static readonly Dimension ThermodynamicTemperature = new BasicDimension("Θ", "thermodynamic temperature");
    /// <summary>Luminous intensity (J)</summary>
    /// <remarks>QUDT dimension symbol: <c>J</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L0-I1-M0-H0-T0-D0)</remarks>
    public static readonly Dimension LuminousIntensity = new BasicDimension("J", "luminous intensity");
    /// <summary>Amount of substance (N)</summary>
    /// <remarks>QUDT dimension symbol: <c>N</c> (http://qudt.org/vocab/dimensionvector/A0-E0-L0-I0-M0-H0-T0-D1)</remarks>
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

    /// <summary>
    /// Addition requiring identical dimensions. No coercion or inference is attempted. Returns the left
    /// operand to avoid allocation.
    /// </summary>
    public static Dimension operator +(Dimension d1, Dimension d2)
    {
        if (d1 != d2)
        {
            throw new DimensionException(d1, d2);
        }

        return d1;
    }

    /// <summary>
    /// Subtraction requiring identical dimensions. Returns the left operand. Throws
    /// <see cref="DimensionException"/> on mismatch.
    /// </summary>
    public static Dimension operator -(Dimension d1, Dimension d2)
    {
        if (d1 != d2)
        {
            throw new DimensionException(d1, d2);
        }

        return d1;
    }

    /// <summary>Multiplicative composition with reduction / cancellation of reciprocal factors.</summary>
    public static Dimension operator *(Dimension d1, Dimension d2)
    {
        if (d1 == None) // and if d2 == None, return d2 (=None)
        {
            return d2;
        }

        if (d2 == None)
        {
            return d1;
        }

        // where to put OperationUtility.ReduceMultiplication
        return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, d1, d2) ??
            OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, None), (x, y) => x ^ y, d1, d2) ??
            Multiply(d1, d2);
    }

    /// <summary>Reciprocal (1/divisor) with reduction applied.</summary>
    public static Dimension operator /(int dividend, Dimension divisor)
    {
        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, None, divisor) ??
            Divide(None, divisor);
    }

    /// <summary>Division with rearrangement and reduction (cancellation + power aggregation).</summary>
    public static Dimension operator /(Dimension dividend, Dimension divisor)
    {
        if (divisor == None)
        {
            return dividend;
        }

        return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
            OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
            Divide(dividend, divisor);
    }

    /// <summary>Exponentiation (negative exponents produce reciprocal; expansion applied for composites).</summary>
    public static Dimension operator ^(Dimension @base, int exponent)
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

    /// <summary>Structural equality (reduction aware + commutative for products).</summary>
    public static bool operator ==(Dimension d1, Dimension d2)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(d1, d2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)d1 is null) || ((object)d2 is null))
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