using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitStructuralEqualityEdgeTests
{
    [Fact]
    public void GivenProductsWithDifferentAuthoringOrder_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = Unit.SI.m * Unit.SI.s * Unit.SI.kg;
        var right = Unit.SI.kg * Unit.SI.m * Unit.SI.s;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenNestedDivisionPatternsThatNormalizeSameStructure_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Unit.SI.m / Unit.SI.s) / (Unit.SI.kg / Unit.SI.kg); // => (m/s)/(kg/kg) => (m/s)/(1) => m/s
        var right = Unit.SI.m / Unit.SI.s;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentCompositeViaPowerDistribution_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Unit.SI.m * Unit.SI.s) ^ 2; // (m*s)^2 => m^2 * s^2
        var right = (Unit.SI.m ^ 2) * (Unit.SI.s ^ 2);

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentExponentCombinationResults_WhenComparingInequalityOperator_ThenTheyAreNotEqual()
    {
        // Arrange
        var left = (Unit.SI.m ^ 2) ^ 3; // => m^6
        var right = Unit.SI.m ^ 5;      // m^5

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDivisionWithCancelledNullUnit_WhenComparingEqualityOperator_ThenTheyAreEqual()
    {
        // Arrange
        var left = (Unit.None / Unit.SI.m) ^ 2;   // (1/m)^2 => 1/m^2
        var right = Unit.None / (Unit.SI.m ^ 2);  // 1/m^2

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }
}