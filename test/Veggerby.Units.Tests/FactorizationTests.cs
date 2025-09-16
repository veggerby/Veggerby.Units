using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests;

public class FactorizationTests
{
    [Fact]
    public void GivenExpressionProducingZeroExponent_WhenReduced_ThenZeroFactorDisappears()
    {
        // Arrange
        var expr = (Unit.SI.m * Unit.SI.s) / (Unit.SI.m * Unit.SI.s); // => 1

        // Act
        var result = expr; // triggers division reduction

        // Assert
        (result == Unit.None).Should().BeTrue();
    }

    [Fact]
    public void GivenPrefixedAndBaseUnits_WhenReduced_ThenTheyAreNotMergedIncorrectly()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m; // kilometre
        var expr = (km * Unit.SI.m) / km; // => m (should not cancel base m)

        // Act
        var result = expr;

        // Assert
        (result == Unit.SI.m).Should().BeTrue();
    }
}
