using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityInferenceTests
{
    [Fact]
    public void GivenEntropyAndAbsoluteTemperature_WhenMultiply_ThenEnergy()
    {
        // Arrange
        var S = Quantity.Entropy(5.0);              // 5 J/K
        var T = TemperatureQuantity.Absolute(300.0, Unit.SI.K); // 300 K

        // Act
        var E = S * T;

        // Assert
        E.Kind.Should().BeSameAs(QuantityKinds.Energy);
        E.Measurement.Unit.Dimension.Should().Be(QuantityKinds.Energy.CanonicalUnit.Dimension);
        E.Measurement.Value.Should().BeApproximately(1500.0, 1e-12);
    }

    [Fact]
    public void GivenAbsoluteTemperatureAndEntropy_WhenMultiply_ThenEnergy_Commutes()
    {
        var S = Quantity.Entropy(2.0);
        var T = TemperatureQuantity.Absolute(400.0, Unit.SI.K);
        var E1 = S * T;
        var E2 = T * S;
        E1.Kind.Should().BeSameAs(QuantityKinds.Energy);
        E2.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)E1.Measurement).Should().Be(((double)E2.Measurement));
    }

    [Fact]
    public void GivenEnergyAndAbsoluteTemperature_WhenDivide_ThenEntropy()
    {
        var E = Quantity.Energy(600.0); // 600 J
        var T = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var S = E / T;
        S.Kind.Should().BeSameAs(QuantityKinds.Entropy);
        S.Measurement.Value.Should().BeApproximately(2.0, 1e-12);
    }

    [Fact]
    public void GivenEnergyAndEntropy_WhenDivide_ThenAbsoluteTemperature()
    {
        var E = Quantity.Energy(900.0);
        var S = Quantity.Entropy(3.0);
        var T = E / S;
        T.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        T.Measurement.Value.Should().BeApproximately(300.0, 1e-12);
    }

    [Fact]
    public void GivenNoInferenceRule_WhenMultiply_ThenThrows()
    {
        var torque = Quantity.Torque(5.0);
        var energy = Quantity.Energy(2.0);
        var act = () => _ = torque * energy; // no rule defined
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenNoInferenceRule_WhenDivide_ThenThrows()
    {
        var torque = Quantity.Torque(5.0);
        var energy = Quantity.Energy(2.0);
        var act = () => _ = torque / energy; // no rule defined
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDimensionlessQuantity_WhenMultiply_ThenOtherKindPreserved()
    {
        // Arrange: create a dimensionless quantity (value 2, Unit.None) with an arbitrary kind (reuse Energy kind by giving dimensionless unit via direct construction)
        var dimless = new Quantity<double>(new DoubleMeasurement(2.0, Unit.None), QuantityKinds.Energy, strictDimensionCheck: false);
        var entropy = Quantity.Entropy(3.0);

        // Act
        var result = dimless * entropy;

        // Assert
        result.Kind.Should().BeSameAs(entropy.Kind);
        result.Measurement.Value.Should().BeApproximately(6.0, 1e-12);
    }
}