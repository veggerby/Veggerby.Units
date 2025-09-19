using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterAmbiguityQualificationTests
{
    [Theory]
    [InlineData("J", "Energy")]
    [InlineData("J", "Work")]
    [InlineData("J", "Heat")]
    [InlineData("Pa", "Pressure")]
    [InlineData("Pa", "Stress")]
    [InlineData("W", "Power")]
    [InlineData("W", "RadiantFlux")]
    public void GivenAmbiguousSymbolAndKind_WhenQualifiedFormat_ThenAppendsKind(string symbol, string kindName)
    {
        // Arrange
        var kind = GetKind(kindName);
        var unit = GetUnit(symbol, kindName);

        // Act
        var qualified = UnitFormatter.Format(unit, UnitFormat.Qualified, kind, strict: true);
        var mixed = UnitFormatter.Format(unit, UnitFormat.Mixed, kind, strict: true); // Mixed should also append when ambiguous & kind supplied

        // Assert
        qualified.Should().Be(symbol + " (" + kind.Name + ")");
        mixed.Should().Be(symbol + " (" + kind.Name + ")");
    }

    [Fact]
    public void GivenTorqueKind_WhenFormattingEnergyDimension_MixedUsesTorqueOverride()
    {
        // Arrange
        var torqueKind = QuantityKinds.Torque; // dimensionally J
        var energyUnit = QuantityKinds.Energy.CanonicalUnit; // kg·m^2/s^2

        // Act
        var mixed = UnitFormatter.Format(energyUnit, UnitFormat.Mixed, torqueKind, strict: true);
        var qualified = UnitFormatter.Format(energyUnit, UnitFormat.Qualified, torqueKind, strict: true);

        // Assert
        mixed.Should().Be("N·m"); // torque override
        qualified.Should().Be("J (Torque)"); // Qualified still uses symbol + kind, not override
    }

    private static QuantityKind GetKind(string name)
    {
        return name switch
        {
            "Energy" => QuantityKinds.Energy,
            "Work" => QuantityKinds.Work,
            "Heat" => QuantityKinds.Heat,
            "Pressure" => QuantityKinds.Pressure,
            "Stress" => QuantityKinds.YoungsModulus, // stress-like Pa (Young's modulus shares Pa dimensions)
            "Power" => QuantityKinds.Power,
            "RadiantFlux" => QuantityKinds.RadiantFlux,
            _ => QuantityKinds.Energy
        };
    }

    private static Unit GetUnit(string symbol, string kindName)
    {
        return symbol switch
        {
            "J" => QuantityKinds.Energy.CanonicalUnit,
            "Pa" => QuantityKinds.Pressure.CanonicalUnit,
            "W" => QuantityKinds.Power.CanonicalUnit,
            _ => QuantityKinds.Energy.CanonicalUnit
        };
    }
}