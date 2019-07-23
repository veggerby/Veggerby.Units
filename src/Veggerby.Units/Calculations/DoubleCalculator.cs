namespace Veggerby.Units.Calculations
{
    public class DoubleCalculator : Calculator<double>
    {
        public override double Add(double v1, double v2) => v1 + v2;
        public override double Divide(double v1, double v2) => v1 / v2;
        public override double Multiply(double v1, double v2) => v1 * v2;
        public override double Subtract(double v1, double v2) => v1 - v2;
        public static Calculator<double> Instance = new DoubleCalculator();
    }
}