using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class PrefixCompositeInteractionTests
{
    [Fact]
    public void Given_KiloMetreTimesMetreDividedByMetreSquared_When_Reduced_Then_ResultIsKiloMetreOverMetre()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m; // km
        var expected = km / Unit.SI.m; // km/m (cannot reduce prefix away)

        // Act
        var actual = (km * Unit.SI.m) / (Unit.SI.m ^ 2); // (km*m)/m^2

        // Assert
        actual.Should().Be(expected);
    }
}