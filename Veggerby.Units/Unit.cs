using System.Text;
using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;
using Veggerby.Units.Visitors;

namespace Veggerby.Units
{
    public abstract class Unit : IOperand
    {
        public static Unit None = new NullUnit();

        public static InternationalUnitSystem SI = new InternationalUnitSystem();
        public static ImperialUnitSystem Imperial = new ImperialUnitSystem();

        public abstract string Symbol { get; }
        public abstract string Name { get; }
        public abstract UnitSystem System { get; }
        public abstract Dimension Dimension { get; }

        public static Unit Mult(params Unit[] operands)
        {
            return new ProductUnit(operands);
        }

        public static Unit Div(Unit dividend, Unit divisor)
        {
            return new DivisionUnit(dividend, divisor);
        }

        public static Unit Pow(Unit @base, int exponent)
        {
            return new PowerUnit(@base, exponent);
        }

        public static Unit operator +(Unit d1, Unit d2)
        {
            if (d1 != d2)
            {
                throw new UnitException(d1, d2);
            }

            return d1;
        }

        public static Unit operator -(Unit d1, Unit d2)
        {
            if (d1 != d2)
            {
                throw new UnitException(d1, d2);
            }

            return d1;
        }

        public static Unit operator *(Unit d1, Unit d2)
        {
            if (d1 == Unit.None) // and if d2 == None, return d2 (=None)
            {
                return d2;
            }

            if (d2 == Unit.None)
            {
                return d1;
            }

            // where to put OperationUtility.ReduceMultiplication
            return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x / y, d1, d2) ??
                OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x ^ y, d1, d2) ??
                Mult(d1, d2);
        }

        public static Unit operator *(Prefix pre, Unit unit)
        {
            return new PrefixedUnit(pre, unit);
        }

        public static Unit operator /(int dividend, Unit divisor)
        {
            return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, Unit.None, divisor) ??
                Div(Unit.None, divisor);
        }

        public static Unit operator /(Unit dividend, Unit divisor)
        {
            if (divisor == Unit.None)
            {
                return dividend;
            }

            return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
                OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
                Div(dividend, divisor);
        }

        public static Unit operator ^(Unit @base, int exponent)
        {
            if (exponent < 0)
            {
                return 1 / (@base ^ (-exponent));
            }

            if (exponent == 0)
            {
                return Unit.None;
            }

            if (exponent == 1)
            {
                return @base;
            }

            return OperationUtility.ExpandPower(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x / y, (x, y) => x ^ y, @base, exponent) ??
                   Pow(@base, exponent);
        }

        public static bool operator ==(Unit d1, Unit d2)
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

        public static bool operator !=(Unit d1, Unit d2)
        {
            return !d1.Equals(d2);
        }

        public static implicit operator string(Unit d)
        {
            return d.Symbol;
        }

        public override bool Equals(object obj)
        {
            if (obj is Unit)
            {
                return OperationUtility.Equals(this, (obj as Unit));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }

        internal abstract T Accept<T>(Visitor<T> visitor);

        public override string ToString()
        {
            return this.Symbol;
        }
    }
}
