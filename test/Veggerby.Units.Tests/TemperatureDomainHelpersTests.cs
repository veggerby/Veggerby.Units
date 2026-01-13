using System;

using AwesomeAssertions;

using Veggerby.Units.Calculations;
using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class TemperatureDomainHelpersTests
{
    [Fact]
    public void GivenCelsiusTemperatureAndHumidity_WhenDewPointCalculated_ThenReturnsCelsius()
    {
        // Arrange
        var temp = new DoubleMeasurement(20.0, Unit.SI.C); // 20°C
        var rh = 50.0; // 50% RH

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Unit.Should().Be(Unit.SI.C);
        dewPoint.Value.Should().BeApproximately(9.3, 0.1); // Expected dew point ~9.3°C
    }

    [Fact]
    public void GivenFahrenheitTemperatureAndHumidity_WhenDewPointCalculated_ThenReturnsFahrenheit()
    {
        // Arrange
        var temp = new DoubleMeasurement(68.0, Unit.Imperial.F); // 68°F ≈ 20°C
        var rh = 50.0;

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Unit.Should().Be(Unit.Imperial.F);
        dewPoint.Value.Should().BeApproximately(48.7, 0.2); // ~9.3°C in Fahrenheit
    }

    [Fact]
    public void GivenKelvinTemperatureAndHumidity_WhenDewPointCalculated_ThenReturnsKelvin()
    {
        // Arrange
        var temp = new DoubleMeasurement(293.15, Unit.SI.K); // 20°C
        var rh = 50.0;

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Unit.Should().Be(Unit.SI.K);
        dewPoint.Value.Should().BeApproximately(282.45, 0.1); // ~9.3°C in Kelvin
    }

    [Fact]
    public void GivenHighHumidity_WhenDewPointCalculated_ThenDewPointNearTemperature()
    {
        // Arrange
        var temp = new DoubleMeasurement(25.0, Unit.SI.C);
        var rh = 95.0; // Very high humidity

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeApproximately(24.1, 0.2); // Dew point very close to temperature
    }

    [Fact]
    public void GivenLowHumidity_WhenDewPointCalculated_ThenDewPointMuchLower()
    {
        // Arrange
        var temp = new DoubleMeasurement(30.0, Unit.SI.C);
        var rh = 20.0; // Low humidity

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeApproximately(4.6, 1.0); // Dew point significantly lower
    }

    [Fact]
    public void GivenNegativeHumidity_WhenDewPointCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(20.0, Unit.SI.C);
        var rh = -10.0;

        // Act
        var act = () => TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenHumidityOver100_WhenDewPointCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(20.0, Unit.SI.C);
        var rh = 105.0;

        // Act
        var act = () => TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenZeroHumidity_WhenDewPointCalculated_ThenReturnsVeryLowDewPoint()
    {
        // Arrange
        var temp = new DoubleMeasurement(20.0, Unit.SI.C);
        var rh = 1.0; // Nearly zero humidity (use 1% to avoid log(0))

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeLessThan(-20.0); // Very low dew point
    }

    [Fact]
    public void Given100PercentHumidity_WhenDewPointCalculated_ThenEqualsTemperature()
    {
        // Arrange
        var temp = new DoubleMeasurement(15.0, Unit.SI.C);
        var rh = 100.0;

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeApproximately(15.0, 0.01); // Dew point equals temperature
    }

    [Fact]
    public void GivenHighTempAndHighHumidity_WhenHeatIndexCalculated_ThenReturnsHigherFeelsLike()
    {
        // Arrange
        var temp = new DoubleMeasurement(95.0, Unit.Imperial.F); // Hot day
        var rh = 60.0; // Moderate humidity

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Unit.Should().Be(Unit.Imperial.F);
        heatIndex.Value.Should().BeGreaterThan(temp.Value); // Feels hotter than actual
        heatIndex.Value.Should().BeApproximately(114.0, 5.0); // Expected heat index
    }

    [Fact]
    public void GivenCelsiusHighTemp_WhenHeatIndexCalculated_ThenReturnsCelsius()
    {
        // Arrange
        var temp = new DoubleMeasurement(35.0, Unit.SI.C); // ~95°F
        var rh = 60.0;

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Unit.Should().Be(Unit.SI.C);
        heatIndex.Value.Should().BeGreaterThan(temp.Value);
    }

    [Fact]
    public void GivenKelvinHighTemp_WhenHeatIndexCalculated_ThenReturnsKelvin()
    {
        // Arrange
        var temp = new DoubleMeasurement(308.15, Unit.SI.K); // 35°C
        var rh = 60.0;

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Unit.Should().Be(Unit.SI.K);
        heatIndex.Value.Should().BeGreaterThan(temp.Value);
    }

    [Fact]
    public void GivenLowTemp_WhenHeatIndexCalculated_ThenReturnsOriginalTemp()
    {
        // Arrange
        var temp = new DoubleMeasurement(70.0, Unit.Imperial.F); // Below 80°F threshold
        var rh = 60.0;

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Value.Should().Be(temp.Value); // Returns original temperature
    }

    [Fact]
    public void GivenLowHumidityHighTemp_WhenHeatIndexCalculated_ThenReturnsOriginalTemp()
    {
        // Arrange
        var temp = new DoubleMeasurement(95.0, Unit.Imperial.F);
        var rh = 30.0; // Below 40% threshold

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Value.Should().Be(temp.Value); // Returns original temperature
    }

    [Fact]
    public void GivenNegativeHumidity_WhenHeatIndexCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(95.0, Unit.Imperial.F);
        var rh = -10.0;

        // Act
        var act = () => TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenHumidityOver100_WhenHeatIndexCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(95.0, Unit.Imperial.F);
        var rh = 110.0;

        // Act
        var act = () => TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenExtremeConditions_WhenHeatIndexCalculated_ThenReturnsVeryHighValue()
    {
        // Arrange
        var temp = new DoubleMeasurement(100.0, Unit.Imperial.F);
        var rh = 90.0; // Extreme heat and humidity

        // Act
        var heatIndex = TemperatureDomainHelpers.HeatIndex(temp, rh);

        // Assert
        heatIndex.Value.Should().BeGreaterThan(140.0); // Dangerously high heat index
    }

    [Fact]
    public void GivenCelsiusTemperatureAndHumidity_WhenHumidexCalculated_ThenReturnsCelsius()
    {
        // Arrange
        var temp = new DoubleMeasurement(30.0, Unit.SI.C);
        var rh = 70.0;

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        humidex.Unit.Should().Be(Unit.SI.C);
        humidex.Value.Should().BeGreaterThan(temp.Value); // Feels hotter
        humidex.Value.Should().BeApproximately(41.0, 2.0); // Expected humidex
    }

    [Fact]
    public void GivenFahrenheitTemperature_WhenHumidexCalculated_ThenReturnsFahrenheit()
    {
        // Arrange
        var temp = new DoubleMeasurement(86.0, Unit.Imperial.F); // 30°C
        var rh = 70.0;

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        humidex.Unit.Should().Be(Unit.Imperial.F);
        humidex.Value.Should().BeGreaterThan(temp.Value);
    }

    [Fact]
    public void GivenKelvinTemperature_WhenHumidexCalculated_ThenReturnsKelvin()
    {
        // Arrange
        var temp = new DoubleMeasurement(303.15, Unit.SI.K); // 30°C
        var rh = 70.0;

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        humidex.Unit.Should().Be(Unit.SI.K);
        humidex.Value.Should().BeGreaterThan(temp.Value);
    }

    [Fact]
    public void GivenLowHumidity_WhenHumidexCalculated_ThenHumidexNearTemperature()
    {
        // Arrange
        var temp = new DoubleMeasurement(25.0, Unit.SI.C);
        var rh = 20.0; // Low humidity

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        var difference = humidex.Value - temp.Value;
        difference.Should().BeLessThan(5.0); // Small difference at low humidity
    }

    [Fact]
    public void GivenHighHumidity_WhenHumidexCalculated_ThenHumidexMuchHigher()
    {
        // Arrange
        var temp = new DoubleMeasurement(30.0, Unit.SI.C);
        var rh = 90.0; // High humidity

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        var difference = humidex.Value - temp.Value;
        difference.Should().BeGreaterThan(10.0); // Significant difference at high humidity
    }

    [Fact]
    public void GivenNegativeHumidity_WhenHumidexCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(25.0, Unit.SI.C);
        var rh = -5.0;

        // Act
        var act = () => TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenHumidityOver100_WhenHumidexCalculated_ThenThrows()
    {
        // Arrange
        var temp = new DoubleMeasurement(25.0, Unit.SI.C);
        var rh = 105.0;

        // Act
        var act = () => TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenBoundaryHumidity_WhenHumidexCalculated_ThenSucceeds()
    {
        // Arrange
        var temp = new DoubleMeasurement(20.0, Unit.SI.C);

        // Act & Assert - 0% humidity
        var humidex0 = TemperatureDomainHelpers.Humidex(temp, 0.0);
        humidex0.Value.Should().BeLessThanOrEqualTo(temp.Value); // Can be lower due to vapor pressure

        // Act & Assert - 100% humidity
        var humidex100 = TemperatureDomainHelpers.Humidex(temp, 100.0);
        humidex100.Value.Should().BeGreaterThan(temp.Value);
    }

    [Fact]
    public void GivenColdTemperature_WhenHumidexCalculated_ThenReturnsReasonableValue()
    {
        // Arrange
        var temp = new DoubleMeasurement(5.0, Unit.SI.C); // Cold temperature
        var rh = 60.0;

        // Act
        var humidex = TemperatureDomainHelpers.Humidex(temp, rh);

        // Assert
        humidex.Unit.Should().Be(Unit.SI.C);
        // Humidex at cold temps is less meaningful but should still compute
        double.IsFinite(humidex.Value).Should().BeTrue();
    }

    [Fact]
    public void GivenSameConditionsInDifferentScales_WhenDewPointCalculated_ThenResultsEquivalent()
    {
        // Arrange
        var tempC = new DoubleMeasurement(20.0, Unit.SI.C);
        var tempF = new DoubleMeasurement(68.0, Unit.Imperial.F);
        var rh = 50.0;

        // Act
        var dewPointC = TemperatureDomainHelpers.DewPoint(tempC, rh);
        var dewPointF = TemperatureDomainHelpers.DewPoint(tempF, rh);

        // Assert - convert both to Celsius for comparison
        var dewPointFtoC = dewPointF.ConvertTo(Unit.SI.C);
        dewPointC.Value.Should().BeApproximately(dewPointFtoC.Value, 0.1);
    }

    [Fact]
    public void GivenSameConditionsInDifferentScales_WhenHeatIndexCalculated_ThenResultsEquivalent()
    {
        // Arrange
        var tempC = new DoubleMeasurement(35.0, Unit.SI.C);
        var tempF = new DoubleMeasurement(95.0, Unit.Imperial.F);
        var rh = 60.0;

        // Act
        var heatIndexC = TemperatureDomainHelpers.HeatIndex(tempC, rh);
        var heatIndexF = TemperatureDomainHelpers.HeatIndex(tempF, rh);

        // Assert - convert both to Celsius for comparison
        var heatIndexFtoC = heatIndexF.ConvertTo(Unit.SI.C);
        heatIndexC.Value.Should().BeApproximately(heatIndexFtoC.Value, 0.5);
    }

    [Fact]
    public void GivenSameConditionsInDifferentScales_WhenHumidexCalculated_ThenResultsEquivalent()
    {
        // Arrange
        var tempC = new DoubleMeasurement(30.0, Unit.SI.C);
        var tempK = new DoubleMeasurement(303.15, Unit.SI.K);
        var rh = 70.0;

        // Act
        var humidexC = TemperatureDomainHelpers.Humidex(tempC, rh);
        var humidexK = TemperatureDomainHelpers.Humidex(tempK, rh);

        // Assert - convert both to Celsius for comparison
        var humidexKtoC = humidexK.ConvertTo(Unit.SI.C);
        humidexC.Value.Should().BeApproximately(humidexKtoC.Value, 0.1);
    }

    [Fact]
    public void GivenExtremeColdTemperature_WhenDewPointCalculated_ThenHandlesGracefully()
    {
        // Arrange
        var temp = new DoubleMeasurement(-40.0, Unit.SI.C); // Extreme cold
        var rh = 50.0;

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeLessThan(temp.Value);
        double.IsFinite(dewPoint.Value).Should().BeTrue();
    }

    [Fact]
    public void GivenExtremeHotTemperature_WhenDewPointCalculated_ThenHandlesGracefully()
    {
        // Arrange
        var temp = new DoubleMeasurement(50.0, Unit.SI.C); // Extreme heat
        var rh = 30.0;

        // Act
        var dewPoint = TemperatureDomainHelpers.DewPoint(temp, rh);

        // Assert
        dewPoint.Value.Should().BeLessThan(temp.Value);
        double.IsFinite(dewPoint.Value).Should().BeTrue();
    }
}
