using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitPrefixScaleTests
{
    [Fact]
    public void Unit_PrefixUnits_EqualsFails()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Unit_PrefixUnits_EqualityFails()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m;
        var u2 = Prefix.k * Unit.SI.m;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void Unit_ScaleUnits_EqualsFails()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equals = u1.Equals(u2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Unit_ScaleUnits_EqualityFails()
    {
        // Arrange
        var u1 = Unit.Imperial.ft;
        var u2 = Unit.Imperial.ft;

        // Act
        var equal = u1 == u2;

        // Assert
        equal.Should().BeTrue();
    }
}
