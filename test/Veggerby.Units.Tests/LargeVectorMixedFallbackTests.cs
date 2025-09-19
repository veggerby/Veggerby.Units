using AwesomeAssertions;

using Veggerby.Units.Formatting;

using Xunit;

namespace Veggerby.Units.Tests;

public class LargeVectorMixedFallbackTests
{
    [Fact]
    public void GivenHugeExponents_WhenMixedFormatting_ThenFallsBackToBaseFactors()
    {
        // Arrange: construct large magnitude exponents to exceed Mixed factoring heuristic threshold
        var huge = (Unit.SI.kg ^ 5) * (Unit.SI.m ^ 7) / (Unit.SI.s ^ 11) / (Unit.SI.A ^ 3) * (Unit.SI.K ^ 2);

        // Act
        var mixed = UnitFormatter.Format(huge, UnitFormat.Mixed, null, strict: true);

        // Assert: expect plain base factor style (no derived substitutions) because enumeration is skipped
        mixed.Should().Contain("kg");
        mixed.Should().Contain("m^7");
        mixed.Should().Contain("s^11");
        mixed.Should().Contain("A^3");
    }
}