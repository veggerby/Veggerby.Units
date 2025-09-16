using AwesomeAssertions;

using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests;

public class DimensionMetadataTests
{
    [Fact]
    public void Dimension_DimensionLength_TestSymbol()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var symbol = dimension.Symbol;

        // Assert
        symbol.Should().Be("L");
    }

    [Fact]
    public void Dimension_DimensionLength_TestName()
    {
        // Arrange
        var dimension = Dimension.Length;

        // Act
        var name = dimension.Name;

        // Assert
        name.Should().Be("length");
    }
}