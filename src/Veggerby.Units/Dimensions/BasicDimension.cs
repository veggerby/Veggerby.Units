namespace Veggerby.Units.Dimensions;

public class BasicDimension(string symbol, string name) : Dimension
{
    public override string Symbol { get; } = symbol;
    public override string Name { get; } = name;
}