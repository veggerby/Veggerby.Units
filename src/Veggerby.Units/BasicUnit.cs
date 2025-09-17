using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Represents a fundamental (non-derived) unit with a fixed symbol, name, system and dimension.
/// </summary>
/// <param name="symbol">Short symbolic representation (e.g. m, s).</param>
/// <param name="name">Long form name (e.g. meter, second).</param>
/// <param name="system">Owning unit system (e.g. SI).</param>
/// <param name="dimension">Associated physical dimension.</param>
public class BasicUnit(string symbol, string name, UnitSystem system, Dimension dimension) : Unit
{
    /// <inheritdoc />
    public override string Symbol { get; } = symbol;
    /// <inheritdoc />
    public override string Name { get; } = name;
    /// <inheritdoc />
    public override UnitSystem System { get; } = system;
    /// <inheritdoc />
    public override Dimension Dimension { get; } = dimension;
}