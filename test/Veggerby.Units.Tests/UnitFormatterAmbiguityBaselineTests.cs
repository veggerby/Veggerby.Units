using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterAmbiguityBaselineTests
{
    [Theory]
    [InlineData("J")]
    [InlineData("Pa")]
    [InlineData("W")]
    [InlineData("H")]
    public void GivenAmbiguousDerivedSymbol_WhenFormattingWithoutKind_ThenNoQualification(string symbol)
    {
        // Arrange
        Unit unit = symbol switch
        {
            "J" => QuantityKinds.Energy.CanonicalUnit, // kg·m^2/s^2
            "Pa" => QuantityKinds.Pressure.CanonicalUnit, // kg/(m·s^2)
            "W" => QuantityKinds.Power.CanonicalUnit, // J/s
            "H" => QuantityKinds.Inductance.CanonicalUnit, // kg·m^2/(s^2·A^2)
            _ => QuantityKinds.Energy.CanonicalUnit
        };

        // Act
        var derived = UnitFormatter.Format(unit, UnitFormat.DerivedSymbols, null, strict: true);
        var qualified = UnitFormatter.Format(unit, UnitFormat.Qualified, null, strict: true);
        var mixed = UnitFormatter.Format(unit, UnitFormat.Mixed, null, strict: true);

        // Assert
        derived.Should().Be(symbol);
        qualified.Should().Be(symbol); // no kind appended when not supplied
        mixed.Should().Be(symbol); // Mixed maintains exact symbol for simple vector
    }
}