using System;

using AwesomeAssertions;

using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class CompositeConversionTests
{
    [Fact]
    public void GivenCompatibleUnits_WhenTryConvertTo_ThenReturnsTrueAndOutputsConverted()
    {
        // Arrange
        var distance = new DoubleMeasurement(1.2, Prefix.k * Unit.SI.m); // 1.2 km
        var target = Unit.SI.m;

        // Act
        var ok = distance.TryConvertTo(target, out var converted);

        // Assert
        ok.Should().BeTrue();
        converted.Unit.Should().Be(target);
        ((double)converted).Should().Be(1200d);
    }

    [Fact]
    public void GivenDimensionMismatch_WhenTryConvertTo_ThenReturnsFalseAndResultNull()
    {
        // Arrange
        var length = new DoubleMeasurement(5d, Unit.SI.m);
        var target = Unit.SI.s;

        // Act
        var ok = length.TryConvertTo(target, out var converted);

        // Assert
        ok.Should().BeFalse();
        converted.Should().BeNull();
    }

    [Fact]
    public void GivenSquareFeet_WhenConvertingToSquareMetres_ThenAppliesSquaredScaleFactor()
    {
        // Arrange
        var area = new DoubleMeasurement(3.0, Unit.Imperial.ft ^ 2); // 3 ft^2
        var target = Unit.SI.m ^ 2;
        var expected = 3.0 * Math.Pow(ImperialUnitSystem.FeetToMetres, 2); // 3 * (0.3048^2)

        // Act
        var converted = area.ConvertTo(target);

        // Assert
        ((double)converted).Should().Be(expected);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenGallon_WhenConvertingToCubicMetres_ThenUsesChainedVolumeFactors()
    {
        // Arrange
        var vol = new DoubleMeasurement(1.0, Unit.Imperial.gal); // 1 gallon
        var target = Unit.SI.m ^ 3;

        // Reconstruct expected from definitions:
        // 1 gal = 8 pt; 1 pt = 34.677 in^3; 1 in = 0.0254 m ⇒ 1 in^3 = 0.0254^3 m^3
        var expected = 8d * ImperialUnitSystem.CubicInchToPint * Math.Pow(0.0254, 3); // ≈ 0.00454609 m^3

        // Act
        var converted = vol.ConvertTo(target);

        // Assert
        ((double)converted).Should().Be(expected);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenPoundFoot_WhenConvertingToKilogramMetre_ThenAppliesProductOfScaleFactors()
    {
        // Arrange
        var torqueLike = new DoubleMeasurement(4.0, Unit.Imperial.lb * Unit.Imperial.ft); // 4 lb·ft
        var target = Unit.SI.kg * Unit.SI.m;
        var expected = 4.0 * ImperialUnitSystem.PoundToKilogram * ImperialUnitSystem.FeetToMetres;

        // Act
        var converted = torqueLike.ConvertTo(target);

        // Assert
        ((double)converted).Should().BeApproximately(expected, 1e-12);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenKilometresPerSecond_WhenConvertingToMetresPerSecond_ThenValueIsScaled()
    {
        // Arrange
        var speed = new DoubleMeasurement(2.5, (Prefix.k * Unit.SI.m) / Unit.SI.s); // 2.5 km/s
        var target = Unit.SI.m / Unit.SI.s;
        var expected = 2.5 * 1000d;

        // Act
        var converted = speed.ConvertTo(target);

        // Assert
        ((double)converted).Should().Be(expected);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenMilliGramSquared_WhenConvertingToKiloGramSquared_ThenAppliesExponentToPrefixFactors()
    {
        // Arrange
        var value = new DoubleMeasurement(4d, (Prefix.m * Unit.SI.g) ^ 2); // 4 (mg)^2
        var target = (Prefix.k * Unit.SI.g) ^ 2; // (kg)^2
        // mg factor = 1e-3 g; kg factor = 1e3 g ⇒ (mg)^2 factor = 1e-6, (kg)^2 factor = 1e6
        var expected = 4d * 1e-6 / 1e6; // 4e-12

        // Act
        var converted = value.ConvertTo(target);

        // Assert
        ((double)converted).Should().Be(4e-12);
        ((double)converted).Should().Be(expected);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenDecimalKilometres_WhenConvertingToMetres_ThenProducesDecimalResult()
    {
        // Arrange
        var distance = new DecimalMeasurement(3.25m, Prefix.k * Unit.SI.m); // 3.25 km
        var target = Unit.SI.m;

        // Act
        var converted = distance.ConvertTo(target);

        // Assert
        ((decimal)converted).Should().Be(3250m);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenDecimalTryConvertTo_WhenSuccessful_ThenReturnsTrue()
    {
        // Arrange
        var speed = new DecimalMeasurement(1.5m, (Prefix.k * Unit.SI.m) / Unit.SI.s); // 1.5 km/s
        var target = Unit.SI.m / Unit.SI.s;

        // Act
        var ok = speed.TryConvertTo(target, out var converted);

        // Assert
        ok.Should().BeTrue();
        ((decimal)converted).Should().Be(1500m);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenIntMeasurement_WhenConvertingToPrefixedUnit_ThenRoundsResult()
    {
        // Arrange
        var length = new Int32Measurement(1500, Unit.SI.m); // 1500 m
        var target = Prefix.k * Unit.SI.m; // km

        // Act
        var converted = length.ConvertTo(target);

        // Assert
        // 1500 m = 1.5 km -> rounds to 2
        ((int)converted).Should().Be(2);
        converted.Unit.Should().Be(target);
    }

    [Fact]
    public void GivenDimensionMismatch_WhenConverting_ThenThrows()
    {
        // Arrange
        var length = new DoubleMeasurement(1d, Unit.SI.m);
        var target = Unit.SI.s; // time

        // Act
        var act = () => length.ConvertTo(target);

        // Assert
        act.Should().Throw<MeasurementConversionException>();
    }
}