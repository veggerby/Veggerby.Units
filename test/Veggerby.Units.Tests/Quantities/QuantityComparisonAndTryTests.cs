namespace Veggerby.Units.Tests.Quantities;

public class QuantityComparisonAndTryTests
{
    [Fact]
    public void GivenSameKindDifferentUnits_WhenCompare_ThenAligned()
    {
        var e1 = Quantity.Energy(1000.0); // J
        var e2 = Quantity.Of(1.0, Prefix.k * QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Energy); // 1 kJ
        (e1 == e2).Should().BeTrue();
        (e1 >= e2).Should().BeTrue();
        (e1 <= e2).Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentKindSameDimension_WhenCompare_ThenThrows()
    {
        var energy = Quantity.Energy(5.0);
        var torque = Quantity.Torque(5.0);
        var act = () => _ = energy == torque;
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenNoRule_WhenTryMultiply_ThenFalse()
    {
        var v = Quantity.Velocity(3.0);
        var t = Quantity.Of(2.0, Unit.SI.s, QuantityKinds.Time);
        var ok = Quantity<double>.TryMultiply(v, t, out var _);
        ok.Should().BeFalse();
    }

    [Fact]
    public void GivenRule_WhenTryMultiply_ThenTrue()
    {
        var f = Quantity.Force(10.0);
        var d = Quantity.Of(2.0, Unit.SI.m, QuantityKinds.Length);
        var ok = Quantity<double>.TryMultiply(f, d, out var e);
        ok.Should().BeTrue();
        e.Kind.Should().BeSameAs(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenRegistrySealed_WhenRegister_ThenThrows()
    {
        QuantityKindInferenceRegistry.Seal();
        var act = () => QuantityKindInferenceRegistry.Register(new QuantityKindInference(QuantityKinds.Pressure, QuantityKindBinaryOperator.Multiply, QuantityKinds.Time, QuantityKinds.Energy));
        act.Should().Throw<InvalidOperationException>();
    }
}

public class TemperatureMeanTests
{
    [Fact]
    public void GivenMixedUnits_WhenMean_ThenCorrect()
    {
        var tC = TemperatureQuantity.Absolute(25.0, Unit.SI.C); // 298.15 K
        var tF = TemperatureQuantity.Absolute(77.0, Unit.Imperial.F); // 25 C -> 298.15 K
        var tK = TemperatureQuantity.Absolute(300.15, Unit.SI.K);

        var mean = TemperatureMean.Mean(tC, tF, tK); // average of 298.15, 298.15, 300.15 = 298.8166...
        mean.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        var meanK = mean.Measurement.ConvertTo(Unit.SI.K);
        meanK.Value.Should().BeApproximately((298.15 + 298.15 + 300.15) / 3.0, 1e-9);
    }
}