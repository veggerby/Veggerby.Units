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
    }
}