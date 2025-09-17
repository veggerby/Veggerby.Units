using AwesomeAssertions;

using Veggerby.Units.Tests.Infrastructure;

using Xunit;

namespace Veggerby.Units.Tests.Equality;

[Collection(ReductionSettingsCollection.Name)]
public class NoEarlyFalseCanonicalizationTests
{
    [Fact]
    public void Equality_NoEarlyFalseBeforeCanonicalization()
    {
        ReductionSettingsBaseline.AssertDefaults();
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), equalityNormalizationEnabled: true, lazyPowerExpansion: true))
        {
            // Craft structure where structural product comparison (ordering differences) would fail
            var a = ((Unit.SI.m * Unit.SI.s) ^ 3) * Unit.SI.m; // ((m*s)^3)*m => m^4 s^3
            var b = (Unit.SI.m ^ 4) * (Unit.SI.s ^ 3);      // distributed
            (a == b).Should().BeTrue(); // will exercise canonical multiset path first
        }
    }
}