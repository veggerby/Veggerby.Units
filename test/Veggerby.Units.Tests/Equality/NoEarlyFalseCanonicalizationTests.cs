using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests.Equality;

public class NoEarlyFalseCanonicalizationTests
{
    [Fact]
    public void Equality_NoEarlyFalseBeforeCanonicalization()
    {
        // Arrange
        var origNorm = ReductionSettings.EqualityNormalizationEnabled;
        var origLazy = ReductionSettings.LazyPowerExpansion;
        ReductionSettings.EqualityNormalizationEnabled = true;
        ReductionSettings.LazyPowerExpansion = true;
        try
        {
            // Craft structure where structural product comparison (ordering differences) would fail
            var a = ((Unit.SI.m * Unit.SI.s) ^ 3) * Unit.SI.m; // ((m*s)^3)*m => m^4 s^3
            var b = (Unit.SI.m ^ 4) * (Unit.SI.s ^ 3);      // distributed
            (a == b).Should().BeTrue(); // will exercise canonical multiset path first
        }
        finally
        {
            ReductionSettings.EqualityNormalizationEnabled = origNorm;
            ReductionSettings.LazyPowerExpansion = origLazy;
        }
    }
}
