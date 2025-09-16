using AwesomeAssertions;

using Veggerby.Units.Reduction;

using Xunit;

namespace Veggerby.Units.Tests;

public class LazyPowerExpansionTests
{
    [Fact]
    public void GivenCompositeProductAndLazyExpansionEnabled_WhenRaisingPower_ThenStructureIsPowerUnitAndEqualsEager()
    {
        // Arrange
        var original = ReductionSettings.LazyPowerExpansion;
        var originalFV = ReductionSettings.UseFactorVector;
        var composite = Unit.SI.m * Unit.SI.s; // product

        try
        {
            ReductionSettings.UseFactorVector = false; // isolate
            ReductionSettings.LazyPowerExpansion = false; // eager first
            var eager = composite ^ 3; // distributed
            ReductionSettings.LazyPowerExpansion = true; // enable lazy for comparison path
            var lazy = composite ^ 3; // PowerUnit(Product,3)

            // Assert
            lazy.GetType().Name.Should().Be("PowerUnit");
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = original;
            ReductionSettings.UseFactorVector = originalFV;
        }
    }
    [Fact]
    public void GivenNegativeExponent_WhenLazyExpansionEnabled_ThenReciprocalAppliesCorrectly()
    {
        // Arrange
        var original = ReductionSettings.LazyPowerExpansion;
        var originalFV = ReductionSettings.UseFactorVector;
        var composite = Unit.SI.m * Unit.SI.s; // product
        try
        {
            ReductionSettings.UseFactorVector = false;
            ReductionSettings.LazyPowerExpansion = true;
            var reciprocal = composite ^ -2; // => 1 / ( (m*s)^2 )

            // Act
            var again = reciprocal ^ -1; // ((1/(m*s)^2)^-1) => (m*s)^2

            // Assert
            (again == (composite ^ 2)).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = original;
            ReductionSettings.UseFactorVector = originalFV;
        }
    }

    [Fact]
    public void GivenLazyAndEagerExpansion_WhenComparingSemanticResult_ThenTheyAreEqual()
    {
        // Arrange
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        var originalFV = ReductionSettings.UseFactorVector;
        var composite = Unit.SI.m * Unit.SI.s;
        ReductionSettings.LazyPowerExpansion = false;
        var eager = composite ^ 4; // distributed

        try
        {
            ReductionSettings.UseFactorVector = false;
            ReductionSettings.LazyPowerExpansion = true;
            var lazy = composite ^ 4; // PowerUnit wrapper (lazy)
            lazy.GetType().Name.Should().Be("PowerUnit");
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = originalLazy;
            ReductionSettings.UseFactorVector = originalFV;
        }
    }

    [Fact]
    public void GivenLargeComposite_WhenLazyExpansionEnabled_ThenLazyEqualsEagerForHigherExponent()
    {
        // Arrange
        var original = ReductionSettings.LazyPowerExpansion;
        var originalFV = ReductionSettings.UseFactorVector;
        var composite = Unit.SI.m * Unit.SI.s * Unit.SI.kg * Unit.SI.m; // m s kg m => m^2 s kg
        try
        {
            ReductionSettings.UseFactorVector = false;
            ReductionSettings.LazyPowerExpansion = false;
            var eager = composite ^ 5; // distributed (m^2)^5 * s^5 * kg^5 => m^10 s^5 kg^5
            ReductionSettings.LazyPowerExpansion = true;
            var lazy = composite ^ 5; // PowerUnit
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = original;
            ReductionSettings.UseFactorVector = originalFV;
        }
    }
}