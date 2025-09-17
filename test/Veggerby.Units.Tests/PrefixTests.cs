using System.Linq;

using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class PrefixTests
{
    [Fact]
    public void GivenInvalidNumericFactor_WhenApplyingAsPrefix_ThenThrowsPrefixException()
    {
        // Arrange
        var act = () => { var _ = 42 * Unit.SI.m; }; // 42 not a defined prefix

        // Act
        // Assert
        act.Should().Throw<PrefixException>();
    }

    [Fact]
    public void GivenDoubleFactor_WhenConvertedToPrefix_ThenReturnsMatchingPrefix()
    {
        // Arrange
        var expected = Prefix.k;

        // Act
        var actual = (Prefix)1E3; // implicit conversion

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenAllDefinedPrefixes_WhenInspectingFactors_ThenAllFactorsAreUnique()
    {
        // Arrange
        var factors = Prefix.All.Select(p => p.Factor).ToList();

        // Act
        var distinct = factors.Distinct().Count();

        // Assert
        distinct.Should().Be(factors.Count);
    }
}