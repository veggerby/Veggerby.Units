using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionEqualityTests
{
    [Fact]
    public void GivenDifferentBasicDimensions_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length;
        var right = Dimension.Mass;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentProductDimensions_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length * Dimension.Mass;
        var right = Dimension.Length * Dimension.Mass;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentProductDimensions_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length * Dimension.Mass;
        var right = Dimension.Time * Dimension.Mass;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentDivisionDimensions_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length / Dimension.Mass;
        var right = Dimension.Length / Dimension.Mass;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentDivisionDimensions_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length / Dimension.Mass;
        var right = Dimension.Time / Dimension.Mass;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentPowerDimensions_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length ^ 2;
        var right = Dimension.Length ^ 2;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenPowerDimensionsWithDifferentBases_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length ^ 2;
        var right = Dimension.Time ^ 2;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenPowerDimensionsWithDifferentExponents_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Dimension.Length ^ 2;
        var right = Dimension.Length ^ 3;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }
}