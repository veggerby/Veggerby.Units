using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Analysis;
using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests.Analysis;

public class DimensionAnalyzerExplainMismatchTests
{
    [Fact]
    public void GivenLengthAndMass_WhenExplainingMismatch_ThenReturnsCorrectExplanation()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.kg;

        // Act
        var explanation = DimensionAnalyzer.ExplainMismatch(left, right);

        // Assert
        explanation.Left.Should().Be(left);
        explanation.Right.Should().Be(right);
        explanation.LeftDimension.Should().Be(Dimension.Length);
        explanation.RightDimension.Should().Be(Dimension.Mass);
        explanation.Explanation.Should().Contain("incompatible dimensions");
        explanation.Explanation.Should().Contain("m");
        explanation.Explanation.Should().Contain("kg");
    }

    [Fact]
    public void GivenLengthAndVelocity_WhenExplainingMismatch_ThenIncludesSuggestions()
    {
        // Arrange
        var left = Unit.SI.m;
        var right = Unit.SI.m / Unit.SI.s;

        // Act
        var explanation = DimensionAnalyzer.ExplainMismatch(left, right);

        // Assert
        explanation.Suggestions.Should().NotBeEmpty();
        explanation.Suggestions.Should().Contain(s => s.Contains("time"));
    }

    [Fact]
    public void GivenVelocityAndAcceleration_WhenExplainingMismatch_ThenProvidesMeaningfulExplanation()
    {
        // Arrange
        var velocity = Unit.SI.m / Unit.SI.s;
        var acceleration = Unit.SI.m / (Unit.SI.s ^ 2);

        // Act
        var explanation = DimensionAnalyzer.ExplainMismatch(velocity, acceleration);

        // Assert
        explanation.Explanation.Should().Contain("L·T⁻¹");
        explanation.Explanation.Should().Contain("L·T⁻²");
        explanation.Suggestions.Should().NotBeEmpty();
    }

    [Fact]
    public void GivenNullLeftUnit_WhenExplainingMismatch_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit left = null;
        var right = Unit.SI.m;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.ExplainMismatch(left, right));
    }

    [Fact]
    public void GivenNullRightUnit_WhenExplainingMismatch_ThenThrowsArgumentNullException()
    {
        // Arrange
        var left = Unit.SI.m;
        Unit right = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.ExplainMismatch(left, right));
    }
}

public class DimensionAnalyzerSuggestCorrectionsTests
{
    [Fact]
    public void GivenLengthActualAndVelocityExpected_WhenSuggestingCorrections_ThenSuggestsDivideByTime()
    {
        // Arrange
        var actual = Unit.SI.m;
        var expected = Dimension.Length / Dimension.Time;

        // Act
        var suggestions = DimensionAnalyzer.SuggestCorrections(actual, expected).ToList();

        // Assert
        suggestions.Should().NotBeEmpty();
        suggestions.Should().Contain(s => s.Transformation.Contains("divide by time"));
    }

    [Fact]
    public void GivenVelocityActualAndLengthExpected_WhenSuggestingCorrections_ThenSuggestsMultiplyByTime()
    {
        // Arrange
        var actual = Unit.SI.m / Unit.SI.s;
        var expected = Dimension.Length;

        // Act
        var suggestions = DimensionAnalyzer.SuggestCorrections(actual, expected).ToList();

        // Assert
        suggestions.Should().NotBeEmpty();
        suggestions.Should().Contain(s => s.Transformation.Contains("multiply by time"));
    }

    [Fact]
    public void GivenMatchingDimensions_WhenSuggestingCorrections_ThenReturnsEmptyList()
    {
        // Arrange
        var actual = Unit.SI.m;
        var expected = Dimension.Length;

        // Act
        var suggestions = DimensionAnalyzer.SuggestCorrections(actual, expected).ToList();

        // Assert
        suggestions.Should().BeEmpty();
    }

    [Fact]
    public void GivenNullActualUnit_WhenSuggestingCorrections_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit actual = null;
        var expected = Dimension.Length;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.SuggestCorrections(actual, expected));
    }

    [Fact]
    public void GivenNullExpectedDimension_WhenSuggestingCorrections_ThenThrowsArgumentNullException()
    {
        // Arrange
        var actual = Unit.SI.m;
        Dimension expected = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.SuggestCorrections(actual, expected));
    }
}

public class DimensionAnalyzerFindUnitsTests
{
    [Fact]
    public void GivenAnyDimension_WhenFindingUnits_ThenReturnsEmptyCollection()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var units = DimensionAnalyzer.FindUnitsWithDimension(dimension).ToList();

        // Assert
        // Note: This currently returns empty as unit registry is not exposed
        units.Should().BeEmpty();
    }

    [Fact]
    public void GivenNullDimension_WhenFindingUnits_ThenThrowsArgumentNullException()
    {
        // Arrange
        Dimension dimension = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionAnalyzer.FindUnitsWithDimension(dimension));
    }
}
