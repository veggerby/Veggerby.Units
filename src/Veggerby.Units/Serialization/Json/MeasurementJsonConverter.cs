using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Veggerby.Units.Calculations;
using Veggerby.Units.Parsing;

namespace Veggerby.Units.Serialization.Json;

/// <summary>
/// JSON converter for <see cref="Measurement{T}"/> that serializes measurements as objects
/// containing both value and unit in a compact, human-readable format.
/// </summary>
/// <remarks>
/// Serialization produces JSON in the format: {"value": 100, "unit": "m/s"}
/// Deserialization uses <see cref="UnitParser"/> to reconstruct the unit and appropriate
/// <see cref="Calculator{T}"/> for the measurement type.
/// </remarks>
public class MeasurementJsonConverter<T> : JsonConverter<Measurement<T>> where T : IComparable
{
    private readonly UnitJsonConverter _unitConverter = new();

    /// <summary>
    /// Reads a measurement from JSON.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Serializer options.</param>
    /// <returns>The parsed measurement.</returns>
    /// <exception cref="JsonException">Thrown when the JSON format is invalid or the measurement cannot be constructed.</exception>
    public override Measurement<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Expected start of object for Measurement, but got {reader.TokenType}");
        }

        T value = default;
        Unit unit = null;
        bool hasValue = false;
        bool hasUnit = false;

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
            else
            {
                // Skip unknown properties
                reader.Skip();
            }
        }

        if (!hasValue)
        {
            throw new JsonException("Missing required 'value' property in Measurement JSON");
        }

        if (!hasUnit)
        {
            throw new JsonException("Missing required 'unit' property in Measurement JSON");
        }

        var calculator = GetCalculator();
        return new Measurement<T>(value, unit, calculator);
    }

    /// <summary>
    /// Writes a measurement to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The measurement to serialize.</param>
    /// <param name="options">Serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Measurement<T> value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName("value");
        WriteValue(writer, value.Value);
        writer.WritePropertyName("unit");
        _unitConverter.Write(writer, value.Unit, options);
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

        throw new JsonException($"Unsupported value type {type.Name} for Measurement deserialization");
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
            throw new JsonException($"Unsupported value type {type.Name} for Measurement serialization");
        }
    }

    private Calculator<T> GetCalculator()
    {
        var type = typeof(T);

        if (type == typeof(double))
        {
            return (Calculator<T>)(object)DoubleCalculator.Instance;
        }

        if (type == typeof(int))
        {
            return (Calculator<T>)(object)Int32Calculator.Instance;
        }

        if (type == typeof(decimal))
        {
            return (Calculator<T>)(object)DecimalCalculator.Instance;
        }

        throw new JsonException($"Unsupported calculator type {type.Name} for Measurement deserialization");
    }
}
