using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionDivisionTests
{
    [Fact]
    public void GivenTwoDimensions_WhenDividing_ThenResultIsDivisionDimension()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Mass;

        // Act
        // Assert
        dimension.Should().BeOfType<DivisionDimension>();
    }

    [Fact]
    public void GivenDivisionDimension_WhenAccessingSymbol_ThenReturnsQuotientSymbol()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Time;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L/T");
    }

    [Fact]
    public void GivenDivisionDimension_WhenAccessingName_ThenReturnsQuotientName()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Time;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length / time");
    }

    [Fact]
    public void GivenDivisionOfDivision_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Length, Dimension.Time), Dimension.Multiply(Dimension.Mass, Dimension.ElectricCurrent)); // LT/MI

        // Act
        var actual = (Dimension.Length / Dimension.Mass) / (Dimension.ElectricCurrent / Dimension.Time); // (L/M)/(I/T)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenSimpleDividedByDivision_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Length, Dimension.Time), Dimension.ElectricCurrent); // LT/I

        // Act
        var actual = Dimension.Length / (Dimension.ElectricCurrent / Dimension.Time); // L/(I/T)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionDividedBySimple_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Length, Dimension.Multiply(Dimension.Time, Dimension.ElectricCurrent)); // L/TI

        // Act
        var actual = (Dimension.Length / Dimension.ElectricCurrent) / Dimension.Time; // (L/I)/T

        // Assert
        actual.Should().Be(expected);
    }
}