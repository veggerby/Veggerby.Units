using AwesomeAssertions;

using Veggerby.Units.Fluent.Imperial;

using Xunit;

namespace Veggerby.Units.Tests;

public class ImperialLengthExtensionsExpandedTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingYards_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var length = value.Yards();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.ya);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingYardAlias_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var length = value.Yard();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.ya);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingFathoms_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 10.0;

        // Act
        var length = value.Fathoms();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.fathom);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingFathomAlias_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 10.0;

        // Act
        var length = value.Fathom();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.fathom);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingNauticalMiles_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var length = value.NauticalMiles();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.nmi);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingNauticalMileAlias_ShouldCreateLengthMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var length = value.NauticalMile();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.nmi);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingYards_ShouldCreateLengthMeasurement()
    {
        // Arrange
        decimal value = 50.5m;

        // Act
        var length = value.Yards();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.ya);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingFathoms_ShouldCreateLengthMeasurement()
    {
        // Arrange
        decimal value = 20.5m;

        // Act
        var length = value.Fathoms();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.fathom);
        length.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingNauticalMiles_ShouldCreateLengthMeasurement()
    {
        // Arrange
        decimal value = 3.5m;

        // Act
        var length = value.NauticalMiles();

        // Assert
        length.Unit.Should().Be(Unit.Imperial.nmi);
        length.Value.Should().Be(value);
    }
}
