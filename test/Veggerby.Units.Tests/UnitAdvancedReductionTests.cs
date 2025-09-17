using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitAdvancedReductionTests
{
    [Fact]
    public void GivenDivisionWithinDivision_WhenReassociated_ThenInnerDivisionCancels()
    {
        // Arrange
        var expected = Unit.SI.s; // m / (m / s) => s

        // Act
        var actual = Unit.SI.m / (Unit.SI.m / Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenNestedProductDivisionWithSharedFactors_WhenReducing_ThenDeepCancellationOccurs()
    {
        // Arrange
        var expected = Unit.SI.m / Unit.SI.s; // (m * (kg / (kg * s))) => m/s

        // Act
        var actual = Unit.SI.m * (Unit.SI.kg / (Unit.SI.kg * Unit.SI.s));

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenInterleavedPositiveAndNegativePowers_WhenReducing_ThenExponentsCombine()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // m^2 * m * m^-1 => m^2

        // Act
        var actual = (Unit.SI.m ^ 2) * Unit.SI.m * (Unit.SI.m ^ -1);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenTwoProductsWithSharedFactors_WhenDivided_ThenSharedFactorsCancel()
    {
        // Arrange
        var expected = Unit.SI.s / Unit.SI.kg; // (m*s)/(m*kg)

        // Act
        var actual = (Unit.SI.m * Unit.SI.s) / (Unit.SI.m * Unit.SI.kg);

        // Assert
        actual.Should().Be(expected);
    }
}