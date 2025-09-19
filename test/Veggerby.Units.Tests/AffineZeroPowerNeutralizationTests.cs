using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class AffineZeroPowerNeutralizationTests
{
    [Fact]
    public void GivenAffineUnitZeroPower_WhenMultiplied_ThenNeutralizes()
    {
        var neutral = (Unit.SI.C ^ 0) * Unit.SI.m; // C^0 => None
        neutral.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenCelsiusZeroPower_WhenRaised_ThenReturnsNone()
    {
        (Unit.SI.C ^ 0).Should().Be(Unit.None);
    }

    [Fact]
    public void GivenKelvinZeroPower_WhenRaised_ThenReturnsNone()
    {
        (Unit.SI.K ^ 0).Should().Be(Unit.None);
    }

    [Fact]
    public void GivenFahrenheitZeroPower_WhenRaised_ThenReturnsNone()
    {
        (Unit.Imperial.F ^ 0).Should().Be(Unit.None);
    }

    [Fact]
    public void GivenFahrenheitZeroPower_WhenMultiplied_ThenNeutralizes()
    {
        var neutral = (Unit.Imperial.F ^ 0) * Unit.SI.kg;
        neutral.Should().Be(Unit.SI.kg);
    }
}