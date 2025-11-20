using AwesomeAssertions;

using Veggerby.Units.Fluent.Thermodynamics;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class ThermodynamicFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingJoulesPerKelvin_ShouldCreateEntropyMeasurement()
    {
        // Arrange
        double value = 125.5;

        // Act
        var entropy = value.JoulesPerKelvin();

        // Assert
        entropy.Unit.Should().Be(QuantityKinds.Entropy.CanonicalUnit);
        entropy.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingJoulesPerKelvin_ShouldCreateEntropyMeasurement()
    {
        // Arrange
        decimal value = 125.5m;

        // Act
        var entropy = value.JoulesPerKelvin();

        // Assert
        entropy.Unit.Should().Be(QuantityKinds.Entropy.CanonicalUnit);
        entropy.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingEntropy_ShouldCreateEntropyMeasurement()
    {
        // Arrange
        double value = 42.0;

        // Act
        var entropy = value.Entropy();

        // Assert
        entropy.Unit.Should().Be(QuantityKinds.Entropy.CanonicalUnit);
        entropy.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingHeatCapacity_ShouldCreateHeatCapacityMeasurement()
    {
        // Arrange
        double value = 10.5;

        // Act
        var heatCapacity = value.HeatCapacity();

        // Assert
        heatCapacity.Unit.Should().Be(QuantityKinds.HeatCapacity.CanonicalUnit);
        heatCapacity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingThermalConductivity_ShouldCreateThermalConductivityMeasurement()
    {
        // Arrange
        double value = 0.6;

        // Act
        var thermalConductivity = value.ThermalConductivity();

        // Assert
        thermalConductivity.Unit.Should().Be(QuantityKinds.ThermalConductivity.CanonicalUnit);
        thermalConductivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingThermalDiffusivity_ShouldCreateThermalDiffusivityMeasurement()
    {
        // Arrange
        double value = 1.5e-5;

        // Act
        var thermalDiffusivity = value.ThermalDiffusivity();

        // Assert
        thermalDiffusivity.Unit.Should().Be(QuantityKinds.ThermalDiffusivity.CanonicalUnit);
        thermalDiffusivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingHeatFlux_ShouldCreateHeatFluxMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var heatFlux = value.HeatFlux();

        // Assert
        heatFlux.Unit.Should().Be(QuantityKinds.HeatFlux.CanonicalUnit);
        heatFlux.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingThermalResistance_ShouldCreateThermalResistanceMeasurement()
    {
        // Arrange
        double value = 0.25;

        // Act
        var thermalResistance = value.ThermalResistance();

        // Assert
        thermalResistance.Unit.Should().Be(QuantityKinds.ThermalResistance.CanonicalUnit);
        thermalResistance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingThermalConductance_ShouldCreateThermalConductanceMeasurement()
    {
        // Arrange
        double value = 4.0;

        // Act
        var thermalConductance = value.ThermalConductance();

        // Assert
        thermalConductance.Unit.Should().Be(QuantityKinds.ThermalConductance.CanonicalUnit);
        thermalConductance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingCoefficientOfThermalExpansion_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 1.2e-5;

        // Act
        var coefficient = value.CoefficientOfThermalExpansion();

        // Assert
        coefficient.Unit.Should().Be(QuantityKinds.CoefficientOfThermalExpansion.CanonicalUnit);
        coefficient.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingVolumetricHeatCapacity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 2.5e6;

        // Act
        var volumetricHeatCapacity = value.VolumetricHeatCapacity();

        // Assert
        volumetricHeatCapacity.Unit.Should().Be(QuantityKinds.VolumetricHeatCapacity.CanonicalUnit);
        volumetricHeatCapacity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMolarHeatCapacity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 75.3;

        // Act
        var molarHeatCapacity = value.MolarHeatCapacity();

        // Assert
        molarHeatCapacity.Unit.Should().Be(QuantityKinds.MolarHeatCapacity.CanonicalUnit);
        molarHeatCapacity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSpecificEnthalpy_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 2500.0;

        // Act
        var specificEnthalpy = value.SpecificEnthalpy();

        // Assert
        specificEnthalpy.Unit.Should().Be(QuantityKinds.SpecificEnthalpy.CanonicalUnit);
        specificEnthalpy.Value.Should().Be(value);
    }
}
