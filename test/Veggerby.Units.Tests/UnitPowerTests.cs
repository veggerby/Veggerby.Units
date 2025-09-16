using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitPowerTests
{
    [Fact]
    public void GivenUnit_WhenRaisedToPositiveExponent_ThenResultIsPowerUnit()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        // Assert
        unit.Should().BeOfType<PowerUnit>();
    }

    [Fact]
    public void GivenPowerUnit_WhenAccessingSymbol_ThenReturnsExpectedSymbol()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m^2");
    }

    [Fact]
    public void GivenPowerUnit_WhenAccessingName_ThenReturnsExpectedName()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter ^ 2");
    }

    [Fact]
    public void GivenUnit_WhenRaisedToZero_ThenResultIsNone()
    {
        // Arrange
        var expected = Unit.None; // 1 == None

        // Act
        var actual = Unit.SI.s ^ 0; // s^0

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenUnit_WhenRaisedToOne_ThenResultIsBaseUnit()
    {
        // Arrange
        var expected = Unit.SI.s; // s

        // Act
        var actual = Unit.SI.s ^ 1; // s^1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenUnit_WhenRaisedToMinusOne_ThenResultIsReciprocal()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.SI.s); // 1/s

        // Act
        var actual = Unit.SI.s ^ -1; // s^-1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenUnit_WhenRaisedToNegativeExponent_ThenResultIsReciprocalPower()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.s, 2)); // 1/s^2

        // Act
        var actual = Unit.SI.s ^ -2; // s^-2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductUnit_WhenRaisedToExponent_ThenExponentDistributesAcrossFactors()
    {
        // Arrange
        var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2));

        // Act
        var actual = ((Unit.SI.m * Unit.SI.s) ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductWithDivision_WhenRaisedToExponent_ThenExponentDistributesAcrossAllOperands()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2)), Unit.Power(Unit.SI.kg, 2)); // m^2s^2/kg^2;

        // Act
        var actual = ((Unit.SI.m * Unit.SI.s / Unit.SI.kg) ^ 2); // (ms/kg)^2

        // Assert
        actual.Should().Be(expected);
    }
}