using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityInferenceRuleTests
{
    [Fact]
    public void GivenFluxOverTime_WhenDivide_ThenVoltage()
    {
        // Arrange
        var flux = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);
        var time = new Quantity<double>(new DoubleMeasurement(4.0, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);

        // Act
        var v = flux / time;

        // Assert
        v.Kind.Should().BeSameAs(QuantityKinds.Voltage);
    }

    [Fact]
    public void GivenVoltageTimesTime_WhenMultiply_ThenFlux()
    {
        var voltage = new Quantity<double>(new DoubleMeasurement(3.0, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);
        var time = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);
        var flux = voltage * time;
        flux.Kind.Should().BeSameAs(QuantityKinds.MagneticFlux);
    }

    [Fact]
    public void GivenFluxOverArea_WhenDivide_ThenFluxDensity()
    {
        var flux = new Quantity<double>(new DoubleMeasurement(7.0, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);
        var area = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);
        var density = flux / area;
        density.Kind.Should().BeSameAs(QuantityKinds.MagneticFluxDensity);
    }

    [Fact]
    public void GivenInductanceTimesCurrent_WhenMultiply_ThenFlux()
    {
        var ind = new Quantity<double>(new DoubleMeasurement(1.0, QuantityKinds.Inductance.CanonicalUnit), QuantityKinds.Inductance);
        var current = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.ElectricCurrent.CanonicalUnit), QuantityKinds.ElectricCurrent);
        var flux = ind * current;
        flux.Kind.Should().BeSameAs(QuantityKinds.MagneticFlux);
    }

    [Fact]
    public void GivenUnsupportedCombination_WhenMultiply_ThenThrows()
    {
        var energy = new Quantity<double>(new DoubleMeasurement(1.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var pressure = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Pressure.CanonicalUnit), QuantityKinds.Pressure);
        var act = () => _ = energy * pressure; // No rule; should throw
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Registry_IsSealed_AfterSealCall()
    {
        // Arrange
        QuantityKindInferenceRegistry.ResetForTests();
        QuantityKindInferenceRegistry.Seal();

        // Act
        var isSealed = QuantityKindInferenceRegistry.IsSealed;

        // Assert
        isSealed.Should().BeTrue();
    }

    [Fact]
    public void Registry_ResetForTests_RestoresFluxRule()
    {
        // Arrange
        QuantityKindInferenceRegistry.ResetForTests();

        // Act
        var rule = QuantityKindInferenceRegistry.ResolveOrNull(QuantityKinds.MagneticFlux, QuantityKindBinaryOperator.Divide, QuantityKinds.Time);

        // Assert
        rule.Should().BeSameAs(QuantityKinds.Voltage);
    }
}