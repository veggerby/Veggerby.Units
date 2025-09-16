using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class NegativeExponentChainingTests
{
    [Fact]
    public void GivenNegativeExponentAppliedTwice_WhenReduced_ThenResultIsInverseSquare()
    {
        // Arrange
        var baseUnit = Unit.SI.m;
        var expected = Unit.None / (baseUnit ^ 2); // expected canonical form

        // Act
        var actual = (baseUnit ^ -1) ^ 2; // currently failing (documents gap)

        // Assert
        actual.Should().Be(expected);
    }
}