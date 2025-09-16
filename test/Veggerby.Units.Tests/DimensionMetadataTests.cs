using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionMetadataTests
{
    [Fact]
    public void GivenLengthDimension_WhenAccessingSymbol_ThenReturnsL()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L");
    }

    [Fact]
    public void GivenLengthDimension_WhenAccessingName_ThenReturnsLength()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length");
    }
}