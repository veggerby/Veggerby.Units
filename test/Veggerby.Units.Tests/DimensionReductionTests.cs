using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionReductionTests
{
    [Fact]
    public void GivenProductWithRepeatedDimension_WhenReducing_ThenConvertsToPower()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 3); // L^3

        // Act
        var actual = (Dimension.Length * (Dimension.Length ^ 2)); // L*L^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductChainWithRepeatedDimensions_WhenReducing_ThenAggregatesToPower()
    {
        // Arrange
        var expected = Dimension.Multiply(Dimension.Power(Dimension.Length, 6), Dimension.Time); // L^6T

        // Act
        var actual = ((Dimension.Length ^ 3) * (Dimension.Length ^ 2) * Dimension.Time * Dimension.Length); // L^3^L^2TL

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWithSameDimensionsDifferentPowers_WhenReducing_ThenSubtractsExponents()
    {
        // Arrange
        var expected = Dimension.Length; // L

        // Act
        var actual = ((Dimension.Length ^ 2) / Dimension.Length); // L^2/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionOfIdenticalDimensions_WhenReducing_ThenCancelsToNone()
    {
        // Arrange
        var expected = Dimension.None; // completely reduced

        // Act
        var actual = (Dimension.Length / Dimension.Length); // L/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWhereDividendHasHigherPower_WhenReducing_ThenResultHasRemainingPositivePower()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 2); // L^2

        // Act
        var actual = ((Dimension.Length ^ 3) / Dimension.Length); // L^3/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWhereDivisorHasHigherPower_WhenReducing_ThenResultIsReciprocalPower()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Power(Dimension.Length, 2)); //  1/L^2

        // Act
        var actual = (Dimension.Length / (Dimension.Length ^ 3)); // L/L^3

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenComplexDimensionExpression_WhenReducing_ThenYieldsExpectedCanonicalForm()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Mass, Dimension.Multiply(Dimension.Time, Dimension.Length)); // M/TL

        // Act
        var actual = (Dimension.Time * (((Dimension.Length ^ 2) * Dimension.Mass) / ((Dimension.Time ^ 2) * (Dimension.Length ^ 3)))); // T*L^2*M/(T^2*L^3)

        // Assert
        actual.Should().Be(expected);
    }
}