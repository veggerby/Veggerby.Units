using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class UnitParserDerivedTests
{
    [Fact]
    public void GivenNewton_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "N";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenJoule_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "J";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
    }

    [Fact]
    public void GivenPascal_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "Pa";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)));
    }

    [Fact]
    public void GivenWatt_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "W";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3));
    }

    [Fact]
    public void GivenVolt_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "V";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 3) * Unit.SI.A));
    }

    [Fact]
    public void GivenHertz_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "Hz";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.None / Unit.SI.s);
    }
}
