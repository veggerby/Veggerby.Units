using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitDivisionTests
{
    [Fact]
    public void GivenTwoUnits_WhenDividing_ThenResultIsDivisionUnit()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.kg;

        // Act
        // Assert
        unit.Should().BeOfType<DivisionUnit>();
    }

    [Fact]
    public void GivenDivisionUnit_WhenAccessingSymbol_ThenReturnsQuotientSymbol()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m/s");
    }

    [Fact]
    public void GivenDivisionUnit_WhenAccessingName_ThenReturnsQuotientName()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter / second");
    }

    [Fact]
    public void GivenDivisionOfDivision_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.Multiply(Unit.SI.kg, Unit.SI.A)); // ms/kgA

        // Act
        var actual = (Unit.SI.m / Unit.SI.kg) / (Unit.SI.A / Unit.SI.s); // (m/kg)/(A/s)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenSimpleDividedByDivision_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.SI.A); // ms/A

        // Act
        var actual = Unit.SI.m / (Unit.SI.A / Unit.SI.s); // m/(A/s)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionDividedBySimple_WhenRearranged_ThenYieldsExpectedCanonicalDivision()
    {
        // Arrange
        var expected = Unit.Divide(Unit.SI.m, Unit.Multiply(Unit.SI.s, Unit.SI.A)); // m/sA

        // Act
        var actual = (Unit.SI.m / Unit.SI.A) / Unit.SI.s; // (m/A)/s

        // Assert
        actual.Should().Be(expected);
    }
}