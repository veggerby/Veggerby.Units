using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityKindTagExtensionsTests
{
    [Fact]
    public void IsReservedRoot_KnownReserved_ReturnsTrue()
    {
        // Arrange
        var tag = QuantityKindTag.Get("Energy.StateFunction");

        // Act
        var result = tag.IsReservedRoot();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsReservedRoot_UnknownRoot_ReturnsFalse()
    {
        // Arrange
        var tag = QuantityKindTag.Get("CustomNamespace.ExperimentalFeature");

        // Act
        var result = tag.IsReservedRoot();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void EnumerateByPrefix_FindsMatching()
    {
        // Arrange
        var prefix = "Domain";

        // Act
        var matches = QuantityKindTagExtensions.EnumerateByPrefix(prefix).ToList();

        // Assert
        matches.Should().NotBeEmpty();
        matches.Any(t => t.Name.StartsWith(prefix)).Should().BeTrue();
    }

    [Fact]
    public void EnumerateByPrefix_EmptyPrefix_YieldsNone()
    {
        // Act
        var matches = QuantityKindTagExtensions.EnumerateByPrefix(string.Empty).ToList();

        // Assert
        matches.Should().BeEmpty();
    }
}