using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class ReciprocalSimplificationChainTests
{
    [Fact]
    public void GivenReciprocalProduct_WhenMultiplied_ThenReducesToNone()
    {
        var unit = (Unit.SI.m / Unit.SI.s) * (Unit.SI.s / Unit.SI.m);
        unit.Should().Be(Unit.None);
    }
}