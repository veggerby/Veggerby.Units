using AwesomeAssertions;

using Veggerby.Units.Analysis;
using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests.Analysis;

public class DimensionAnalyzerDecomposeTests
{
    [Fact]
    public void GivenBasicLengthUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Length);
        breakdown.Exponents.Should().HaveCount(1);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(1);
        breakdown.HumanReadable.Should().Be("length");
        breakdown.SymbolicForm.Should().Be("L");
    }

    [Fact]
    public void GivenBasicMassUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.kg;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Mass);
        breakdown.Exponents.Should().HaveCount(1);
        breakdown.Exponents[Dimension.Mass as BasicDimension].Should().Be(1);
        breakdown.HumanReadable.Should().Be("mass");
        breakdown.SymbolicForm.Should().Be("M");
    }

    [Fact]
    public void GivenVelocityUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Length / Dimension.Time);
        breakdown.Exponents.Should().HaveCount(2);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Time as BasicDimension].Should().Be(-1);
        breakdown.HumanReadable.Should().Be("length / time");
        breakdown.SymbolicForm.Should().Be("L·T⁻¹");
    }

    [Fact]
    public void GivenAccelerationUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.m / (Unit.SI.s ^ 2);

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Length / (Dimension.Time ^ 2));
        breakdown.Exponents.Should().HaveCount(2);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Time as BasicDimension].Should().Be(-2);
        breakdown.HumanReadable.Should().Be("length / time^2");
        breakdown.SymbolicForm.Should().Be("L·T⁻²");
    }

    [Fact]
    public void GivenForceUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Mass * Dimension.Length / (Dimension.Time ^ 2));
        breakdown.Exponents.Should().HaveCount(3);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Mass as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Time as BasicDimension].Should().Be(-2);
        breakdown.HumanReadable.Should().Be("length × mass / time^2");
        breakdown.SymbolicForm.Should().Be("L·M·T⁻²");
    }

    [Fact]
    public void GivenAreaUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Length ^ 2);
        breakdown.Exponents.Should().HaveCount(1);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(2);
        breakdown.HumanReadable.Should().Be("length^2");
        breakdown.SymbolicForm.Should().Be("L²");
    }

    [Fact]
    public void GivenVolumeUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.m ^ 3;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Length ^ 3);
        breakdown.Exponents.Should().HaveCount(1);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(3);
        breakdown.HumanReadable.Should().Be("length^3");
        breakdown.SymbolicForm.Should().Be("L³");
    }

    [Fact]
    public void GivenDimensionlessUnit_WhenDecomposingToBase_ThenReturnsEmptyBreakdown()
    {
        // Arrange
        var unit = Unit.None;

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.None);
        breakdown.Exponents.Should().BeEmpty();
        breakdown.HumanReadable.Should().Be("dimensionless");
        breakdown.SymbolicForm.Should().Be("1");
    }

    [Fact]
    public void GivenEnergyUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2);

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Mass * (Dimension.Length ^ 2) / (Dimension.Time ^ 2));
        breakdown.Exponents.Should().HaveCount(3);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(2);
        breakdown.Exponents[Dimension.Mass as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Time as BasicDimension].Should().Be(-2);
        breakdown.HumanReadable.Should().Be("length^2 × mass / time^2");
        breakdown.SymbolicForm.Should().Be("L²·M·T⁻²");
    }

    [Fact]
    public void GivenPowerUnit_WhenDecomposingToBase_ThenReturnsCorrectBreakdown()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3);

        // Act
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        // Assert
        breakdown.Dimension.Should().Be(Dimension.Mass * (Dimension.Length ^ 2) / (Dimension.Time ^ 3));
        breakdown.Exponents.Should().HaveCount(3);
        breakdown.Exponents[Dimension.Length as BasicDimension].Should().Be(2);
        breakdown.Exponents[Dimension.Mass as BasicDimension].Should().Be(1);
        breakdown.Exponents[Dimension.Time as BasicDimension].Should().Be(-3);
        breakdown.HumanReadable.Should().Be("length^2 × mass / time^3");
        breakdown.SymbolicForm.Should().Be("L²·M·T⁻³");
    }

    [Fact]
    public void GivenNullUnit_WhenDecomposingToBase_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit unit = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.DecomposeToBase(unit));
    }
}
