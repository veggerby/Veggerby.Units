using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// Represents a unit defined by a fixed scale relative to another (usually base SI) unit, e.g. foot relative to metre.
/// </summary>
/// <remarks>
/// Scale factor definitions align with QUDT ontology's <c>conversionMultiplier</c> property.
/// All conversion factors have been validated against QUDT canonical values to ensure scientific accuracy.
/// <para>
/// Example QUDT definitions:
/// - Foot: conversionMultiplier = 3.048E-1 (0.3048 m)
/// - Pound-mass: conversionMultiplier = 4.5359237E-1 (0.45359237 kg)
/// </para>
/// <para>
/// See <c>docs/qudt-alignment.md</c> for scale factor verification.
/// </para>
/// </remarks>
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