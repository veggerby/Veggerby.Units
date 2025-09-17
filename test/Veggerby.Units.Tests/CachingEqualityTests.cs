using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests;

public class CachingEqualityTests
{
    [Fact]
    public void Caching_DoesNotAffectEquality_FirstRun()
    {
        // Arrange
        var origFV = ReductionSettings.UseFactorVector;
        var origNorm = ReductionSettings.EqualityNormalizationEnabled;
        var origLazy = ReductionSettings.LazyPowerExpansion;
        try
        {
            ReductionSettings.EqualityNormalizationEnabled = true;
            ReductionSettings.LazyPowerExpansion = true;
            ReductionSettings.UseFactorVector = false; // cold (no cached vectors)
            var a = (Unit.SI.m * Unit.SI.s * Unit.SI.m) ^ 4; // m^8 s^4
            var b = (Unit.SI.m ^ 8) * (Unit.SI.s ^ 4);
            var first = a == b; // warm caches none

            ReductionSettings.UseFactorVector = true; // enable caching path
            var second = a == b; // will compute & cache factor vectors where supported

            first.Should().BeTrue();
            second.Should().BeTrue();
        }
        finally
        {
            ReductionSettings.UseFactorVector = origFV;
            ReductionSettings.EqualityNormalizationEnabled = origNorm;
            ReductionSettings.LazyPowerExpansion = origLazy;
        }
    }
}
