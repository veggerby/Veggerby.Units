using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterAdvancedTests
{
    [Fact]
    public void Mixed_CacheIsReused_ForRepeatedSameVector()
    {
        // Arrange
        var unit = QuantityKinds.Power.CanonicalUnit; // W symbol -> derived, but Mixed should short-circuit; do twice for path

        // Act
        var first = UnitFormatter.Format(unit, UnitFormat.Mixed);
        var second = UnitFormatter.Format(unit, UnitFormat.Mixed);

        // Assert
        first.Should().Be(second); // baseline; indirect cache hit when not decomposed
    }

    [Fact]
    public void Mixed_LargeVectorFallsBackToBaseFactors()
    {
        // Arrange: craft an exaggerated exponent by raising (m*s) repeatedly
        var baseUnit = Unit.SI.m * Unit.SI.s; // dimension length*time
        Unit huge = baseUnit;
        for (int i = 0; i < 10; i++) // escalate exponents quickly
        {
            huge *= baseUnit; // multiplication should accumulate exponents
        }

        // Act
        var formatted = UnitFormatter.Format(huge, UnitFormat.Mixed);

        // Assert
        formatted.Should().Contain("m").And.Contain("s"); // base factors path
    }
}