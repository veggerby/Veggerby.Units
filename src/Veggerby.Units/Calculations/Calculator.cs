namespace Veggerby.Units.Calculations;

/// <summary>
/// Abstract numeric strategy enabling generic arithmetic for measurements. Implementations must be stateless and
/// side‑effect free.
/// </summary>
public abstract class Calculator<T>
{
    /// <summary>Adds two values.</summary>
    public abstract T Add(T v1, T v2);
    /// <summary>Subtracts second from first.</summary>
    public abstract T Subtract(T v1, T v2);
    /// <summary>Multiplies two values.</summary>
    public abstract T Multiply(T v1, T v2);
    /// <summary>Divides first by second.</summary>
    public abstract T Divide(T v1, T v2);
}