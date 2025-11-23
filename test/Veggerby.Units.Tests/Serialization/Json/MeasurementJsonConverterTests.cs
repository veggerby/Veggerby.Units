using System.Text.Json;

using Veggerby.Units.Serialization.Json;

namespace Veggerby.Units.Tests.Serialization.Json;

public class MeasurementJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public MeasurementJsonConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new UnitJsonConverter());
        _options.Converters.Add(new DoubleMeasurementJsonConverter());
        _options.Converters.Add(new Int32MeasurementJsonConverter());
        _options.Converters.Add(new DecimalMeasurementJsonConverter());
    }

    [Fact]
    public void GivenDoubleMeasurement_WhenSerializing_ThenProducesValueAndUnit()
    {
        // Arrange
        var measurement = new DoubleMeasurement(100.0, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);

        // Assert
        json.Should().Contain("\"value\"");
        json.Should().Contain("100");
        json.Should().Contain("\"unit\"");
        json.Should().Contain("\"m\"");
    }

    [Fact]
    public void GivenDoubleMeasurement_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(100.0, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenInt32Measurement_WhenSerializing_ThenProducesValueAndUnit()
    {
        // Arrange
        var measurement = new Int32Measurement(42, Unit.SI.kg);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);

        // Assert
        json.Should().Contain("\"value\"");
        json.Should().Contain("42");
        json.Should().Contain("\"unit\"");
        json.Should().Contain("\"kg\"");
    }

    [Fact]
    public void GivenInt32Measurement_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new Int32Measurement(42, Unit.SI.kg);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<Int32Measurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenDecimalMeasurement_WhenSerializing_ThenProducesValueAndUnit()
    {
        // Arrange
        var measurement = new DecimalMeasurement(99.99m, Unit.SI.s);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);

        // Assert
        json.Should().Contain("\"value\"");
        json.Should().Contain("99.99");
        json.Should().Contain("\"unit\"");
        json.Should().Contain("\"s\"");
    }

    [Fact]
    public void GivenDecimalMeasurement_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DecimalMeasurement(99.99m, Unit.SI.s);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DecimalMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithCompositeUnit_WhenRoundTripping_ThenPreservesEquivalentMeasurement()
    {
        // Arrange
        var unit = Unit.SI.m / Unit.SI.s;
        var measurement = new DoubleMeasurement(25.5, unit);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithPrefixedUnit_WhenRoundTripping_ThenPreservesEquivalentMeasurement()
    {
        // Arrange
        var unit = Prefix.k * Unit.SI.m;
        var measurement = new DoubleMeasurement(5.0, unit);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithPowerUnit_WhenRoundTripping_ThenPreservesEquivalentMeasurement()
    {
        // Arrange
        var unit = Unit.SI.m ^ 2;
        var measurement = new DoubleMeasurement(100.0, unit);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithUnitNone_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(3.14, Unit.None);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenNullMeasurement_WhenSerializing_ThenProducesNull()
    {
        // Arrange
        DoubleMeasurement measurement = null;

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);

        // Assert
        json.Should().Be("null");
    }

    [Fact]
    public void GivenNullJson_WhenDeserializing_ThenReturnsNull()
    {
        // Arrange
        var json = "null";

        // Act
        var measurement = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        measurement.Should().BeNull();
    }

    [Fact]
    public void GivenJsonWithoutValueProperty_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"unit\":\"m\"}";

        // Act
        Action act = () => JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Missing required 'value' property*");
    }

    [Fact]
    public void GivenJsonWithoutUnitProperty_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"value\":100}";

        // Act
        Action act = () => JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Missing required 'unit' property*");
    }

    [Fact]
    public void GivenNonObjectToken_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "\"not an object\"";

        // Act
        Action act = () => JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Expected start of object for Measurement*");
    }

    [Fact]
    public void GivenJsonWithExtraProperties_WhenDeserializing_ThenIgnoresExtraProperties()
    {
        // Arrange
        var json = "{\"value\":100,\"unit\":\"m\",\"extra\":\"ignored\"}";

        // Act
        var measurement = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        measurement.Value.Should().Be(100);
        measurement.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenMeasurementWithNegativeValue_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(-42.5, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithZeroValue_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(0.0, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithVeryLargeValue_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(1.23e308, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenMeasurementWithVerySmallValue_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(1.23e-308, Unit.SI.m);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenImperialMeasurement_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new DoubleMeasurement(10.0, Unit.Imperial.ft);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenComplexDerivedUnitMeasurement_WhenRoundTripping_ThenPreservesEquivalentMeasurement()
    {
        // Arrange
        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2);
        var measurement = new DoubleMeasurement(1000.0, unit);

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenGenericMeasurementOfDouble_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new Measurement<double>(50.0, Unit.SI.m / Unit.SI.s, Calculations.DoubleCalculator.Instance);
        _options.Converters.Add(new MeasurementJsonConverter<double>());

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<Measurement<double>>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenGenericMeasurementOfInt_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new Measurement<int>(100, Unit.SI.kg, Calculations.Int32Calculator.Instance);
        _options.Converters.Add(new MeasurementJsonConverter<int>());

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<Measurement<int>>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }

    [Fact]
    public void GivenGenericMeasurementOfDecimal_WhenRoundTripping_ThenPreservesMeasurement()
    {
        // Arrange
        var measurement = new Measurement<decimal>(75.5m, Unit.SI.s, Calculations.DecimalCalculator.Instance);
        _options.Converters.Add(new MeasurementJsonConverter<decimal>());

        // Act
        var json = JsonSerializer.Serialize(measurement, _options);
        var deserialized = JsonSerializer.Deserialize<Measurement<decimal>>(json, _options);

        // Assert
        deserialized.Value.Should().Be(measurement.Value);
        deserialized.Unit.Should().Be(measurement.Unit);
    }
}
