using System;
using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityKindTagGovernanceTests
{
    [Fact]
    public void QuantityKindTags_ShouldUseReservedRoots_OrBeWhitelisted()
    {
        // Arrange
        var kinds = typeof(QuantityKinds)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.FieldType == typeof(QuantityKind))
            .Select(f => (QuantityKind)f.GetValue(null))
            .ToArray();

        // Act
        var violations = kinds
            .SelectMany(k => k.Tags)
            .Distinct()
            .Where(t => !t.IsReservedRoot())
            .Select(t => t.Name)
            .OrderBy(n => n)
            .ToArray();

        // Assert
        violations.Should().BeEmpty();
    }

    [Fact]
    public void QuantityKindTags_ShouldAvoidPluralizedRootCollisions()
    {
        // Arrange
        var tags = typeof(QuantityKinds)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.FieldType == typeof(QuantityKind))
            .SelectMany(f => ((QuantityKind)f.GetValue(null)).Tags)
            .Select(t => t.Name)
            .Distinct()
            .ToArray();

        // Act
        var pluralConflicts = tags
            .Select(n => new { Name = n, Root = n.Contains('.') ? n[..n.IndexOf('.')] : n })
            .Where(x => x.Root.EndsWith("s", StringComparison.Ordinal))
            .Where(x => tags.Any(o => (o.Contains('.') ? o[..o.IndexOf('.')] : o) == x.Root.TrimEnd('s')))
            .Select(x => x.Root)
            .Distinct()
            .ToArray();

        // Assert
        pluralConflicts.Should().BeEmpty();
    }

    [Fact]
    public void TagPrefixEnumeration_ShouldReturnAllWithGivenRoot()
    {
        // Arrange
        var prefix = "Energy"; // existing reserved root

        // Act
        var enumerated = QuantityKindTagExtensions.EnumerateByPrefix(prefix).Select(t => t.Name).ToArray();

        // Assert
        enumerated.Should().NotBeEmpty();
        enumerated.All(n => n.StartsWith(prefix, StringComparison.Ordinal)).Should().BeTrue();
    }
}