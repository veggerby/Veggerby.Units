using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class TemperatureAdditionalAlgebraTests
{
    [Fact]
    public void GivenDeltaTemperatures_WhenAddOperator_ThenDelta()
    {
        // Arrange
        var d1 = TemperatureQuantity.Delta(5.0);
        var d2 = TemperatureQuantity.Delta(3.0);

        // Act
        var sum = d1 + d2;

        // Assert
        sum.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        ((double)sum.Measurement).Should().BeApproximately(8.0, 1e-12);
    }

    [Fact]
    public void GivenAbsoluteTemperature_WhenScaleByDimensionlessViaHelperAdd_ThenThrows()
    {
        // Arrange
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var scalar = Quantity.Of(2.0, Unit.None, QuantityKinds.Angle); // semantic dimensionless but not scalar fallback allowed

        // Act
        var act = () => _ = t * scalar; // quantity * quantity path should forbid angle scalar fallback later

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}