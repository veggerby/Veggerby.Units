using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitProductTests
{
    [Fact]
    public void GivenTwoUnits_WhenMultiplying_ThenResultIsProductUnit()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        // Assert
        unit.Should().BeOfType<ProductUnit>();
    }

    [Fact]
    public void GivenProductUnit_WhenAccessingSymbol_ThenConcatenatesOperandSymbols()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("mkg");
    }

    [Fact]
    public void GivenProductUnit_WhenAccessingName_ThenJoinsOperandNamesWithAsterisk()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter * kilogram");
    }

    [Fact]
    public void GivenProductUnit_WhenAccessingDimension_ThenMultipliesOperandDimensions()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Mass * Dimension.Length);
    }

    [Fact]
    public void GivenProductUnit_WhenAccessingSystem_ThenUsesFirstOperandSystem()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void GivenTwoUnits_WhenMultiplyingInDifferentOrders_ThenProductsAreEqual()
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
    public void GivenThreeUnits_WhenAssociativelyAndCommutativelyRearranged_ThenProductsAreEqual()
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