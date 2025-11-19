using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests.Parsing;

public class UnitParserPrefixTests
{
    [Fact]
    public void GivenKilometer_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "km";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.k * Unit.SI.m);
    }

    [Fact]
    public void GivenMillisecond_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "ms";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.m * Unit.SI.s);
    }

    [Fact]
    public void GivenMicrogram_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "μg";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.μ * Unit.SI.g);
    }

    [Fact]
    public void GivenMegaAmpere_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "MA";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.M * Unit.SI.A);
    }

    [Fact]
    public void GivenNanometer_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "nm";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.n * Unit.SI.m);
    }

    [Fact]
    public void GivenGigagram_WhenParsing_ThenReturnsCorrectUnit()
    {
        // Arrange
        var expression = "Gg";

        // Act
        var result = UnitParser.Parse(expression);

        // Assert
        result.Should().Be(Prefix.G * Unit.SI.g);
    }
}
