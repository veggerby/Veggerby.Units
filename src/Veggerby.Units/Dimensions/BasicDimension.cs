namespace Veggerby.Units.Dimensions;

/// <summary>Atomic (non-composite) dimension.</summary>
public class BasicDimension(string symbol, string name) : Dimension
{
    /// <inheritdoc />
    public override string Symbol { get; } = symbol;
    /// <inheritdoc />
    public override string Name { get; } = name;
}