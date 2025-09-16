using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitProductTests
{
    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectType()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        // Assert
        unit.Should().BeOfType<ProductUnit>();
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectSymbol()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("mkg");
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectName()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter * kilogram");
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectDimension()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Mass * Dimension.Length);
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectSystem()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void Unit_ProductUnit_IsCommutative()
    {
        // Arrange
        var d1 = Unit.SI.m * Unit.SI.kg;
        var d2 = Unit.SI.kg * Unit.SI.m;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Unit_ProductUnit_IsCommutativeWithMultiple()
    {
        // Arrange
        var d1 = Unit.SI.A * (Unit.SI.m * Unit.SI.kg);
        var d2 = (Unit.SI.kg * Unit.SI.A) * Unit.SI.m;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }
}
