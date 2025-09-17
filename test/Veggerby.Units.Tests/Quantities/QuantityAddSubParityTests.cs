namespace Veggerby.Units.Tests.Quantities;

public class QuantityAddSubParityTests
{
    [Fact]
    public void GivenDifferentSameDimensionKinds_WhenAddHelperRequireSameKind_ThenThrows()
    {
        // Arrange
        var energy = Quantity.Energy(5.0);
        var torque = Quantity.Torque(1.0);

        // Act
        var act = () => _ = Quantity<double>.Add(energy, torque, requireSameKind: true);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDifferentSameDimensionKinds_WhenAddHelperNoRequireSameKind_ThenShouldNotBypassOperatorRules()
    {
        // Arrange
        var energy = Quantity.Energy(5.0);
        var torque = Quantity.Torque(1.0);

        // Act
        var act = () => _ = Quantity<double>.Add(energy, torque); // desired: parity (should throw); current: silently adds -> will make fail

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenAbsoluteTemperatures_WhenAddHelper_ThenThrows()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(10.0, Unit.SI.K);
        var t2 = TemperatureQuantity.Absolute(5.0, Unit.SI.K);

        // Act
        var act = () => _ = Quantity<double>.Add(t1, t2);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenAbsoluteTemperatures_WhenSubtractHelper_ThenDeltaKind()
    {
        // Arrange
        var t2 = TemperatureQuantity.Absolute(20.0, Unit.SI.K);
        var t1 = TemperatureQuantity.Absolute(10.0, Unit.SI.K);

        // Act
        var diff = Quantity<double>.Sub(t2, t1);

        // Assert
        diff.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        diff.Measurement.Unit.Should().Be(Unit.SI.K);
        ((double)diff.Measurement).Should().BeApproximately(10.0, 1e-12);
    }
}