using System.Text.Json;

using Veggerby.Units.Serialization.Json;

namespace Veggerby.Units.Tests.Serialization.Json;

public class UnitJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public UnitJsonConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new UnitJsonConverter());
    }

    [Fact]
    public void GivenBasicSIUnit_WhenSerializing_ThenProducesSymbol()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("\"m\"");
    }

    [Fact]
    public void GivenBasicSIUnit_WhenDeserializing_ThenReconstructsUnit()
    {
        // Arrange
        var json = "\"m\"";

        // Act
        var unit = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenKilogram_WhenRoundTripping_ThenPreservesUnit()
    {
        // Arrange
        var unit = Unit.SI.kg;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenProductUnit_WhenSerializing_ThenProducesProductSymbol()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.s;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        // ProductUnit uses · separator, which may be escaped as \u00B7 in JSON
        // Both forms are valid and parseable
        json.Should().Match(j => j == "\"m·s\"" || j == "\"m\\u00B7s\"");
    }

    [Fact]
    public void GivenProductUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.m * Unit.SI.s;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenDivisionUnit_WhenSerializing_ThenProducesDivisionSymbol()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("\"m/s\"");
    }

    [Fact]
    public void GivenDivisionUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenPowerUnit_WhenSerializing_ThenProducesPowerSymbol()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("\"m^2\"");
    }

    [Fact]
    public void GivenPowerUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenPrefixedUnit_WhenSerializing_ThenProducesPrefixedSymbol()
    {
        // Arrange
        var unit = Prefix.k * Unit.SI.m;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("\"km\"");
    }

    [Fact]
    public void GivenPrefixedUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Prefix.k * Unit.SI.m;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenComplexCompositeUnit_WhenSerializing_ThenProducesCorrectSymbol()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2);

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        // Should produce a parseable representation
        json.Should().Contain("kg");
        json.Should().Contain("m");
        json.Should().Contain("s");
    }

    [Fact]
    public void GivenComplexCompositeUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2);

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenUnitNone_WhenSerializing_ThenProducesEmptyString()
    {
        // Arrange
        var unit = Unit.None;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("\"\"");
    }

    [Fact]
    public void GivenImperialUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.Imperial.ft;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenAffineTemperatureUnit_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.C;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenNullUnit_WhenSerializing_ThenProducesNull()
    {
        // Arrange
        Unit unit = null;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);

        // Assert
        json.Should().Be("null");
    }

    [Fact]
    public void GivenNullJson_WhenDeserializing_ThenReturnsNull()
    {
        // Arrange
        var json = "null";

        // Act
        var unit = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        unit.Should().BeNull();
    }

    [Fact]
    public void GivenInvalidUnitString_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "\"invalid_unit_xyz\"";

        // Act
        Action act = () => JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Failed to parse unit*");
    }

    [Fact]
    public void GivenEmptyString_WhenDeserializing_ThenReturnsUnitNone()
    {
        // Arrange
        var json = "\"\"";

        // Act
        var unit = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        unit.Should().Be(Unit.None);
    }

    [Fact]
    public void GivenNonStringToken_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "123";

        // Act
        Action act = () => JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Expected string token for Unit*");
    }

    [Fact]
    public void GivenDerivedUnitNewton_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenNegativeExponent_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = Unit.SI.s ^ -1;

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }

    [Fact]
    public void GivenMultiplePrefixedUnits_WhenRoundTripping_ThenPreservesEquivalentUnit()
    {
        // Arrange
        var unit = (Prefix.k * Unit.SI.m) / (Prefix.m * Unit.SI.s);

        // Act
        var json = JsonSerializer.Serialize(unit, _options);
        var deserialized = JsonSerializer.Deserialize<Unit>(json, _options);

        // Assert
        deserialized.Should().Be(unit);
    }
}
