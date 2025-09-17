using AwesomeAssertions;

using Veggerby.Units.Reduction;
using Veggerby.Units.Tests.Infrastructure;

using Xunit;

namespace Veggerby.Units.Tests;

[Collection(ReductionSettingsCollection.Name)]
public class LazyPowerExpansionTests
{
    [Fact]
    public void GivenCompositeProductAndLazyExpansionEnabled_WhenRaisingPower_ThenStructureIsPowerUnitAndEqualsEager()
    {
        // Arrange
        ReductionSettingsBaseline.AssertDefaults();
        var composite = Unit.SI.m * Unit.SI.s; // product
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), useFactorVector: false, lazyPowerExpansion: false))
        {
            var eager = composite ^ 3; // distributed
            ReductionSettings.LazyPowerExpansion = true; // enable lazy for comparison path
            var lazy = composite ^ 3; // PowerUnit(Product,3)

            // Assert
            lazy.GetType().Name.Should().Be("PowerUnit");
            (lazy == eager).Should().BeTrue();
        }
    }
    [Fact]
    public void GivenNegativeExponent_WhenLazyExpansionEnabled_ThenReciprocalAppliesCorrectly()
    {
        // Arrange
        ReductionSettingsBaseline.AssertDefaults();
        var composite = Unit.SI.m * Unit.SI.s; // product
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), useFactorVector: false, lazyPowerExpansion: true))
        {
            var reciprocal = composite ^ -2; // => 1 / ( (m*s)^2 )

            // Act
            var again = reciprocal ^ -1; // ((1/(m*s)^2)^-1) => (m*s)^2

            // Assert
            (again == (composite ^ 2)).Should().BeTrue();
        }
    }

    [Fact]
    public void GivenLazyAndEagerExpansion_WhenComparingSemanticResult_ThenTheyAreEqual()
    {
        // Arrange
        ReductionSettingsBaseline.AssertDefaults();
        var composite = Unit.SI.m * Unit.SI.s;
        var eagerScope = new ReductionSettingsScope(new ReductionSettingsFixture(), lazyPowerExpansion: false, useFactorVector: false);
        var eager = composite ^ 4; // distributed
        eagerScope.Dispose();
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), lazyPowerExpansion: true, useFactorVector: false))
        {
            var lazy = composite ^ 4; // PowerUnit wrapper (lazy)
            lazy.GetType().Name.Should().Be("PowerUnit");
            (lazy == eager).Should().BeTrue();
        }
    }

    [Fact]
    public void GivenLargeComposite_WhenLazyExpansionEnabled_ThenLazyEqualsEagerForHigherExponent()
    {
        // Arrange
        ReductionSettingsBaseline.AssertDefaults();
        var composite = Unit.SI.m * Unit.SI.s * Unit.SI.kg * Unit.SI.m; // m s kg m => m^2 s kg
        var eagerScope = new ReductionSettingsScope(new ReductionSettingsFixture(), useFactorVector: false, lazyPowerExpansion: false);
        var eager = composite ^ 5; // distributed (m^2)^5 * s^5 * kg^5 => m^10 s^5 kg^5
        eagerScope.Dispose();
        using (var scope = new ReductionSettingsScope(new ReductionSettingsFixture(), useFactorVector: false, lazyPowerExpansion: true))
        {
            ReductionSettings.LazyPowerExpansion = true;
            var lazy = composite ^ 5; // PowerUnit
            (lazy == eager).Should().BeTrue();
        }
    }
}