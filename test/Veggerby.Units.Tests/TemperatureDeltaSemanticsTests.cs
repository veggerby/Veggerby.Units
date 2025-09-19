using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class TemperatureDeltaSemanticsTests
{
    [Fact]
    public void GivenTwoAbsoluteCelsius_WhenSubtracted_ThenDeltaCelsius()
    {
        var t1 = new Int32Measurement(30, Unit.SI.C); // 30°C
        var t2 = new Int32Measurement(20, Unit.SI.C); // 20°C

        var delta = t1 - t2; // Absolute - Absolute -> Delta (still uses °C unit for differences)

        delta.Unit.Should().Be(Unit.SI.C);
        delta.Value.Should().Be(10);
    }

    [Fact]
    public void GivenAbsoluteCelsiusAndDelta_WhenAdded_ThenAbsolute()
    {
        var absolute = new Int32Measurement(25, Unit.SI.C);
        var delta = new Int32Measurement(5, Unit.SI.C); // interpret as delta

        var result = absolute + delta; // Absolute ± Delta -> Absolute

        result.Unit.Should().Be(Unit.SI.C);
        result.Value.Should().Be(30);
    }

    [Fact]
    public void GivenAbsoluteCelsiusAndDelta_WhenSubtracted_ThenAbsolute()
    {
        var absolute = new Int32Measurement(25, Unit.SI.C);
        var delta = new Int32Measurement(5, Unit.SI.C);

        var result = absolute - delta; // Absolute ± Delta -> Absolute

        result.Unit.Should().Be(Unit.SI.C);
        result.Value.Should().Be(20);
    }
}