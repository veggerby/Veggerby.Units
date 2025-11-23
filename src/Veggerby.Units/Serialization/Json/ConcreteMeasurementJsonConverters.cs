using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Veggerby.Units.Serialization.Json;

/// <summary>
/// JSON converter for <see cref="Int32Measurement"/>.
/// </summary>
public class Int32MeasurementJsonConverter : JsonConverter<Int32Measurement>
{
    private readonly MeasurementJsonConverter<int> _innerConverter = new();

    /// <inheritdoc />
    public override Int32Measurement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var measurement = _innerConverter.Read(ref reader, typeof(Measurement<int>), options);

        if (measurement is null)
        {
            return null;
        }

        return new Int32Measurement(measurement.Value, measurement.Unit);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Int32Measurement value, JsonSerializerOptions options)
    {
        _innerConverter.Write(writer, value, options);
    }
}

/// <summary>
/// JSON converter for <see cref="DoubleMeasurement"/>.
/// </summary>
public class DoubleMeasurementJsonConverter : JsonConverter<DoubleMeasurement>
{
    private readonly MeasurementJsonConverter<double> _innerConverter = new();

    /// <inheritdoc />
    public override DoubleMeasurement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var measurement = _innerConverter.Read(ref reader, typeof(Measurement<double>), options);

        if (measurement is null)
        {
            return null;
        }

        return new DoubleMeasurement(measurement.Value, measurement.Unit);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DoubleMeasurement value, JsonSerializerOptions options)
    {
        _innerConverter.Write(writer, value, options);
    }
}

/// <summary>
/// JSON converter for <see cref="DecimalMeasurement"/>.
/// </summary>
public class DecimalMeasurementJsonConverter : JsonConverter<DecimalMeasurement>
{
    private readonly MeasurementJsonConverter<decimal> _innerConverter = new();

    /// <inheritdoc />
    public override DecimalMeasurement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var measurement = _innerConverter.Read(ref reader, typeof(Measurement<decimal>), options);

        if (measurement is null)
        {
            return null;
        }

        return new DecimalMeasurement(measurement.Value, measurement.Unit);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DecimalMeasurement value, JsonSerializerOptions options)
    {
        _innerConverter.Write(writer, value, options);
    }
}
