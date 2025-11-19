using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class UnitParserErrorTests
{
    [Fact]
    public void GivenNullExpression_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        string expression = null;

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null or empty*");
    }

    [Fact]
    public void GivenEmptyExpression_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "";

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null or empty*");
    }

    [Fact]
    public void GivenWhitespaceExpression_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "   ";

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null or empty*");
    }

    [Fact]
    public void GivenUnknownSymbol_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "xyz";

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*Unknown unit symbol*");
    }

    [Fact]
    public void GivenUnbalancedParentheses_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "(m*s";

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>();
    }

    [Fact]
    public void GivenInvalidCharacter_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "m@s";

        // Act & Assert
        Action act = () => UnitParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*Unexpected character*");
    }

    [Fact]
    public void GivenTryParseWithValidExpression_WhenParsing_ThenReturnsTrue()
    {
        // Arrange
        var expression = "m";

        // Act
        var success = UnitParser.TryParse(expression, out var unit);

        // Assert
        success.Should().BeTrue();
        unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenTryParseWithInvalidExpression_WhenParsing_ThenReturnsFalse()
    {
        // Arrange
        var expression = "xyz";

        // Act
        var success = UnitParser.TryParse(expression, out var unit);

        // Assert
        success.Should().BeFalse();
        unit.Should().BeNull();
    }

    [Fact]
    public void GivenTryParseWithErrorMessage_WhenParsing_ThenProvidesErrorMessage()
    {
        // Arrange
        var expression = "xyz";

        // Act
        var success = UnitParser.TryParse(expression, out var unit, out var errorMessage);

        // Assert
        success.Should().BeFalse();
        unit.Should().BeNull();
        errorMessage.Should().Contain("Unknown unit symbol");
    }
}
