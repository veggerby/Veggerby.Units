using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DivisionRearrangementTests
{
    [Fact]
    public void GivenNestedDivision_WhenReduced_ThenFlattensAndCancels()
    {
        // Arrange
        var expr = Unit.SI.m / (Unit.SI.s / Unit.SI.m); // m / (s/m) => m^2 / s

        // Act
        var reduced = expr;

        // Assert
        reduced.Should().Be((Unit.SI.m ^ 2) / Unit.SI.s);
    }

    [Fact]
    public void GivenCompositeDivision_WhenReduced_ThenProperCancellation()
    {
        // Arrange
        var expr = (Unit.SI.kg * Unit.SI.m) / (Unit.SI.m / Unit.SI.s); // (kg*m)/(m/s) => kg*s

        // Act
        var reduced = expr;

        // Assert
        reduced.Should().Be(Unit.SI.kg * Unit.SI.s);
    }

    [Fact]
    public void GivenSymmetricRatio_WhenMultiplied_ThenNone()
    {
        // Arrange
        var ratio = Unit.SI.m / Unit.SI.s; // m/s
        var inverse = Unit.SI.s / Unit.SI.m; // s/m

        // Act
        var prod = ratio * inverse;

        // Assert
        prod.Should().Be(Unit.None);
    }

    [Fact]
    public void GivenProductQuotientSymmetry_WhenReduced_ThenNone()
    {
        // Arrange
        var expr = (Unit.SI.m * Unit.SI.s) / (Unit.SI.s * Unit.SI.m);

        // Act
        var reduced = expr;

        // Assert
        reduced.Should().Be(Unit.None);
    }
}