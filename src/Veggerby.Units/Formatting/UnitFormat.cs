namespace Veggerby.Units.Formatting;

/// <summary>
/// Controls how units (and measurements) are rendered when using the fluent formatting layer.
/// </summary>
public enum UnitFormat
{
    /// <summary>Render strictly as canonical base factor expression (no substitutions).</summary>
    BaseFactors = 0,
    /// <summary>Render using only recognised derived symbols; if unrecognised and strict requested a fallback occurs.</summary>
    DerivedSymbols = 1,
    /// <summary>Greedy substitution of any recognised sub-expression; remainder in base factors.</summary>
    Mixed = 2,
    /// <summary>Same as <see cref="DerivedSymbols"/> (or Mixed fallback) but appends the quantity kind name when symbol is ambiguous.</summary>
    Qualified = 3
}