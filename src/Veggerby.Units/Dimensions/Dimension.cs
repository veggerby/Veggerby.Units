using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions
{
    public abstract class Dimension : IOperand
    {
        /// <summary>
        /// Dimensionless property, eg. a constant (pi, e, etc.)
        /// </summary>
        public static Dimension None = new NullDimension();

        public static Dimension Length = new BasicDimension("L", "length");
        public static Dimension Mass = new BasicDimension("M", "mass");
        public static Dimension Time = new BasicDimension("T", "time");
        public static Dimension ElectricCurrent = new BasicDimension("I", "electric current");
        public static Dimension ThermodynamicTemperature = new BasicDimension("Θ", "termodynamic temperature");
        public static Dimension LuminousIntensity = new BasicDimension("J", "luminous intensity");
        public static Dimension AmountOfSubstance = new BasicDimension("N", "amount of substance");

        internal Dimension()
        {
        }

        public abstract string Symbol { get; }
        public abstract string Name { get; }

        public static Dimension Mult(params Dimension[] operands)
        {
            return new ProductDimension(operands);
        }

        public static Dimension Div(Dimension dividend, Dimension divisor)
        {
            return new DivisionDimension(dividend, divisor);
        }

        public static Dimension Pow(Dimension @base, int exponent)
        {
            return new PowerDimension(@base, exponent);
        }

        public static Dimension operator +(Dimension d1, Dimension d2)
        {
            if (d1 != d2)
            {
                throw new DimensionException(d1, d2);
            }

            return d1;
        }

        public static Dimension operator -(Dimension d1, Dimension d2)
        {
            if (d1 != d2)
            {
                throw new DimensionException(d1, d2);
            }

            return d1;
        }

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
                Mult(d1, d2);
        }

        public static Dimension operator /(int dividend, Dimension divisor)
        {
            return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, Dimension.None, divisor) ??
                Div(Dimension.None, divisor);
        }

        public static Dimension operator /(Dimension dividend, Dimension divisor)
        {
            if (divisor == Dimension.None)
            {
                return dividend;
            }

            return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, dividend, divisor) ??
                OperationUtility.ReduceDivision(x => x.Multiply((a, b) => a * b, Dimension.None), (x, y) => x / y, (x, y) => x ^ y, dividend, divisor) ??
                Div(dividend, divisor);
        }

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
                   Pow(@base, exponent);
        }

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

        public static bool operator !=(Dimension d1, Dimension d2)
        {
            return !d1.Equals(d2);
        }

        public static implicit operator string(Dimension d)
        {
            return d.Symbol;
        }

        public override bool Equals(object obj)
        {
            if (obj is Dimension)
            {
                return OperationUtility.Equals(this, obj as Dimension);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
