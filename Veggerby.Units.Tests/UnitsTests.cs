using NUnit.Framework;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Tests
{
    [TestFixture]
    public class UnitsTests
    {
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

        //[Test]
        //public void Unit_ProductUnit_ReturnsCorrectType()
        //{
        //    Assert.IsInstanceOf<ProductUnit>(Unit.Length * Unit.Mass);
        //}

        //[Test]
        //public void Unit_ProductUnit_ReturnsCorrectSymbol()
        //{
        //    Assert.AreEqual("LM", (Unit.Length * Unit.Mass).Symbol);
        //}

        //[Test]
        //public void Unit_ProductUnit_ReturnsCorrectName()
        //{
        //    Assert.AreEqual("length * mass", (Unit.Length * Unit.Mass).Name);
        //}

        //[Test]
        //public void Unit_ProductUnit_IsCommutative()
        //{
        //    var d1 = Unit.Length * Unit.Mass;
        //    var d2 = Unit.Mass * Unit.Length;
        //    Assert.IsTrue(d1 == d2);
        //}

        //[Test]
        //public void Unit_ProductUnit_IsCommutativeWithMultiple()
        //{
        //    var d1 = Unit.ElectricCurrent * (Unit.Length * Unit.Mass);
        //    var d2 = (Unit.Mass * Unit.ElectricCurrent) * Unit.Length;
        //    Assert.IsTrue(d1 == d2);
        //}

        //[Test]
        //public void Unit_DivisionUnit_ReturnsCorrectType()
        //{
        //    Assert.IsInstanceOf<DivisionUnit>(Unit.Length / Unit.Mass);
        //}

        //[Test]
        //public void Unit_DivisionUnit_ReturnsCorrectSymbol()
        //{
        //    Assert.AreEqual("L/T", (Unit.Length / Unit.Time).Symbol);
        //}

        //[Test]
        //public void Unit_DivisionUnit_ReturnsCorrectName()
        //{
        //    Assert.AreEqual("length / time", (Unit.Length / Unit.Time).Name);
        //}

        //[Test]
        //public void Unit_DivisionUnitRearrangeDivByDiv_ReturnsCorrect()
        //{
        //    var actual = (Unit.Length / Unit.Mass) / (Unit.ElectricCurrent / Unit.Time); // (L/M)/(I/T)
        //    var expected = Unit.Div(Unit.Mult(Unit.Length, Unit.Time), Unit.Mult(Unit.Mass, Unit.ElectricCurrent)); // LT/MI
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionUnitRearrangeSimpleByDiv_ReturnsCorrect()
        //{
        //    var actual = Unit.Length / (Unit.ElectricCurrent / Unit.Time); // L/(I/T)
        //    var expected = Unit.Div(Unit.Mult(Unit.Length, Unit.Time), Unit.ElectricCurrent); // LT/I
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionUnitRearrangeDivBySimple_ReturnsCorrect()
        //{
        //    var actual = (Unit.Length / Unit.ElectricCurrent) / Unit.Time; // (L/I)/T
        //    var expected = Unit.Div(Unit.Length, Unit.Mult(Unit.Time, Unit.ElectricCurrent)); // L/TI
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_PowerUnit_ReturnsCorrectType()
        //{
        //    Assert.IsInstanceOf<PowerUnit>(Unit.Length ^ 2);
        //}

        //[Test]
        //public void Unit_PowerUnit_ReturnsCorrectSymbol()
        //{
        //    Assert.AreEqual("L^2", (Unit.Length ^ 2).Symbol);
        //}

        //[Test]
        //public void Unit_PowerUnit_ReturnsCorrectName()
        //{
        //    Assert.AreEqual("length ^ 2", (Unit.Length ^ 2).Name);
        //}

        //[Test]
        //public void Unit_PowerUnit0_ReturnsNone()
        //{
        //    var actual = Unit.Time ^ 0; // T^0
        //    var expected = Unit.None; // 1 == None
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_PowerUnit1_ReturnsBase()
        //{
        //    var actual = Unit.Time ^ 1; // T^1
        //    var expected = Unit.Time; // T
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_PowerUnitMinus1_ReturnsDivision()
        //{
        //    var actual = Unit.Time ^ -1; // T^-1
        //    var expected = Unit.Div(Unit.None, Unit.Time); // 1/T
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_PowerUnitNegative_ReturnsDivision()
        //{
        //    var actual = Unit.Time ^ -2; // T^-2
        //    var expected = Unit.Div(Unit.None, Unit.Pow(Unit.Time, 2)); // 1/T^2
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_MultiplePowerUnit_ExpandsEachPower()
        //{
        //    var actual = ((Unit.Length * Unit.Time) ^ 2);
        //    var expected = Unit.Mult(Unit.Pow(Unit.Length, 2), Unit.Pow(Unit.Time, 2));
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_MultiplePowerUnitWithDivision_ExpandsEachPower()
        //{
        //    var actual = ((Unit.Length * Unit.Time / Unit.Mass) ^ 2); // (LT/M)^2
        //    var expected = Unit.Div(Unit.Mult(Unit.Pow(Unit.Length, 2), Unit.Pow(Unit.Time, 2)), Unit.Pow(Unit.Mass, 2)); // L^2T^2/M^2;
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_MultipleIdenticalOperandsForProduct_ReduceToPower()
        //{
        //    var actual = (Unit.Length * (Unit.Length ^ 2)); // L*L^2
        //    var expected = Unit.Pow(Unit.Length, 3); // L^3
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
        //{
        //    var actual = ((Unit.Length ^ 3) * (Unit.Length ^ 2) * Unit.Time * Unit.Length); // L^3^L^2TL            
        //    var expected = Unit.Mult(Unit.Pow(Unit.Length, 6), Unit.Time); // L^6T
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionOperationWithSameOperands_ShouldReduceOperands()
        //{
        //    var actual = ((Unit.Length ^ 2) / Unit.Length); // L^2/L            
        //    var expected = Unit.Length; // L
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
        //{
        //    var actual = (Unit.Length / Unit.Length); // L/L
        //    var expected = Unit.None; // completely reduced
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
        //{
        //    var actual = ((Unit.Length ^ 3) / Unit.Length); // L^3/L
        //    var expected = Unit.Pow(Unit.Length, 2); // L^2
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
        //{
        //    var actual = (Unit.Length / (Unit.Length ^ 3)); // L/L^3
        //    var expected = Unit.Div(Unit.None, Unit.Pow(Unit.Length, 2)); //  1/L^2
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Unit_ComplexReduction_YieldsExpected()
        //{
        //    var actual = (Unit.Time * (((Unit.Length ^ 2) * Unit.Mass) / ((Unit.Time ^ 2) * (Unit.Length ^ 3)))); // T*L^2*M/(T^2*L^3)            
        //    var expected = Unit.Div(Unit.Mass, Unit.Mult(Unit.Time, Unit.Length)); // M/TL
        //    Assert.AreEqual(expected, actual);
        //}
    }
}