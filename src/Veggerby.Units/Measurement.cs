using System;

using Veggerby.Units.Calculations;
using Veggerby.Units.Conversion;

namespace Veggerby.Units;

/// <summary>
/// Represents a typed numeric value paired with a physical unit enabling safe arithmetic and comparison.
/// Relational comparisons implicitly align units by converting the right operand to the left operand's unit.
/// No implicit alignment is performed for equality or additive operators (they require identical units).
/// </summary>
public class Measurement<T>(T value, Unit unit, Calculator<T> calculator) where T : IComparable
{
    /// <summary>
    /// Adds two measurements retaining the left unit. Units must be structurally equal; no conversion is performed.
    /// Null operands are treated as identity (null + x => x).
    /// </summary>
    public static Measurement<T> operator +(Measurement<T> v1, Measurement<T> v2)
    {
        if (v1 is null || v2 is null)
        {
            return v1 ?? v2;
        }

        if (v1.Unit != v2.Unit)
        {
            throw new UnitException(v1.Unit, v2.Unit);
        }

        return new Measurement<T>(v1.Calculator.Add(v1.Value, v2.Value), v1.Unit, v1.Calculator);
    }

    /// <summary>
    /// Subtracts two measurements retaining the left unit. Units must be structurally equal.
    /// Null operands are treated as identity (null - x => x, x - null => x).
    /// </summary>
    public static Measurement<T> operator -(Measurement<T> v1, Measurement<T> v2)
    {
        if (v1 is null || v2 is null)
        {
            return v1 ?? v2;
        }

        if (v1.Unit != v2.Unit)
        {
            throw new UnitException(v1.Unit, v2.Unit);
        }

        return new Measurement<T>(v1.Calculator.Subtract(v1.Value, v2.Value), v1.Unit, v1.Calculator);
    }

    /// <summary>Multiplies two measurements and composes their units via <see cref="Unit.op_Multiply(Unit, Unit)"/>.</summary>
    public static Measurement<T> operator *(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Multiply(v1.Value, v2.Value), v1.Unit * v2.Unit, v1.Calculator);
    }

    /// <summary>Divides two measurements and composes their units via <see cref="Unit.op_Division(Unit, Unit)"/>.</summary>
    public static Measurement<T> operator /(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Divide(v1.Value, v2.Value), v1.Unit / v2.Unit, v1.Calculator);
    }

    /// <summary>Applies unit multiplication to this measurement (value unchanged) producing a new measurement.</summary>
    public static Measurement<T> operator *(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit * u, v.Calculator);
    }

    /// <summary>Applies unit division to this measurement (value unchanged) producing a new measurement.</summary>
    public static Measurement<T> operator /(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit / u, v.Calculator);
    }

    /// <summary>Relational less-than after aligning the right operand's unit to the left operand's unit.</summary>
    public static bool operator <(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) < 0;
    }

    /// <summary>Relational less-than-or-equal after aligning the right operand's unit to the left operand's unit.</summary>
    public static bool operator <=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) <= 0;
    }

    /// <summary>Relational greater-than after aligning the right operand's unit to the left operand's unit.</summary>
    public static bool operator >(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) > 0;
    }

    /// <summary>Relational greater-than-or-equal after aligning the right operand's unit to the left operand's unit.</summary>
    public static bool operator >=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) >= 0;
    }

    /// <summary>Structural equality: both numeric value and unit must match exactly (no conversion performed).</summary>
    public static bool operator ==(Measurement<T> v1, Measurement<T> v2)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(v1, v2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)v1 is null) || ((object)v2 is null))
        {
            return false;
        }

        return v1.Equals(v2);
    }

    /// <summary>Inequality inverse of structural equality (see <see cref="operator ==(Measurement{T}, Measurement{T})"/>).</summary>
    public static bool operator !=(Measurement<T> v1, Measurement<T> v2)
    {
        // If both are null, or both are same instance => equal => inequality false
        if (ReferenceEquals(v1, v2))
        {
            return false;
        }

        // If one is null (but not both) => not equal => inequality true
        if ((object)v1 is null || (object)v2 is null)
        {
            return true;
        }

        return !v1.Equals(v2);
    }

    /// <summary>Implicitly returns the raw numeric value (unit information is discarded).</summary>
    public static implicit operator T(Measurement<T> v)
    {
        return v.Value;
    }

    /// <summary>Associated unit (never null).</summary>
    public Unit Unit { get; } = unit;
    /// <summary>Underlying numeric value.</summary>
    public T Value { get; } = value;
    /// <summary>Numeric calculator abstraction used for generic arithmetic operations.</summary>
    public Calculator<T> Calculator { get; } = calculator;

    /// <summary>Returns a culture‑invariant string in the form "value unit".</summary>
    public override string ToString() => $"{Value} {Unit}";

    /// <summary>Value + unit equality check (implements object equality contract).</summary>
    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is Measurement<T>)
        {
            return Value.Equals((obj as Measurement<T>).Value) && Unit.Equals((obj as Measurement<T>).Unit);
        }

        return base.Equals(obj);
    }

    /// <summary>Combines value and unit hash codes ensuring equal measurements share the same hash.</summary>
    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode() ^ Unit.GetHashCode();
}

/// <summary>Concrete 32-bit integer measurement convenience type with implicit dimensionless construction.</summary>
public class Int32Measurement(int value, Unit unit) : Measurement<int>(value, unit, Int32Calculator.Instance)
{
    /// <summary>Implicit construction from raw integer producing a dimensionless measurement.</summary>
    public static implicit operator Int32Measurement(int v)
    {
        return new Int32Measurement(v, Unit.None);
    }
}

/// <summary>Concrete double precision measurement convenience type with implicit dimensionless construction.</summary>
public class DoubleMeasurement(double value, Unit unit) : Measurement<double>(value, unit, DoubleCalculator.Instance)
{
    /// <summary>Implicit construction from raw integer (dimensionless) widening through double.</summary>
    public static implicit operator DoubleMeasurement(int v)
    {
        return new DoubleMeasurement(v, Unit.None);
    }
}

/// <summary>Concrete decimal precision measurement convenience type.</summary>
public class DecimalMeasurement(decimal value, Unit unit) : Measurement<decimal>(value, unit, DecimalCalculator.Instance)
{
    /// <summary>Implicit construction from raw integer producing a dimensionless decimal measurement.</summary>
    public static implicit operator DecimalMeasurement(int v)
    {
        return new DecimalMeasurement(v, Unit.None);
    }
}