using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class HashCodeConsistencyTests
{
    [Fact]
    public void GivenIdenticalProductUnits_WhenGettingHashCode_ThenEqualsImpliesSameHash()
    {
        // Arrange
        var u1 = Unit.SI.m * Unit.SI.s;
        var u2 = Unit.SI.s * Unit.SI.m; // commutative

        // Act
        var eq = u1 == u2;
        var hashEq = u1.GetHashCode() == u2.GetHashCode();

        // Assert
        eq.Should().BeTrue();
        hashEq.Should().BeTrue();
    }

    [Fact]
    public void GivenIdenticalPowerUnits_WhenGettingHashCode_ThenEqualsImpliesSameHash()
    {
        // Arrange
        var u1 = Unit.SI.m ^ 3;
        var u2 = Unit.Power(Unit.SI.m, 3);

        // Act
        var eq = u1 == u2;
        var hashEq = u1.GetHashCode() == u2.GetHashCode();

        // Assert
        eq.Should().BeTrue();
        hashEq.Should().BeTrue();
    }
}