using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionEqualityTests
{
    [Fact]
    public void Dimension_DimensionInEquality_TestSimple()
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
    public void Dimension_DimensionEquality_TestProduct()
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
    public void Dimension_DimensionInEquality_TestProduct()
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
    public void Dimension_DimensionEquality_TestDivision()
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
    public void Dimension_DimensionInEquality_TestDivision()
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
    public void Dimension_DimensionEquality_TestPower()
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
    public void Dimension_DimensionInEquality_TestPowerBaseDifferent()
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
    public void Dimension_DimensionInEquality_TestPowerExponentDifferent()
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
