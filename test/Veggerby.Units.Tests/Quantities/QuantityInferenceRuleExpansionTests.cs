using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityInferenceRuleExpansionTests
{
    [Fact]
    public void GivenPowerAndTime_WhenMultiply_ThenEnergy()
    {
        // Arrange
        var p = Quantity.Power(5.0); // 5 J/s
        var t = Quantity.Of(2.0, Unit.SI.s, QuantityKinds.Time);

        // Act
        var e = p * t;

        // Assert
        e.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)e.Measurement).Should().BeApproximately(10.0, 1e-12);
    }

    [Fact]
    public void GivenEnergyAndTime_WhenDivide_ThenPower()
    {
        var e = Quantity.Energy(100.0);
        var t = Quantity.Of(4.0, Unit.SI.s, QuantityKinds.Time);
        var p = e / t;
        p.Kind.Should().BeSameAs(QuantityKinds.Power);
        ((double)p.Measurement).Should().BeApproximately(25.0, 1e-12);
    }

    [Fact]
    public void GivenEnergyAndPower_WhenDivide_ThenTime()
    {
        var e = Quantity.Energy(60.0);
        var p = Quantity.Power(30.0);
        var t = e / p;
        t.Kind.Should().BeSameAs(QuantityKinds.Time);
        ((double)t.Measurement).Should().BeApproximately(2.0, 1e-12);
    }

    [Fact]
    public void GivenForceAndLength_WhenMultiply_ThenEnergy()
    {
        var f = Quantity.Force(10.0);
        var l = Quantity.Of(3.0, Unit.SI.m, QuantityKinds.Length);
        var e = f * l;
        e.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)e.Measurement).Should().BeApproximately(30.0, 1e-12);
    }

    [Fact]
    public void GivenPressureAndVolume_WhenMultiply_ThenEnergy()
    {
        var p = Quantity.Pressure(2.0); // Pa
        var v = Quantity.Volume(5.0);    // m^3
        var e = p * v;
        e.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)e.Measurement).Should().BeApproximately(10.0, 1e-12);
    }

    [Fact]
    public void GivenPressureAndArea_WhenMultiply_ThenForce()
    {
        var p = Quantity.Pressure(4.0); // Pa
        var a = Quantity.Area(2.0);     // m^2
        var f = p * a;
        f.Kind.Should().BeSameAs(QuantityKinds.Force);
        ((double)f.Measurement).Should().BeApproximately(8.0, 1e-12);
    }

    [Fact]
    public void GivenVelocityAndTime_WhenMultiply_ThenNoRule_Throws()
    {
        var v = Quantity.Velocity(10.0);
        var t = Quantity.Of(3.0, Unit.SI.s, QuantityKinds.Time);
        var act = () => _ = v * t; // intentionally absent rule
        act.Should().Throw<InvalidOperationException>();
    }
}
