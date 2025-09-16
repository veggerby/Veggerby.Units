using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class EqualityTests
{
    [Fact]
    public void Given_SameBaseUnits_When_Compared_Then_AreEqual()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.m;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }
}