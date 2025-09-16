using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitReductionTests
{
    [Fact]
    public void Unit_MultipleIdenticalOperandsForProduct_ReduceToPower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 3); // m^3

        // Act
        var actual = (Unit.SI.m * (Unit.SI.m ^ 2)); // m*m^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
    {
        // Arrange
        var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 6), Unit.SI.s); // m^6s

        // Act
        var actual = ((Unit.SI.m ^ 3) * (Unit.SI.m ^ 2) * Unit.SI.s * Unit.SI.m); // m^3^m^2sm

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionOperationWithSameOperands_ShouldReduceOperands()
    {
        // Arrange
        var expected = Unit.SI.m; // m

        // Act
        var actual = ((Unit.SI.m ^ 2) / Unit.SI.m); // m^2/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
    {
        // Arrange
        var expected = Unit.None; // completely reduced

        // Act
        var actual = (Unit.SI.m / Unit.SI.m); // m/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // m^2

        // Act
        var actual = ((Unit.SI.m ^ 3) / Unit.SI.m); // m^3/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.m, 2)); //  1/m^2

        // Act
        var actual = (Unit.SI.m / (Unit.SI.m ^ 3)); // m/m^3

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ComplexReduction_YieldsExpected()
    {
        // Arrange
        var expected = Unit.Divide(Unit.SI.kg, Unit.Multiply(Unit.SI.s, Unit.SI.m)); // kg/sm

        // Act
        var actual = (Unit.SI.s * (((Unit.SI.m ^ 2) * Unit.SI.kg) / ((Unit.SI.s ^ 2) * (Unit.SI.m ^ 3)))); // s*m^2*kg/(s^2*m^3)

        // Assert
        actual.Should().Be(expected);
    }

    // Additional explicit reduction scenarios (previously in ReductionTests)
    [Fact]
    public void Unit_ProductAndDivisionWithCancellation_ReducesToSingleUnit()
    {
        // Arrange
        var expected = Unit.SI.kg; // m * (kg / m) => kg

        // Act
        var actual = Unit.SI.m * (Unit.SI.kg / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductDivisionChain_ReducesToSingleUnit()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * kg / s) * (s / m) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.kg / Unit.SI.s) * (Unit.SI.s / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductAndDivisionMultipleCancellation_ReducesToRemaining()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * s * kg)/(m * s) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.s * Unit.SI.kg) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_NestedCancellation_ReducesToNone()
    {
        // Arrange
        var expected = Unit.None; // (m * s)/(m * s) => 1

        // Act
        var actual = (Unit.SI.m * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductWithPowerCancellation_ReducesCompletely()
    {
        // Arrange
        var expected = Unit.None; // (m * m^2) / m^3 => 1

        // Act
        var actual = (Unit.SI.m * (Unit.SI.m ^ 2)) / (Unit.SI.m ^ 3);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_ProductWithPartialPowerCancellation_ReducesExponent()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^4)/(m^2) => m^2

        // Act
        var actual = (Unit.SI.m ^ 4) / (Unit.SI.m ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_MixedProductPowerDivision_ReducesToPower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^3 * s)/(m * s) => m^2

        // Act
        var actual = ((Unit.SI.m ^ 3) * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }
}