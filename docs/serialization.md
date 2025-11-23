# Serialization Guide

Veggerby.Units provides comprehensive JSON serialization support for units, measurements, and quantities using System.Text.Json. This enables persistence, data interchange, and integration with external systems.

## Table of Contents

- [Quick Start](#quick-start)
- [Unit Serialization](#unit-serialization)
- [Measurement Serialization](#measurement-serialization)
- [Quantity Serialization](#quantity-serialization)
- [Usage Examples](#usage-examples)
- [Advanced Topics](#advanced-topics)

## Quick Start

```csharp
using System.Text.Json;
using Veggerby.Units;
using Veggerby.Units.Serialization.Json;

// Configure JSON options
var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());
options.Converters.Add(new DoubleMeasurementJsonConverter());

// Serialize a measurement
var speed = new DoubleMeasurement(100, Unit.SI.m / Unit.SI.s);
string json = JsonSerializer.Serialize(speed, options);
// Output: {"value":100,"unit":"m/s"}

// Deserialize
var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, options);
```

## Unit Serialization

### Basic Usage

The `UnitJsonConverter` serializes units to a compact, human-readable string representation and deserializes them back using the built-in unit parser.

```csharp
var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());

// Serialize basic units
var meter = Unit.SI.m;
var json = JsonSerializer.Serialize(meter, options);
// Output: "m"

// Serialize composite units
var velocity = Unit.SI.m / Unit.SI.s;
json = JsonSerializer.Serialize(velocity, options);
// Output: "m/s"

// Serialize prefixed units
var kilometer = Prefix.k * Unit.SI.m;
json = JsonSerializer.Serialize(kilometer, options);
// Output: "km"
```

### Supported Unit Types

All unit types are fully supported:

**Basic Units:**
```csharp
Unit.SI.m → "m"
Unit.SI.kg → "kg"
Unit.SI.s → "s"
Unit.Imperial.ft → "ft"
```

**Prefixed Units:**
```csharp
Prefix.k * Unit.SI.m → "km"
Prefix.m * Unit.SI.s → "ms"
```

**Product Units:**
```csharp
Unit.SI.m * Unit.SI.s → "m·s"
Unit.SI.kg * Unit.SI.m → "kg·m"
```

**Division Units:**
```csharp
Unit.SI.m / Unit.SI.s → "m/s"
Unit.SI.kg / (Unit.SI.m ^ 3) → "kg/m^3"
```

**Power Units:**
```csharp
Unit.SI.m ^ 2 → "m^2"
Unit.SI.s ^ -1 → "s^-1"
```

**Complex Composite Units:**
```csharp
Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2) → "kg·m^2/s^2"
```

**Affine Units (Temperature):**
```csharp
Unit.SI.C → "°C"
Unit.Imperial.F → "°F"
```

### Formatting Details

The converter uses a custom formatting strategy that:

1. **Preserves unit structure** - Prefixed units (km) and imperial units (ft) are preserved exactly
2. **Adds separators for products** - Uses `·` to separate product terms (m·s) to avoid ambiguity with prefixed units (ms = millisecond)
3. **Handles reciprocals** - Formats `1/s` as `s^-1` to match parser expectations
4. **Recursively formats composites** - Properly handles nested structures like `(m·s)^2`

## Measurement Serialization

### Basic Usage

Measurements are serialized as objects containing both the numeric value and the unit:

```csharp
var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());
options.Converters.Add(new DoubleMeasurementJsonConverter());

var distance = new DoubleMeasurement(100.5, Unit.SI.m);
string json = JsonSerializer.Serialize(distance, options);
// Output: {"value":100.5,"unit":"m"}

var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, options);
// deserialized.Value == 100.5
// deserialized.Unit == Unit.SI.m
```

### Supported Measurement Types

**Concrete Types:**
```csharp
// Int32Measurement
var count = new Int32Measurement(42, Unit.SI.m);
options.Converters.Add(new Int32MeasurementJsonConverter());

// DoubleMeasurement
var temperature = new DoubleMeasurement(298.15, Unit.SI.K);
options.Converters.Add(new DoubleMeasurementJsonConverter());

// DecimalMeasurement
var price = new DecimalMeasurement(99.99m, Unit.None);
options.Converters.Add(new DecimalMeasurementJsonConverter());
```

**Generic Measurements:**
```csharp
// For generic Measurement<T>, use MeasurementJsonConverter<T>
options.Converters.Add(new MeasurementJsonConverter<double>());
var measurement = new Measurement<double>(50.0, Unit.SI.m / Unit.SI.s, DoubleCalculator.Instance);
```

### JSON Format

```json
{
  "value": 100.5,
  "unit": "m/s"
}
```

## Quantity Serialization

Quantities include semantic information (kind) in addition to the measurement:

```csharp
using Veggerby.Units.Quantities;

var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());
options.Converters.Add(new QuantityJsonConverter<double>());

var energy = new Quantity<double>(
    new DoubleMeasurement(1000, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
    QuantityKinds.Energy
);

string json = JsonSerializer.Serialize(energy, options);
// Output: {"value":1000,"unit":"kg·m^2/s^2","kind":"Energy"}

var deserialized = JsonSerializer.Deserialize<Quantity<double>>(json, options);
// deserialized.Kind == QuantityKinds.Energy
```

### JSON Format

```json
{
  "value": 1000,
  "unit": "J",
  "kind": "Energy"
}
```

### Semantic Validation

The kind metadata enables semantic validation during deserialization:

```csharp
// Energy and Torque have the same dimensions (kg·m²/s²) but different meanings
var energy = new Quantity<double>(
    new DoubleMeasurement(100, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
    QuantityKinds.Energy
);

var torque = new Quantity<double>(
    new DoubleMeasurement(100, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)),
    QuantityKinds.Torque
);

// Serialized with different kind metadata
JsonSerializer.Serialize(energy) // {"value":100,...,"kind":"Energy"}
JsonSerializer.Serialize(torque) // {"value":100,...,"kind":"Torque"}
```

## Usage Examples

### Configuration Files

```json
{
  "defaults": {
    "lengthUnit": "m",
    "timeUnit": "s",
    "speedLimit": {
      "value": 120,
      "unit": "km/h"
    }
  }
}
```

```csharp
// Loading configuration
public class AppSettings
{
    public DefaultSettings Defaults { get; set; }
}

public class DefaultSettings
{
    public string LengthUnit { get; set; }
    public string TimeUnit { get; set; }
    public DoubleMeasurement SpeedLimit { get; set; }
}

var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());
options.Converters.Add(new DoubleMeasurementJsonConverter());

var settings = JsonSerializer.Deserialize<AppSettings>(json, options);
// settings.Defaults.SpeedLimit.Value == 120
// settings.Defaults.SpeedLimit.Unit == (Prefix.k * Unit.SI.m) / Unit.SI.h
```

### REST API Payloads

```csharp
public class SensorReading
{
    public DoubleMeasurement Temperature { get; set; }
    public DoubleMeasurement Pressure { get; set; }
    public DateTime Timestamp { get; set; }
}

// API Controller
[HttpPost("readings")]
public IActionResult PostReading([FromBody] SensorReading reading)
{
    // reading.Temperature.Value == 298.15
    // reading.Temperature.Unit == Unit.SI.K
    return Ok();
}

// Client code
var reading = new SensorReading
{
    Temperature = new DoubleMeasurement(298.15, Unit.SI.K),
    Pressure = new DoubleMeasurement(101325, Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2))),
    Timestamp = DateTime.UtcNow
};

var json = JsonSerializer.Serialize(reading, options);
// POST to API
```

### Structured Logging

```csharp
using Microsoft.Extensions.Logging;

var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();

var speed = new DoubleMeasurement(100, Unit.SI.m / Unit.SI.s);
logger.LogInformation("Vehicle speed: {Speed}", JsonSerializer.Serialize(speed, options));
// Output: Vehicle speed: {"value":100,"unit":"m/s"}
```

### Database Storage (EF Core)

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

// Value converter for EF Core
public class DoubleMeasurementConverter : ValueConverter<DoubleMeasurement, string>
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new UnitJsonConverter(), new DoubleMeasurementJsonConverter() }
    };

    public DoubleMeasurementConverter()
        : base(
            m => JsonSerializer.Serialize(m, Options),
            s => JsonSerializer.Deserialize<DoubleMeasurement>(s, Options))
    {
    }
}

// DbContext configuration
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .Property(p => p.Weight)
        .HasConversion(new DoubleMeasurementConverter());
}
```

## Advanced Topics

### Handling Unknown Units

If deserialization encounters an unknown unit string, it throws a `JsonException`:

```csharp
try
{
    var json = "{\"value\":100,\"unit\":\"invalid_unit\"}";
    var measurement = JsonSerializer.Deserialize<DoubleMeasurement>(json, options);
}
catch (JsonException ex)
{
    // ex.Message contains: "Failed to parse unit 'invalid_unit': ..."
}
```

### Custom Serialization Options

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Converters =
    {
        new UnitJsonConverter(),
        new DoubleMeasurementJsonConverter(),
        new QuantityJsonConverter<double>()
    }
};
```

### Type Safety

The converters preserve type information:

```csharp
// Int32Measurement preserves integer values
var intMeasurement = new Int32Measurement(42, Unit.SI.m);
var json = JsonSerializer.Serialize(intMeasurement, options);
var deserialized = JsonSerializer.Deserialize<Int32Measurement>(json, options);
// deserialized.Value is int, not double

// DecimalMeasurement preserves precision
var decimalMeasurement = new DecimalMeasurement(99.999m, Unit.SI.kg);
json = JsonSerializer.Serialize(decimalMeasurement, options);
var decDeserialized = JsonSerializer.Deserialize<DecimalMeasurement>(json, options);
// No loss of precision
```

### Performance Considerations

The serialization implementation is optimized for:

- **Minimal allocations** - Reuses converters and avoids unnecessary object creation
- **Fast parsing** - Uses the existing UnitParser which is battle-tested
- **Efficient formatting** - Custom formatting logic with minimal string operations

Typical performance:
- Unit serialization/deserialization: < 1ms
- Measurement serialization/deserialization: < 1ms
- Bulk operations (1000s of measurements): < 100ms

### Round-Trip Guarantees

All serialization operations guarantee structural equality on round-trip:

```csharp
var original = new DoubleMeasurement(100, Unit.SI.m / Unit.SI.s);
var json = JsonSerializer.Serialize(original, options);
var deserialized = JsonSerializer.Deserialize<DoubleMeasurement>(json, options);

// Guaranteed:
// deserialized.Value == original.Value
// deserialized.Unit == original.Unit (structural equality)
```

### Null Handling

```csharp
// Null units serialize as JSON null
Unit unit = null;
var json = JsonSerializer.Serialize(unit, options); // "null"

// Null measurements serialize as JSON null
DoubleMeasurement measurement = null;
json = JsonSerializer.Serialize(measurement, options); // "null"

// Deserializing null JSON produces null references
var unit2 = JsonSerializer.Deserialize<Unit>("null", options); // null
var meas2 = JsonSerializer.Deserialize<DoubleMeasurement>("null", options); // null
```

### Integration with ASP.NET Core

```csharp
// Startup.cs or Program.cs
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new UnitJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new DoubleMeasurementJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new Int32MeasurementJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new DecimalMeasurementJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new QuantityJsonConverter<double>());
    });
```

## Best Practices

1. **Register all needed converters** - Always register UnitJsonConverter along with your measurement converters
2. **Use concrete types** - Prefer Int32Measurement, DoubleMeasurement, DecimalMeasurement over generic Measurement<T> when possible
3. **Validate on deserialization** - Handle JsonException for invalid unit strings or malformed JSON
4. **Store semantic information** - Use Quantity instead of Measurement when the kind of measurement matters
5. **Test round-trips** - Always verify that serialize → deserialize preserves your data
6. **Document units in schemas** - Include unit expectations in your API documentation
7. **Handle unit mismatches** - Design your APIs to handle unit conversions when needed

## Limitations

- **No XML serialization** - Currently only System.Text.Json is supported
- **No binary serialization** - Binary formats (MessagePack, Protocol Buffers) are not yet supported
- **Kind lookup by name** - QuantityKind deserialization uses reflection to find kinds by name
- **No unit aliases** - Unit strings must match the exact format produced by the parser

## Future Enhancements

Planned improvements:
- XML serialization support (DataContract/XmlSerializer)
- MessagePack binary serialization for high-performance scenarios
- Custom unit aliases and abbreviations
- Schema generation for OpenAPI/Swagger
- Better integration with System.Text.Json source generators
