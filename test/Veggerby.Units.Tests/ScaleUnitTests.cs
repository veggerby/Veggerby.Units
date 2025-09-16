using AwesomeAssertions;
using Veggerby.Units.Conversion;
using Xunit;

namespace Veggerby.Units.Tests;

public class ScaleUnitTests
{
    [Fact]
    public void ScaleUnit_Equality_SameScaleAndBase()
    {
        // Arrange
        var ft1 = Unit.Imperial.ft; // base definition
        var ft2 = Unit.Imperial.ft; // same instance reference, but asserts correctness anyway

        // Act
        var equal = ft1 == ft2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void ScaleUnit_ConvertToSameUnit_ReturnsSameReference()
    {
        // Arrange
        var value = new DoubleMeasurement(10, Unit.Imperial.ft);

        // Act
        var converted = value.ConvertTo(Unit.Imperial.ft);

        // Assert
        ReferenceEquals(value, converted).Should().BeTrue();
    }
}
