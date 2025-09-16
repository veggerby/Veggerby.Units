namespace Veggerby.Units.Calculations;

/// <summary>
/// Double precision implementation of <see cref="Calculator{T}"/> using IEEE 754 semantics for all operations.
/// Suitable for physical calculations requiring fractional precision. No special handling for NaN / Infinity is
/// introduced beyond native double behaviour.
/// </summary>
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

    /// <summary>Singleton instance (reuse to avoid allocations).</summary>
    public static readonly Calculator<double> Instance = new DoubleCalculator();
}