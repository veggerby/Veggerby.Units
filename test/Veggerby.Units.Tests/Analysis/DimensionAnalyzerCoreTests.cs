using AwesomeAssertions;

using Veggerby.Units.Analysis;
using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests.Analysis;

public class DimensionAnalyzerEquivalenceTests
{
    [Fact]
    public void GivenIdenticalUnits_WhenCheckingEquivalence_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.m;

        // Act
        var areEquivalent = DimensionAnalyzer.AreDimensionallyEquivalent(left, right);

        // Assert
        areEquivalent.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentUnitsWithSameDimension_WhenCheckingEquivalence_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.Imperial.ft;

        // Act
        var areEquivalent = DimensionAnalyzer.AreDimensionallyEquivalent(left, right);

        // Assert
        areEquivalent.Should().BeTrue();
    }

    [Fact]
    public void GivenDifferentDimensions_WhenCheckingEquivalence_ThenReturnsFalse()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.kg;

        // Act
        var areEquivalent = DimensionAnalyzer.AreDimensionallyEquivalent(left, right);

        // Assert
        areEquivalent.Should().BeFalse();
    }

    [Fact]
    public void GivenCompositeUnitsWithSameDimension_WhenCheckingEquivalence_ThenReturnsTrue()
    {
        // Arrange
        var left = Unit.SI.m / Unit.SI.s;
        var right = Unit.SI.m / Unit.SI.s;

        // Act
        var areEquivalent = DimensionAnalyzer.AreDimensionallyEquivalent(left, right);

        // Assert
        areEquivalent.Should().BeTrue();
    }

    [Fact]
    public void GivenNullLeftUnit_WhenCheckingEquivalence_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit left = null;
        var right = Unit.SI.m;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.AreDimensionallyEquivalent(left, right));
    }

    [Fact]
    public void GivenNullRightUnit_WhenCheckingEquivalence_ThenThrowsArgumentNullException()
    {
        // Arrange
        var left = Unit.SI.m;
        Unit right = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.AreDimensionallyEquivalent(left, right));
    }
}

public class DimensionAnalyzerValidateTests
{
    [Fact]
    public void GivenUnitMatchingExpectedDimension_WhenValidating_ThenReturnsValidResult()
    {
        // Arrange
        var unit = Unit.SI.m;
        var expectedDimension = Dimension.Length;

        // Act
        var result = DimensionAnalyzer.Validate(unit, expectedDimension);

        // Assert
        result.IsValid.Should().BeTrue();
        result.ActualDimension.Should().Be(Dimension.Length);
        result.ExpectedDimension.Should().Be(expectedDimension);
        result.Message.Should().Contain("Valid");
    }

    [Fact]
    public void GivenUnitNotMatchingExpectedDimension_WhenValidating_ThenReturnsInvalidResult()
    {
        // Arrange
        var unit = Unit.SI.m;
        var expectedDimension = Dimension.Mass;

        // Act
        var result = DimensionAnalyzer.Validate(unit, expectedDimension);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ActualDimension.Should().Be(Dimension.Length);
        result.ExpectedDimension.Should().Be(expectedDimension);
        result.Message.Should().Contain("Invalid");
    }

    [Fact]
    public void GivenForceUnitWithCorrectDimension_WhenValidating_ThenReturnsValidResult()
    {
        // Arrange
        var unit = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);
        var expectedDimension = Dimension.Mass * Dimension.Length / (Dimension.Time ^ 2);

        // Act
        var result = DimensionAnalyzer.Validate(unit, expectedDimension);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GivenNullUnit_WhenValidating_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit unit = null;
        var expectedDimension = Dimension.Length;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.Validate(unit, expectedDimension));
    }

    [Fact]
    public void GivenNullExpectedDimension_WhenValidating_ThenThrowsArgumentNullException()
    {
        // Arrange
        var unit = Unit.SI.m;
        Dimension expectedDimension = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.Validate(unit, expectedDimension));
    }
}

public class DimensionAnalyzerHomogeneityTests
{
    [Fact]
    public void GivenHomogeneousUnits_WhenAnalyzingHomogeneity_ThenReturnsHomogeneousReport()
    {
        // Arrange
        var units = new[] { Unit.SI.m, Unit.Imperial.ft, Unit.SI.m ^ 1 };

        // Act
        var report = DimensionAnalyzer.AnalyzeHomogeneity(units);

        // Assert
        report.IsHomogeneous.Should().BeTrue();
        report.AnalyzedUnits.Should().HaveCount(3);
        report.Summary.Should().Contain("share dimension");
    }

    [Fact]
    public void GivenNonHomogeneousUnits_WhenAnalyzingHomogeneity_ThenReturnsNonHomogeneousReport()
    {
        // Arrange
        var units = new[] { Unit.SI.m, Unit.SI.kg, Unit.SI.s };

        // Act
        var report = DimensionAnalyzer.AnalyzeHomogeneity(units);

        // Assert
        report.IsHomogeneous.Should().BeFalse();
        report.AnalyzedUnits.Should().HaveCount(3);
        report.Summary.Should().Contain("mixed dimensions");
    }

    [Fact]
    public void GivenEmptyArray_WhenAnalyzingHomogeneity_ThenReturnsEmptyReport()
    {
        // Arrange
        var units = new Unit[0];

        // Act
        var report = DimensionAnalyzer.AnalyzeHomogeneity(units);

        // Assert
        report.IsHomogeneous.Should().BeTrue();
        report.AnalyzedUnits.Should().BeEmpty();
        report.Summary.Should().Contain("No units");
    }

    [Fact]
    public void GivenSingleUnit_WhenAnalyzingHomogeneity_ThenReturnsHomogeneousReport()
    {
        // Arrange
        var units = new[] { Unit.SI.m };

        // Act
        var report = DimensionAnalyzer.AnalyzeHomogeneity(units);

        // Assert
        report.IsHomogeneous.Should().BeTrue();
        report.AnalyzedUnits.Should().HaveCount(1);
    }

    [Fact]
    public void GivenNullArray_WhenAnalyzingHomogeneity_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit[] units = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.AnalyzeHomogeneity(units));
    }
}
