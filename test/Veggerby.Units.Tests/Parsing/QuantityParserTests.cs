using AwesomeAssertions;

using System;

using Veggerby.Units.Parsing;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Parsing;

public class QuantityParserTests
{
    [Fact]
    public void GivenEnergyName_WhenParsing_ThenReturnsEnergyKind()
    {
        // Arrange
        var kindName = "Energy";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenTorqueName_WhenParsing_ThenReturnsTorqueKind()
    {
        // Arrange
        var kindName = "Torque";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Torque);
    }

    [Fact]
    public void GivenPressureName_WhenParsing_ThenReturnsPressureKind()
    {
        // Arrange
        var kindName = "Pressure";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Pressure);
    }

    [Fact]
    public void GivenStressName_WhenParsing_ThenReturnsStressKind()
    {
        // Arrange
        var kindName = "Stress";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Stress);
    }

    [Fact]
    public void GivenPowerName_WhenParsing_ThenReturnsPowerKind()
    {
        // Arrange
        var kindName = "Power";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Power);
    }

    [Fact]
    public void GivenRadiantFluxName_WhenParsing_ThenReturnsRadiantFluxKind()
    {
        // Arrange
        var kindName = "RadiantFlux";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.RadiantFlux);
    }

    [Fact]
    public void GivenInductanceName_WhenParsing_ThenReturnsInductanceKind()
    {
        // Arrange
        var kindName = "Inductance";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Inductance);
    }

    [Fact]
    public void GivenWorkName_WhenParsing_ThenReturnsWorkKind()
    {
        // Arrange
        var kindName = "Work";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Work);
    }

    [Fact]
    public void GivenHeatName_WhenParsing_ThenReturnsHeatKind()
    {
        // Arrange
        var kindName = "Heat";

        // Act
        var result = QuantityParser.Parse(kindName);

        // Assert
        result.Should().Be(QuantityKinds.Heat);
    }

    [Fact]
    public void GivenUnknownName_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var kindName = "UnknownQuantityKind";

        // Act
        Action act = () => QuantityParser.Parse(kindName);

        // Assert
        act.Should().Throw<ParseException>()
            .WithMessage("*Unknown quantity kind*");
    }

    [Fact]
    public void GivenNullName_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        string kindName = null;

        // Act
        Action act = () => QuantityParser.Parse(kindName);

        // Assert
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null*");
    }

    [Fact]
    public void GivenEmptyName_WhenParsing_ThenThrowsParseException()
    {
        // Arrange
        var kindName = "";

        // Act
        Action act = () => QuantityParser.Parse(kindName);

        // Assert
        act.Should().Throw<ParseException>()
            .WithMessage("*cannot be null*");
    }

    [Fact]
    public void GivenValidName_WhenTryParsing_ThenReturnsTrue()
    {
        // Arrange
        var kindName = "Energy";

        // Act
        var success = QuantityParser.TryParse(kindName, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().Be(QuantityKinds.Energy);
    }

    [Fact]
    public void GivenInvalidName_WhenTryParsing_ThenReturnsFalse()
    {
        // Arrange
        var kindName = "InvalidKind";

        // Act
        var success = QuantityParser.TryParse(kindName, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GivenNullName_WhenTryParsing_ThenReturnsFalse()
    {
        // Arrange
        string kindName = null;

        // Act
        var success = QuantityParser.TryParse(kindName, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GetAllKindNames_ReturnsExpectedKinds()
    {
        // Act
        var allNames = QuantityParser.GetAllKindNames();

        // Assert
        allNames.Should().Contain("Energy");
        allNames.Should().Contain("Torque");
        allNames.Should().Contain("Pressure");
        allNames.Should().Contain("Power");
        allNames.Should().Contain("Force");
        allNames.Should().Contain("Length");
        allNames.Should().Contain("Time");
    }

    [Fact]
    public void GetAllKindNames_IncludesAllAmbiguousKinds()
    {
        // Act
        var allNames = QuantityParser.GetAllKindNames();

        // Assert - check all kinds from AmbiguityRegistry
        // J -> Energy, Work, Heat, Torque
        allNames.Should().Contain("Energy");
        allNames.Should().Contain("Work");
        allNames.Should().Contain("Heat");
        allNames.Should().Contain("Torque");

        // Pa -> Pressure, Stress
        allNames.Should().Contain("Pressure");
        allNames.Should().Contain("Stress");

        // W -> Power, RadiantFlux
        allNames.Should().Contain("Power");
        allNames.Should().Contain("RadiantFlux");

        // H -> Inductance
        allNames.Should().Contain("Inductance");
    }
}
