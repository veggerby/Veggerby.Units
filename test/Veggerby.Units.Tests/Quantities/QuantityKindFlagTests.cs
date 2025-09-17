using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityKindFlagTests
{
    [Fact]
    public void GivenKindWithAdditionAllowed_WhenPlusOperator_ThenSucceeds()
    {
        // Arrange
        var a = Quantity.Energy(10.0); // Energy allows + / -
        var b = Quantity.Energy(2.0);

        // Act
        var sum = a + b;

        // Assert
        sum.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)sum.Measurement).Should().Be(12.0);
    }

    [Fact]
    public void GivenKindWithAdditionDisallowed_WhenPlusOperator_ThenThrows()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(10.0, Unit.SI.K); // TemperatureAbsolute forbids direct +
        var t2 = TemperatureQuantity.Absolute(5.0, Unit.SI.K);

        // Act
        var act = () => _ = t1 + t2;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenAbsoluteTemperatures_WhenMinusOperator_ThenDeltaProduced()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(20.0, Unit.SI.K);
        var t2 = TemperatureQuantity.Absolute(5.0, Unit.SI.K);

        // Act
        var delta = t1 - t2;

        // Assert
        delta.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        delta.Measurement.Value.Should().BeApproximately(15.0, 1e-12);
    }

    [Fact]
    public void GivenAllowedDeltaComputation_WhenDeltaCalled_ThenSucceeds()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(30.0, Unit.SI.C);
        var t2 = TemperatureQuantity.Absolute(20.0, Unit.SI.C);

        // Act
        var delta = TemperatureOps.Delta(t1, t2);

        // Assert
        delta.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        delta.Measurement.Unit.Should().Be(Unit.SI.K);
        delta.Measurement.Value.Should().BeApproximately(10.0, 1e-12);
    }

    [Fact]
    public void GivenDeltaApplication_WhenAddDelta_ThenAbsoluteAdvanced()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(15.0, Unit.SI.C);
        var d = TemperatureQuantity.DeltaC(5.0);

        // Act
        var t2 = TemperatureOps.AddDelta(t1, d);

        // Assert
        t2.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        t2.Measurement.Unit.Should().Be(t1.Measurement.Unit);
        t2.Measurement.Value.Should().BeApproximately(20.0, 1e-12);
    }
}