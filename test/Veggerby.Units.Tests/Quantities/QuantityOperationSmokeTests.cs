using System;
using AwesomeAssertions;
using Veggerby.Units.Quantities;
using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityOperationSmokeTests
{
    [Fact]
    public void Adding_Energy_Quantities_Succeeds_With_Kind_Enforcement()
    {
        // Arrange
        var e1 = Quantity.Energy(100);
        var e2 = Quantity.Energy(50);

        // Act
        var sum = Quantity<double>.Add(e1, e2, requireSameKind: true);

        // Assert
        ((double)sum.Measurement).Should().Be(150d);
        sum.Kind.Should().Be(QuantityKinds.Energy);
    }

    [Fact]
    public void Adding_Different_Kinds_With_Enforcement_Throws()
    {
        // Arrange
        var e = Quantity.Energy(10);
        var torque = Quantity.Torque(2);

        // Act
        var act = () => Quantity<double>.Add(e, torque, requireSameKind: true);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Gibbs_Computation_From_Enthalpy_Temperature_Entropy_Succeeds()
    {
        // Arrange
        var H = Quantity.Enthalpy(1250);              // 1250 J
        var S = Quantity.Entropy(12.5);               // 12.5 J/K
        var T = new DoubleMeasurement(298.15, Unit.SI.K);
    var TS = new DoubleMeasurement(T.Value * S.Measurement.Value, QuantityKinds.Energy.CanonicalUnit);

        // Act
        var G = Quantity.Gibbs(H.Measurement.Value - TS.Value);

        // Assert
        G.Kind.Should().Be(QuantityKinds.GibbsFreeEnergy);
        G.Measurement.Unit.Should().Be(H.Measurement.Unit);
    }
}
