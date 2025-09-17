using System;
using System.Collections.Generic;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Open semantic tag attached to <see cref="QuantityKind"/> instances. Tags are purely descriptive metadata
/// and have no influence on algebra or dimensional reduction. Canonical instances are provided via <see cref="Get"/>.
/// </summary>
public sealed class QuantityKindTag : IEquatable<QuantityKindTag>
{
    private static readonly Dictionary<string, QuantityKindTag> Cache = new(StringComparer.Ordinal);

    /// <summary>Name of the tag (case-sensitive; use dotted namespaces for hierarchy e.g. "Energy.StateFunction").</summary>
    public string Name { get; }

    private QuantityKindTag(string name)
    {
        Name = name;
    }

    /// <summary>Returns a canonical tag instance for the specified name (creates if missing).</summary>
    public static QuantityKindTag Get(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tag name must be non-empty", nameof(name));
        }

        if (Cache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var tag = new QuantityKindTag(name);
        Cache[name] = tag;
        return tag;
    }

    /// <summary>Returns the tag name.</summary>
    public override string ToString() => Name;

    /// <summary>Reference or name-based equality (canonical instances make reference equality typical).</summary>
    public bool Equals(QuantityKindTag other) => ReferenceEquals(this, other) || (other is not null && Name == other.Name);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is QuantityKindTag t && Equals(t);

    /// <summary>Hash code based on the tag name (ordinal).</summary>
    public override int GetHashCode() => Name.GetHashCode(StringComparison.Ordinal);
}