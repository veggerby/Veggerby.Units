using AwesomeAssertions;

using Veggerby.Units.Tests.Infrastructure;

using Xunit;

namespace Veggerby.Units.Tests;

[Collection(ReductionSettingsCollection.Name)]
public class TrivialPowerTests
{
    [Fact]
    public void Power_ExponentZero_Vanish()
    {
        ReductionSettingsBaseline.AssertDefaults();
        var u = Unit.SI.m * Unit.SI.s; // composite
        var zero = u ^ 0; // expectation: identity (Unit.None) or neutral element
        zero.Symbol.Should().BeEmpty();
    }

    [Fact]
    public void Power_ExponentOne_NoExpand()
    {
        ReductionSettingsBaseline.AssertDefaults();
        var u = Unit.SI.m * Unit.SI.s;
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), lazyPowerExpansion: true))
        {
            var one = u ^ 1;
            (one == u).Should().BeTrue();
        }
    }
}