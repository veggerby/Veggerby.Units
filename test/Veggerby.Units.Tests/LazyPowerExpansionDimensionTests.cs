using AwesomeAssertions;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

using Xunit;

namespace Veggerby.Units.Tests;

public class LazyPowerExpansionDimensionTests
{
    [Fact]
    public void GivenCompositeProductDimension_WhenLazyExpansionEnabled_ThenStructureIsPowerDimensionAndEqualsDistributed()
    {
        // Arrange
        var original = ReductionSettings.LazyPowerExpansion;
        var composite = Dimension.Length * Dimension.Time; // LT
        try
        {
            ReductionSettings.LazyPowerExpansion = false;
            var eager = composite ^ 3; // L^3T^3
            ReductionSettings.LazyPowerExpansion = true;
            var lazy = composite ^ 3; // PowerDimension(Product,3)

            lazy.GetType().Name.Should().Be("PowerDimension");
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = original;
        }
    }
}