﻿using System;
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
            return new Measurement(v.Value + 1, v.Unit);
        }

        public static Measurement operator --(Measurement v)
        {
            return new Measurement(v.Value - 1, v.Unit);
        }

        public static Measurement operator +(Measurement v1, Measurement v2)
        {
            if ((v1 == null) || (v2 == null))
            {
                return v1 ?? v2;
            }

            if (!v1.Unit.Equals(v2.Unit))
            {
                throw new UnitException(v1.Unit, v2.Unit);
            }

            return new Measurement(v1.Value + v2.Value, v1.Unit);
        }

        public static Measurement operator -(Measurement v1, Measurement v2)
        {
            if ((v1 == null) || (v2 == null))
            {
                return v1 ?? v2;
            }

            if (!v1.Unit.Equals(v2.Unit))
            {
                throw new UnitException(v1.Unit, v2.Unit);
            }

            return new Measurement(v1.Value - v2.Value, v1.Unit);
        }

        public static Measurement operator *(Measurement v1, Measurement v2)
        {
            return new Measurement(v1.Value * v2.Value, v1.Unit * v2.Unit);
        }

        public static Measurement operator /(Measurement v1, Measurement v2)
        {
            return new Measurement(v1.Value / v2.Value, v1.Unit / v2.Unit);
        }

        public static Measurement operator *(Measurement v, Unit u)
        {
            return new Measurement(v.Value, v.Unit * u);
        }

        public static Measurement operator /(Measurement v, Unit u)
        {
            return new Measurement(v.Value, v.Unit / u);
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
            return v.Value;
        }

        public static implicit operator Measurement(double v)
        {
            return new Measurement(v, Unit.None);
        }

        public Unit Unit
        {
            get { return _Unit; }
        }

        public double Value
        {
            get { return _Value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Value, this.Unit);
        }

        public override bool Equals(object obj)
        {
            if (obj is Measurement)
            {
                return this.Value.Equals((obj as Measurement).Value) && this.Unit.Equals((obj as Measurement).Unit);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode() ^ this.Unit.GetHashCode();
        }
    }
}
