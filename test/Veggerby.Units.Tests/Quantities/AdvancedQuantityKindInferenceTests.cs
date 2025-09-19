using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class AdvancedQuantityKindInferenceTests
{
    [Fact]
    public void GivenForceAndTime_WhenMultiplied_ThenImpulseInferred()
    {
        // Arrange
        var force = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var time = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var impulse = force * time;

        // Assert
        impulse.Kind.Should().BeSameAs(QuantityKinds.Impulse);
    }

    [Fact]
    public void GivenEnergyAndTime_WhenMultiplied_ThenActionInferred()
    {
        // Arrange
        var energy = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var time = new Quantity<double>(new DoubleMeasurement(4, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var action = energy * time;

        // Assert
        action.Kind.Should().BeSameAs(QuantityKinds.Action);
    }

    [Fact]
    public void GivenVolumetricHeatCapacityAndVolume_WhenMultiplied_ThenHeatCapacityInferred()
    {
        // Arrange
        var volumetric = new Quantity<double>(new DoubleMeasurement(7, QuantityKinds.VolumetricHeatCapacity.CanonicalUnit), QuantityKinds.VolumetricHeatCapacity);
        var volume = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Volume.CanonicalUnit), QuantityKinds.Volume);

        // Act
        var heatCapacity = volumetric * volume;

        // Assert
        heatCapacity.Kind.Should().BeSameAs(QuantityKinds.HeatCapacity);
    }

    [Fact]
    public void GivenForceAndVolume_WhenDivided_ThenSpecificWeightInferred()
    {
        // Arrange
        var force = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var volume = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Volume.CanonicalUnit), QuantityKinds.Volume);

        // Act
        var specificWeight = force / volume;

        // Assert
        specificWeight.Kind.Should().BeSameAs(QuantityKinds.SpecificWeight);
    }

    [Fact]
    public void GivenRadianceAndLength_WhenDivided_ThenSpectralRadianceInferred()
    {
        // Arrange
        var radiance = new Quantity<double>(new DoubleMeasurement(9, QuantityKinds.Radiance.CanonicalUnit), QuantityKinds.Radiance);
        var length = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var specRad = radiance / length;

        // Assert
        specRad.Kind.Should().BeSameAs(QuantityKinds.SpectralRadiance);
    }

    [Fact]
    public void GivenPressureAndMoleFraction_WhenMultiplied_ThenPartialPressureInferred()
    {
        // Arrange
        var pressure = new Quantity<double>(new DoubleMeasurement(101325, QuantityKinds.Pressure.CanonicalUnit), QuantityKinds.Pressure);
        var moleFraction = new Quantity<double>(new DoubleMeasurement(0.2, QuantityKinds.MoleFraction.CanonicalUnit), QuantityKinds.MoleFraction);

        // Act
        var partial = pressure * moleFraction;

        // Assert
        partial.Kind.Should().BeSameAs(QuantityKinds.PartialPressure);
    }

    [Fact]
    public void GivenActivityCoefficientAndMoleFraction_WhenMultiplied_ThenActivityInferred()
    {
        // Arrange
        var gamma = new Quantity<double>(new DoubleMeasurement(1.5, QuantityKinds.ActivityCoefficient.CanonicalUnit), QuantityKinds.ActivityCoefficient);
        var moleFraction = new Quantity<double>(new DoubleMeasurement(0.3, QuantityKinds.MoleFraction.CanonicalUnit), QuantityKinds.MoleFraction);

        // Act
        var activity = gamma * moleFraction;

        // Assert
        activity.Kind.Should().BeSameAs(QuantityKinds.Activity);
    }
}