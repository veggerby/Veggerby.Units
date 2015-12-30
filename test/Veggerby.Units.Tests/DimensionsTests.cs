using Xunit;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Tests
{
    public class DimensionsTests
    {
        [Fact]
        public void Dimension_DimensionEquality_TestSimple()
        {
            Assert.True(Dimension.Length == Dimension.Length);
        }

        [Fact]
        public void Dimension_DimensionInEquality_TestSimple()
        {
            Assert.True(Dimension.Length != Dimension.Mass);
        }

        [Fact]
        public void Dimension_DimensionEquality_TestProduct()
        {
            Assert.True((Dimension.Length * Dimension.Mass) == (Dimension.Length * Dimension.Mass));
        }

        [Fact]
        public void Dimension_DimensionInEquality_TestProduct()
        {
            Assert.True((Dimension.Length * Dimension.Mass) != (Dimension.Time * Dimension.Mass));
        }

        [Fact]
        public void Dimension_DimensionEquality_TestDivision()
        {
            Assert.True((Dimension.Length / Dimension.Mass) == (Dimension.Length / Dimension.Mass));
        }

        [Fact]
        public void Dimension_DimensionInEquality_TestDivision()
        {
            Assert.True((Dimension.Length / Dimension.Mass) != (Dimension.Time / Dimension.Mass));
        }

        [Fact]
        public void Dimension_DimensionEquality_TestPower()
        {
            Assert.True((Dimension.Length ^ 2) == (Dimension.Length ^ 2));
        }

        [Fact]
        public void Dimension_DimensionInEquality_TestPowerBaseDifferent()
        {
            Assert.True((Dimension.Length ^ 2) != (Dimension.Time ^ 2));
        }

        [Fact]
        public void Dimension_DimensionInEquality_TestPowerExponentDifferent()
        {
            Assert.True((Dimension.Length ^ 2) != (Dimension.Length ^ 3));
        }

        [Fact]
        public void Dimension_DimensionLength_TestSymbol()
        {
            Assert.Equal("L", Dimension.Length.Symbol);
        }

        [Fact]
        public void Dimension_DimensionLength_TestName()
        {
            Assert.Equal("length", Dimension.Length.Name);
        }

        [Fact]
        public void Dimension_ProductDimension_ReturnsCorrectType()
        {
            Assert.IsType<ProductDimension>(Dimension.Length * Dimension.Mass);
        }

        [Fact]
        public void Dimension_ProductDimension_ReturnsCorrectSymbol()
        {
            Assert.Equal("LM", (Dimension.Length * Dimension.Mass).Symbol);
        }

        [Fact]
        public void Dimension_ProductDimension_ReturnsCorrectName()
        {
            Assert.Equal("length * mass", (Dimension.Length * Dimension.Mass).Name);
        }

        [Fact]
        public void Dimension_ProductDimension_IsCommutative()
        {
            var d1 = Dimension.Length * Dimension.Mass;
            var d2 = Dimension.Mass * Dimension.Length;
            Assert.True(d1 == d2);
        }

        [Fact]
        public void Dimension_ProductDimension_IsCommutativeWithMultiple()
        {
            var d1 = Dimension.ElectricCurrent * (Dimension.Length * Dimension.Mass);
            var d2 = (Dimension.Mass * Dimension.ElectricCurrent) * Dimension.Length;
            Assert.True(d1 == d2);
        }

        [Fact]
        public void Dimension_DivisionDimension_ReturnsCorrectType()
        {
            Assert.IsType<DivisionDimension>(Dimension.Length / Dimension.Mass);
        }

        [Fact]
        public void Dimension_DivisionDimension_ReturnsCorrectSymbol()
        {
            Assert.Equal("L/T", (Dimension.Length / Dimension.Time).Symbol);
        }

        [Fact]
        public void Dimension_DivisionDimension_ReturnsCorrectName()
        {
            Assert.Equal("length / time", (Dimension.Length / Dimension.Time).Name);
        }

        [Fact]
        public void Dimension_DivisionDimensionRearrangeDivByDiv_ReturnsCorrect()
        {
            var actual = (Dimension.Length / Dimension.Mass) / (Dimension.ElectricCurrent / Dimension.Time); // (L/M)/(I/T)
            var expected = Dimension.Div(Dimension.Mult(Dimension.Length, Dimension.Time), Dimension.Mult(Dimension.Mass, Dimension.ElectricCurrent)); // LT/MI
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionDimensionRearrangeSimpleByDiv_ReturnsCorrect()
        {
            var actual = Dimension.Length / (Dimension.ElectricCurrent / Dimension.Time); // L/(I/T)
            var expected = Dimension.Div(Dimension.Mult(Dimension.Length, Dimension.Time), Dimension.ElectricCurrent); // LT/I
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionDimensionRearrangeDivBySimple_ReturnsCorrect()
        {
            var actual = (Dimension.Length / Dimension.ElectricCurrent) / Dimension.Time; // (L/I)/T
            var expected = Dimension.Div(Dimension.Length, Dimension.Mult(Dimension.Time, Dimension.ElectricCurrent)); // L/TI
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_PowerDimension_ReturnsCorrectType()
        {
            Assert.IsType<PowerDimension>(Dimension.Length ^ 2);
        }

        [Fact]
        public void Dimension_PowerDimension_ReturnsCorrectSymbol()
        {
            Assert.Equal("L^2", (Dimension.Length ^ 2).Symbol);
        }

        [Fact]
        public void Dimension_PowerDimension_ReturnsCorrectName()
        {
            Assert.Equal("length ^ 2", (Dimension.Length ^ 2).Name);
        }

        [Fact]
        public void Dimension_PowerDimension0_ReturnsNone()
        {
            var actual = Dimension.Time ^ 0; // T^0
            var expected = Dimension.None; // 1 == None
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_PowerDimension1_ReturnsBase()
        {
            var actual = Dimension.Time ^ 1; // T^1
            var expected = Dimension.Time; // T
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_PowerDimensionMinus1_ReturnsDivision()
        {
            var actual = Dimension.Time ^ -1; // T^-1
            var expected = Dimension.Div(Dimension.None, Dimension.Time); // 1/T
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_PowerDimensionNegative_ReturnsDivision()
        {
            var actual = Dimension.Time ^ -2; // T^-2
            var expected = Dimension.Div(Dimension.None, Dimension.Pow(Dimension.Time, 2)); // 1/T^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_MultiplePowerDimension_ExpandsEachPower()
        {
            var actual = ((Dimension.Length * Dimension.Time) ^ 2);
            var expected = Dimension.Mult(Dimension.Pow(Dimension.Length, 2), Dimension.Pow(Dimension.Time, 2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_MultiplePowerDimensionWithDivision_ExpandsEachPower()
        {
            var actual = ((Dimension.Length * Dimension.Time / Dimension.Mass) ^ 2); // (LT/M)^2
            var expected = Dimension.Div(Dimension.Mult(Dimension.Pow(Dimension.Length, 2), Dimension.Pow(Dimension.Time, 2)), Dimension.Pow(Dimension.Mass, 2)); // L^2T^2/M^2;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_MultipleIdenticalOperandsForProduct_ReduceToPower()
        {
            var actual = (Dimension.Length * (Dimension.Length ^ 2)); // L*L^2
            var expected = Dimension.Pow(Dimension.Length, 3); // L^3
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
        {
            var actual = ((Dimension.Length ^ 3) * (Dimension.Length ^ 2) * Dimension.Time * Dimension.Length); // L^3^L^2TL            
            var expected = Dimension.Mult(Dimension.Pow(Dimension.Length, 6), Dimension.Time); // L^6T
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionOperationWithSameOperands_ShouldReduceOperands()
        {
            var actual = ((Dimension.Length ^ 2) / Dimension.Length); // L^2/L            
            var expected = Dimension.Length; // L
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
        {
            var actual = (Dimension.Length / Dimension.Length); // L/L
            var expected = Dimension.None; // completely reduced
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
        {
            var actual = ((Dimension.Length ^ 3) / Dimension.Length); // L^3/L
            var expected = Dimension.Pow(Dimension.Length, 2); // L^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
        {
            var actual = (Dimension.Length / (Dimension.Length ^ 3)); // L/L^3
            var expected = Dimension.Div(Dimension.None, Dimension.Pow(Dimension.Length, 2)); //  1/L^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Dimension_ComplexReduction_YieldsExpected()
        {
            var actual = (Dimension.Time * (((Dimension.Length ^ 2) * Dimension.Mass) / ((Dimension.Time ^ 2) * (Dimension.Length ^ 3)))); // T*L^2*M/(T^2*L^3)            
            var expected = Dimension.Div(Dimension.Mass, Dimension.Mult(Dimension.Time, Dimension.Length)); // M/TL
            Assert.Equal(expected, actual);
        }
    }
}