using AwesomeAssertions;
using Veggerby.Units.Dimensions;
using Xunit;

namespace Veggerby.Units.Tests.Dimensions;

public class HashCodeConsistencyTests
{
    [Fact]
    public void Given_IdenticalProductDimensions_When_GetHashCode_Then_EqualsImpliesSameHash()
    {
        // Arrange
        var d1 = Dimension.Length * Dimension.Time;
        var d2 = Dimension.Time * Dimension.Length;

        // Act
        var eq = d1 == d2;
        var hashEq = d1.GetHashCode() == d2.GetHashCode();

        // Assert
        eq.Should().BeTrue();
        hashEq.Should().BeTrue();
    }

    [Fact]
    public void Given_IdenticalPowerDimensions_When_GetHashCode_Then_EqualsImpliesSameHash()
    {
        // Arrange
        var d1 = Dimension.Length ^ 4;
        var d2 = Dimension.Power(Dimension.Length, 4);

        // Act
        var eq = d1 == d2;
        var hashEq = d1.GetHashCode() == d2.GetHashCode();

        // Assert
        eq.Should().BeTrue();
        hashEq.Should().BeTrue();
    }
}
