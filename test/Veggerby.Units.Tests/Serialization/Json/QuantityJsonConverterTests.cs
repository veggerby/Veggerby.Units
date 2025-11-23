using System.Text.Json;

using Veggerby.Units.Quantities;
using Veggerby.Units.Serialization.Json;

namespace Veggerby.Units.Tests.Serialization.Json;

public class QuantityJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public QuantityJsonConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new UnitJsonConverter());
        _options.Converters.Add(new QuantityJsonConverter<double>());
        _options.Converters.Add(new QuantityJsonConverter<int>());
        _options.Converters.Add(new QuantityJsonConverter<decimal>());
    }

    [Fact]
    public void GivenEnergyQuantity_WhenSerializing_ThenProducesValueUnitAndKind()
    {
        // Arrange
        var measurement = new DoubleMeasurement(1000.0, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
        var quantity = new Quantity<double>(measurement, QuantityKinds.Energy);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);

        // Assert
        json.Should().Contain("\"value\"");
        json.Should().Contain("1000");
        json.Should().Contain("\"unit\"");
        json.Should().Contain("\"kind\"");
        json.Should().Contain("\"Energy\"");
    }

    [Fact]
    public void GivenEnergyQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(1000.0, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
        var quantity = new Quantity<double>(measurement, QuantityKinds.Energy);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenTorqueQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(50.0, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
        var quantity = new Quantity<double>(measurement, QuantityKinds.Torque);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
        deserialized.Kind.Name.Should().Be("Torque");
    }

    [Fact]
    public void GivenLengthQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(100.0, Unit.SI.m);
        var quantity = new Quantity<double>(measurement, QuantityKinds.Length);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenTimeQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(60.0, Unit.SI.s);
        var quantity = new Quantity<double>(measurement, QuantityKinds.Time);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenMassQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(75.0, Unit.SI.kg);
        var quantity = new Quantity<double>(measurement, QuantityKinds.Mass);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenNullQuantity_WhenSerializing_ThenProducesNull()
    {
        // Arrange
        Quantity<double> quantity = null;

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);

        // Assert
        json.Should().Be("null");
    }

    [Fact]
    public void GivenNullJson_WhenDeserializing_ThenReturnsNull()
    {
        // Arrange
        var json = "null";

        // Act
        var quantity = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        quantity.Should().BeNull();
    }

    [Fact]
    public void GivenJsonWithoutValueProperty_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"unit\":\"J\",\"kind\":\"Energy\"}";

        // Act
        Action act = () => JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Missing required 'value' property*");
    }

    [Fact]
    public void GivenJsonWithoutUnitProperty_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"value\":100,\"kind\":\"Energy\"}";

        // Act
        Action act = () => JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Missing required 'unit' property*");
    }

    [Fact]
    public void GivenJsonWithoutKindProperty_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"value\":100,\"unit\":\"J\"}";

        // Act
        Action act = () => JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Missing required 'kind' property*");
    }

    [Fact]
    public void GivenJsonWithUnknownKind_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "{\"value\":100,\"unit\":\"J\",\"kind\":\"UnknownKind\"}";

        // Act
        Action act = () => JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Unknown QuantityKind 'UnknownKind'*");
    }

    [Fact]
    public void GivenNonObjectToken_WhenDeserializing_ThenThrowsJsonException()
    {
        // Arrange
        var json = "\"not an object\"";

        // Act
        Action act = () => JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        act.Should().Throw<JsonException>()
            .WithMessage("*Expected start of object for Quantity*");
    }

    [Fact]
    public void GivenJsonWithExtraProperties_WhenDeserializing_ThenIgnoresExtraProperties()
    {
        // Arrange
        var json = "{\"value\":100,\"unit\":\"m\",\"kind\":\"Length\",\"extra\":\"ignored\"}";

        // Act
        var quantity = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        quantity.Measurement.Value.Should().Be(100);
        quantity.Measurement.Unit.Should().Be(Unit.SI.m);
        quantity.Kind.Should().Be(QuantityKinds.Length);
    }

    [Fact]
    public void GivenIntegerQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new Measurement<int>(42, Unit.SI.m, Calculations.Int32Calculator.Instance);
        var quantity = new Quantity<int>(measurement, QuantityKinds.Length);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<int>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenDecimalQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new Measurement<decimal>(99.99m, Unit.SI.kg, Calculations.DecimalCalculator.Instance);
        var quantity = new Quantity<decimal>(measurement, QuantityKinds.Mass);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<decimal>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenVelocityQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(25.0, Unit.SI.m / Unit.SI.s);
        var quantity = new Quantity<double>(measurement, QuantityKinds.Velocity);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }

    [Fact]
    public void GivenPowerQuantity_WhenRoundTripping_ThenPreservesQuantity()
    {
        // Arrange
        var measurement = new DoubleMeasurement(1000.0, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3));
        var quantity = new Quantity<double>(measurement, QuantityKinds.Power);

        // Act
        var json = JsonSerializer.Serialize(quantity, _options);
        var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, _options);

        // Assert
        deserialized.Measurement.Value.Should().Be(quantity.Measurement.Value);
        deserialized.Measurement.Unit.Should().Be(quantity.Measurement.Unit);
        deserialized.Kind.Should().Be(quantity.Kind);
    }
}
