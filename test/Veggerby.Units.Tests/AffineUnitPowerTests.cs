using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class AffineUnitPowerTests
{
    [Fact]
    public void GivenAffineUnit_WhenRaisedToSecondPower_ThenThrows()
    {
        // Arrange
        var c = Unit.SI.C; // affine Celsius

        // Act
        var act = () => _ = c ^ 2;

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenAffineUnit_WhenRaisedToNegativePower_ThenThrows()
    {
        // Arrange
        var c = Unit.SI.C;

        // Act
        var act = () => _ = c ^ -1; // invokes reciprocal path after exponent guard

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenAffineUnit_WhenRaisedToFirstPower_ThenReturnsSameInstance()
    {
        // Arrange
        var c = Unit.SI.C;

        // Act
        var r = c ^ 1;

        // Assert
        r.Should().Be(c);
    }

    [Fact]
    public void GivenAffineUnit_WhenRaisedToZeroPower_ThenReturnsNone()
    {
        // Arrange
        var c = Unit.SI.C;

        // Act
        var r = c ^ 0;

        // Assert
        r.Should().Be(Unit.None);
    }
}