using System;

using Veggerby.Units.Calculations;
using Veggerby.Units.Conversion;

namespace Veggerby.Units;

public class Measurement<T>(T value, Unit unit, Calculator<T> calculator) where T : IComparable
{
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

    public static Measurement<T> operator *(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Multiply(v1.Value, v2.Value), v1.Unit * v2.Unit, v1.Calculator);
    }

    public static Measurement<T> operator /(Measurement<T> v1, Measurement<T> v2)
    {
        return new Measurement<T>(v1.Calculator.Divide(v1.Value, v2.Value), v1.Unit / v2.Unit, v1.Calculator);
    }

    public static Measurement<T> operator *(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit * u, v.Calculator);
    }

    public static Measurement<T> operator /(Measurement<T> v, Unit u)
    {
        return new Measurement<T>(v.Value, v.Unit / u, v.Calculator);
    }

    public static bool operator <(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) < 0;
    }

    public static bool operator <=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) <= 0;
    }

    public static bool operator >(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) > 0;
    }

    public static bool operator >=(Measurement<T> v1, Measurement<T> v2)
    {
        return v1.Value.CompareTo(v2.AlignUnits(v1).Value) >= 0;
    }

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

    public static implicit operator T(Measurement<T> v)
    {
        return v.Value;
    }

    public Unit Unit { get; } = unit;
    public T Value { get; } = value;
    public Calculator<T> Calculator { get; } = calculator;

    public override string ToString() => $"{Value} {Unit}";

    public override bool Equals(object obj)
    {
        if (obj is Measurement<T>)
        {
            return Value.Equals((obj as Measurement<T>).Value) && Unit.Equals((obj as Measurement<T>).Unit);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode() => Value.GetHashCode() ^ Unit.GetHashCode();
}

public class Int32Measurement(int value, Unit unit) : Measurement<int>(value, unit, Int32Calculator.Instance)
{
    public static implicit operator Int32Measurement(int v)
    {
        return new Int32Measurement(v, Unit.None);
    }
}

public class DoubleMeasurement(double value, Unit unit) : Measurement<double>(value, unit, DoubleCalculator.Instance)
{
    public static implicit operator DoubleMeasurement(int v)
    {
        return new DoubleMeasurement(v, Unit.None);
    }
}