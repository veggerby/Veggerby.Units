namespace Veggerby.Units.Calculations;

/// <summary>Double precision implementation of <see cref="Calculator{T}"/>.</summary>
public class DoubleCalculator : Calculator<double>
{
    /// <inheritdoc />
    public override double Add(double v1, double v2) => v1 + v2;
    /// <inheritdoc />
    public override double Divide(double v1, double v2) => v1 / v2;
    /// <inheritdoc />
    public override double Multiply(double v1, double v2) => v1 * v2;
    /// <inheritdoc />
    public override double Subtract(double v1, double v2) => v1 - v2;

    /// <summary>Singleton instance.</summary>
    public static readonly Calculator<double> Instance = new DoubleCalculator();
}