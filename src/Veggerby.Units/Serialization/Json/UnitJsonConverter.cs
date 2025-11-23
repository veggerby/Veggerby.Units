using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Veggerby.Units.Parsing;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Serialization.Json;

/// <summary>
/// JSON converter for <see cref="Unit"/> that serializes units to their symbolic representation
/// and deserializes using <see cref="UnitParser"/>.
/// </summary>
/// <remarks>
/// Serialization uses a custom unambiguous format that preserves unit structure (prefixes, imperial units)
/// while adding separators for composite units to enable correct parsing.
/// Deserialization uses <see cref="UnitParser"/> to parse the symbolic string back into a Unit.
/// This approach provides compact, human-readable JSON while maintaining full fidelity for round-trip conversions.
/// </remarks>
public class UnitJsonConverter : JsonConverter<Unit>
{
    /// <summary>
    /// Reads a unit from JSON by parsing its symbolic representation.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Serializer options.</param>
    /// <returns>The parsed unit.</returns>
    /// <exception cref="JsonException">Thrown when the unit string cannot be parsed.</exception>
    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string token for Unit, but got {reader.TokenType}");
        }

        var unitString = reader.GetString();

        // Handle empty string as Unit.None
        if (string.IsNullOrWhiteSpace(unitString))
        {
            return Unit.None;
        }

        if (!UnitParser.TryParse(unitString, out var unit, out var errorMessage))
        {
            throw new JsonException($"Failed to parse unit '{unitString}': {errorMessage}");
        }

        return unit;
    }

    /// <summary>
    /// Writes a unit to JSON as its symbolic representation.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The unit to serialize.</param>
    /// <param name="options">Serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        var symbol = FormatUnit(value);
        writer.WriteStringValue(symbol);
    }

    private static string FormatUnit(Unit unit)
    {
        if (unit == Unit.None)
        {
            return string.Empty;
        }

        // For ProductUnit, add separators between operands to avoid ambiguity
        if (unit is IProductOperation product)
        {
            var operands = product.Operands.Cast<Unit>();
            return string.Join("·", operands.Select(FormatUnit));
        }

        // For DivisionUnit, format dividend and divisor recursively
        if (unit is DivisionUnit divisionUnit)
        {
            var dividend = GetDividend(divisionUnit);
            var divisor = GetDivisor(divisionUnit);

            var dividendStr = FormatUnit(dividend);
            var divisorStr = FormatUnit(divisor);

            // Special case: if dividend is Unit.None (empty), format as divisor^-1 instead of 1/divisor
            // This is because the parser doesn't accept "1/s" format
            if (string.IsNullOrEmpty(dividendStr))
            {
                // If divisor needs parentheses (composite), wrap it
                if (divisor is IProductOperation || divisor is DivisionUnit)
                {
                    divisorStr = $"({divisorStr})";
                }

                return $"{divisorStr}^-1";
            }

            return $"{dividendStr}/{divisorStr}";
        }

        // For PowerUnit, format base recursively
        if (unit is PowerUnit powerUnit)
        {
            var baseUnit = GetPowerBase(powerUnit);
            var exponent = GetPowerExponent(powerUnit);

            var baseStr = FormatUnit(baseUnit);

            // If base needs parentheses (composite), wrap it
            if (baseUnit is IProductOperation || baseUnit is DivisionUnit)
            {
                baseStr = $"({baseStr})";
            }

            return $"{baseStr}^{exponent}";
        }

        // For other types (BasicUnit, DerivedUnit, PrefixedUnit, AffineUnit, ScaleUnit, NullUnit),
        // use Symbol which is already unambiguous
        return unit.Symbol;
    }

    private static Unit GetDividend(DivisionUnit divisionUnit)
    {
        var field = typeof(DivisionUnit).GetField("_dividend", BindingFlags.NonPublic | BindingFlags.Instance);
        return (Unit)field.GetValue(divisionUnit);
    }

    private static Unit GetDivisor(DivisionUnit divisionUnit)
    {
        var field = typeof(DivisionUnit).GetField("_divisor", BindingFlags.NonPublic | BindingFlags.Instance);
        return (Unit)field.GetValue(divisionUnit);
    }

    private static Unit GetPowerBase(PowerUnit powerUnit)
    {
        var field = typeof(PowerUnit).GetField("_base", BindingFlags.NonPublic | BindingFlags.Instance);
        return (Unit)field.GetValue(powerUnit);
    }

    private static int GetPowerExponent(PowerUnit powerUnit)
    {
        var field = typeof(PowerUnit).GetField("_exponent", BindingFlags.NonPublic | BindingFlags.Instance);
        return (int)field.GetValue(powerUnit);
    }
}
