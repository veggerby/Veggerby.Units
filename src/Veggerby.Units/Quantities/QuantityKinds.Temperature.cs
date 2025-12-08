namespace Veggerby.Units.Quantities;

/// <summary>
/// Temperature related quantity kinds separating absolute (affine) and delta (linear) semantics.
/// </summary>
/// <remarks>
/// Temperature semantics align with QUDT ontology's distinction between absolute temperature scales
/// (with affine offsets) and differential temperature units (linear scale only).
/// <para>
/// QUDT <c>quantitykind:ThermodynamicTemperature</c> represents absolute temperatures.
/// QUDT <c>quantitykind:TemperatureDifference</c> represents temperature deltas.
/// </para>
/// <para>
/// QUDT affine units: <c>unit:K</c> (base), <c>unit:DEG_C</c> (offset +273.15), <c>unit:DEG_F</c> (offset via Rankine).
/// </para>
/// <para>
/// See <c>docs/qudt-alignment.md</c> for temperature affine semantics validation.
/// </para>
/// </remarks>
public static partial class QuantityKinds
{
    /// <summary>Temperature difference (linear): canonical K (Δ°C == K scale, Δ°F scaled).</summary>
    /// <remarks>QUDT: <c>quantitykind:TemperatureDifference</c></remarks>
    public static readonly QuantityKind TemperatureDelta = new("TemperatureDelta", Unit.SI.K, "ΔT");
    /// <summary>Absolute temperature (affine): K, °C, °F. Point − Point → ΔT.</summary>
    /// <remarks>QUDT: <c>quantitykind:ThermodynamicTemperature</c></remarks>
    public static readonly QuantityKind TemperatureAbsolute = new("TemperatureAbsolute", Unit.SI.K, "T_abs", allowDirectAddition: false, allowDirectSubtraction: false, differenceResultKind: TemperatureDelta);
}