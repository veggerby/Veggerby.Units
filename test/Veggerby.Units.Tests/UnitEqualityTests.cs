using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitEqualityTests
{
    [Fact]
    public void GivenIdenticalBaseUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.m;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentBaseUnits_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.kg;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentProductUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m * Unit.SI.kg;
        var right = Unit.SI.m * Unit.SI.kg;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentProductUnits_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m * Unit.SI.kg;
        var right = Unit.SI.s * Unit.SI.kg;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentDivisionUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m / Unit.SI.kg;
        var right = Unit.SI.m / Unit.SI.kg;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentDivisionUnits_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m / Unit.SI.kg;
        var right = Unit.SI.s / Unit.SI.kg;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenEquivalentPowerUnits_WhenComparingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m ^ 2;
        var right = Unit.SI.m ^ 2;

        // Act
        var equal = left == right;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentPowerBases_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m ^ 2;
        var right = Unit.SI.s ^ 2;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentPowerExponents_WhenComparingInequalityOperator_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m ^ 2;
        var right = Unit.SI.m ^ 3;

        // Act
        var notEqual = left != right;

        // Assert
        notEqual.Should().BeTrue();
    }
}