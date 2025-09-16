using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitNegativeExponentChainingTests
{
    [Fact]
    public void Unit_ChainedNegativeExponent_Reduces()
    {
        // Arrange
        var baseUnit = Unit.SI.m;
        var expected = Unit.None / (baseUnit ^ 2); // (m^-1)^2 => m^-2 => 1/m^2

        // Act
        var actual = (baseUnit ^ -1) ^ 2; // note: second power applies to a Power/Division structure

        // Assert
        actual.Should().Be(expected);
    }
}