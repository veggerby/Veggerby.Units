namespace Veggerby.Units;

/// <summary>
/// Represents an affine unit where conversion to the underlying base unit requires an additive offset in
/// addition to a multiplicative scale (e.g. Celsius relative to Kelvin: K = C + 273.15).
/// Only used for temperature at present; affine units must not participate in arbitrary multiplicative algebra
/// that would invalidate offset semantics (library treats them as regular units for composition but correctness
/// is only guaranteed for direct conversion of values, not for products like °C * m).
/// </summary>
/// <remarks>
/// Affine transformation semantics align with QUDT ontology's handling of temperature scales.
/// QUDT distinguishes absolute temperature scales (Kelvin, Celsius, Fahrenheit, Rankine) from
/// differential temperature units (delta K, delta °C, etc.) using <c>conversionOffset</c> and
/// <c>conversionMultiplier</c> properties.
/// <para>
/// QUDT Celsius: <c>unit:DEG_C</c> with conversionOffset: 273.15, conversionMultiplier: 1.0
/// </para>
/// <para>
/// QUDT Fahrenheit: <c>unit:DEG_F</c> with conversionOffset: 459.67 (to Rankine), then scaled to Kelvin
/// </para>
/// <para>
/// See <c>docs/qudt-alignment.md</c> for detailed temperature affine semantics validation.
/// </para>
/// Creates a new affine unit.
/// </remarks>
/// <param name="symbol">Symbolic representation (e.g. °C).</param>
/// <param name="name">Human readable name.</param>
/// <param name="baseUnit">Underlying base unit (Kelvin for Celsius).</param>
/// <param name="scale">Multiplicative scale relative to the base unit.</param>
/// <param name="offset">Additive offset in base unit space.</param>
public sealed class AffineUnit(string symbol, string name, Unit baseUnit, double scale, double offset) : Unit
{

    /// <inheritdoc />
    public override string Symbol => symbol;
    /// <inheritdoc />
    public override string Name => name;
    /// <inheritdoc />
    public override UnitSystem System => baseUnit.System;
    /// <inheritdoc />
    public override Dimensions.Dimension Dimension => baseUnit.Dimension;

    internal override double GetScaleFactor() => scale * baseUnit.GetScaleFactor();

    internal override double ToBase(double value) => (value * scale) + offset; // affine forward
    internal override double FromBase(double baseValue) => (baseValue - offset) / scale; // inverse

    // Internal exposure for optimization in conversion helpers.
    internal double Scale => scale;
    internal double Offset => offset;
    internal Unit BaseUnit => baseUnit;
}