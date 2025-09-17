using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class ElectromagneticInferenceTests
{
    [Fact]
    public void GivenCurrentAndTime_WhenMultiplied_ThenChargeInferred()
    {
        // Arrange
        var current = new Quantity<double>(new DoubleMeasurement(2.0, Unit.SI.A), QuantityKinds.ElectricCurrent);
        var time = new Quantity<double>(new DoubleMeasurement(3.0, Unit.SI.s), QuantityKinds.Time);

        // Act
        var charge = current * time;

        // Assert
        charge.Kind.Should().BeSameAs(QuantityKinds.ElectricCharge);
        charge.Measurement.Unit.Should().Be(Unit.SI.A * Unit.SI.s);
    }

    [Fact]
    public void GivenVoltageAndCharge_WhenMultiplied_ThenEnergyInferred()
    {
        // Arrange
        var voltage = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);
        var charge = new Quantity<double>(new DoubleMeasurement(10.0, QuantityKinds.ElectricCharge.CanonicalUnit), QuantityKinds.ElectricCharge);

        // Act
        var energy = voltage * charge;

        // Assert
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenCurrentAndVoltage_WhenMultiplied_ThenPowerInferred()
    {
        // Arrange
        var current = new Quantity<double>(new DoubleMeasurement(4.0, Unit.SI.A), QuantityKinds.ElectricCurrent);
        var voltage = new Quantity<double>(new DoubleMeasurement(10.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);

        // Act
        var power = current * voltage;

        // Assert
        power.Kind.Should().BeSameAs(QuantityKinds.Power);
    }

    [Fact]
    public void GivenCurrentAndResistance_WhenMultiplied_ThenVoltageInferred()
    {
        // Arrange
        var current = new Quantity<double>(new DoubleMeasurement(3.0, Unit.SI.A), QuantityKinds.ElectricCurrent);
        var resistance = new Quantity<double>(new DoubleMeasurement(6.0, QuantityKinds.ElectricResistance.CanonicalUnit), QuantityKinds.ElectricResistance);

        // Act
        var voltage = current * resistance;

        // Assert
        voltage.Kind.Should().BeSameAs(QuantityKinds.Voltage);
    }

    [Fact]
    public void GivenFluxAndTime_WhenDivided_ThenVoltageInferred()
    {
        // Arrange
        var flux = new Quantity<double>(new DoubleMeasurement(8.0, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);
        var time = new Quantity<double>(new DoubleMeasurement(2.0, Unit.SI.s), QuantityKinds.Time);

        // Act
        var voltage = flux / time;

        // Assert
        voltage.Kind.Should().BeSameAs(QuantityKinds.Voltage);
    }

    [Fact]
    public void GivenVoltageAndTime_WhenMultiplied_ThenFluxInferred()
    {
        // Arrange
        var voltage = new Quantity<double>(new DoubleMeasurement(12.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);
        var time = new Quantity<double>(new DoubleMeasurement(2.0, Unit.SI.s), QuantityKinds.Time);

        // Act
        var flux = voltage * time;

        // Assert
        flux.Kind.Should().BeSameAs(QuantityKinds.MagneticFlux);
    }

    [Fact]
    public void GivenFluxAndArea_WhenDivided_ThenFluxDensityInferred()
    {
        // Arrange
        var flux = new Quantity<double>(new DoubleMeasurement(1.5, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);
        var area = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);

        // Act
        var density = flux / area;

        // Assert
        density.Kind.Should().BeSameAs(QuantityKinds.MagneticFluxDensity);
    }

    [Fact]
    public void GivenCapacitanceAndVoltage_WhenMultiplied_ThenChargeInferred()
    {
        // Arrange
        var capacitance = new Quantity<double>(new DoubleMeasurement(7.0, QuantityKinds.Capacitance.CanonicalUnit), QuantityKinds.Capacitance);
        var voltage = new Quantity<double>(new DoubleMeasurement(9.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);

        // Act
        var charge = capacitance * voltage;

        // Assert
        charge.Kind.Should().BeSameAs(QuantityKinds.ElectricCharge);
    }

    [Fact]
    public void GivenInductanceAndCurrent_WhenMultiplied_ThenFluxInferred()
    {
        // Arrange
        var inductance = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Inductance.CanonicalUnit), QuantityKinds.Inductance);
        var current = new Quantity<double>(new DoubleMeasurement(3.0, Unit.SI.A), QuantityKinds.ElectricCurrent);

        // Act
        var flux = inductance * current;

        // Assert
        flux.Kind.Should().BeSameAs(QuantityKinds.MagneticFlux);
    }

    [Fact]
    public void GivenConductanceAndVoltage_WhenMultiplied_ThenCurrentInferred()
    {
        // Arrange
        var conductance = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.ElectricConductance.CanonicalUnit), QuantityKinds.ElectricConductance);
        var voltage = new Quantity<double>(new DoubleMeasurement(4.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);

        // Act
        var current = conductance * voltage;

        // Assert
        current.Kind.Should().BeSameAs(QuantityKinds.ElectricCurrent);
    }
}