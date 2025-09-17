using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Represents a unit defined by a fixed scale relative to another (usually base SI) unit, e.g. foot relative to metre.
/// </summary>
/// <param name="symbol">Unit symbol.</param>
/// <param name="name">Unit name.</param>
/// <param name="scale">Scale factor relative to base.</param>
/// <param name="base">Underlying base unit.</param>
/// <param name="system">Optional owning unit system override.</param>
public class ScaleUnit(string symbol, string name, double scale, Unit @base, UnitSystem system = null) : Unit
{
    /// <inheritdoc />
    public override string Symbol { get; } = symbol;
    /// <inheritdoc />
    public override string Name { get; } = name;
    /// <inheritdoc />
    public override Dimension Dimension => @base.Dimension;
    /// <summary>Scaling factor relative to the underlying base unit.</summary>
    public double Scale { get; } = scale;

    /// <inheritdoc />
    public override UnitSystem System => system ?? @base.System;

    internal override double GetScaleFactor() => Scale * @base.GetScaleFactor();
}