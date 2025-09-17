using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class EqualityTests
{
    [Fact]
    public void GivenSameBaseUnits_WhenCompared_ThenAreEqual()
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