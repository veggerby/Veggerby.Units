using AwesomeAssertions;

using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class ConversionTests
{
    [Fact]
    public void GivenKilometreMeasurement_WhenConvertingToMetres_ThenScalesByThousand()
    {
        // Arrange
        var m = new DoubleMeasurement(3.5, Prefix.k * Unit.SI.m); // 3.5 km

        // Act
        var converted = m.ConvertTo(Unit.SI.m);

        // Assert
        ((double)converted).Should().Be(3500d);
        converted.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenFootMeasurement_WhenConvertingToMetres_ThenAppliesFeetToMetresFactor()
    {
        // Arrange
        var m = new DoubleMeasurement(10, Unit.Imperial.ft);
        var expected = 10 * ImperialUnitSystem.FeetToMetres;

        // Act
        var converted = m.ConvertTo(Unit.SI.m);

        // Assert
        ((double)converted).Should().Be(expected);
        converted.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenCompatibleMeasurements_WhenComparing_ThenAlignmentAllowsValueComparison()
    {
        // Arrange
        var v1 = new DoubleMeasurement(1, Prefix.k * Unit.SI.m); // 1000 m
        var v2 = new DoubleMeasurement(500, Unit.SI.m);

        // Act
        var comparison = v2 < v1; // 500 m < 1000 m

        // Assert
        comparison.Should().BeTrue();
    }
}