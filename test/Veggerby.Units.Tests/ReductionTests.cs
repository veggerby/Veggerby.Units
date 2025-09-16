using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

// Tests explicitly demonstrating unit reduction / cancellation scenarios
public class ReductionTests
{
    [Fact]
    public void Unit_ProductAndDivisionWithCancellation_ReducesToSingleUnit()
    {
        // Arrange
        var expected = Unit.SI.kg; // m * (kg / m) => kg

        // Act
        var actual = Unit.SI.m * (Unit.SI.kg / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductDivisionChain_ReducesToSingleUnit()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * kg / s) * (s / m) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.kg / Unit.SI.s) * (Unit.SI.s / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductAndDivisionMultipleCancellation_ReducesToRemaining()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * s * kg)/(m * s) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.s * Unit.SI.kg) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_NestedCancellation_ReducesToNone()
    {
        // Arrange
        var expected = Unit.None; // (m * s)/(m * s) => 1

        // Act
        var actual = (Unit.SI.m * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductWithPowerCancellation_ReducesCompletely()
    {
        // Arrange
        var expected = Unit.None; // (m * m^2) / m^3 => 1

        // Act
        var actual = (Unit.SI.m * (Unit.SI.m ^ 2)) / (Unit.SI.m ^ 3);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductWithPartialPowerCancellation_ReducesExponent()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^4)/(m^2) => m^2

        // Act
        var actual = (Unit.SI.m ^ 4) / (Unit.SI.m ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_MixedProductPowerDivision_ReducesToPower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^3 * s)/(m * s) => m^2

        // Act
        var actual = ((Unit.SI.m ^ 3) * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }
}
