using AwesomeAssertions;

using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class AffineConversionTests
{
    [Fact]
    public void GivenCelsiusMeasurement_WhenConvertingToKelvin_ThenAddsOffset()
    {
        // Arrange
        var tempC = new DoubleMeasurement(25d, Unit.SI.C); // 25 °C

        // Act
        var tempK = tempC.ConvertTo(Unit.SI.K);

        // Assert
        ((double)tempK).Should().Be(25d + 273.15d);
        tempK.Unit.Should().Be(Unit.SI.K);
    }

    [Fact]
    public void GivenKelvinMeasurement_WhenConvertingToCelsius_ThenSubtractsOffset()
    {
        // Arrange
        var tempK = new DoubleMeasurement(300d, Unit.SI.K);

        // Act
        var tempC = tempK.ConvertTo(Unit.SI.C);

        // Assert
        ((double)tempC).Should().Be(300d - 273.15d);
        tempC.Unit.Should().Be(Unit.SI.C);
    }

    [Fact]
    public void GivenCelsiusMeasurement_WhenTryConvertMismatchDimension_ThenFalse()
    {
        // Arrange
        var tempC = new DoubleMeasurement(10d, Unit.SI.C);

        // Act
        var ok = tempC.TryConvertTo(Unit.SI.m, out var converted);

        // Assert
        ok.Should().BeFalse();
        converted.Should().BeNull();
    }
}