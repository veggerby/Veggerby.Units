using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class UnitParserBasicTests
{
    [Fact]
    public void GivenSimpleSIUnit_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "m";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenKilogram_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "kg";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.kg);
    }

    [Fact]
    public void GivenSecond_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "s";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.s);
    }

    [Fact]
    public void GivenAmpere_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "A";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.A);
    }

    [Fact]
    public void GivenKelvin_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "K";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.K);
    }

    [Fact]
    public void GivenMole_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "mol";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.n);
    }

    [Fact]
    public void GivenCandela_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "cd";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.cd);
    }

    [Fact]
    public void GivenRadian_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "rad";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.rad);
    }

    [Fact]
    public void GivenSteradian_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "sr";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.SI.sr);
    }

    [Fact]
    public void GivenImperialFoot_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "ft";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.Imperial.ft);
    }

    [Fact]
    public void GivenImperialInch_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "in";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.Imperial.@in);
    }

    [Fact]
    public void GivenImperialPound_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "lb";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Unit.Imperial.lb);
    }
}
