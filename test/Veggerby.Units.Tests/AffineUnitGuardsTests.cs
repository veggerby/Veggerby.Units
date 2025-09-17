using System;

using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class AffineUnitGuardsTests
{
    [Fact]
    public void GivenCelsiusAndMetre_WhenMultiplying_ThenThrowsUnitException()
    {
        // Arrange
        var c = new DoubleMeasurement(1, Unit.SI.C);
        var m = Unit.SI.m;

        // Act
        var act = () => { var _ = c.Unit * m; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenFahrenheit_WhenExponentiating_ThenThrowsUnitException()
    {
        // Arrange
        var f = Unit.Imperial.F;

        // Act
        var act = () => { var _ = f ^ 2; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenPrefixAppliedToCelsius_WhenScaling_ThenThrowsUnitException()
    {
        // Arrange
        var c = Unit.SI.C;

        // Act
        var act = () => { var _ = 2 * c; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenCelsiusEqualToSelf_WhenComparing_ThenTrue()
    {
        // Arrange
        var c1 = Unit.SI.C;
        var c2 = Unit.SI.C;

        // Act / Assert
        (c1 == c2).Should().BeTrue();
        (c1 != c2).Should().BeFalse();
    }

    [Fact]
    public void GivenCelsiusAndFahrenheit_WhenComparing_ThenNotEqual()
    {
        // Arrange
        var c = Unit.SI.C;
        var f = Unit.Imperial.F;

        // Act / Assert
        (c == f).Should().BeFalse();
        (c != f).Should().BeTrue();
    }
}