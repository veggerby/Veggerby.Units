namespace Veggerby.Units;

/// <summary>
/// Marker/identity class grouping related units (e.g. SI, Imperial). Immutable after construction.
/// </summary>
public class UnitSystem
{
    /// <summary>
    /// Sentinel unit system used when no real system applies (e.g. for Unit.None or composites without explicit system resolution).
    /// </summary>
    public static readonly UnitSystem None = new();
}