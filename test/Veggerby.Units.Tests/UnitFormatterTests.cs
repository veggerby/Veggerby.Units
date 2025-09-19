using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterTests
{
    [Fact]
    public void BaseFactors_ReturnsRawSymbol()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2); // J dimension

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.BaseFactors);

        // Assert
        formatted.Should().Contain("kg").And.Contain("m^2");
    }

    [Fact]
    public void DerivedSymbols_Known_ReturnsSymbol()
    {
        // Arrange
        var unit = QuantityKinds.Energy.CanonicalUnit; // J

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.DerivedSymbols);

        // Assert
        formatted.Should().Be("J");
    }

    [Fact]
    public void DerivedSymbols_StrictUnknown_FallsBackToMixed()
    {
        // Arrange
        var unit = Unit.SI.kg * Unit.SI.kg; // not a standard derived symbol

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.DerivedSymbols, strict: true);

        // Assert
        formatted.Should().Contain("kg^2");
    }

    [Fact]
    public void Mixed_AmbiguousEnergyWithKind_AppendsKind()
    {
        // Arrange
        var unit = QuantityKinds.Energy.CanonicalUnit; // J ambiguous

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.Mixed, QuantityKinds.Work);

        // Assert
        formatted.Should().Be("J (Work)");
    }

    [Fact]
    public void Qualified_AmbiguousWithKind_AppendsKind()
    {
        // Arrange
        var unit = QuantityKinds.Stress.CanonicalUnit; // Pa ambiguous with Pressure

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.Qualified, QuantityKinds.Stress);

        // Assert
        formatted.Should().Be("Pa (Stress)");
    }

    [Fact]
    public void Mixed_TorquePreferenceShowsNewtonMetre()
    {
        // Arrange
        var unit = QuantityKinds.Energy.CanonicalUnit; // same dimension as torque

        // Act
        var formatted = UnitFormatter.Format(unit, UnitFormat.Mixed, QuantityKinds.Torque);

        // Assert
        formatted.Should().Be("N·m");
    }
}