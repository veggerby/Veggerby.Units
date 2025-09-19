using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterWbMixedTests
{
    [Fact]
    public void GivenMagneticFlux_WhenMixedStrict_NoKind_ThenDecomposesWb()
    {
        // Arrange
        var fluxUnit = QuantityKinds.MagneticFlux.CanonicalUnit; // Wb = kg·m^2/(s^2·A)

        // Act
        var mixed = UnitFormatter.Format(fluxUnit, UnitFormat.Mixed, null, strict: true);

        // Assert
        mixed.Should().NotBe("Wb"); // forced decomposition
        mixed.Should().Contain("W"); // expect factoring via W when beneficial
        mixed.Should().Contain("s");
        mixed.Should().Contain("A");
    }

    [Fact]
    public void GivenMagneticFlux_WhenMixedWithKind_ThenDecomposesAndQualifies()
    {
        var fluxUnit = QuantityKinds.MagneticFlux.CanonicalUnit;
        var kind = QuantityKinds.MagneticFlux;

        // Act
        var mixed = UnitFormatter.Format(fluxUnit, UnitFormat.Mixed, kind, strict: true);

        // Assert
        mixed.Should().NotBe("Wb"); // still decomposed
        mixed.Should().Contain("W·s/A"); // expected factoring path
        mixed.Should().EndWith("(MagneticFlux)"); // qualification appended due to ambiguous W symbol
    }

    [Fact]
    public void GivenMagneticFlux_WhenMixedNonStrictWithKind_ThenDecomposesAndQualifies()
    {
        var fluxUnit = QuantityKinds.MagneticFlux.CanonicalUnit;
        var kind = QuantityKinds.MagneticFlux;

        // Act
        var mixed = UnitFormatter.Format(fluxUnit, UnitFormat.Mixed, kind, strict: false);

        // Assert
        mixed.Should().NotBe("Wb"); // still decomposed
        mixed.Should().Contain("W·s/A"); // expected factoring path
        mixed.Should().EndWith("(MagneticFlux)"); // qualification appended due to ambiguous W symbol
    }

    [Fact]
    public void GivenMagneticFlux_WhenDerivedSymbolsStrict_ThenShowsWb()
    {
        var fluxUnit = QuantityKinds.MagneticFlux.CanonicalUnit;
        var derived = UnitFormatter.Format(fluxUnit, UnitFormat.DerivedSymbols, null, strict: true);
        derived.Should().Be("Wb");
    }
}