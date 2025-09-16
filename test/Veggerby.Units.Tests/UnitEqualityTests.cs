using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitEqualityTests
{
    [Fact]
    public void Unit_UnitEquality_TestSimple()
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
    public void Unit_UnitInEquality_TestSimple()
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
    public void Unit_UnitEquality_TestProduct()
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
    public void Unit_UnitInEquality_TestProduct()
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
    public void Unit_UnitEquality_TestDivision()
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
    public void Unit_UnitInEquality_TestDivision()
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
    public void Unit_UnitEquality_TestPower()
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
    public void Unit_UnitInEquality_TestPowerBaseDifferent()
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
    public void Unit_UnitInEquality_TestPowerExponentDifferent()
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