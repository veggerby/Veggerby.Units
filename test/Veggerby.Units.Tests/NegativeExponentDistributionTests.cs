using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class NegativeExponentDistributionTests
{
    [Fact]
    public void GivenProductRaisedToNegativePower_WhenExpanded_ThenReciprocalOfPositiveExpansion()
    {
        // Arrange
        var product = Unit.SI.m * Unit.SI.s; // m·s

        // Act
        var neg = product ^ -2; // 1 / (m^2·s^2)
        var pos = product ^ 2;

        // Assert
        (pos * neg).Should().Be(Unit.None);
        pos.Should().Be((Unit.SI.m ^ 2) * (Unit.SI.s ^ 2));
        neg.Should().Be(Unit.None / ((Unit.SI.m ^ 2) * (Unit.SI.s ^ 2)));
    }

    [Fact]
    public void GivenQuotientRaisedToNegativePower_WhenExpanded_ThenInvertsAndDistributes()
    {
        // Arrange
        var quotient = Unit.SI.kg / (Unit.SI.m * Unit.SI.s); // kg/(m·s)

        // Act
        var neg = quotient ^ -1; // reciprocal => (m·s)/kg

        // Assert
        neg.Should().Be((Unit.SI.m * Unit.SI.s) / Unit.SI.kg);
        (neg * quotient).Should().Be(Unit.None);
    }
}