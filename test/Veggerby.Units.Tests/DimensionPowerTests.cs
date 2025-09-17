using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionPowerTests
{
    [Fact]
    public void GivenDimension_WhenRaisedToPositiveExponent_ThenResultIsPowerDimension()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        // Assert
        dimension.Should().BeOfType<PowerDimension>();
    }

    [Fact]
    public void GivenPowerDimension_WhenAccessingSymbol_ThenReturnsExpectedSymbol()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L^2");
    }

    [Fact]
    public void GivenPowerDimension_WhenAccessingName_ThenReturnsExpectedName()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length ^ 2");
    }

    [Fact]
    public void GivenDimension_WhenRaisedToZero_ThenResultIsNone()
    {
        // Arrange
        var expected = Dimension.None; // 1 == None

        // Act
        var actual = Dimension.Time ^ 0; // T^0

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDimension_WhenRaisedToOne_ThenResultIsBaseDimension()
    {
        // Arrange
        var expected = Dimension.Time; // T

        // Act
        var actual = Dimension.Time ^ 1; // T^1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDimension_WhenRaisedToMinusOne_ThenResultIsReciprocal()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Time); // 1/T

        // Act
        var actual = Dimension.Time ^ -1; // T^-1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDimension_WhenRaisedToNegativeExponent_ThenResultIsReciprocalPower()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Power(Dimension.Time, 2)); // 1/T^2

        // Act
        var actual = Dimension.Time ^ -2; // T^-2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductDimension_WhenRaisedToExponent_ThenExponentDistributesAcrossFactors()
    {
        // Arrange
        var expected = Dimension.Multiply(Dimension.Power(Dimension.Length, 2), Dimension.Power(Dimension.Time, 2));

        // Act
        var actual = ((Dimension.Length * Dimension.Time) ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductWithDivision_WhenRaisedToExponent_ThenExponentDistributesAcrossAllOperands()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Power(Dimension.Length, 2), Dimension.Power(Dimension.Time, 2)), Dimension.Power(Dimension.Mass, 2)); // L^2T^2/M^2;

        // Act
        var actual = ((Dimension.Length * Dimension.Time / Dimension.Mass) ^ 2); // (LT/M)^2

        // Assert
        actual.Should().Be(expected);
    }
}