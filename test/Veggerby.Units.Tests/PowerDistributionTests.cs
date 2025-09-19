using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class PowerDistributionTests
{
    [Fact]
    public void GivenProductRaisedToPower_WhenExpanded_ThenDistributed()
    {
        var product = Unit.SI.m * Unit.SI.s; // m·s
        var p3 = product ^ 3;
        p3.Should().Be((Unit.SI.m ^ 3) * (Unit.SI.s ^ 3));
    }

    [Fact]
    public void GivenQuotientRaisedToPower_WhenExpanded_ThenDistributed()
    {
        var quotient = Unit.SI.kg / (Unit.SI.m * Unit.SI.s); // kg/(m·s)
        var p2 = quotient ^ 2;
        p2.Should().Be((Unit.SI.kg ^ 2) / ((Unit.SI.m ^ 2) * (Unit.SI.s ^ 2)));
    }
}