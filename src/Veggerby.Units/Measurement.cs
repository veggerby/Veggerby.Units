using System;

using Veggerby.Units.Calculations;
using Veggerby.Units.Conversion;

namespace Veggerby.Units;

/// <summary>
/// Represents a typed numeric value paired with a physical unit enabling safe arithmetic and comparison. Unit
/// alignment occurs automatically for relational comparisons via conversion to the left operand's unit.
/// </summary>
public class Measurement<T>(T value, Unit unit, Calculator<T> calculator) where T : IComparable
{
    /// <summary>Addition retaining the left unit; throws on dimensional mismatch.</summary>
    public static Measurement<T> operator +(Measurement<T> v1, Measurement<T> v2)
    {
        if (v1 == null || v2 == null)
        {
            return v1 ?? v2;
        }

        if (v1.Unit != v2.Unit)
        {
            throw new UnitException(v1.Unit, v2.Unit);
        }

        return new Measurement<T>(v1.Calculator.Add(v1.Value, v2.Value), v1.Unit, v1.Calculator);
    }

    /// <summary>Subtraction retaining the left unit; throws on dimensional mismatch.</summary>
    public static Measurement<T> operator -(Measurement<T> v1, Measurement<T> v2)
    {
        if (v1 == null || v2 == null)
        {
            return v1 ?? v2;
        }

        if (v1.Unit != v2.Unit)
        {
            throw new UnitException(v1.Unit, v2.Unit);
        }

        return new Measurement<T>(v1.Calculator.Subtract(v1.Value, v2.Value), v1.Unit, v1.Calculator);
    }

    /// <summary>Multiplication producing the product of units.</summary>
    public static Measurement<T> operator *(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Multiply(v1.Value, v2.Value), v1.Unit * v2.Unit, v1.Calculator);
    }

    /// <summary>Division producing the quotient of units.</summary>
    public static Measurement<T> operator /(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Divide(v1.Value, v2.Value), v1.Unit / v2.Unit, v1.Calculator);
    }

    /// <summary>Applies unit multiplication to this measurement (value unchanged).</summary>
    public static Measurement<T> operator *(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit * u, v.Calculator);
    }

    /// <summary>Applies unit division to this measurement (value unchanged).</summary>
    public static Measurement<T> operator /(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit / u, v.Calculator);
    }

    /// <summary>Relational less-than after aligning the right operand's unit to the left.</summary>
    public static bool operator <(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) < 0;
    }

    /// <summary>Relational less-than-or-equal after aligning the right operand's unit to the left.</summary>
    public static bool operator <=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) <= 0;
    }

    /// <summary>Relational greater-than after aligning the right operand's unit to the left.</summary>
    public static bool operator >(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) > 0;
    }

    /// <summary>Relational greater-than-or-equal after aligning the right operand's unit to the left.</summary>
    public static bool operator >=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) >= 0;
    }

    /// <summary>Structural equality: both value and unit must match.</summary>
    public static bool operator ==(Measurement<T> v1, Measurement<T> v2)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(v1, v2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)v1 == null) || ((object)v2 == null))
        {
            return false;
        }

        return v1.Equals(v2);
    }

    /// <summary>Inequality inverse of structural equality.</summary>
    public static bool operator !=(Measurement<T> v1, Measurement<T> v2)
    {
        // If both are null, or both are same instance => equal => inequality false
        if (ReferenceEquals(v1, v2))
        {
            return false;
        }

        // If one is null (but not both) => not equal => inequality true
        if ((object)v1 == null || (object)v2 == null)
        {
            return true;
        }

        return !v1.Equals(v2);
    }

    /// <summary>Implicitly returns the raw numeric value.</summary>
    public static implicit operator T(Measurement<T> v)
    {
        return v.Value;
    }

    /// <summary>Associated unit.</summary>
    public Unit Unit { get; } = unit;
    /// <summary>Numeric value.</summary>
    public T Value { get; } = value;
    /// <summary>Numeric calculator abstraction used for generic arithmetic.</summary>
    public Calculator<T> Calculator { get; } = calculator;

    /// <summary>Returns a culture‑invariant string in the form "value unit".</summary>
    public override string ToString() => $"{Value} {Unit}";

    /// <summary>Value + unit equality check.</summary>
    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is Measurement<T>)
        {
            return Value.Equals((obj as Measurement<T>).Value) && Unit.Equals((obj as Measurement<T>).Unit);
        }

        return base.Equals(obj);
    }

    /// <summary>Combines value and unit hash codes.</summary>
    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode() ^ Unit.GetHashCode();
}

/// <summary>Concrete 32-bit integer measurement convenience type.</summary>
public class Int32Measurement(int value, Unit unit) : Measurement<int>(value, unit, Int32Calculator.Instance)
{
    /// <summary>Implicit construction from raw integer (dimensionless).</summary>
    public static implicit operator Int32Measurement(int v)
    {
        return new Int32Measurement(v, Unit.None);
    }
}

/// <summary>Concrete double precision measurement convenience type.</summary>
public class DoubleMeasurement(double value, Unit unit) : Measurement<double>(value, unit, DoubleCalculator.Instance)
{
    /// <summary>Implicit construction from raw integer (dimensionless) widening to double.</summary>
    public static implicit operator DoubleMeasurement(int v)
    {
        return new DoubleMeasurement(v, Unit.None);
    }
}