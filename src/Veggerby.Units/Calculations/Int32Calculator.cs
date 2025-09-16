namespace Veggerby.Units.Calculations;

/// <summary>
/// 32‑bit integer implementation of <see cref="Calculator{T}"/>. Division uses integer truncation consistent
/// with CLR semantics. Overflow behaviour (wrap / exception) is whatever the runtime and compilation context specify
/// (no checked arithmetic enforced here).
/// </summary>
public class Int32Calculator : Calculator<int>
{
    /// <inheritdoc />
    public override int Add(int v1, int v2) => v1 + v2;
    /// <inheritdoc />
    public override int Divide(int v1, int v2) => v1 / v2;
    /// <inheritdoc />
    public override int Multiply(int v1, int v2) => v1 * v2;
    /// <inheritdoc />
    public override int Subtract(int v1, int v2) => v1 - v2;

    /// <summary>Singleton instance (reuse to avoid allocations).</summary>
    public static readonly Calculator<int> Instance = new Int32Calculator();
}