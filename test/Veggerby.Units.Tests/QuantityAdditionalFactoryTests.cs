using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityAdditionalFactoryTests
{
    [Fact]
    public void GivenValue_WhenCreatingStress_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 123;

        // Act
        var q = Quantity.Stress(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.Stress);
        q.Measurement.Unit.Should().Be(QuantityKinds.Stress.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingVolumetricFlowRate_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 2.5;

        // Act
        var q = Quantity.VolumetricFlowRate(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.VolumetricFlowRate);
        q.Measurement.Unit.Should().Be(QuantityKinds.VolumetricFlowRate.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingThermalResistance_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 0.12;

        // Act
        var q = Quantity.ThermalResistance(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.ThermalResistance);
        q.Measurement.Unit.Should().Be(QuantityKinds.ThermalResistance.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingElectricalConductivity_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 9.81;

        // Act
        var q = Quantity.ElectricalConductivity(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.ElectricalConductivity);
        q.Measurement.Unit.Should().Be(QuantityKinds.ElectricalConductivity.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingRadiance_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 42;

        // Act
        var q = Quantity.Radiance(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.Radiance);
        q.Measurement.Unit.Should().Be(QuantityKinds.Radiance.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingDiffusionCoefficient_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 1e-9;

        // Act
        var q = Quantity.DiffusionCoefficient(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.DiffusionCoefficient);
        q.Measurement.Unit.Should().Be(QuantityKinds.DiffusionCoefficient.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingReynoldsNumber_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 15000;

        // Act
        var q = Quantity.Reynolds(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.Reynolds);
        q.Measurement.Unit.Should().Be(QuantityKinds.Reynolds.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingSpectralFlux_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 3.14;

        // Act
        var q = Quantity.SpectralFlux(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.SpectralFlux);
        q.Measurement.Unit.Should().Be(QuantityKinds.SpectralFlux.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingIsentropicExponent_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 1.4;

        // Act
        var q = Quantity.IsentropicExponent(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.IsentropicExponent);
        q.Measurement.Unit.Should().Be(QuantityKinds.IsentropicExponent.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }

    [Fact]
    public void GivenValue_WhenCreatingActivityConcentration_ThenKindAndUnitMatch()
    {
        // Arrange
        const double value = 2500;

        // Act
        var q = Quantity.ActivityConcentration(value);

        // Assert
        q.Kind.Should().BeSameAs(QuantityKinds.ActivityConcentration);
        q.Measurement.Unit.Should().Be(QuantityKinds.ActivityConcentration.CanonicalUnit);
        q.Measurement.Value.Should().Be(value);
    }
}