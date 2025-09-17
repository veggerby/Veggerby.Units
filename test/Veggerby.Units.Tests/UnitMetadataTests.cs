using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitMetadataTests
{
    [Fact]
    public void GivenMetreUnit_WhenAccessingSymbol_ThenReturnsM()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m");
    }

    [Fact]
    public void GivenMetreUnit_WhenAccessingName_ThenReturnsMeter()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter");
    }

    [Fact]
    public void GivenMetreUnit_WhenAccessingSystem_ThenReturnsSiSystem()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void GivenMetreUnit_WhenAccessingDimension_ThenReturnsLengthDimension()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Length);
    }
}