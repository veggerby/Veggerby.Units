namespace Veggerby.Units.Calculations;

public class Int32Calculator : Calculator<int>
{
    public override int Add(int v1, int v2) => v1 + v2;
    public override int Divide(int v1, int v2) => v1 / v2;
    public override int Multiply(int v1, int v2) => v1 * v2;
    public override int Subtract(int v1, int v2) => v1 - v2;

    public static readonly Calculator<int> Instance = new Int32Calculator();
}