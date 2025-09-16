using System.Linq;
using AwesomeAssertions;
using Xunit;

namespace Veggerby.Units.Tests;

public class PrefixTests
{
    [Fact]
    public void Prefix_InvalidFactor_Throws()
    {
        // Arrange
        var act = () => { var _ = 42 * Unit.SI.m; }; // 42 not a defined prefix

        // Act
        // Assert
        act.Should().Throw<PrefixException>();
    }

    [Fact]
    public void Prefix_RoundTrip_DoubleToPrefix()
    {
        // Arrange
        var expected = Prefix.k;

        // Act
        var actual = (Prefix)1E3; // implicit conversion

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Prefix_All_AreUniqueByFactor()
    {
        // Arrange
        var factors = Prefix.All.Select(p => p.Factor).ToList();

        // Act
        var distinct = factors.Distinct().Count();

        // Assert
        distinct.Should().Be(factors.Count);
    }
}
