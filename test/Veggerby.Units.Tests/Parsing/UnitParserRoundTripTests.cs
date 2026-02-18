using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Parsing;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Parsing;

/// <summary>
/// Tests that verify round-trip serialization and deserialization for all formatting modes.
/// Ensures formatter output can be parsed back to the original unit.
/// </summary>
public class UnitParserRoundTripTests
{
    [Fact]
    public void GivenBaseFactorsFormat_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Energy.CanonicalUnit; // kg·m²/s²

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.BaseFactors);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenDerivedSymbolsFormat_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Energy.CanonicalUnit;

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.DerivedSymbols); // "J"
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenNewtonMeterFormat_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Torque.CanonicalUnit; // N·m

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.Mixed, QuantityKinds.Torque); // "N·m"
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenMixedFormat_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Pressure.CanonicalUnit; // Pa

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.Mixed);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenPowerUnit_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Power.CanonicalUnit; // W

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.DerivedSymbols);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenVoltUnit_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = QuantityKinds.Voltage.CanonicalUnit; // V

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.DerivedSymbols);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenSimpleVoltUnit_WhenRoundTripDerivedSymbols_ThenReturnsEquivalentUnit()
    {
        // Arrange  
        var original = QuantityKinds.Voltage.CanonicalUnit; // V

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.DerivedSymbols);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenHertzUnit_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = Unit.None / Unit.SI.s; // Hz = 1/s

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.DerivedSymbols);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }

    [Fact]
    public void GivenNegativeExponent_WhenRoundTrip_ThenReturnsEquivalentUnit()
    {
        // Arrange
        var original = Unit.None / Unit.SI.s; // s^-1

        // Act
        var formatted = UnitFormatter.Format(original, UnitFormat.BaseFactors);
        var parsed = UnitParser.Parse(formatted);

        // Assert
        parsed.Should().Be(original);
    }
}
