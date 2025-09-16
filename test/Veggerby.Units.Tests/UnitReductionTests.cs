using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitReductionTests
{
    [Fact]
    public void GivenProductWithRepeatedOperand_WhenReducing_ThenConvertsToPower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 3); // m^3

        // Act
        var actual = (Unit.SI.m * (Unit.SI.m ^ 2)); // m*m^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductChainWithRepeatedOperands_WhenReducing_ThenAggregatesToPower()
    {
        // Arrange
        var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 6), Unit.SI.s); // m^6s

        // Act
        var actual = ((Unit.SI.m ^ 3) * (Unit.SI.m ^ 2) * Unit.SI.s * Unit.SI.m); // m^3^m^2sm

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWithSameOperandsDifferentPowers_WhenReducing_ThenSubtractsExponents()
    {
        // Arrange
        var expected = Unit.SI.m; // m

        // Act
        var actual = ((Unit.SI.m ^ 2) / Unit.SI.m); // m^2/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionOfIdenticalUnits_WhenReducing_ThenCancelsToNone()
    {
        // Arrange
        var expected = Unit.None; // completely reduced

        // Act
        var actual = (Unit.SI.m / Unit.SI.m); // m/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWhereDividendHasHigherPower_WhenReducing_ThenResultHasRemainingPositivePower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // m^2

        // Act
        var actual = ((Unit.SI.m ^ 3) / Unit.SI.m); // m^3/m

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWhereDivisorHasHigherPower_WhenReducing_ThenResultIsReciprocalPower()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.m, 2)); //  1/m^2

        // Act
        var actual = (Unit.SI.m / (Unit.SI.m ^ 3)); // m/m^3

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenComplexExpression_WhenReducing_ThenYieldsExpectedCanonicalForm()
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
    public void GivenProductContainingDivisionPair_WhenReducing_ThenIntermediateCancelsLeavingSingleUnit()
    {
        // Arrange
        var expected = Unit.SI.kg; // m * (kg / m) => kg

        // Act
        var actual = Unit.SI.m * (Unit.SI.kg / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenInterleavedProductDivisionChain_WhenReducing_ThenAllIntermediateUnitsCancel()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * kg / s) * (s / m) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.kg / Unit.SI.s) * (Unit.SI.s / Unit.SI.m);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductAndDivisionWithMultipleSharedOperands_WhenReducing_ThenOnlyUniqueUnitRemains()
    {
        // Arrange
        var expected = Unit.SI.kg; // (m * s * kg)/(m * s) => kg

        // Act
        var actual = (Unit.SI.m * Unit.SI.s * Unit.SI.kg) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenBalancedProductAndDivisionOperands_WhenReducing_ThenAllCancelToNone()
    {
        // Arrange
        var expected = Unit.None; // (m * s)/(m * s) => 1

        // Act
        var actual = (Unit.SI.m * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenProductWithPowerAndMatchingDivisor_WhenReducing_ThenCancelsCompletely()
    {
        // Arrange
        var expected = Unit.None; // (m * m^2) / m^3 => 1

        // Act
        var actual = (Unit.SI.m * (Unit.SI.m ^ 2)) / (Unit.SI.m ^ 3);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionOfDifferingPowers_WhenReducing_ThenRemainingPositiveExponentReflectsDifference()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^4)/(m^2) => m^2

        // Act
        var actual = (Unit.SI.m ^ 4) / (Unit.SI.m ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenMixedProductPowerDivision_WhenReducing_ThenResultsInPower()
    {
        // Arrange
        var expected = Unit.Power(Unit.SI.m, 2); // (m^3 * s)/(m * s) => m^2

        // Act
        var actual = ((Unit.SI.m ^ 3) * Unit.SI.s) / (Unit.SI.m * Unit.SI.s);

        // Assert
        actual.Should().Be(expected);
    }
}