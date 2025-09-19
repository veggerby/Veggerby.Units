using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class MeasurementReciprocalSimplificationTests
{
    [Fact]
    public void GivenReciprocalMeasurements_WhenMultiplied_ThenDimensionlessAndValueMultiplies()
    {
        // Arrange
        var speed = new Int32Measurement(5, Unit.SI.m / Unit.SI.s);
        var inverse = new Int32Measurement(2, Unit.SI.s / Unit.SI.m);

        // Act
        var result = speed * inverse; // should be dimensionless

        // Assert
        result.Unit.Should().Be(Unit.None);
        result.Value.Should().Be(10);
    }
}