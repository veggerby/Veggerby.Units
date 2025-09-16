using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionProductTests
{
    [Fact]
    public void GivenTwoDimensions_WhenMultiplying_ThenResultIsProductDimension()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        // Assert
        dimension.Should().BeOfType<ProductDimension>();
    }

    [Fact]
    public void GivenProductDimension_WhenAccessingSymbol_ThenConcatenatesOperandSymbols()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("LM");
    }

    [Fact]
    public void GivenProductDimension_WhenAccessingName_ThenJoinsOperandNamesWithAsterisk()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length * mass");
    }

    [Fact]
    public void GivenTwoDimensions_WhenMultiplyingInDifferentOrders_ThenProductsAreEqual()
    {
        // Arrange
        var d1 = Dimension.Length * Dimension.Mass;
        var d2 = Dimension.Mass * Dimension.Length;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenThreeDimensions_WhenAssociativelyAndCommutativelyRearranged_ThenProductsAreEqual()
    {
        // Arrange
        var d1 = Dimension.ElectricCurrent * (Dimension.Length * Dimension.Mass);
        var d2 = (Dimension.Mass * Dimension.ElectricCurrent) * Dimension.Length;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }
}