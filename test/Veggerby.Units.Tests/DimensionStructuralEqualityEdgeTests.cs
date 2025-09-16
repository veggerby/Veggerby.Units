using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionStructuralEqualityEdgeTests
{
    [Fact]
    public void GivenProductsWithDifferentAuthoringOrder_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = Dimension.Length * Dimension.Time * Dimension.Mass;
        var right = Dimension.Mass * Dimension.Length * Dimension.Time;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenNestedDivisionPatternsThatNormalizeSameStructure_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Dimension.Length / Dimension.Time) / (Dimension.Mass / Dimension.Mass); // => (L/T)/(M/M) => L/T
        var right = Dimension.Length / Dimension.Time;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentCompositeViaPowerDistribution_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Dimension.Length * Dimension.Time) ^ 3; // (L*T)^3 => L^3 * T^3
        var right = (Dimension.Length ^ 3) * (Dimension.Time ^ 3);

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentExponentCombinationResults_WhenComparingInequalityOperator_ThenTheyAreNotEqual()
    {
        // Arrange
        var left = (Dimension.Length ^ 2) ^ 3; // => L^6
        var right = Dimension.Length ^ 5;      // L^5

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDivisionWithCancelledNoneDimension_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Dimension.None / Dimension.Length) ^ 2;   // (1/L)^2 => 1/L^2
        var right = Dimension.None / (Dimension.Length ^ 2);  // 1/L^2

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }
}