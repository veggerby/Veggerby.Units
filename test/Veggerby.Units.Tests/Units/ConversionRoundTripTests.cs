using AwesomeAssertions;
using Veggerby.Units.Conversion;
using Xunit;

namespace Veggerby.Units.Tests.Units;

public class ConversionRoundTripTests
{
    [Fact]
    public void Given_IntMeasurement_When_RoundTripConversion_Then_ValueExact()
    {
        // Arrange
        var original = new Int32Measurement(1, Prefix.k * Unit.SI.m); // 1000 m

        // Act
        var roundTrip = original.ConvertTo(Unit.SI.m).ConvertTo(Prefix.k * Unit.SI.m);

        // Assert
        roundTrip.Value.Should().Be(original.Value);
        roundTrip.Unit.Should().Be(original.Unit);
    }

    [Fact]
    public void Given_DoubleMeasurement_When_RoundTripConversion_Then_ValueWithinTolerance()
    {
        // Arrange
        var original = new DoubleMeasurement(3.14159265, Unit.SI.m);
        var target = Prefix.k * Unit.SI.m; // km

        // Act
        var roundTrip = original.ConvertTo(target).ConvertTo(Unit.SI.m);

        // Assert
        ((double)roundTrip).Should().BeApproximately(original.Value, 1E-12);
        roundTrip.Unit.Should().Be(Unit.SI.m);
    }
}
