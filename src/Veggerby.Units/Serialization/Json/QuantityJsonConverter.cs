using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Veggerby.Units.Quantities;

namespace Veggerby.Units.Serialization.Json;

/// <summary>
/// JSON converter for <see cref="Quantity{T}"/> that serializes quantities with their value, unit, and kind metadata.
/// </summary>
/// <remarks>
/// Serialization produces JSON in the format: {"value": 100, "unit": "J", "kind": "Energy"}
/// This enables semantic validation on deserialization by reconstructing the QuantityKind from its name.
/// </remarks>
public class QuantityJsonConverter<T> : JsonConverter<Quantity<T>> where T : IComparable
{
    private readonly UnitJsonConverter _unitConverter = new();
    private readonly MeasurementJsonConverter<T> _measurementConverter = new();

    /// <summary>
    /// Reads a quantity from JSON.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Serializer options.</param>
    /// <returns>The parsed quantity.</returns>
    /// <exception cref="JsonException">Thrown when the JSON format is invalid or the quantity cannot be constructed.</exception>
    public override Quantity<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Expected start of object for Quantity, but got {reader.TokenType}");
        }

        T value = default;
        Unit unit = null;
        string kindName = null;
        bool hasValue = false;
        bool hasUnit = false;
        bool hasKind = false;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException($"Expected property name, but got {reader.TokenType}");
            }

            var propertyName = reader.GetString();
            reader.Read();

            if (string.Equals(propertyName, "value", StringComparison.OrdinalIgnoreCase))
            {
                value = ReadValue(ref reader);
                hasValue = true;
            }
            else if (string.Equals(propertyName, "unit", StringComparison.OrdinalIgnoreCase))
            {
                unit = _unitConverter.Read(ref reader, typeof(Unit), options);
                hasUnit = true;
            }
            else if (string.Equals(propertyName, "kind", StringComparison.OrdinalIgnoreCase))
            {
                kindName = reader.GetString();
                hasKind = true;
            }
            else
            {
                // Skip unknown properties
                reader.Skip();
            }
        }

        if (!hasValue)
        {
            throw new JsonException("Missing required 'value' property in Quantity JSON");
        }

        if (!hasUnit)
        {
            throw new JsonException("Missing required 'unit' property in Quantity JSON");
        }

        if (!hasKind)
        {
            throw new JsonException("Missing required 'kind' property in Quantity JSON");
        }

        // Reconstruct Measurement
        var calculator = GetCalculator();
        var measurement = new Measurement<T>(value, unit, calculator);

        // Find QuantityKind by name
        var kind = FindQuantityKindByName(kindName);
        if (kind == null)
        {
            throw new JsonException($"Unknown QuantityKind '{kindName}'");
        }

        return new Quantity<T>(measurement, kind, strictDimensionCheck: true);
    }

    /// <summary>
    /// Writes a quantity to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The quantity to serialize.</param>
    /// <param name="options">Serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Quantity<T> value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName("value");
        WriteValue(writer, value.Measurement.Value);
        writer.WritePropertyName("unit");
        _unitConverter.Write(writer, value.Measurement.Unit, options);
        writer.WritePropertyName("kind");
        writer.WriteStringValue(value.Kind.Name);
        writer.WriteEndObject();
    }

    private T ReadValue(ref Utf8JsonReader reader)
    {
        var type = typeof(T);

        if (type == typeof(double))
        {
            return (T)(object)reader.GetDouble();
        }

        if (type == typeof(int))
        {
            return (T)(object)reader.GetInt32();
        }

        if (type == typeof(decimal))
        {
            return (T)(object)reader.GetDecimal();
        }

        if (type == typeof(float))
        {
            return (T)(object)reader.GetSingle();
        }

        if (type == typeof(long))
        {
            return (T)(object)reader.GetInt64();
        }

        throw new JsonException($"Unsupported value type {type.Name} for Quantity deserialization");
    }

    private void WriteValue(Utf8JsonWriter writer, T value)
    {
        var type = typeof(T);

        if (type == typeof(double))
        {
            writer.WriteNumberValue((double)(object)value);
        }
        else if (type == typeof(int))
        {
            writer.WriteNumberValue((int)(object)value);
        }
        else if (type == typeof(decimal))
        {
            writer.WriteNumberValue((decimal)(object)value);
        }
        else if (type == typeof(float))
        {
            writer.WriteNumberValue((float)(object)value);
        }
        else if (type == typeof(long))
        {
            writer.WriteNumberValue((long)(object)value);
        }
        else
        {
            throw new JsonException($"Unsupported value type {type.Name} for Quantity serialization");
        }
    }

    private Calculations.Calculator<T> GetCalculator()
    {
        var type = typeof(T);

        if (type == typeof(double))
        {
            return (Calculations.Calculator<T>)(object)Calculations.DoubleCalculator.Instance;
        }

        if (type == typeof(int))
        {
            return (Calculations.Calculator<T>)(object)Calculations.Int32Calculator.Instance;
        }

        if (type == typeof(decimal))
        {
            return (Calculations.Calculator<T>)(object)Calculations.DecimalCalculator.Instance;
        }

        throw new JsonException($"Unsupported calculator type {type.Name} for Quantity deserialization");
    }

    private static QuantityKind FindQuantityKindByName(string name)
    {
        // Find all static readonly QuantityKind fields in the QuantityKinds class
        var quantityKindsType = typeof(QuantityKinds);
        var fields = quantityKindsType.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(QuantityKind) && f.IsInitOnly);

        foreach (var field in fields)
        {
            var kind = (QuantityKind)field.GetValue(null);
            if (kind != null && kind.Name == name)
            {
                return kind;
            }
        }

        return null;
    }
}
