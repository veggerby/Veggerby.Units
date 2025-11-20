using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class MeasurementParserTests
{
    [Fact]
    public void GivenSimpleMeasurement_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "5.0 m";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(5.0);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenMeasurementWithCompoundUnit_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "10 kg/s";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(10.0);
        result.Unit.Should().Be(Unit.SI.kg / Unit.SI.s);
    }

    [Fact]
    public void GivenMeasurementWithScientificNotation_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "1.5e3 Pa";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(1500.0);
        result.Unit.Should().Be(Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)));
    }

    [Fact]
    public void GivenMeasurementWithNegativeValue_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "-5.5 m";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(-5.5);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenMeasurementWithPrefixedUnit_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "300 km";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(300.0);
        result.Unit.Should().Be(Prefix.k * Unit.SI.m);
    }

    [Fact]
    public void GivenIntegerMeasurement_WhenParsingAsInt_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "42 m";

        // Act
        var result = MeasurementParser.Parse<int>(expression);

        // Assert
        result.Value.Should().Be(42);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenDecimalMeasurement_WhenParsingAsDecimal_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "3.14159 rad";

        // Act
        var result = MeasurementParser.Parse<decimal>(expression);

        // Assert
        result.Value.Should().Be(3.14159m);
        result.Unit.Should().Be(Unit.SI.rad);
    }

    [Fact]
    public void GivenTryParseWithValidExpression_WhenParsing_ThenReturnsTrue()
    {
        // Arrange
        var expression = "100 W";

        // Act
        var success = MeasurementParser.TryParse(expression, out var measurement);

        // Assert
        success.Should().BeTrue();
        measurement.Value.Should().Be(100.0);
        measurement.Unit.Should().Be(Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3));
    }

    [Fact]
    public void GivenTryParseWithInvalidExpression_WhenParsing_ThenReturnsFalse()
    {
        // Arrange
        var expression = "invalid";

        // Act
        var success = MeasurementParser.TryParse(expression, out var measurement);

        // Assert
        success.Should().BeFalse();
        measurement.Should().BeNull();
    }

    [Fact]
    public void GivenMeasurementWithExtraWhitespace_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "  25.5   m  ";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(25.5);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenMeasurementWithComplexUnit_WhenParsing_ThenReturnsCorrectMeasurement()
    {
        // Arrange
        var expression = "9.81 m/s²";

        // Act
        var result = MeasurementParser.Parse(expression);

        // Assert
        result.Value.Should().Be(9.81);
        result.Unit.Should().Be(Unit.SI.m / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenNullExpression_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        string expression = null;

        // Act & Assert
        Action act = () => MeasurementParser.Parse(expression);
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null or empty*");
    }

    [Fact]
    public void GivenMeasurementWithoutUnit_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "42";

        // Act & Assert
        Action act = () => MeasurementParser.Parse(expression);
        act.Should().Throw<ParseException>();
    }
}
