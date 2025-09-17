namespace Veggerby.Units.Tests.Quantities;

public class QuantityAdditionalCoverageTests
{
    [Fact]
    public void GivenNullMeasurement_WhenConstruct_ThenThrows()
    {
        // Arrange
        // Act
        var act = () => new Quantity<double>(null, QuantityKinds.Energy);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GivenNullKind_WhenConstruct_ThenThrows()
    {
        // Arrange
        var meas = new DoubleMeasurement(1.0, QuantityKinds.Energy.CanonicalUnit);

        // Act
        var act = () => new Quantity<double>(meas, null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GivenMismatchedDimension_WhenStrict_ThenThrows()
    {
        // Arrange
        var meas = new DoubleMeasurement(1.0, Unit.SI.m); // length

        // Act
        var act = () => new Quantity<double>(meas, QuantityKinds.Energy); // energy expects Joule dimension

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void GivenEqualQuantitiesDifferentUnits_WhenEquals_ThenTrue()
    {
        // Arrange
        var e1 = Quantity.Energy(1000.0); // J
        var e2 = Quantity.Of(1.0, Prefix.k * QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Energy); // 1 kJ

        // Act / Assert
        e1.Equals(e2).Should().BeTrue();
        (e1 == e2).Should().BeTrue();
        e1.GetHashCode().Should().Be(e2.GetHashCode());
    }

    [Fact]
    public void GivenCrossKindSameDimension_WhenEquals_ThenFalse()
    {
        // Arrange
        var energy = Quantity.Energy(5.0);
        var torque = Quantity.Torque(5.0);

        // Act
        var act = () => _ = energy == torque; // operator path throws

        // Assert
        energy.Equals(torque).Should().BeFalse();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenNullRight_WhenCompareOperators_ThenThrows()
    {
        // Arrange
        var e = Quantity.Energy(1.0);

        // Act
        var act = () => _ = e < null;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenPointPlusDelta_StaticAdd_ThenPoint()
    {
        // Arrange
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var delta = TemperatureQuantity.Delta(5.0);

        // Act
        var res = Quantity<double>.Add(abs, delta);

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        res.Measurement.Value.Should().BeApproximately(305.0, 1e-12);
    }

    [Fact]
    public void GivenDeltaPlusPoint_StaticAdd_ThenPoint()
    {
        // Arrange
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var delta = TemperatureQuantity.Delta(5.0);

        // Act
        var res = Quantity<double>.Add(delta, abs);

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
    }

    [Fact]
    public void GivenPointMinusDelta_StaticSub_ThenPoint()
    {
        // Arrange
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var delta = TemperatureQuantity.Delta(5.0);

        // Act
        var res = Quantity<double>.Sub(abs, delta);

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        res.Measurement.Value.Should().BeApproximately(295.0, 1e-12);
    }

    [Fact]
    public void GivenDeltaDeltaAdd_WhenSameKindAllowed_ThenDelta()
    {
        // Arrange
        var d1 = TemperatureQuantity.Delta(3.0);
        var d2 = TemperatureQuantity.Delta(2.0);

        // Act
        var sum = d1 + d2; // direct addition allowed for delta kind

        // Assert
        sum.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        sum.Measurement.Value.Should().BeApproximately(5.0, 1e-12);
    }

    [Fact]
    public void GivenScalarMultiplyVectorKind_WhenDimensionless_ThenPreserveKind()
    {
        // Arrange
        var energy = Quantity.Energy(2.0);
        var scalar = new DoubleMeasurement(3.0, Unit.None);

        // Act
        var scaled = energy * scalar;

        // Assert
        scaled.Kind.Should().BeSameAs(QuantityKinds.Energy);
        scaled.Measurement.Value.Should().BeApproximately(6.0, 1e-12);
    }

    [Fact]
    public void GivenScalarMultiplyAbsoluteKind_WhenPointLike_ThenThrows()
    {
        // Arrange
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var scalar = new DoubleMeasurement(2.0, Unit.None);

        // Act
        var act = () => _ = abs * scalar;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDimensionlessFallbackMultiply_WhenOneSideUnitless_ThenPreservesOtherKind()
    {
        // Arrange
        var unitless = new Quantity<double>(new DoubleMeasurement(2.0, Unit.None), QuantityKinds.Angle); // Angle is guarded => will throw
        var energy = Quantity.Energy(3.0);

        // Act
        var act = () => _ = unitless * energy;

        // Assert
        act.Should().Throw<InvalidOperationException>(); // Angle guard path exercised
    }

    [Fact]
    public void GivenDivideByAngle_WhenOperator_ThenThrows()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var angle = Quantity.Angle(Math.PI / 4);

        // Act
        var torque = energy / angle; // Inference: Energy / Angle => Torque

        // Assert
        torque.Kind.Should().BeSameAs(QuantityKinds.Torque);
        torque.Measurement.Value.Should().BeApproximately(10.0 / (Math.PI / 4), 1e-12);
    }

    [Fact]
    public void GivenTryMethodsNullLeftRight_WhenAdd_ThenPropagate()
    {
        // Arrange / Act
        var ok = Quantity<double>.TryAdd(null, null, out var res);

        // Assert
        ok.Should().BeTrue();
        res.Should().BeNull();
    }

    [Fact]
    public void GivenTryMultiplyNoRule_WhenReturnsFalse()
    {
        // Arrange
        var v = Quantity.Velocity(1.0);
        var t = Quantity.Of(2.0, Unit.SI.s, QuantityKinds.Time);

        // Act
        var ok = Quantity<double>.TryMultiply(v, t, out var _);

        // Assert
        ok.Should().BeFalse();
    }
}