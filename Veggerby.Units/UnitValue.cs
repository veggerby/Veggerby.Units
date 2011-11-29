using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Units
{
    public class Measurement
    {
        private readonly double _Value;
        private readonly Unit _Unit;

        public Measurement(double value, Unit unit)
        {
            this._Value = value;
            this._Unit = unit;
        }

        public static Measurement operator ++(Measurement v)
        {
            return new Measurement(v._Value + 1, v._Unit);
        }

        public static Measurement operator --(Measurement v)
        {
            return new Measurement(v._Value - 1, v._Unit);
        }

        public static Measurement operator +(Measurement v1, Measurement v2)
        {
            if ((v1 == null) || (v2 == null))
            {
                return v1 ?? v2;
            }

            if (!v1._Unit.Equals(v2._Unit))
            {
                throw new UnitException(v1._Unit, v2._Unit);
            }

            return new Measurement(v1._Value + v2._Value, v1._Unit);
        }

        public static Measurement operator -(Measurement v1, Measurement v2)
        {
            if ((v1 == null) || (v2 == null))
            {
                return v1 ?? v2;
            }

            if (!v1._Unit.Equals(v2._Unit))
            {
                throw new UnitException(v1._Unit, v2._Unit);
            }

            return new Measurement(v1._Value - v2._Value, v1._Unit);
        }

        public static Measurement operator *(Measurement v1, Measurement v2)
        {
            return new Measurement(v1._Value * v2._Value, v1._Unit * v2._Unit);
        }

        public static Measurement operator /(Measurement v1, Measurement v2)
        {
            return new Measurement(v1._Value / v2._Value, v1._Unit / v2._Unit);
        }

        public static Measurement operator *(Measurement v, Unit u)
        {
            return new Measurement(v._Value, v._Unit * u);
        }

        public static Measurement operator /(Measurement v, Unit u)
        {
            return new Measurement(v._Value, v._Unit / u);
        }

        public static Measurement operator <(Measurement v1, Measurement v2)
        {
            throw new NotImplementedException("Less than is not implemented. Yet...");
        }

        public static Measurement operator <=(Measurement v1, Measurement v2)
        {
            throw new NotImplementedException("Less than or equal to is not implemented. Yet...");
        }

        public static Measurement operator >(Measurement v1, Measurement v2)
        {
            throw new NotImplementedException("Greater than is not implemented. Yet...");
        }

        public static Measurement operator >=(Measurement v1, Measurement v2)
        {
            throw new NotImplementedException("Greater than or equal to is not implemented. Yet...");
        }

        public static bool operator ==(Measurement v1, Measurement v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Measurement v1, Measurement v2)
        {
            return !v1.Equals(v2);
        }

        public static implicit operator double(Measurement v)
        {
            return v._Value;
        }

        public static implicit operator Measurement(double v)
        {
            return new Measurement(v, Unit.None);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this._Value, this._Unit);
        }

        public override bool Equals(object obj)
        {
            if (obj is Measurement)
            {
                return this._Value.Equals((obj as Measurement)._Value) && this._Unit.Equals((obj as Measurement)._Unit);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this._Value.GetHashCode() ^ this._Unit.GetHashCode();
        }
    }
}
