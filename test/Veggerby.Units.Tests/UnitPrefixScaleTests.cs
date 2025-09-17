using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitPrefixScaleTests
{
    [Fact]
    public void GivenTwoEquivalentPrefixedUnits_WhenCallingEquals_ThenReturnsTrue()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void GivenTwoEquivalentPrefixedUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenTwoEquivalentScaleUnits_WhenCallingEquals_ThenReturnsTrue()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void GivenTwoEquivalentScaleUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }
}