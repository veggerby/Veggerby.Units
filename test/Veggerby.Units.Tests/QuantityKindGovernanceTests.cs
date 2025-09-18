using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

/// <summary>
/// Governance tests ensuring QuantityKinds partial definitions remain consistent:
/// 1. All public static QuantityKind fields are unique by Name and Symbol.
/// 2. All dimensionless kinds include the FormDimensionless tag.
/// 3. (Placeholder) Documentation completeness can be wired here when a canonical list is externalized.
/// </summary>
public class QuantityKindGovernanceTests
{
    private static IEnumerable<FieldInfo> GetAllKindFields() => typeof(QuantityKinds)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(f => f.FieldType == typeof(QuantityKind));

    private static IEnumerable<QuantityKind> GetAllKinds() => GetAllKindFields()
        .Select(f => (QuantityKind)f.GetValue(null)!);

    [Fact]
    public void All_Public_Static_Kind_Fields_Should_Have_Unique_Names()
    {
        // Arrange
        var kinds = GetAllKinds().ToList();

        // Act
        var duplicateNames = kinds
            .GroupBy(k => k.Name, StringComparer.Ordinal)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        // Assert
        duplicateNames.Should().BeEmpty("each QuantityKind.Name must be unique");
    }

    [Fact]
    public void Symbol_Collisions_With_Identical_Dimensions_Should_Not_Exist()
    {
        // Arrange
        var kinds = GetAllKinds().ToList();

        // Build groups by (Symbol, CanonicalUnit structural signature)
        static string Sig(QuantityKind k) => k.CanonicalUnit.ToString();

        var hardCollisions = kinds
            .GroupBy(k => k.Symbol + "|" + Sig(k), StringComparer.Ordinal)
            .Where(g => g.Count() > 1)
            .Select(g => new { Key = g.Key, Symbol = g.First().Symbol, Signature = Sig(g.First()), Names = g.Select(x => x.Name).OrderBy(n => n, StringComparer.Ordinal).ToList() })
            .ToList();

        hardCollisions.Should().BeEmpty("distinct kinds must not share identical symbol AND identical unit signature");

        // Soft report (not assert): overlapping symbols with different dimension signatures – acceptable but informative.
        var soft = kinds
            .GroupBy(k => k.Symbol, StringComparer.Ordinal)
            .Where(g => g.Count() > 1)
            .Select(g => new { Symbol = g.Key, Names = g.Select(x => x.Name).OrderBy(n => n, StringComparer.Ordinal).ToList() })
            .ToList();

        if (soft.Count > 0)
        {
            // Emit diagnostic information (does not fail test)
            System.Diagnostics.Debug.WriteLine("[QuantityKind Governance] Overlapping symbols (allowed):");
            foreach (var s in soft)
            {
                System.Diagnostics.Debug.WriteLine($"  {s.Symbol}: {string.Join(", ", s.Names)}");
            }
        }
    }

    [Fact]
    public void Dimensionless_Units_Must_Be_Tagged_FormDimensionless()
    {
        // Arrange
        var kinds = GetAllKinds().ToList();

        // Act
        var offenders = kinds
            .Where(k => k.CanonicalUnit == Unit.None)
            .Where(k => !k.Tags.Contains(QuantityKindTags.FormDimensionless))
            .Select(k => k.Name)
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToList();

        // Assert
        offenders.Should().BeEmpty("dimensionless kinds must carry FormDimensionless tag for governance");
    }

    [Fact]
    public void No_QuantityKind_Should_Have_Null_Or_Empty_Metadata()
    {
        // Arrange
        var kinds = GetAllKinds().ToList();

        // Assert
        kinds.Should().OnlyContain(k => !string.IsNullOrWhiteSpace(k.Name));
        kinds.Should().OnlyContain(k => !string.IsNullOrWhiteSpace(k.Symbol));
        kinds.Should().OnlyContain(k => k.Tags != null, "tags collection must not be null (can be empty)");
    }
}