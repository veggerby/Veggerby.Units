using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitsTests
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

    [Fact]
    public void Unit_UnitMeter_TestSymbol()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m");
    }

    [Fact]
    public void Unit_UnitMeter_TestName()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter");
    }

    [Fact]
    public void Unit_UnitMeter_TestSystem()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void Unit_UnitMeter_TestDimension()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Length);
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectType()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        // Assert
        unit.Should().BeOfType<ProductUnit>();
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectSymbol()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("mkg");
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectName()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter * kilogram");
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectDimension()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var dimension = unit.Dimension;

        // Assert
        dimension.Should().Be(Dimension.Mass * Dimension.Length);
    }

    [Fact]
    public void Unit_ProductUnit_ReturnsCorrectSystem()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.kg;

        // Act
        var system = unit.System;

        // Assert
        system.Should().Be(Unit.SI);
    }

    [Fact]
    public void Unit_ProductUnit_IsCommutative()
    {
        // Arrange
        var d1 = Unit.SI.m * Unit.SI.kg;
        var d2 = Unit.SI.kg * Unit.SI.m;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Unit_ProductUnit_IsCommutativeWithMultiple()
    {
        // Arrange
        var d1 = Unit.SI.A * (Unit.SI.m * Unit.SI.kg);
        var d2 = (Unit.SI.kg * Unit.SI.A) * Unit.SI.m;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Unit_DivisionUnit_ReturnsCorrectType()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.kg;

        // Act
        // Assert
        unit.Should().BeOfType<DivisionUnit>();
    }

    [Fact]
    public void Unit_DivisionUnit_ReturnsCorrectSymbol()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m/s");
    }

    [Fact]
    public void Unit_DivisionUnit_ReturnsCorrectName()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter / second");
    }

    [Fact]
    public void Unit_DivisionUnitRearrangeDivByDiv_ReturnsCorrect()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.Multiply(Unit.SI.kg, Unit.SI.A)); // ms/kgA

        // Act
        var actual = (Unit.SI.m / Unit.SI.kg) / (Unit.SI.A / Unit.SI.s); // (m/kg)/(A/s)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionUnitRearrangeSimpleByDiv_ReturnsCorrect()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.SI.A); // ms/A

        // Act
        var actual = Unit.SI.m / (Unit.SI.A / Unit.SI.s); // m/(A/s)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_DivisionUnitRearrangeDivBySimple_ReturnsCorrect()
    {
        // Arrange
        var expected = Unit.Divide(Unit.SI.m, Unit.Multiply(Unit.SI.s, Unit.SI.A)); // m/sA

        // Act
        var actual = (Unit.SI.m / Unit.SI.A) / Unit.SI.s; // (m/A)/s

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_PowerUnit_ReturnsCorrectType()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        // Assert
        unit.Should().BeOfType<PowerUnit>();
    }

    [Fact]
    public void Unit_PowerUnit_ReturnsCorrectSymbol()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var symbol = unit.Symbol;

        // Assert
        symbol.Should().Be("m^2");
    }

    [Fact]
    public void Unit_PowerUnit_ReturnsCorrectName()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var name = unit.Name;

        // Assert
        name.Should().Be("meter ^ 2");
    }

    [Fact]
    public void Unit_PowerUnit0_ReturnsNone()
    {
        // Arrange
        var expected = Unit.None; // 1 == None

        // Act
        var actual = Unit.SI.s ^ 0; // s^0

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_PowerUnit1_ReturnsBase()
    {
        // Arrange
        var expected = Unit.SI.s; // s

        // Act
        var actual = Unit.SI.s ^ 1; // s^1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_PowerUnitMinus1_ReturnsDivision()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.SI.s); // 1/s

        // Act
        var actual = Unit.SI.s ^ -1; // s^-1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_PowerUnitNegative_ReturnsDivision()
    {
        // Arrange
        var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.s, 2)); // 1/s^2

        // Act
        var actual = Unit.SI.s ^ -2; // s^-2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_MultiplePowerUnit_ExpandsEachPower()
    {
        // Arrange
        var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2));

        // Act
        var actual = ((Unit.SI.m * Unit.SI.s) ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Unit_MultiplePowerUnitWithDivision_ExpandsEachPower()
    {
        // Arrange
        var expected = Unit.Divide(Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2)), Unit.Power(Unit.SI.kg, 2)); // m^2s^2/kg^2;

        // Act
        var actual = ((Unit.SI.m * Unit.SI.s / Unit.SI.kg) ^ 2); // (ms/kg)^2

        // Assert
        actual.Should().Be(expected);
    }

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

    [Fact]
    public void Unit_PrefixUnits_EqualsFails()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Unit_PrefixUnits_EqualityFails()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Unit_ScaleUnits_EqualsFails()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Unit_ScaleUnits_EqualityFails()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }
}