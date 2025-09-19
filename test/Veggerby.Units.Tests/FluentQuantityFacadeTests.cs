using AwesomeAssertions;

using Veggerby.Units.Fluent;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class FluentQuantityFacadeTests
{
    [Fact]
    public void LengthDouble_DefaultUnit()
    {
        // Arrange
        const double v = 5.5;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Length(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Length.CanonicalUnit);
    }

    [Fact]
    public void LengthDouble_CustomUnit()
    {
        // Arrange
        const double v = 2;
        var km = Prefix.k * Unit.SI.m;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Length(v, km);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(km);
    }

    [Fact]
    public void MassInt_DefaultUnit()
    {
        // Arrange
        const int v = 3;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Mass(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Mass.CanonicalUnit);
    }

    [Fact]
    public void TimeDecimal_CustomUnit()
    {
        // Arrange
        const decimal v = 10m;
        var ms = Prefix.m * Unit.SI.s;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Time(v, ms);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(ms);
    }

    [Fact]
    public void EnergyInt_DefaultUnit()
    {
        // Arrange
        const int v = 7;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Energy(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Energy.CanonicalUnit);
    }

    [Fact]
    public void ForceDecimal_DefaultUnit()
    {
        // Arrange
        const decimal v = 9m;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Force(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Force.CanonicalUnit);
    }

    [Fact]
    public void VelocityDouble_DefaultUnit()
    {
        // Arrange
        const double v = 12.34;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Velocity(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Velocity.CanonicalUnit);
    }

    [Fact]
    public void AccelerationDouble_DefaultUnit()
    {
        // Arrange
        const double v = 0.5;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Acceleration(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Acceleration.CanonicalUnit);
    }

    [Fact]
    public void PowerInt_DefaultUnit()
    {
        // Arrange
        const int v = 100;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Power(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Power.CanonicalUnit);
    }

    [Fact]
    public void PressureDouble_DefaultUnit()
    {
        // Arrange
        const double v = 2000;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.Pressure(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.Pressure.CanonicalUnit);
    }

    [Fact]
    public void ElectricCurrentDouble_DefaultUnit()
    {
        // Arrange
        const double v = 4.2;

        // Act
        var m = Veggerby.Units.Fluent.Quantity.ElectricCurrent(v);

        // Assert
        m.Value.Should().Be(v);
        m.Unit.Should().Be(QuantityKinds.ElectricCurrent.CanonicalUnit);
    }
}