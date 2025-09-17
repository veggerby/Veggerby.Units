using System;
using AwesomeAssertions;
using Veggerby.Units.Quantities;
using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityScalarTests
{
    [Fact]
    public void GivenEnergy_WhenScaledByDimensionless_ThenKindPreserved()
    {
        // Arrange
        var energy = Quantity.Energy(10.0); // 10 J
        var two = new DoubleMeasurement(2.0, Unit.None);

        // Act
        var scaled = energy * two;

        // Assert
        scaled.Kind.Should().BeSameAs(QuantityKinds.Energy);
        scaled.Measurement.Value.Should().BeApproximately(20.0, 1e-12);
    }

    [Fact]
    public void GivenEnergy_WhenDividedByDimensionless_ThenKindPreserved()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var two = new DoubleMeasurement(2.0, Unit.None);

        // Act
        var scaled = energy / two;

        // Assert
        scaled.Kind.Should().BeSameAs(QuantityKinds.Energy);
        scaled.Measurement.Value.Should().BeApproximately(5.0, 1e-12);
    }

    [Fact]
    public void GivenEnergy_WhenScaledByNonDimensionless_ThenThrows()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var notScalar = new DoubleMeasurement(2.0, Unit.SI.K);

        // Act
        var act = () => _ = energy * notScalar;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenAbsoluteTemperature_WhenScaledByDimensionless_ThenThrows()
    {
        // Arrange
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var two = new DoubleMeasurement(2.0, Unit.None);

        // Act
        var act = () => _ = t * two;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}
