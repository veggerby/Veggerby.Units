using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionReductionTests
{
    [Fact]
    public void Dimension_MultipleIdenticalOperandsForProduct_ReduceToPower()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 3); // L^3

        // Act
        var actual = (Dimension.Length * (Dimension.Length ^ 2)); // L*L^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
    {
        // Arrange
        var expected = Dimension.Multiply(Dimension.Power(Dimension.Length, 6), Dimension.Time); // L^6T

        // Act
        var actual = ((Dimension.Length ^ 3) * (Dimension.Length ^ 2) * Dimension.Time * Dimension.Length); // L^3^L^2TL

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperands_ShouldReduceOperands()
    {
        // Arrange
        var expected = Dimension.Length; // L

        // Act
        var actual = ((Dimension.Length ^ 2) / Dimension.Length); // L^2/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
    {
        // Arrange
        var expected = Dimension.None; // completely reduced

        // Act
        var actual = (Dimension.Length / Dimension.Length); // L/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 2); // L^2

        // Act
        var actual = ((Dimension.Length ^ 3) / Dimension.Length); // L^3/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Power(Dimension.Length, 2)); //  1/L^2

        // Act
        var actual = (Dimension.Length / (Dimension.Length ^ 3)); // L/L^3

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_ComplexReduction_YieldsExpected()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Mass, Dimension.Multiply(Dimension.Time, Dimension.Length)); // M/TL

        // Act
        var actual = (Dimension.Time * (((Dimension.Length ^ 2) * Dimension.Mass) / ((Dimension.Time ^ 2) * (Dimension.Length ^ 3)))); // T*L^2*M/(T^2*L^3)

        // Assert
        actual.Should().Be(expected);
    }
}