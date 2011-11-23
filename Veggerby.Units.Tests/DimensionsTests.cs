using NUnit.Framework;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Tests
{
    [TestFixture]
    public class DimensionsTests
    {
        [Test]
        public void Dimension_DimensionLength_TestSymbol()
        {
            Assert.AreEqual("L", Dimension.Length.Symbol);
        }

        [Test]
        public void Dimension_DimensionLength_TestName()
        {
            Assert.AreEqual("length", Dimension.Length.Name);
        }

        [Test]
        public void Dimension_ProductDimension_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<ProductDimension>(Dimension.Length * Dimension.Mass);
        }

        [Test]
        public void Dimension_ProductDimension_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("LM", (Dimension.Length * Dimension.Mass).Symbol);
        }

        [Test]
        public void Dimension_ProductDimension_ReturnsCorrectName()
        {
            Assert.AreEqual("length * mass", (Dimension.Length * Dimension.Mass).Name);
        }

        [Test]
        public void Dimension_ProductDimension_IsCommutative()
        {
            var d1 = Dimension.Length * Dimension.Mass;
            var d2 = Dimension.Mass * Dimension.Length;
            var equal = d1 == d2;
            Assert.IsTrue(equal);
        }

        [Test]
        public void Dimension_ProductDimension_IsCommutativeWithMultiple()
        {
            var d1 = Dimension.ElectricCurrent * (Dimension.Length * Dimension.Mass);
            var d2 = (Dimension.Mass * Dimension.ElectricCurrent) * Dimension.Length;
            var equal = d1 == d2;
            Assert.IsTrue(equal);
        }

        [Test]
        public void Dimension_DivisionDimension_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<DivisionDimension>(Dimension.Length / Dimension.Mass);
        }

        [Test]
        public void Dimension_DivisionDimension_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("L/T", (Dimension.Length / Dimension.Time).Symbol);
        }

        [Test]
        public void Dimension_DivisionDimension_ReturnsCorrectName()
        {
            Assert.AreEqual("length / time", (Dimension.Length / Dimension.Time).Name);
        }

        [Test]
        public void Dimension_DivisionDimensionRearrangeDivByDiv_ReturnsCorrect()
        {
            var d1 = (Dimension.Length / Dimension.Mass) / (Dimension.ElectricCurrent / Dimension.Time);
            Assert.AreEqual("LT/MI", d1.Symbol);
        }

        [Test]
        public void Dimension_DivisionDimensionRearrangeSimpleByDiv_ReturnsCorrect()
        {
            var d1 = Dimension.Length / (Dimension.ElectricCurrent / Dimension.Time);
            Assert.AreEqual("LT/I", d1.Symbol);
        }

        [Test]
        public void Dimension_DivisionDimensionRearrangeDivBySimple_ReturnsCorrect()
        {
            var d1 = (Dimension.Length / Dimension.ElectricCurrent) / Dimension.Time;
            Assert.AreEqual("L/IT", d1.Symbol);
        }

        [Test]
        public void Dimension_PowerDimension_ReturnsCorrectType()
        {
            Assert.IsInstanceOf<PowerDimension>(Dimension.Length ^ 2);
        }

        [Test]
        public void Dimension_PowerDimension_ReturnsCorrectSymbol()
        {
            Assert.AreEqual("L^2", (Dimension.Length ^ 2).Symbol);
        }

        [Test]
        public void Dimension_PowerDimension_ReturnsCorrectName()
        {
            Assert.AreEqual("length ^ 2", (Dimension.Length ^ 2).Name);
        }

        [Test]
        public void Dimension_PowerDimension0_ReturnsNone()
        {
            Assert.AreEqual("", (Dimension.Time ^ 0).Symbol);
        }

        [Test]
        public void Dimension_PowerDimension1_ReturnsBase()
        {
            Assert.AreEqual("T", (Dimension.Time ^ 1).Symbol);
        }

        [Test]
        public void Dimension_PowerDimensionMinus1_ReturnsDivision()
        {
            Assert.AreEqual("1/T", (Dimension.Time ^ -1).Symbol);
        }

        [Test]
        public void Dimension_PowerDimensionNegative_ReturnsDivision()
        {
            Assert.AreEqual("1/T^2", (Dimension.Time ^ -2).Symbol);
        }

        [Test]
        public void Dimension_MultiplePowerDimension_ExpandsEachPower()
        {
            Assert.AreEqual("L^2T^2", ((Dimension.Length * Dimension.Time) ^ 2).Symbol);
        }

        [Test]
        public void Dimension_MultiplePowerDimensionWithDivision_ExpandsEachPower()
        {
            Assert.AreEqual("L^2T^2/M^2", ((Dimension.Length * Dimension.Time / Dimension.Mass) ^ 2).Symbol);
        }

        [Test]
        public void Dimension_MultipleIdenticalOperandsForProduct_ReduceToPower()
        {
            Assert.AreEqual("L^3", (Dimension.Length * (Dimension.Length ^ 2)).Symbol);
        }

        [Test]
        public void Dimension_MultipleIdenticalOperandsForMultipleProduct_ReduceToPower()
        {
            Assert.AreEqual("L^6T", ((Dimension.Length ^ 3) * (Dimension.Length ^ 2) * Dimension.Time * Dimension.Length).Symbol);
        }

        [Test]
        public void Dimension_DivisionOperationWithSameOperands_ShouldReduceOperands()
        {
            Assert.AreEqual("L", ((Dimension.Length ^ 2) / Dimension.Length).Symbol);
        }

        [Test]
        public void Dimension_DivisionOperationWithSameOperandsAndPower_ShouldReduceOperandsCompletely()
        {
            Assert.AreEqual("", (Dimension.Length / Dimension.Length).Symbol);
        }

        [Test]
        public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDividend_ShouldReduceOperandsPartially()
        {
            Assert.AreEqual("L^2", ((Dimension.Length ^ 3) / Dimension.Length).Symbol);
        }

        [Test]
        public void Dimension_DivisionOperationWithSameOperandsButDifferentPowerDivisor_ShouldReduceOperandsPartially()
        {
            Assert.AreEqual("1/L^2", (Dimension.Length / (Dimension.Length ^ 3)).Symbol);
        }


        [Test]
        public void Dimension_ComplexReduction_YieldsExpected()
        {
            // T*L^2*M/(T^2*L^3) => M/TL
            Assert.AreEqual("M/TL", (Dimension.Time * (((Dimension.Length ^ 2) * Dimension.Mass) / ((Dimension.Time ^ 2) * (Dimension.Length ^ 3)))).Symbol);
        }
    }
}