using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionsTests
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

    [Fact]
    public void Dimension_DimensionLength_TestSymbol()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L");
    }

    [Fact]
    public void Dimension_DimensionLength_TestName()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length");
    }

    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectType()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        // Assert
        dimension.Should().BeOfType<ProductDimension>();
    }

    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectSymbol()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("LM");
    }

    [Fact]
    public void Dimension_ProductDimension_ReturnsCorrectName()
    {
        // Arrange
        var dimension = Dimension.Length * Dimension.Mass;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length * mass");
    }

    [Fact]
    public void Dimension_ProductDimension_IsCommutative()
    {
        // Arrange
        var d1 = Dimension.Length * Dimension.Mass;
        var d2 = Dimension.Mass * Dimension.Length;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Dimension_ProductDimension_IsCommutativeWithMultiple()
    {
        // Arrange
        var d1 = Dimension.ElectricCurrent * (Dimension.Length * Dimension.Mass);
        var d2 = (Dimension.Mass * Dimension.ElectricCurrent) * Dimension.Length;

        // Act
        var equal = d1 == d2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Dimension_DivisionDimension_ReturnsCorrectType()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Mass;

        // Act
        // Assert
        dimension.Should().BeOfType<DivisionDimension>();
    }

    [Fact]
    public void Dimension_DivisionDimension_ReturnsCorrectSymbol()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Time;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L/T");
    }

    [Fact]
    public void Dimension_DivisionDimension_ReturnsCorrectName()
    {
        // Arrange
        var dimension = Dimension.Length / Dimension.Time;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length / time");
    }

    [Fact]
    public void Dimension_DivisionDimensionRearrangeDivByDiv_ReturnsCorrect()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Length, Dimension.Time), Dimension.Multiply(Dimension.Mass, Dimension.ElectricCurrent)); // LT/MI

        // Act
        var actual = (Dimension.Length / Dimension.Mass) / (Dimension.ElectricCurrent / Dimension.Time); // (L/M)/(I/T)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionDimensionRearrangeSimpleByDiv_ReturnsCorrect()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Length, Dimension.Time), Dimension.ElectricCurrent); // LT/I

        // Act
        var actual = Dimension.Length / (Dimension.ElectricCurrent / Dimension.Time); // L/(I/T)

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionDimensionRearrangeDivBySimple_ReturnsCorrect()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Length, Dimension.Multiply(Dimension.Time, Dimension.ElectricCurrent)); // L/TI

        // Act
        var actual = (Dimension.Length / Dimension.ElectricCurrent) / Dimension.Time; // (L/I)/T

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_PowerDimension_ReturnsCorrectType()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        // Assert
        dimension.Should().BeOfType<PowerDimension>();
    }

    [Fact]
    public void Dimension_PowerDimension_ReturnsCorrectSymbol()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L^2");
    }

    [Fact]
    public void Dimension_PowerDimension_ReturnsCorrectName()
    {
        // Arrange
        var dimension = Dimension.Length ^ 2;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length ^ 2");
    }

    [Fact]
    public void Dimension_PowerDimension0_ReturnsNone()
    {
        // Arrange
        var expected = Dimension.None; // 1 == None

        // Act
        var actual = Dimension.Time ^ 0; // T^0

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_PowerDimension1_ReturnsBase()
    {
        // Arrange
        var expected = Dimension.Time; // T

        // Act
        var actual = Dimension.Time ^ 1; // T^1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_PowerDimensionMinus1_ReturnsDivision()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Time); // 1/T

        // Act
        var actual = Dimension.Time ^ -1; // T^-1

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_PowerDimensionNegative_ReturnsDivision()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Power(Dimension.Time, 2)); // 1/T^2

        // Act
        var actual = Dimension.Time ^ -2; // T^-2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_MultiplePowerDimension_ExpandsEachPower()
    {
        // Arrange
        var expected = Dimension.Multiply(Dimension.Power(Dimension.Length, 2), Dimension.Power(Dimension.Time, 2));

        // Act
        var actual = ((Dimension.Length * Dimension.Time) ^ 2);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_MultiplePowerDimensionWithDivision_ExpandsEachPower()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Multiply(Dimension.Power(Dimension.Length, 2), Dimension.Power(Dimension.Time, 2)), Dimension.Power(Dimension.Mass, 2)); // L^2T^2/M^2;

        // Act
        var actual = ((Dimension.Length * Dimension.Time / Dimension.Mass) ^ 2); // (LT/M)^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_MultipleIdenticalOperandsForProduct_ReduceToPower()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 3); // L^3

        // Act
        var actual = (Dimension.Length * (Dimension.Length ^ 2)); // L*L^2

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
    {
        // Arrange
        var expected = Dimension.Multiply(Dimension.Power(Dimension.Length, 6), Dimension.Time); // L^6T

        // Act
        var actual = ((Dimension.Length ^ 3) * (Dimension.Length ^ 2) * Dimension.Time * Dimension.Length); // L^3^L^2TL

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperands_ShouldReduceOperands()
    {
        // Arrange
        var expected = Dimension.Length; // L

        // Act
        var actual = ((Dimension.Length ^ 2) / Dimension.Length); // L^2/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
    {
        // Arrange
        var expected = Dimension.None; // completely reduced

        // Act
        var actual = (Dimension.Length / Dimension.Length); // L/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Dimension.Power(Dimension.Length, 2); // L^2

        // Act
        var actual = ((Dimension.Length ^ 3) / Dimension.Length); // L^3/L

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.None, Dimension.Power(Dimension.Length, 2)); //  1/L^2

        // Act
        var actual = (Dimension.Length / (Dimension.Length ^ 3)); // L/L^3

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Dimension_ComplexReduction_YieldsExpected()
    {
        // Arrange
        var expected = Dimension.Divide(Dimension.Mass, Dimension.Multiply(Dimension.Time, Dimension.Length)); // M/TL

        // Act
        var actual = (Dimension.Time * (((Dimension.Length ^ 2) * Dimension.Mass) / ((Dimension.Time ^ 2) * (Dimension.Length ^ 3)))); // T*L^2*M/(T^2*L^3)

        // Assert
        actual.Should().Be(expected);
    }
}