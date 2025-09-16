using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitMetadataTests
{
    [Fact]
    public void Unit_UnitMeter_TestSymbol()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m");
    }

    [Fact]
    public void Unit_UnitMeter_TestName()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter");
    }

    [Fact]
    public void Unit_UnitMeter_TestSystem()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void Unit_UnitMeter_TestDimension()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Length);
    }
}