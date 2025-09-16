using AwesomeAssertions;
using Xunit;

namespace Veggerby.Units.Tests;

public class PrefixPowerTests
{
    [Fact]
    public void PrefixedUnit_Power_DoesNotAlterPrefixAssociation()
    {
        // Arrange
        var prefixed = Prefix.k * Unit.SI.m; // km
        var expected = Unit.Power(prefixed, 2); // km^2 structural

        // Act
        var actual = (prefixed ^ 2);

        // Assert
        actual.Should().Be(expected);
    }
}
