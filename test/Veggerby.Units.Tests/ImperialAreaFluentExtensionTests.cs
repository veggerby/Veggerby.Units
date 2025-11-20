using AwesomeAssertions;

using Veggerby.Units.Fluent.Imperial;

using Xunit;

namespace Veggerby.Units.Tests;

public class ImperialAreaFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingSquareInches_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var area = value.SquareInches();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.sq_in);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSquareFeet_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 50.0;

        // Act
        var area = value.SquareFeet();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.sq_ft);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSquareYards_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 10.0;

        // Act
        var area = value.SquareYards();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.sq_yd);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAcres_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var area = value.Acres();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.acre);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSquareMiles_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 1.0;

        // Act
        var area = value.SquareMiles();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.sq_mi);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingRoods_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 4.0;

        // Act
        var area = value.Roods();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.rood);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingPerches_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 40.0;

        // Act
        var area = value.Perches();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.perch);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingAcres_ShouldCreateAreaMeasurement()
    {
        // Arrange
        decimal value = 2.5m;

        // Act
        var area = value.Acres();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.acre);
        area.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSquareInchAlias_ShouldCreateAreaMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var area = value.SquareInch();

        // Assert
        area.Unit.Should().Be(Unit.Imperial.sq_in);
        area.Value.Should().Be(value);
    }
}
