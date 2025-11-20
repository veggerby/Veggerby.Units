using AwesomeAssertions;

using Veggerby.Units.Fluent.Electromagnetics;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class ElectromagneticFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingAmperes_ShouldCreateCurrentMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var current = value.Amperes();

        // Assert
        current.Unit.Should().Be(QuantityKinds.ElectricCurrent.CanonicalUnit);
        current.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAmpereAlias_ShouldCreateCurrentMeasurement()
    {
        // Arrange
        double value = 5.0;

        // Act
        var current = value.Ampere();

        // Assert
        current.Unit.Should().Be(QuantityKinds.ElectricCurrent.CanonicalUnit);
        current.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingCoulombs_ShouldCreateChargeMeasurement()
    {
        // Arrange
        double value = 10.0;

        // Act
        var charge = value.Coulombs();

        // Assert
        charge.Unit.Should().Be(QuantityKinds.ElectricCharge.CanonicalUnit);
        charge.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingOhms_ShouldCreateResistanceMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var resistance = value.Ohms();

        // Assert
        resistance.Unit.Should().Be(QuantityKinds.ElectricResistance.CanonicalUnit);
        resistance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSiemens_ShouldCreateConductanceMeasurement()
    {
        // Arrange
        double value = 0.01;

        // Act
        var conductance = value.Siemens();

        // Assert
        conductance.Unit.Should().Be(QuantityKinds.ElectricConductance.CanonicalUnit);
        conductance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingFarads_ShouldCreateCapacitanceMeasurement()
    {
        // Arrange
        double value = 1e-6;

        // Act
        var capacitance = value.Farads();

        // Assert
        capacitance.Unit.Should().Be(QuantityKinds.Capacitance.CanonicalUnit);
        capacitance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingHenries_ShouldCreateInductanceMeasurement()
    {
        // Arrange
        double value = 0.5;

        // Act
        var inductance = value.Henries();

        // Assert
        inductance.Unit.Should().Be(QuantityKinds.Inductance.CanonicalUnit);
        inductance.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingWebers_ShouldCreateMagneticFluxMeasurement()
    {
        // Arrange
        double value = 2.5;

        // Act
        var flux = value.Webers();

        // Assert
        flux.Unit.Should().Be(QuantityKinds.MagneticFlux.CanonicalUnit);
        flux.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingTeslas_ShouldCreateMagneticFluxDensityMeasurement()
    {
        // Arrange
        double value = 1.5;

        // Act
        var fluxDensity = value.Teslas();

        // Assert
        fluxDensity.Unit.Should().Be(QuantityKinds.MagneticFluxDensity.CanonicalUnit);
        fluxDensity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingHertz_ShouldCreateFrequencyMeasurement()
    {
        // Arrange
        double value = 50.0;

        // Act
        var frequency = value.Hertz();

        // Assert
        frequency.Unit.Should().Be(QuantityKinds.Frequency.CanonicalUnit);
        frequency.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingVoltsPerMeter_ShouldCreateElectricFieldStrengthMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var fieldStrength = value.VoltsPerMeter();

        // Assert
        fieldStrength.Unit.Should().Be(QuantityKinds.ElectricFieldStrength.CanonicalUnit);
        fieldStrength.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAmperesPerMeter_ShouldCreateMagneticFieldStrengthMeasurement()
    {
        // Arrange
        double value = 500.0;

        // Act
        var fieldStrength = value.AmperesPerMeter();

        // Assert
        fieldStrength.Unit.Should().Be(QuantityKinds.MagneticFieldStrength.CanonicalUnit);
        fieldStrength.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingAmperes_ShouldCreateCurrentMeasurement()
    {
        // Arrange
        decimal value = 3.5m;

        // Act
        var current = value.Amperes();

        // Assert
        current.Unit.Should().Be(QuantityKinds.ElectricCurrent.CanonicalUnit);
        current.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingElectricalConductivity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 5.96e7;

        // Act
        var conductivity = value.ElectricalConductivity();

        // Assert
        conductivity.Unit.Should().Be(QuantityKinds.ElectricalConductivity.CanonicalUnit);
        conductivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingElectricalResistivity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 1.68e-8;

        // Act
        var resistivity = value.ElectricalResistivity();

        // Assert
        resistivity.Unit.Should().Be(QuantityKinds.ElectricalResistivity.CanonicalUnit);
        resistivity.Value.Should().Be(value);
    }
}
