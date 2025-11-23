using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityTransitiveOperatorTests
{
    public QuantityTransitiveOperatorTests()
    {
        // Reset state before each test
        QuantityKindInferenceRegistry.ResetForTests();
    }

    [Fact]
    public void GivenTransitiveInferenceDisabled_WhenMultiplyWithDirectRule_ThenWorks()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = false;
        var force = new Quantity<double>(new DoubleMeasurement(100.0, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var length = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act - Direct rule exists: Force × Length → Energy
        var energy = force * length;

        // Assert
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
        energy.Measurement.Value.Should().Be(200.0);
    }

    [Fact]
    public void GivenTransitiveInferenceEnabled_WhenMultiplyWithDirectRule_ThenStillUsesDirectRule()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = true;
        QuantityKindInferenceRegistry.MaxInferenceDepth = 3;

        var force = new Quantity<double>(new DoubleMeasurement(100.0, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var length = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var energy = force * length;

        // Assert - Should use direct rule (depth 1)
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
        energy.Measurement.Value.Should().Be(200.0);
    }

    [Fact]
    public void GivenTransitiveInferenceDisabled_WhenMultiplyWithNoDirectRule_ThenThrows()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = false;
        QuantityKindInferenceRegistry.MaxInferenceDepth = 1;

        var energy = new Quantity<double>(new DoubleMeasurement(100.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var mass = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Mass.CanonicalUnit), QuantityKinds.Mass);

        // Act & Assert - No direct rule for Energy × Mass
        var act = () => _ = energy * mass;
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No semantic inference rule for Energy * Mass.");
    }

    [Fact]
    public void GivenTransitiveInferenceEnabled_WhenDivideWithDirectRule_ThenWorks()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = true;
        QuantityKindInferenceRegistry.MaxInferenceDepth = 3;

        var energy = new Quantity<double>(new DoubleMeasurement(200.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var time = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act - Direct rule: Energy / Time → Power
        var power = energy / time;

        // Assert
        power.Kind.Should().BeSameAs(QuantityKinds.Power);
        power.Measurement.Value.Should().Be(100.0);
    }

    [Fact]
    public void GivenTryMultiply_WhenNoRule_ThenReturnsFalse()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = false;

        var energy = new Quantity<double>(new DoubleMeasurement(100.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var mass = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Mass.CanonicalUnit), QuantityKinds.Mass);

        // Act
        var success = Quantity<double>.TryMultiply(energy, mass, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GivenTryDivide_WhenRuleExists_ThenReturnsTrue()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = false;

        var energy = new Quantity<double>(new DoubleMeasurement(200.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var power = new Quantity<double>(new DoubleMeasurement(100.0, QuantityKinds.Power.CanonicalUnit), QuantityKinds.Power);

        // Act - Energy / Power → Time
        var success = Quantity<double>.TryDivide(energy, power, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Kind.Should().BeSameAs(QuantityKinds.Time);
        result.Measurement.Value.Should().Be(2.0);
    }

    [Fact]
    public void GivenCommutativeMultiply_WhenBothOrders_ThenSameResult()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = true;
        QuantityKindInferenceRegistry.MaxInferenceDepth = 3;

        var voltage = new Quantity<double>(new DoubleMeasurement(10.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);
        var current = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.ElectricCurrent.CanonicalUnit), QuantityKinds.ElectricCurrent);

        // Act
        var power1 = voltage * current;
        var power2 = current * voltage;

        // Assert
        power1.Kind.Should().BeSameAs(QuantityKinds.Power);
        power2.Kind.Should().BeSameAs(QuantityKinds.Power);
        power1.Measurement.Value.Should().Be(50.0);
        power2.Measurement.Value.Should().Be(50.0);
    }

    [Fact]
    public void GivenBackwardCompatibility_WhenTransitiveDisabled_ThenBehavesAsBeforeForDirectRules()
    {
        // Arrange - default configuration (transitive disabled)
        QuantityKindInferenceRegistry.ResetForTests();
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled.Should().BeFalse();

        var pressure = new Quantity<double>(new DoubleMeasurement(101325.0, QuantityKinds.Pressure.CanonicalUnit), QuantityKinds.Pressure);
        var volume = new Quantity<double>(new DoubleMeasurement(0.5, QuantityKinds.Volume.CanonicalUnit), QuantityKinds.Volume);

        // Act - Direct rule: Pressure × Volume → Energy
        var energy = pressure * volume;

        // Assert
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenTransitiveInferenceEnabled_WhenChainExists_ThenCanInferThroughOperators()
    {
        // Arrange
        QuantityKindInferenceRegistry.TransitiveInferenceEnabled = true;
        QuantityKindInferenceRegistry.MaxInferenceDepth = 3;

        // Build a chain using operators:
        // Pressure × Area → Force (direct)
        var pressure = new Quantity<double>(new DoubleMeasurement(1000.0, QuantityKinds.Pressure.CanonicalUnit), QuantityKinds.Pressure);
        var area = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);

        // Force × Length → Energy (direct)
        var length = new Quantity<double>(new DoubleMeasurement(3.0, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act - Chain: (Pressure × Area) × Length
        var force = pressure * area;
        var energy = force * length;

        // Assert
        force.Kind.Should().BeSameAs(QuantityKinds.Force);
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
        energy.Measurement.Value.Should().Be(6000.0); // 1000 * 2 * 3
    }
}
