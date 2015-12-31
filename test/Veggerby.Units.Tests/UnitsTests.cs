using Xunit;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Tests
{
    public class UnitsTests
    {
        [Fact]
        public void Unit_UnitEquality_TestSimple()
        {
            Assert.True(Unit.SI.m == Unit.SI.m);
        }

        [Fact]
        public void Unit_UnitInEquality_TestSimple()
        {
            Assert.True(Unit.SI.m != Unit.SI.kg);
        }

        [Fact]
        public void Unit_UnitEquality_TestProduct()
        {
            Assert.True((Unit.SI.m * Unit.SI.kg) == (Unit.SI.m * Unit.SI.kg));
        }

        [Fact]
        public void Unit_UnitInEquality_TestProduct()
        {
            Assert.True((Unit.SI.m * Unit.SI.kg) != (Unit.SI.s * Unit.SI.kg));
        }

        [Fact]
        public void Unit_UnitEquality_TestDivision()
        {
            Assert.True((Unit.SI.m / Unit.SI.kg) == (Unit.SI.m / Unit.SI.kg));
        }

        [Fact]
        public void Unit_UnitInEquality_TestDivision()
        {
            Assert.True((Unit.SI.m / Unit.SI.kg) != (Unit.SI.s / Unit.SI.kg));
        }

        [Fact]
        public void Unit_UnitEquality_TestPower()
        {
            Assert.True((Unit.SI.m ^ 2) == (Unit.SI.m ^ 2));
        }

        [Fact]
        public void Unit_UnitInEquality_TestPowerBaseDifferent()
        {
            Assert.True((Unit.SI.m ^ 2) != (Unit.SI.s ^ 2));
        }

        [Fact]
        public void Unit_UnitInEquality_TestPowerExponentDifferent()
        {
            Assert.True((Unit.SI.m ^ 2) != (Unit.SI.m ^ 3));
        }

        [Fact]
        public void Unit_UnitMeter_TestSymbol()
        {
            Assert.Equal("m", Unit.SI.m.Symbol);
        }

        [Fact]
        public void Unit_UnitMeter_TestName()
        {
            Assert.Equal("meter", Unit.SI.m.Name);
        }

        [Fact]
        public void Unit_UnitMeter_TestSystem()
        {
            Assert.Equal(Unit.SI, Unit.SI.m.System);
        }

        [Fact]
        public void Unit_UnitMeter_TestDimension()
        {
            Assert.Equal(Dimension.Length, Unit.SI.m.Dimension);
        }

        [Fact]
        public void Unit_ProductUnit_ReturnsCorrectType()
        {
            Assert.IsType<ProductUnit>(Unit.SI.m * Unit.SI.kg);
        }

        [Fact]
        public void Unit_ProductUnit_ReturnsCorrectSymbol()
        {
            Assert.Equal("mkg", (Unit.SI.m * Unit.SI.kg).Symbol);
        }

        [Fact]
        public void Unit_ProductUnit_ReturnsCorrectName()
        {
            Assert.Equal("meter * kilogram", (Unit.SI.m * Unit.SI.kg).Name);
        }

        [Fact]
        public void Unit_ProductUnit_ReturnsCorrectDimension()
        {
            Assert.Equal(Dimension.Mass * Dimension.Length, (Unit.SI.m * Unit.SI.kg).Dimension);
        }

        [Fact]
        public void Unit_ProductUnit_ReturnsCorrectSystem()
        {
            Assert.Equal(Unit.SI, (Unit.SI.m * Unit.SI.kg).System);
        }

        [Fact]
        public void Unit_ProductUnit_IsCommutative()
        {
            var d1 = Unit.SI.m * Unit.SI.kg;
            var d2 = Unit.SI.kg * Unit.SI.m;
            Assert.True(d1 == d2);
        }

        [Fact]
        public void Unit_ProductUnit_IsCommutativeWithMultiple()
        {
            var d1 = Unit.SI.A * (Unit.SI.m * Unit.SI.kg);
            var d2 = (Unit.SI.kg * Unit.SI.A) * Unit.SI.m;
            Assert.True(d1 == d2);
        }

        [Fact]
        public void Unit_DivisionUnit_ReturnsCorrectType()
        {
            Assert.IsType<DivisionUnit>(Unit.SI.m / Unit.SI.kg);
        }

        [Fact]
        public void Unit_DivisionUnit_ReturnsCorrectSymbol()
        {
            Assert.Equal("m/s", (Unit.SI.m / Unit.SI.s).Symbol);
        }

        [Fact]
        public void Unit_DivisionUnit_ReturnsCorrectName()
        {
            Assert.Equal("meter / second", (Unit.SI.m / Unit.SI.s).Name);
        }

        [Fact]
        public void Unit_DivisionUnitRearrangeDivByDiv_ReturnsCorrect()
        {
            var actual = (Unit.SI.m / Unit.SI.kg) / (Unit.SI.A / Unit.SI.s); // (m/kg)/(A/s)
            var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.Multiply(Unit.SI.kg, Unit.SI.A)); // ms/kgA
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionUnitRearrangeSimpleByDiv_ReturnsCorrect()
        {
            var actual = Unit.SI.m / (Unit.SI.A / Unit.SI.s); // m/(A/s)
            var expected = Unit.Divide(Unit.Multiply(Unit.SI.m, Unit.SI.s), Unit.SI.A); // ms/A
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionUnitRearrangeDivBySimple_ReturnsCorrect()
        {
            var actual = (Unit.SI.m / Unit.SI.A) / Unit.SI.s; // (m/A)/s
            var expected = Unit.Divide(Unit.SI.m, Unit.Multiply(Unit.SI.s, Unit.SI.A)); // m/sA
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_PowerUnit_ReturnsCorrectType()
        {
            Assert.IsType<PowerUnit>(Unit.SI.m ^ 2);
        }

        [Fact]
        public void Unit_PowerUnit_ReturnsCorrectSymbol()
        {
            Assert.Equal("m^2", (Unit.SI.m ^ 2).Symbol);
        }

        [Fact]
        public void Unit_PowerUnit_ReturnsCorrectName()
        {
            Assert.Equal("meter ^ 2", (Unit.SI.m ^ 2).Name);
        }

        [Fact]
        public void Unit_PowerUnit0_ReturnsNone()
        {
            var actual = Unit.SI.s ^ 0; // s^0
            var expected = Unit.None; // 1 == None
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_PowerUnit1_ReturnsBase()
        {
            var actual = Unit.SI.s ^ 1; // s^1
            var expected = Unit.SI.s; // s
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_PowerUnitMinus1_ReturnsDivision()
        {
            var actual = Unit.SI.s ^ -1; // s^-1
            var expected = Unit.Divide(Unit.None, Unit.SI.s); // 1/s
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_PowerUnitNegative_ReturnsDivision()
        {
            var actual = Unit.SI.s ^ -2; // s^-2
            var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.s, 2)); // 1/s^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_MultiplePowerUnit_ExpandsEachPower()
        {
            var actual = ((Unit.SI.m * Unit.SI.s) ^ 2);
            var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_MultiplePowerUnitWithDivision_ExpandsEachPower()
        {
            var actual = ((Unit.SI.m * Unit.SI.s / Unit.SI.kg) ^ 2); // (ms/kg)^2
            var expected = Unit.Divide(Unit.Multiply(Unit.Power(Unit.SI.m, 2), Unit.Power(Unit.SI.s, 2)), Unit.Power(Unit.SI.kg, 2)); // m^2s^2/kg^2;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_MultipleIdenticalOperandsForProduct_ReduceToPower()
        {
            var actual = (Unit.SI.m * (Unit.SI.m ^ 2)); // m*m^2
            var expected = Unit.Power(Unit.SI.m, 3); // m^3
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
        {
            var actual = ((Unit.SI.m ^ 3) * (Unit.SI.m ^ 2) * Unit.SI.s * Unit.SI.m); // m^3^m^2sm
            var expected = Unit.Multiply(Unit.Power(Unit.SI.m, 6), Unit.SI.s); // m^6s
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionOperationWithSameOperands_ShouldReduceOperands()
        {
            var actual = ((Unit.SI.m ^ 2) / Unit.SI.m); // m^2/m
            var expected = Unit.SI.m; // m
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
        {
            var actual = (Unit.SI.m / Unit.SI.m); // m/m
            var expected = Unit.None; // completely reduced
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
        {
            var actual = ((Unit.SI.m ^ 3) / Unit.SI.m); // m^3/m
            var expected = Unit.Power(Unit.SI.m, 2); // m^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
        {
            var actual = (Unit.SI.m / (Unit.SI.m ^ 3)); // m/m^3
            var expected = Unit.Divide(Unit.None, Unit.Power(Unit.SI.m, 2)); //  1/m^2
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_ComplexReduction_YieldsExpected()
        {
            var actual = (Unit.SI.s * (((Unit.SI.m ^ 2) * Unit.SI.kg) / ((Unit.SI.s ^ 2) * (Unit.SI.m ^ 3)))); // s*m^2*kg/(s^2*m^3)            
            var expected = Unit.Divide(Unit.SI.kg, Unit.Multiply(Unit.SI.s, Unit.SI.m)); // kg/sm
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Unit_PrefixUnits_EqualsFails()
        {
            var u1 = Prefix.k * Unit.SI.m;
            var u2 = Prefix.k * Unit.SI.m;

            Assert.True(u1.Equals(u2));
        }

        [Fact]
        public void Unit_PrefixUnits_EqualityFails()
        {
            var u1 = Prefix.k * Unit.SI.m;
            var u2 = Prefix.k * Unit.SI.m;

            Assert.True(u1 == u2);
        }

        [Fact]
        public void Unit_ScaleUnits_EqualsFails()
        {
            var u1 = Unit.Imperial.ft;
            var u2 = Unit.Imperial.ft;

            Assert.True(u1.Equals(u2));
        }

        [Fact]
        public void Unit_ScaleUnits_EqualityFails()
        {
            var u1 = Unit.Imperial.ft;
            var u2 = Unit.Imperial.ft;

            Assert.True(u1 == u2);
        }
    }
}