namespace Veggerby.Units.Calculations;

/// <summary>
/// Abstract numeric strategy enabling generic arithmetic for <see cref="Measurement{T}"/> without relying on dynamic
/// or reflection. Implementations must be:
/// <list type="bullet">
/// <item><description>Stateless (safe to cache singletons)</description></item>
/// <item><description>Pure (no side‑effects, deterministic)</description></item>
/// <item><description>Total for all valid <typeparamref name="T"/> inputs (no unexpected overflow management here)</description></item>
/// </list>
/// Consumers rely on consistent semantics so behaviour must not change between releases.
/// </summary>
public abstract class Calculator<T>
{
    /// <summary>Adds two values (v1 + v2).</summary>
    public abstract T Add(T v1, T v2);
    /// <summary>Subtracts second from first (v1 - v2).</summary>
    public abstract T Subtract(T v1, T v2);
    /// <summary>Multiplies two values (v1 * v2).</summary>
    public abstract T Multiply(T v1, T v2);
    /// <summary>Divides first by second (v1 / v2). Implementations may rely on underlying type division semantics (e.g. integer truncation).</summary>
    public abstract T Divide(T v1, T v2);
}