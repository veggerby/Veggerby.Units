using AwesomeAssertions;

using Veggerby.Units.Fluent.Imperial;

using Xunit;

namespace Veggerby.Units.Tests;

public class ImperialVolumeFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingGallons_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 10.0;

        // Act
        var volume = value.Gallons();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.gal);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingQuarts_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 8.0;

        // Act
        var volume = value.Quarts();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.qt);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingPints_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 16.0;

        // Act
        var volume = value.Pints();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.pt);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingFluidOunces_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 128.0;

        // Act
        var volume = value.FluidOunces();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.fl_oz);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGills_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 4.0;

        // Act
        var volume = value.Gills();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.gi);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingPecks_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 2.0;

        // Act
        var volume = value.Pecks();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.peck);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingBushels_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var volume = value.Bushels();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.bushel);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingBarrels_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 1.0;

        // Act
        var volume = value.Barrels();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.barrel);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingGallons_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        decimal value = 10.5m;

        // Act
        var volume = value.Gallons();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.gal);
        volume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGallonAlias_ShouldCreateVolumeMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var volume = value.Gallon();

        // Assert
        volume.Unit.Should().Be(Unit.Imperial.gal);
        volume.Value.Should().Be(value);
    }
}
