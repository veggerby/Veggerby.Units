using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityOperatorTests
{
    [Fact]
    public void GivenSameKind_WhenAddOperator_ThenAdded()
    {
        // Arrange
        var a = Quantity.Energy(10.0);
        var b = Quantity.Energy(5.0);

        // Act
        var sum = a + b;

        // Assert
        sum.Kind.Should().BeSameAs(a.Kind);
        ((double)sum.Measurement).Should().Be(15.0);
    }

    [Fact]
    public void GivenDifferentKindSameDimension_WhenAddOperator_ThenThrows()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var torque = Quantity.Torque(2.0);

        // Act
        var act = () => _ = energy + torque;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenSameKindWithDifferentUnits_WhenAddOperator_ThenAligned()
    {
        // Arrange
        var joule = Quantity.Energy(1.0);
        var kiloJoule = Quantity.Of(1.0, Prefix.k * QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Energy);

        // Act
        var sum = joule + kiloJoule; // 1 J + 1000 J

        // Assert
        ((double)sum.Measurement).Should().Be(1001.0);
        sum.Measurement.Unit.Should().Be(joule.Measurement.Unit);
    }

    [Fact]
    public void GivenSameKind_WhenSubOperator_ThenSubtracted()
    {
        // Arrange
        var a = Quantity.Energy(10.0);
        var b = Quantity.Energy(3.0);

        // Act
        var diff = a - b;

        // Assert
        ((double)diff.Measurement).Should().Be(7.0);
        diff.Kind.Should().BeSameAs(a.Kind);
    }

    [Fact]
    public void GivenDifferentKindSameDimension_WhenSubOperator_ThenThrows()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var torque = Quantity.Torque(2.0);

        // Act
        var act = () => _ = energy - torque;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}