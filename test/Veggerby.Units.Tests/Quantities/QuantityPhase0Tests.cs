namespace Veggerby.Units.Tests.Quantities;

/// <summary>
/// Tests for Phase 0 guardrails: Angle scalar fallback prevention and Add/Sub parity enforcement.
/// </summary>
public class QuantityPhase0Tests
{
    [Fact]
    public void GivenAngle_WhenMultipliedByNonAngleWithoutRule_ThenThrows()
    {
        // Arrange
        var angle = new Quantity<double>(new DoubleMeasurement(Math.PI / 2, Unit.None), QuantityKinds.Angle);
        var energy = Quantity.Energy(10.0);

        // Act - Angle should not fall back to dimensionless scalar behavior
        var act = () => _ = angle * energy;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenAngle_WhenDividedByNonAngleWithoutRule_ThenThrows()
    {
        // Arrange
        var entropy = Quantity.Entropy(10.0); // Use Entropy instead of Energy - no rule with Angle
        var angle = new Quantity<double>(new DoubleMeasurement(2.0, Unit.None), QuantityKinds.Angle);

        // Act - Angle should not fall back to dimensionless scalar behavior
        var act = () => _ = entropy / angle;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenTorqueAndAngle_WhenMultiplied_ThenSucceedsWithExplicitRule()
    {
        // Arrange - this should work because there's an explicit rule
        var torque = Quantity.Torque(5.0);
        var angle = new Quantity<double>(new DoubleMeasurement(Math.PI / 2, Unit.None), QuantityKinds.Angle);

        // Act
        var energy = torque * angle;

        // Assert
        energy.Kind.Should().BeSameAs(QuantityKinds.Energy);
        energy.Measurement.Value.Should().BeApproximately(5.0 * Math.PI / 2, 1e-12);
    }

    [Fact]
    public void GivenStaticAddMethod_WhenDifferentKindsDisallowAddition_ThenThrows()
    {
        // Arrange
        var absoluteTemp = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var anotherAbsoluteTemp = TemperatureQuantity.Absolute(310.0, Unit.SI.K);

        // Act - Add method should enforce AllowDirectAddition like + operator
        var act = () => Quantity<double>.Add(absoluteTemp, anotherAbsoluteTemp);

        // Assert - Should throw because TemperatureAbsolute has AllowDirectAddition = false
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenStaticSubMethod_WhenKindDisallowsDirectSubtraction_ThenThrows()
    {
        // Arrange
        var absoluteTemp1 = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var absoluteTemp2 = TemperatureQuantity.Absolute(290.0, Unit.SI.K);

        // Act - Sub method should enforce AllowDirectSubtraction like - operator
        var act = () => Quantity<double>.Sub(absoluteTemp1, absoluteTemp2);

        // Assert - Should throw when direct subtraction not allowed, unless DifferenceResultKind is set
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenTemperatureAbsoluteSubtraction_WhenUsingOperator_ThenProducesDelta()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var t2 = TemperatureQuantity.Absolute(290.0, Unit.SI.K);

        // Act - operator should produce delta result
        var delta = t1 - t2;

        // Assert
        delta.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        delta.Measurement.Value.Should().BeApproximately(10.0, 1e-12);
    }

    [Fact]
    public void GivenTemperatureDelta_WhenAddedToAbsolute_ThenProducesAbsolute()
    {
        // Arrange
        var delta = TemperatureQuantity.Delta(5.0);
        var absolute = TemperatureQuantity.Absolute(300.0, Unit.SI.K);

        // Act
        var result = delta + absolute;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        result.Measurement.Value.Should().BeApproximately(305.0, 1e-12);
    }

    [Fact]
    public void GivenTemperatureDelta_WhenSubtractedFromAbsolute_ThenProducesAbsolute()
    {
        // Arrange
        var absolute = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var delta = TemperatureQuantity.Delta(5.0);

        // Act
        var result = absolute - delta;

        // Assert
        result.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        result.Measurement.Value.Should().BeApproximately(295.0, 1e-12);
    }
}