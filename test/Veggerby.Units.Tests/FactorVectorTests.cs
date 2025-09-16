using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests;

public class FactorVectorTests
{
    [Fact]
    public void GivenEquivalentLargeProducts_WhenFastPathEnabled_ThenEqualityMatchesLegacy()
    {
        // Arrange
        var original = ReductionSettings.UseFactorVector;
        var left = (Unit.SI.m * Unit.SI.s * Unit.SI.kg) * (Unit.SI.s ^ 2);
        var right = (Unit.SI.kg * (Unit.SI.s ^ 3) * Unit.SI.m);

        try
        {
            ReductionSettings.UseFactorVector = false;
            var legacy = left == right;

            ReductionSettings.UseFactorVector = true;
            var fast = left == right;

            // Assert
            legacy.Should().BeTrue();
            fast.Should().BeTrue();
        }
        finally
        {
            ReductionSettings.UseFactorVector = original;
        }
    }

    [Fact]
    public void GivenDifferentExponentProducts_WhenFastPathEnabled_ThenInequalityMatchesLegacy()
    {
        // Arrange
        var original = ReductionSettings.UseFactorVector;
        var left = (Unit.SI.m ^ 2) * Unit.SI.s;
        var right = (Unit.SI.m ^ 3) * Unit.SI.s;
        try
        {
            ReductionSettings.UseFactorVector = false;
            var legacy = left == right;
            ReductionSettings.UseFactorVector = true;
            var fast = left == right;

            // Assert
            legacy.Should().BeFalse();
            fast.Should().BeFalse();
        }
        finally
        {
            ReductionSettings.UseFactorVector = original;
        }
    }
}
