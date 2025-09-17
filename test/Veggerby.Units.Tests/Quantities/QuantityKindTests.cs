using System;
using AwesomeAssertions;
using Veggerby.Units.Quantities;
using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityKindTests
{
    [Fact]
    public void Entropy_Rejects_PlainJoule_WhenStrict()
    {
        // Arrange / Act
    var jouleUnit = QuantityKinds.Energy.CanonicalUnit; // explicit Joule expression
    var act = () => Quantity.Of(1.0, jouleUnit, QuantityKinds.Entropy, strict: true);

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void Entropy_Accepts_JoulePerKelvin()
    {
        // Arrange
        var q = Quantity.Entropy(2.5);

        // Assert
    q.Measurement.Unit.Dimension.Should().Be((QuantityKinds.Energy.CanonicalUnit / Unit.SI.K).Dimension);
        q.Kind.Should().Be(QuantityKinds.Entropy);
    }

    [Fact]
    public void Torque_And_Energy_Share_Dimensions_But_Are_Not_Same_Kind()
    {
        // Arrange
        var torque = Quantity.Torque(5);
        var energy = Quantity.Energy(5);

        // Assert
        torque.Measurement.Unit.Dimension.Should().Be(energy.Measurement.Unit.Dimension);
        torque.Kind.Should().NotBe(energy.Kind);
    }
}
