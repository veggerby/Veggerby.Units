using AwesomeAssertions;

using Veggerby.Units.Fluent.Optics;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class OpticsFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingCandelas_ShouldCreateLuminousIntensityMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var intensity = value.Candelas();

        // Assert
        intensity.Unit.Should().Be(QuantityKinds.LuminousIntensity.CanonicalUnit);
        intensity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingLumens_ShouldCreateLuminousFluxMeasurement()
    {
        // Arrange
        double value = 800.0;

        // Act
        var flux = value.Lumens();

        // Assert
        flux.Unit.Should().Be(QuantityKinds.LuminousFlux.CanonicalUnit);
        flux.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingLux_ShouldCreateIlluminanceMeasurement()
    {
        // Arrange
        double value = 500.0;

        // Act
        var illuminance = value.Lux();

        // Assert
        illuminance.Unit.Should().Be(QuantityKinds.Illuminance.CanonicalUnit);
        illuminance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingRadians_ShouldCreateAngleMeasurement()
    {
        // Arrange
        double value = 3.14159;

        // Act
        var angle = value.Radians();

        // Assert
        angle.Unit.Should().Be(Unit.SI.rad);
        angle.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSteradians_ShouldCreateSolidAngleMeasurement()
    {
        // Arrange
        double value = 1.0;

        // Act
        var solidAngle = value.Steradians();

        // Assert
        solidAngle.Unit.Should().Be(Unit.SI.sr);
        solidAngle.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingWattsPerSquareMeter_ShouldCreateIrradianceMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var irradiance = value.WattsPerSquareMeter();

        // Assert
        irradiance.Unit.Should().Be(QuantityKinds.Irradiance.CanonicalUnit);
        irradiance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingRadiance_ShouldCreateRadianceMeasurement()
    {
        // Arrange
        double value = 50.0;

        // Act
        var radiance = value.Radiance();

        // Assert
        radiance.Unit.Should().Be(QuantityKinds.Radiance.CanonicalUnit);
        radiance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingCandelas_ShouldCreateLuminousIntensityMeasurement()
    {
        // Arrange
        decimal value = 100.0m;

        // Act
        var intensity = value.Candelas();

        // Assert
        intensity.Unit.Should().Be(QuantityKinds.LuminousIntensity.CanonicalUnit);
        intensity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingLuminance_ShouldCreateLuminanceMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var luminance = value.Luminance();

        // Assert
        luminance.Unit.Should().Be(QuantityKinds.Luminance.CanonicalUnit);
        luminance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingRadiantIntensity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 25.0;

        // Act
        var radiantIntensity = value.RadiantIntensity();

        // Assert
        radiantIntensity.Unit.Should().Be(QuantityKinds.RadiantIntensity.CanonicalUnit);
        radiantIntensity.Value.Should().Be(value);
    }
}
