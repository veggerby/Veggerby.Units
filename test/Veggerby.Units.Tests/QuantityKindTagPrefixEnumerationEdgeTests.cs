using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityKindTagPrefixEnumerationEdgeTests
{
    [Fact]
    public void GivenEmptyPrefix_WhenEnumerate_ThenReturnsEmpty()
    {
        var results = QuantityKindTagExtensions.EnumerateByPrefix("").ToList();
        results.Count.Should().Be(0);
    }

    [Fact]
    public void GivenNonMatchingPrefix_WhenEnumerate_ThenReturnsEmpty()
    {
        var results = QuantityKindTagExtensions.EnumerateByPrefix("ZZZ_").ToList();
        results.Count.Should().Be(0);
    }

    [Fact]
    public void GivenCommonPrefix_WhenEnumerate_ThenReturnsAllMatching()
    {
        // Choose 'Energy' root-based tags (e.g., Energy.*) present on energy kinds
        var results = QuantityKindTagExtensions.EnumerateByPrefix("Energy").ToList();
        results.Should().NotBeEmpty();
        results.Any(t => t.Name.StartsWith("Energy", System.StringComparison.Ordinal)).Should().BeTrue();
    }
}