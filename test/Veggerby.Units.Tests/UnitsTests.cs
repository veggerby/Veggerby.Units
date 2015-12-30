using NUnit.Framework;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Tests
{
    [TestFixture]
    public class UnitsTests
    {
        [Test]
        public void Unit_UnitEquality_TestSimple()
        {
            Assert.IsTrue(Unit.SI.m == Unit.SI.m);
        }

        [Test]
        public void Unit_UnitInEquality_TestSimple()
        {
            Assert.IsTrue(Unit.SI.m != Unit.SI.kg);
        }

        [Test]
        public void Unit_UnitEquality_TestProduct()
        {
            Assert.IsTrue((Unit.SI.m * Unit.SI.kg) == (Unit.SI.m * Unit.SI.kg));
        }

        [Test]
        public void Unit_UnitInEquality_TestProduct()
        {
            Assert.IsTrue((Unit.SI.m * Unit.SI.kg) != (Unit.SI.s * Unit.SI.kg));
        }

        [Test]
        public void Unit_UnitEquality_TestDivision()
        {
            Assert.IsTrue((Unit.SI.m / Unit.SI.kg) == (Unit.SI.m / Unit.SI.kg));
        }

        [Test]
        public void Unit_UnitInEquality_TestDivision()
        {
            Assert.IsTrue((Unit.SI.m / Unit.SI.kg) != (Unit.SI.s / Unit.SI.kg));
        }

        [Test]
        public void Unit_UnitEquality_TestPower()
        {
            Assert.IsTrue((Unit.SI.m ^ 2) == (Unit.SI.m ^ 2));
        }

        [Test]
        public void Unit_UnitInEquality_TestPowerBaseDifferent()
        {
            Assert.IsTrue((Unit.SI.m ^ 2) != (Unit.SI.s ^ 2));
        }

        [Test]
        public void Unit_UnitInEquality_TestPowerExponentDifferent()
        {
            Assert.IsTrue((Unit.SI.m ^ 2) != (Unit.SI.m ^ 3));
        }

        [Test]
        public void Unit_UnitMeter_TestSymbol()
        {
            Assert.AreEqual("m", Unit.SI.m.Symbol);
        }

        [Test]
        public void Unit_UnitMeter_TestName()
        {
            Assert.AreEqual("meter", Unit.SI.m.Name);
        }

        [Test]
        public void Unit_UnitMeter_TestSystem()
        {
            Assert.AreEqual(Unit.SI, Unit.SI.m.System);
        }

        [Test]
        public void Unit_UnitMeter_TestDimension()
        {
            Assert.AreEqual(Dimension.Length, Unit.SI.m.Dimension);
        }

        [Test]
        public void Unit_ProductUnit_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<ProductUnit>(Unit.SI.m * Unit.SI.kg);
        }

        [Test]
        public void Unit_ProductUnit_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("mkg", (Unit.SI.m * Unit.SI.kg).Symbol);
        }

        [Test]
        public void Unit_ProductUnit_ReturnsCorrectName()
        {
            Assert.AreEqual("meter * kilogram", (Unit.SI.m * Unit.SI.kg).Name);
        }

        [Test]
        public void Unit_ProductUnit_ReturnsCorrectDimension()
        {
            Assert.AreEqual(Dimension.Mass * Dimension.Length, (Unit.SI.m * Unit.SI.kg).Dimension);
        }

        [Test]
        public void Unit_ProductUnit_ReturnsCorrectSystem()
        {
            Assert.AreEqual(Unit.SI, (Unit.SI.m * Unit.SI.kg).System);
        }

        [Test]
        public void Unit_ProductUnit_IsCommutative()
        {
            var d1 = Unit.SI.m * Unit.SI.kg;
            var d2 = Unit.SI.kg * Unit.SI.m;
            Assert.IsTrue(d1 == d2);
        }

        [Test]
        public void Unit_ProductUnit_IsCommutativeWithMultiple()
        {
            var d1 = Unit.SI.A * (Unit.SI.m * Unit.SI.kg);
            var d2 = (Unit.SI.kg * Unit.SI.A) * Unit.SI.m;
            Assert.IsTrue(d1 == d2);
        }

        [Test]
        public void Unit_DivisionUnit_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<DivisionUnit>(Unit.SI.m / Unit.SI.kg);
        }

        [Test]
        public void Unit_DivisionUnit_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("m/s", (Unit.SI.m / Unit.SI.s).Symbol);
        }

        [Test]
        public void Unit_DivisionUnit_ReturnsCorrectName()
        {
            Assert.AreEqual("meter / second", (Unit.SI.m / Unit.SI.s).Name);
        }

        [Test]
        public void Unit_DivisionUnitRearrangeDivByDiv_ReturnsCorrect()
        {
            var actual = (Unit.SI.m / Unit.SI.kg) / (Unit.SI.A / Unit.SI.s); // (m/kg)/(A/s)
            var expected = Unit.Div(Unit.Mult(Unit.SI.m, Unit.SI.s), Unit.Mult(Unit.SI.kg, Unit.SI.A)); // ms/kgA
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionUnitRearrangeSimpleByDiv_ReturnsCorrect()
        {
            var actual = Unit.SI.m / (Unit.SI.A / Unit.SI.s); // m/(A/s)
            var expected = Unit.Div(Unit.Mult(Unit.SI.m, Unit.SI.s), Unit.SI.A); // ms/A
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionUnitRearrangeDivBySimple_ReturnsCorrect()
        {
            var actual = (Unit.SI.m / Unit.SI.A) / Unit.SI.s; // (m/A)/s
            var expected = Unit.Div(Unit.SI.m, Unit.Mult(Unit.SI.s, Unit.SI.A)); // m/sA
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_PowerUnit_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<PowerUnit>(Unit.SI.m ^ 2);
        }

        [Test]
        public void Unit_PowerUnit_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("m^2", (Unit.SI.m ^ 2).Symbol);
        }

        [Test]
        public void Unit_PowerUnit_ReturnsCorrectName()
        {
            Assert.AreEqual("meter ^ 2", (Unit.SI.m ^ 2).Name);
        }

        [Test]
        public void Unit_PowerUnit0_ReturnsNone()
        {
            var actual = Unit.SI.s ^ 0; // s^0
            var expected = Unit.None; // 1 == None
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_PowerUnit1_ReturnsBase()
        {
            var actual = Unit.SI.s ^ 1; // s^1
            var expected = Unit.SI.s; // s
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_PowerUnitMinus1_ReturnsDivision()
        {
            var actual = Unit.SI.s ^ -1; // s^-1
            var expected = Unit.Div(Unit.None, Unit.SI.s); // 1/s
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_PowerUnitNegative_ReturnsDivision()
        {
            var actual = Unit.SI.s ^ -2; // s^-2
            var expected = Unit.Div(Unit.None, Unit.Pow(Unit.SI.s, 2)); // 1/s^2
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_MultiplePowerUnit_ExpandsEachPower()
        {
            var actual = ((Unit.SI.m * Unit.SI.s) ^ 2);
            var expected = Unit.Mult(Unit.Pow(Unit.SI.m, 2), Unit.Pow(Unit.SI.s, 2));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_MultiplePowerUnitWithDivision_ExpandsEachPower()
        {
            var actual = ((Unit.SI.m * Unit.SI.s / Unit.SI.kg) ^ 2); // (ms/kg)^2
            var expected = Unit.Div(Unit.Mult(Unit.Pow(Unit.SI.m, 2), Unit.Pow(Unit.SI.s, 2)), Unit.Pow(Unit.SI.kg, 2)); // m^2s^2/kg^2;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_MultipleIdenticalOperandsForProduct_ReduceToPower()
        {
            var actual = (Unit.SI.m * (Unit.SI.m ^ 2)); // m*m^2
            var expected = Unit.Pow(Unit.SI.m, 3); // m^3
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
        {
            var actual = ((Unit.SI.m ^ 3) * (Unit.SI.m ^ 2) * Unit.SI.s * Unit.SI.m); // m^3^m^2sm
            var expected = Unit.Mult(Unit.Pow(Unit.SI.m, 6), Unit.SI.s); // m^6s
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionOperationWithSameOperands_ShouldReduceOperands()
        {
            var actual = ((Unit.SI.m ^ 2) / Unit.SI.m); // m^2/m
            var expected = Unit.SI.m; // m
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
        {
            var actual = (Unit.SI.m / Unit.SI.m); // m/m
            var expected = Unit.None; // completely reduced
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
        {
            var actual = ((Unit.SI.m ^ 3) / Unit.SI.m); // m^3/m
            var expected = Unit.Pow(Unit.SI.m, 2); // m^2
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
        {
            var actual = (Unit.SI.m / (Unit.SI.m ^ 3)); // m/m^3
            var expected = Unit.Div(Unit.None, Unit.Pow(Unit.SI.m, 2)); //  1/m^2
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_ComplexReduction_YieldsExpected()
        {
            var actual = (Unit.SI.s * (((Unit.SI.m ^ 2) * Unit.SI.kg) / ((Unit.SI.s ^ 2) * (Unit.SI.m ^ 3)))); // s*m^2*kg/(s^2*m^3)            
            var expected = Unit.Div(Unit.SI.kg, Unit.Mult(Unit.SI.s, Unit.SI.m)); // kg/sm
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Unit_PrefixUnits_EqualsFails()
        {
            var u1 = Prefix.k * Unit.SI.m;
            var u2 = Prefix.k * Unit.SI.m;

            Assert.IsTrue(u1.Equals(u2));
        }

        [Test]
        public void Unit_PrefixUnits_EqualityFails()
        {
            var u1 = Prefix.k * Unit.SI.m;
            var u2 = Prefix.k * Unit.SI.m;

            Assert.IsTrue(u1 == u2);
        }

        [Test]
        public void Unit_ScaleUnits_EqualsFails()
        {
            var u1 = Unit.Imperial.ft;
            var u2 = Unit.Imperial.ft;

            Assert.IsTrue(u1.Equals(u2));
        }

        [Test]
        public void Unit_ScaleUnits_EqualityFails()
        {
            var u1 = Unit.Imperial.ft;
            var u2 = Unit.Imperial.ft;

            Assert.IsTrue(u1 == u2);
        }
    }
}