using AwesomeAssertions;

using Veggerby.Units.Reduction;
using Veggerby.Units.Tests.Infrastructure;

using Xunit;

namespace Veggerby.Units.Tests;

[Collection(ReductionSettingsCollection.Name)]
public class CachingEqualityTests
{
    [Fact]
    public void Caching_DoesNotAffectEquality_FirstRun()
    {
        ReductionSettingsBaseline.AssertDefaults();
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), equalityNormalizationEnabled: true, lazyPowerExpansion: true, useFactorVector: false))
        {
            var a = (Unit.SI.m * Unit.SI.s * Unit.SI.m) ^ 4; // m^8 s^4
            var b = (Unit.SI.m ^ 8) * (Unit.SI.s ^ 4);
            var first = a == b; // warm caches none

            ReductionSettings.UseFactorVector = true; // enable caching path inside same scope
            var second = a == b; // will compute & cache factor vectors where supported

            first.Should().BeTrue();
            second.Should().BeTrue();
        }
    }
}