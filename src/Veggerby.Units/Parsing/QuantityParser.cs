using System;
using System.Collections.Generic;
using System.Reflection;

using Veggerby.Units.Quantities;

namespace Veggerby.Units.Parsing;

/// <summary>
/// Parses qualified symbol strings (e.g., "Energy", "Torque", "Pressure") into <see cref="QuantityKind"/> instances.
/// Supports reverse mapping from qualified formatting output back to semantic quantity kinds.
/// </summary>
public static class QuantityParser
{
    private static readonly Dictionary<string, QuantityKind> _kindRegistry = BuildKindRegistry();

    /// <summary>
    /// Parses a quantity kind name into a <see cref="QuantityKind"/> instance.
    /// </summary>
    /// <param name="kindName">The quantity kind name (e.g., "Energy", "Torque", "Pressure").</param>
    /// <returns>The corresponding QuantityKind.</returns>
    /// <exception cref="ParseException">Thrown when the kind name is not recognized.</exception>
    public static QuantityKind Parse(string kindName)
    {
        if (string.IsNullOrWhiteSpace(kindName))
        {
            throw new ParseException("Quantity kind name cannot be null or empty.");
        }

        if (_kindRegistry.TryGetValue(kindName, out var kind))
        {
            return kind;
        }

        throw new ParseException($"Unknown quantity kind '{kindName}'");
    }

    /// <summary>
    /// Tries to parse a quantity kind name into a <see cref="QuantityKind"/> instance.
    /// </summary>
    /// <param name="kindName">The quantity kind name to parse.</param>
    /// <param name="kind">The parsed quantity kind if successful, otherwise null.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParse(string kindName, out QuantityKind kind)
    {
        kind = null;

        if (string.IsNullOrWhiteSpace(kindName))
        {
            return false;
        }

        return _kindRegistry.TryGetValue(kindName, out kind);
    }

    /// <summary>
    /// Gets all registered quantity kind names.
    /// </summary>
    public static IEnumerable<string> GetAllKindNames()
    {
        return _kindRegistry.Keys;
    }

    /// <summary>
    /// Builds the registry of quantity kind names to instances by reflecting over
    /// all public static readonly QuantityKind fields in the QuantityKinds class.
    /// </summary>
    private static Dictionary<string, QuantityKind> BuildKindRegistry()
    {
        var registry = new Dictionary<string, QuantityKind>();

        // Get all static readonly fields of type QuantityKind from QuantityKinds class
        var fields = typeof(QuantityKinds).GetFields(
            BindingFlags.Public | BindingFlags.Static);

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(QuantityKind) && field.IsStatic)
            {
                var kind = (QuantityKind)field.GetValue(null);
                if (kind != null)
                {
                    // Register by the kind's Name property (e.g., "Energy")
                    registry[kind.Name] = kind;
                }
            }
        }

        return registry;
    }
}
