using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitIdentityTests
{
    [Fact]
    public void GivenNoneOnLeft_WhenMultiplying_ThenReturnsRightOperand()
    {
        // Arrange
        var expected = Unit.SI.m;

        // Act
        var actual = Unit.None * Unit.SI.m;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenNoneOnRight_WhenMultiplying_ThenReturnsLeftOperand()
    {
        // Arrange
        var expected = Unit.SI.m;

        // Act
        var actual = Unit.SI.m * Unit.None;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionByNone_WhenDividing_ThenReturnsDividend()
    {
        // Arrange
        var expected = Unit.SI.kg;

        // Act
        var actual = Unit.SI.kg / Unit.None; // divisor None => short-circuit

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenLiteralOne_WhenDividingByUnit_ThenCreatesReciprocal()
    {
        // Arrange
        var expected = Unit.None / Unit.SI.m; // structure from operator

        // Act
        var actual = 1 / Unit.SI.m;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenLiteralGreaterThanOne_WhenDividingByUnit_ThenCurrentBehaviorMatchesOneOverUnit()
    {
        // Arrange
        var expected = 1 / Unit.SI.m; // current implementation ignores int value

        // Act
        var actual = 2 / Unit.SI.m;

        // Assert
        actual.Should().Be(expected); // documenting current behavior (potential future change)
    }
}