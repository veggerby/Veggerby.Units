using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionProductTests
{
    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectType()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        // Assert
        dimension.Should().BeOfType<ProductDimension>();
    }

    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectSymbol()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("LM");
    }

    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectName()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length * mass");
    }

    [Fact]
    public void Dimension_ProductDimension_IsCommutative()
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
    public void Dimension_ProductDimension_IsCommutativeWithMultiple()
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