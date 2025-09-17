namespace Veggerby.Units.Calculations;

/// <summary>
/// Decimal precision implementation of <see cref="Calculator{T}"/> using <see cref="decimal"/> arithmetic.
/// Provides higher precision for financial or high‑accuracy engineering calculations at the cost of performance.
/// </summary>
public class DecimalCalculator : Calculator<decimal>
{
    /// <inheritdoc />
    public override decimal Add(decimal v1, decimal v2) => v1 + v2;
    /// <inheritdoc />
    public override decimal Divide(decimal v1, decimal v2) => v1 / v2;
    /// <inheritdoc />
    public override decimal Multiply(decimal v1, decimal v2) => v1 * v2;
    /// <inheritdoc />
    public override decimal Subtract(decimal v1, decimal v2) => v1 - v2;

    /// <summary>Singleton instance (reuse to avoid allocations).</summary>
    public static readonly Calculator<decimal> Instance = new DecimalCalculator();
}