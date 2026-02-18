using AwesomeAssertions;

using System;

using Veggerby.Units.Formatting;
using Veggerby.Units.Parsing;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Parsing;

/// <summary>
/// Tests for parsing qualified unit expressions like "J (Energy)", "Pa (Pressure)".
/// </summary>
public class UnitParserQualifiedTests
{
    [Fact]
    public void GivenJouleEnergy_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "J (Energy)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Energy.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenJouleTorque_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "J (Torque)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Torque.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Torque);
    }

    [Fact]
    public void GivenPascalPressure_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "Pa (Pressure)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Pressure.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Pressure);
    }

    [Fact]
    public void GivenPascalStress_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "Pa (Stress)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Stress.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Stress);
    }

    [Fact]
    public void GivenWattPower_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "W (Power)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Power.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Power);
    }

    [Fact]
    public void GivenWattRadiantFlux_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "W (RadiantFlux)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.RadiantFlux.CanonicalUnit);
        kind.Should().Be(QuantityKinds.RadiantFlux);
    }

    [Fact]
    public void GivenUnitWithoutQualifier_WhenParsingQualified_ThenReturnsUnitAndNullKind()
    {
        // Arrange
        var expression = "N";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Force.CanonicalUnit);
        kind.Should().BeNull();
    }

    [Fact]
    public void GivenInvalidQualifier_WhenParsingQualified_ThenThrowsParseException()
    {
        // Arrange
        var expression = "J (InvalidKind)";

        // Act
        Action act = () => UnitParser.ParseQualified(expression);

        // Assert
        act.Should().Throw<ParseException>()
            .WithMessage("*Unknown quantity kind*");
    }

    [Fact]
    public void GivenMissingClosingParen_WhenParsingQualified_ThenThrowsParseException()
    {
        // Arrange
        var expression = "J (Energy";

        // Act
        Action act = () => UnitParser.ParseQualified(expression);

        // Assert
        act.Should().Throw<ParseException>();
    }

    [Fact]
    public void GivenValidQualified_WhenTryParsing_ThenReturnsTrue()
    {
        // Arrange
        var expression = "J (Energy)";

        // Act
        var success = UnitParser.TryParseQualified(expression, out var unit, out var kind);

        // Assert
        success.Should().BeTrue();
        unit.Should().Be(QuantityKinds.Energy.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenInvalidQualified_WhenTryParsing_ThenReturnsFalse()
    {
        // Arrange
        var expression = "J (InvalidKind)";

        // Act
        var success = UnitParser.TryParseQualified(expression, out var unit, out var kind, out var error);

        // Assert
        success.Should().BeFalse();
        unit.Should().BeNull();
        kind.Should().BeNull();
        error.Should().Contain("Unknown quantity kind");
    }

    [Fact]
    public void GivenQualifiedWithExtraText_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var expression = "J (Energy) extra";

        // Act
        Action act = () => UnitParser.ParseQualified(expression);

        // Assert
        act.Should().Throw<ParseException>()
            .WithMessage("*Unexpected token*");
    }

    [Fact]
    public void GivenComplexUnitWithQualifier_WhenParsingQualified_ThenReturnsUnitAndKind()
    {
        // Arrange
        var expression = "N·m (Torque)";

        // Act
        var (unit, kind) = UnitParser.ParseQualified(expression);

        // Assert
        unit.Should().Be(QuantityKinds.Torque.CanonicalUnit);
        kind.Should().Be(QuantityKinds.Torque);
    }

    [Fact]
    public void GivenQualifiedRoundTrip_WhenParsingFormatterOutput_ThenReturnsOriginalKind()
    {
        // Arrange
        var originalUnit = QuantityKinds.Energy.CanonicalUnit;
        var originalKind = QuantityKinds.Energy;
        var formatted = UnitFormatter.Format(originalUnit, UnitFormat.Qualified, originalKind);

        // Act
        var (parsedUnit, parsedKind) = UnitParser.ParseQualified(formatted);

        // Assert
        parsedUnit.Should().Be(originalUnit);
        parsedKind.Should().Be(originalKind);
    }

    [Fact]
    public void GivenAllAmbiguousSymbols_WhenRoundTrip_ThenSucceeds()
    {
        // Arrange - test all ambiguous symbols from AmbiguityRegistry
        var testCases = new[]
        {
            (QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Energy),
            (QuantityKinds.Work.CanonicalUnit, QuantityKinds.Work),
            (QuantityKinds.Heat.CanonicalUnit, QuantityKinds.Heat),
            (QuantityKinds.Torque.CanonicalUnit, QuantityKinds.Torque),
            (QuantityKinds.Pressure.CanonicalUnit, QuantityKinds.Pressure),
            (QuantityKinds.Stress.CanonicalUnit, QuantityKinds.Stress),
            (QuantityKinds.Power.CanonicalUnit, QuantityKinds.Power),
            (QuantityKinds.RadiantFlux.CanonicalUnit, QuantityKinds.RadiantFlux),
            (QuantityKinds.Inductance.CanonicalUnit, QuantityKinds.Inductance)
        };

        foreach (var (unit, kind) in testCases)
        {
            // Act
            var formatted = UnitFormatter.Format(unit, UnitFormat.Qualified, kind);
            var (parsedUnit, parsedKind) = UnitParser.ParseQualified(formatted);

            // Assert
            parsedUnit.Should().Be(unit, $"Failed for {kind.Name}");
            parsedKind.Should().Be(kind, $"Failed for {kind.Name}");
        }
    }
}
