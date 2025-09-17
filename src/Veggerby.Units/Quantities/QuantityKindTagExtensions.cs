using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Utility helpers and governance surface for <see cref="QuantityKindTag"/> reserved root validation.
/// DEBUG builds emit soft warnings for non-reserved roots; RELEASE builds incur no overhead.
/// </summary>
internal static class QuantityKindTagExtensions
{
    private static readonly HashSet<string> ReservedRoots = new(StringComparer.Ordinal)
    {
        "Energy", "Domain", "Form", "State", "Path", "Geometry", "Material", "Process", "Signal", "Temporal"
    };

    private static readonly HashSet<string> WhitelistRoots = new(StringComparer.Ordinal)
    {
        "Legacy", "Experimental"
    };

    private static bool _validated;

    /// <summary>Returns true if the tag's first dotted segment is a reserved or whitelisted root.</summary>
    public static bool IsReservedRoot(this QuantityKindTag tag)
    {
        if (tag == null)
        {
            return false;
        }

        var root = GetRoot(tag.Name);
        return ReservedRoots.Contains(root) || WhitelistRoots.Contains(root);
    }

    /// <summary>Enumerate all canonical tags currently created whose name starts with the provided prefix (case-sensitive).</summary>
    public static IEnumerable<QuantityKindTag> EnumerateByPrefix(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            yield break;
        }

        foreach (var tag in GetAllKnownTags())
        {
            if (tag.Name.StartsWith(prefix, StringComparison.Ordinal))
            {
                yield return tag;
            }
        }
    }

    /// <summary>Invoke once (DEBUG only) to soft-validate all tags attached to built-in kinds.</summary>
    internal static void ValidateReservedRootsOnce(IEnumerable<QuantityKind> kinds)
    {
#if DEBUG
        if (_validated)
        {
            return;
        }
        _validated = true;

        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var kind in kinds)
        {
            foreach (var tag in kind.Tags)
            {
                var root = GetRoot(tag.Name);
                if (!ReservedRoots.Contains(root) && !WhitelistRoots.Contains(root))
                {
                    EmitWarning($"[QuantityKindTag] Unreserved root '{root}' in tag '{tag.Name}'. Consider adding to reserved list or whitelist.");
                }

                if (root.EndsWith("s", StringComparison.Ordinal) && ReservedRoots.Contains(root.TrimEnd('s')))
                {
                    EmitWarning($"[QuantityKindTag] Possible plural root collision: '{root}' vs '{root.TrimEnd('s')}'. Prefer singular roots.");
                }

                if (!seen.Add(tag.Name))
                {
                    continue;
                }
            }
        }
#endif
    }

    private static string GetRoot(string name)
    {
        var idx = name.IndexOf('.');
        return idx < 0 ? name : name.Substring(0, idx);
    }

    private static IEnumerable<QuantityKindTag> GetAllKnownTags()
    {
        // Reflect into QuantityKindTag's cache via public API side-effects (no direct exposure needed now).
        // Since we only ever obtain tags through Get(name), we can enumerate by accessing known kinds' tags.
        // This helper is intentionally minimal; not exposing internal cache to keep surface small.
        foreach (var field in typeof(QuantityKinds).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
        {
            if (field.FieldType == typeof(QuantityKind) && field.GetValue(null) is QuantityKind k)
            {
                foreach (var t in k.Tags)
                {
                    yield return t;
                }
            }
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    private static void EmitWarning(string message)
    {
        System.Diagnostics.Trace.WriteLine(message);
        Console.Error.WriteLine(message);
    }
}