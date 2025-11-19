using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class UnitParserCompositeTests
{
    [Fact]
    public void GivenProductWithAsterisk_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m*s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m * Unit.SI.s);
    }

    [Fact]
    public void GivenProductWithDot_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m·s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m * Unit.SI.s);
    }

    [Fact]
    public void GivenProductWithSpace_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m * Unit.SI.s);
    }

    [Fact]
    public void GivenDivision_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m/s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m / Unit.SI.s);
    }

    [Fact]
    public void GivenVelocity_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "km/s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be((Prefix.k * Unit.SI.m) / Unit.SI.s);
    }

    [Fact]
    public void GivenAcceleration_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m/s^2";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenPowerWithCaret_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m^2";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m ^ 2);
    }

    [Fact]
    public void GivenPowerWithSuperscript_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m²";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m ^ 2);
    }

    [Fact]
    public void GivenCubicMeter_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m³";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m ^ 3);
    }

    [Fact]
    public void GivenComplexExpression_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "kg·m/s²";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenParentheses_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "(kg·m)/s²";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be((Unit.SI.kg * Unit.SI.m) / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenNegativeExponent_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "s^-1";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.s ^ -1);
    }

    [Fact]
    public void GivenNegativeSuperscript_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "s⁻¹";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.s ^ -1);
    }
}
