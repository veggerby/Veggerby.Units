namespace Veggerby.Units.Quantities;

/// <summary>
/// Temperature related quantity kinds separating absolute (affine) and delta (linear) semantics.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Temperature difference (linear): canonical K (Δ°C == K scale, Δ°F scaled).</summary>
    public static readonly QuantityKind TemperatureDelta = new("TemperatureDelta", Unit.SI.K, "ΔT");
    /// <summary>Absolute temperature (affine): K, °C, °F. Point − Point → ΔT.</summary>
    public static readonly QuantityKind TemperatureAbsolute = new("TemperatureAbsolute", Unit.SI.K, "T_abs", allowDirectAddition: false, allowDirectSubtraction: false, differenceResultKind: TemperatureDelta);
}