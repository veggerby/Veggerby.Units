using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions
{
    public abstract class Dimension : IOperand
    {
        public static Dimension Empty = default(Dimension);

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
            return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b), (x, y) => x / y, d1, d2) ?? new ProductDimension(d1, d2);
        }

        public static Dimension operator /(Dimension d1, Dimension d2)
        {
            return OperationUtility.RearrangeDivision((x, y) => x * y, (x, y) => x / y, d1, d2) ?? new DivisionDimension(d1, d2);

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
                return string.Equals(this.Symbol, (obj as Dimension).Symbol);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }
    }
}
