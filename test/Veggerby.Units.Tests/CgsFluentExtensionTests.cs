using AwesomeAssertions;

using Veggerby.Units.Fluent.CGS;

using Xunit;

namespace Veggerby.Units.Tests;

public class CgsFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingCentimeters_ThenCreatesCentimeterMeasurement()
    {
        // Arrange
        var value = 5.0;

        // Act
        var measurement = value.Centimeters();

        // Assert
        ((double)measurement).Should().Be(5.0);
        measurement.Unit.Should().Be(Unit.CGS.cm);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingCentimeter_ThenCreatesCentimeterMeasurement()
    {
        // Arrange
        var value = 3.5m;

        // Act
        var measurement = value.Centimeter();

        // Assert
        ((decimal)measurement).Should().Be(3.5m);
        measurement.Unit.Should().Be(Unit.CGS.cm);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGrams_ThenCreatesGramMeasurement()
    {
        // Arrange
        var value = 100.0;

        // Act
        var measurement = value.Grams();

        // Assert
        ((double)measurement).Should().Be(100.0);
        measurement.Unit.Should().Be(Unit.CGS.g);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingDynes_ThenCreatesDyneMeasurement()
    {
        // Arrange
        var value = 50.0;

        // Act
        var measurement = value.Dynes();

        // Assert
        ((double)measurement).Should().Be(50.0);
        measurement.Unit.Should().Be(Unit.CGS.dyn);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingErgs_ThenCreatesErgMeasurement()
    {
        // Arrange
        var value = 1000.0;

        // Act
        var measurement = value.Ergs();

        // Assert
        ((double)measurement).Should().Be(1000.0);
        measurement.Unit.Should().Be(Unit.CGS.erg);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingBaryes_ThenCreatesBaryeMeasurement()
    {
        // Arrange
        var value = 10.0;

        // Act
        var measurement = value.Baryes();

        // Assert
        ((double)measurement).Should().Be(10.0);
        measurement.Unit.Should().Be(Unit.CGS.Ba);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingPoise_ThenCreatesPoiseMeasurement()
    {
        // Arrange
        var value = 2.5;

        // Act
        var measurement = value.Poise();

        // Assert
        ((double)measurement).Should().Be(2.5);
        measurement.Unit.Should().Be(Unit.CGS.P);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingStokes_ThenCreatesStokeMeasurement()
    {
        // Arrange
        var value = 7.5;

        // Act
        var measurement = value.Stokes();

        // Assert
        ((double)measurement).Should().Be(7.5);
        measurement.Unit.Should().Be(Unit.CGS.St);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGauss_ThenCreatesGaussMeasurement()
    {
        // Arrange
        var value = 15.0;

        // Act
        var measurement = value.Gauss();

        // Assert
        ((double)measurement).Should().Be(15.0);
        measurement.Unit.Should().Be(Unit.CGS.G);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMaxwells_ThenCreatesMaxwellMeasurement()
    {
        // Arrange
        var value = 100.0;

        // Act
        var measurement = value.Maxwells();

        // Assert
        ((double)measurement).Should().Be(100.0);
        measurement.Unit.Should().Be(Unit.CGS.Mx);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingOersteds_ThenCreatesOerstedMeasurement()
    {
        // Arrange
        var value = 25.0;

        // Act
        var measurement = value.Oersteds();

        // Assert
        ((double)measurement).Should().Be(25.0);
        measurement.Unit.Should().Be(Unit.CGS.Oe);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAbamperes_ThenCreatesAbampereMeasurement()
    {
        // Arrange
        var value = 0.5;

        // Act
        var measurement = value.Abamperes();

        // Assert
        ((double)measurement).Should().Be(0.5);
        measurement.Unit.Should().Be(Unit.CGS.abA);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAbcoulombs_ThenCreatesAbcoulombMeasurement()
    {
        // Arrange
        var value = 3.0;

        // Act
        var measurement = value.Abcoulombs();

        // Assert
        ((double)measurement).Should().Be(3.0);
        measurement.Unit.Should().Be(Unit.CGS.abC);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAbvolts_ThenCreatesAbvoltMeasurement()
    {
        // Arrange
        var value = 1.5;

        // Act
        var measurement = value.Abvolts();

        // Assert
        ((double)measurement).Should().Be(1.5);
        measurement.Unit.Should().Be(Unit.CGS.abV);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAbohms_ThenCreatesAbohmMeasurement()
    {
        // Arrange
        var value = 8.0;

        // Act
        var measurement = value.Abohms();

        // Assert
        ((double)measurement).Should().Be(8.0);
        measurement.Unit.Should().Be(Unit.CGS.abohm);
    }

    [Fact]
    public void GivenSymbolAlias_WhenCallingCm_ThenCreatesCentimeterMeasurement()
    {
        // Arrange & Act
        var measurement = 10.0.cm();

        // Assert
        measurement.Unit.Should().Be(Unit.CGS.cm);
    }

    [Fact]
    public void GivenSymbolAlias_WhenCallingDyn_ThenCreatesDyneMeasurement()
    {
        // Arrange & Act
        var measurement = 5.0.dyn();

        // Assert
        measurement.Unit.Should().Be(Unit.CGS.dyn);
    }

    [Fact]
    public void GivenSymbolAlias_WhenCallingErg_ThenCreatesErgMeasurement()
    {
        // Arrange & Act
        var measurement = 100.0.erg();

        // Assert
        measurement.Unit.Should().Be(Unit.CGS.erg);
    }
}
