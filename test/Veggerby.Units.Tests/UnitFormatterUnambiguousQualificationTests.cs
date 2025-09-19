using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class UnitFormatterUnambiguousQualificationTests
{
    [Fact]
    public void GivenUnambiguousSymbol_WhenQualifiedWithKind_ThenNoKindAppended()
    {
        // Arrange
        var unit = QuantityKinds.Force.CanonicalUnit; // N
        var kind = QuantityKinds.Force;

        // Act
        var qualified = UnitFormatter.Format(unit, UnitFormat.Qualified, kind, strict: true);
        var mixed = UnitFormatter.Format(unit, UnitFormat.Mixed, kind, strict: true);

        // Assert
        qualified.Should().Be("N");
        mixed.Should().Be("N");
    }

    [Fact]
    public void GivenAmbiguousSymbol_WhenQualifiedWithKind_ThenKindAppended()
    {
        // Arrange
        var unit = QuantityKinds.Energy.CanonicalUnit; // J
        var kind = QuantityKinds.Energy;

        // Act
        var qualified = UnitFormatter.Format(unit, UnitFormat.Qualified, kind, strict: true);
        var mixed = UnitFormatter.Format(unit, UnitFormat.Mixed, kind, strict: true);

        // Assert
        qualified.Should().Be("J (Energy)");
        mixed.Should().Be("J (Energy)");
    }
}