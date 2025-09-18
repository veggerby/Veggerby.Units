using System.Collections.Generic;

namespace Veggerby.Units.Formatting;

/// <summary>
/// Internal centralized registry of ambiguous derived symbols mapped to the set of
/// quantity kind names that legitimately render with that symbol. This avoids brittle
/// structural inference in favor of explicit semantic disambiguation.
/// </summary>
internal static class AmbiguityRegistry
{
    // Immutable symbol -> set(kind names). Only truly ambiguous collisions are included.
    private static readonly IReadOnlyDictionary<string, IReadOnlySet<string>> _map =
        new Dictionary<string, IReadOnlySet<string>>
        {
            ["J"] = new HashSet<string> { "Energy", "Work", "Heat", "Torque" },
            ["Pa"] = new HashSet<string> { "Pressure", "Stress" },
            ["W"] = new HashSet<string> { "Power", "RadiantFlux" },
            ["H"] = new HashSet<string> { "Inductance", "MagneticFieldStrength" },
        };

    public static bool TryGetAmbiguities(string symbol, out IReadOnlySet<string> kindNames)
    {
        if (_map.TryGetValue(symbol, out var set))
        {
            kindNames = set;
            return true;
        }

        kindNames = null;
        return false;
    }
}
