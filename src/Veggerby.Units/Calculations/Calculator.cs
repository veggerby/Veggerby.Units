namespace Veggerby.Units.Calculations;

public abstract class Calculator<T>
{
    public abstract T Add(T v1, T v2);
    public abstract T Subtract(T v1, T v2);
    public abstract T Multiply(T v1, T v2);
    public abstract T Divide(T v1, T v2);
}