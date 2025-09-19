using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class Advanced3InferenceTests
{
    [Fact]
    public void GivenForceAndLength_WhenDivided_ThenStiffness()
    {
        // Arrange
        var force = new Quantity<double>(new DoubleMeasurement(1, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var length = new Quantity<double>(new DoubleMeasurement(1, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var result = force / length;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.Stiffness);
    }

    [Fact]
    public void GivenStiffnessAndLength_WhenMultiplied_ThenForce()
    {
        // Arrange
        var k = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Stiffness.CanonicalUnit), QuantityKinds.Stiffness);
        var length = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var result = k * length;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.Force);
    }

    [Fact]
    public void GivenLengthAndForce_WhenDivided_ThenCompliance()
    {
        // Arrange
        var length = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);
        var force = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);

        // Act
        var result = length / force;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.Compliance);
    }

    [Fact]
    public void GivenIlluminanceAndTime_WhenMultiplied_ThenLuminousExposure()
    {
        // Arrange
        var illuminance = new Quantity<double>(new DoubleMeasurement(100, QuantityKinds.Illuminance.CanonicalUnit), QuantityKinds.Illuminance);
        var time = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var result = illuminance * time;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.LuminousExposure);
    }

    [Fact]
    public void GivenIrradianceAndTime_WhenMultiplied_ThenRadiantExposure()
    {
        // Arrange
        var irradiance = new Quantity<double>(new DoubleMeasurement(50, QuantityKinds.Irradiance.CanonicalUnit), QuantityKinds.Irradiance);
        var time = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var result = irradiance * time;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.RadiantExposure);
    }

    [Fact]
    public void GivenVelocityAndLength_WhenMultiplied_ThenCirculation()
    {
        // Arrange
        var velocity = new Quantity<double>(new DoubleMeasurement(4, QuantityKinds.Velocity.CanonicalUnit), QuantityKinds.Velocity);
        var length = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var result = velocity * length;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.Circulation);
    }

    [Fact]
    public void GivenRadioactivityAndVolume_WhenDivided_ThenActivityConcentration()
    {
        // Arrange
        var radio = new Quantity<double>(new DoubleMeasurement(1000, QuantityKinds.Radioactivity.CanonicalUnit), QuantityKinds.Radioactivity);
        var volume = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Volume.CanonicalUnit), QuantityKinds.Volume);

        // Act
        var result = radio / volume;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.ActivityConcentration);
    }

    [Fact]
    public void GivenFluenceAndTime_WhenDivided_ThenFluenceRate()
    {
        // Arrange
        var fluence = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.Fluence.CanonicalUnit), QuantityKinds.Fluence);
        var time = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var result = fluence / time;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.FluenceRate);
    }

    [Fact]
    public void GivenVoltageAndTemperatureDelta_WhenDivided_ThenSeebeckCoefficient()
    {
        // Arrange
        var voltage = new Quantity<double>(new DoubleMeasurement(0.01, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);
        var dT = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.TemperatureDelta.CanonicalUnit), QuantityKinds.TemperatureDelta);

        // Act
        var result = voltage / dT;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.SeebeckCoefficient);
    }

    [Fact]
    public void GivenStrainAndTemperatureDelta_WhenDivided_ThenCoefficientOfThermalExpansion()
    {
        // Arrange
        var strain = new Quantity<double>(new DoubleMeasurement(0.001, QuantityKinds.Strain.CanonicalUnit), QuantityKinds.Strain);
        var dT = new Quantity<double>(new DoubleMeasurement(20, QuantityKinds.TemperatureDelta.CanonicalUnit), QuantityKinds.TemperatureDelta);

        // Act
        var result = strain / dT;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.CoefficientOfThermalExpansion);
    }

    [Fact]
    public void GivenLinearAttenuationCoefficientAndLength_WhenMultiplied_ThenOpticalDepth()
    {
        // Arrange
        var mu = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.LinearAttenuationCoefficient.CanonicalUnit), QuantityKinds.LinearAttenuationCoefficient);
        var length = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var result = mu * length;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.OpticalDepth);
    }

    [Fact]
    public void GivenOpticalDepthAndLength_WhenDivided_ThenLinearAttenuationCoefficient()
    {
        // Arrange
        var tau = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.OpticalDepth.CanonicalUnit), QuantityKinds.OpticalDepth);
        var length = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var result = tau / length;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.LinearAttenuationCoefficient);
    }
}