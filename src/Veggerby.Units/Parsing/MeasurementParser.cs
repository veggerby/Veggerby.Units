using System;
using System.Globalization;
using Veggerby.Units.Calculations;

namespace Veggerby.Units.Parsing;

/// <summary>
/// Parses string representations of measurements into <see cref="Measurement{T}"/> objects.
/// Supports numeric values with units (e.g., "5.0 m", "10 kg/s", "300 K").
/// </summary>
public static class MeasurementParser
{
    /// <summary>
    /// Parses a measurement expression string into a <see cref="Measurement{T}"/> of type double.
    /// </summary>
    /// <param name="expression">The measurement expression to parse (e.g., "5.0 m", "10 kg/s").</param>
    /// <returns>The parsed measurement.</returns>
    /// <exception cref="ParseException">Thrown when the expression cannot be parsed.</exception>
    public static Measurement<double> Parse(string expression)
    {
        return Parse<double>(expression);
    }

    /// <summary>
    /// Parses a measurement expression string into a <see cref="Measurement{T}"/> of the specified type.
    /// </summary>
    /// <typeparam name="T">The numeric type for the measurement value.</typeparam>
    /// <param name="expression">The measurement expression to parse.</param>
    /// <returns>The parsed measurement.</returns>
    /// <exception cref="ParseException">Thrown when the expression cannot be parsed.</exception>
    public static Measurement<T> Parse<T>(string expression) where T : IComparable
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ParseException("Measurement expression cannot be null or empty.");
        }

        var lexer = new Lexer(expression);
        var tokens = lexer.Tokenize();

        if (tokens.Count < 2)
        {
            throw new ParseException("Measurement expression must contain both a numeric value and a unit.");
        }

        // First token should be a number
        if (tokens[0].Type != TokenType.Number)
        {
            throw new ParseException($"Expected numeric value at position {tokens[0].Position}, but found '{tokens[0].Value}'", tokens[0].Position);
        }

        var numericValue = tokens[0].Value;
        T value;

        try
        {
            value = ConvertValue<T>(numericValue);
        }
        catch (Exception ex)
        {
            throw new ParseException($"Failed to convert '{numericValue}' to type {typeof(T).Name}: {ex.Message}", tokens[0].Position);
        }

        // Parse the remaining tokens as a unit
        var unitTokens = tokens.GetRange(1, tokens.Count - 1);
        var parser = new UnitParser.Parser(unitTokens);
        var unit = parser.ParseUnit();

        // Get the appropriate calculator for the type
        var calculator = GetCalculator<T>();

        return new Measurement<T>(value, unit, calculator);
    }

    /// <summary>
    /// Tries to parse a measurement expression string into a <see cref="Measurement{T}"/> of type double.
    /// </summary>
    /// <param name="expression">The measurement expression to parse.</param>
    /// <param name="measurement">The parsed measurement if successful, otherwise null.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParse(string expression, out Measurement<double> measurement)
    {
        return TryParse<double>(expression, out measurement);
    }

    /// <summary>
    /// Tries to parse a measurement expression string into a <see cref="Measurement{T}"/> of the specified type.
    /// </summary>
    /// <typeparam name="T">The numeric type for the measurement value.</typeparam>
    /// <param name="expression">The measurement expression to parse.</param>
    /// <param name="measurement">The parsed measurement if successful, otherwise null.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParse<T>(string expression, out Measurement<T> measurement) where T : IComparable
    {
        measurement = null;

        try
        {
            measurement = Parse<T>(expression);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static T ConvertValue<T>(string value)
    {
        var type = typeof(T);

        if (type == typeof(double))
        {
            return (T)(object)double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        if (type == typeof(int))
        {
            // Try to parse as int, but handle decimal values by truncating
            if (value.Contains('.') || value.Contains('e') || value.Contains('E'))
            {
                var doubleValue = double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
                return (T)(object)(int)doubleValue;
            }

            return (T)(object)int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        if (type == typeof(decimal))
        {
            return (T)(object)decimal.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        if (type == typeof(float))
        {
            return (T)(object)float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        if (type == typeof(long))
        {
            if (value.Contains('.') || value.Contains('e') || value.Contains('E'))
            {
                var doubleValue = double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
                return (T)(object)(long)doubleValue;
            }

            return (T)(object)long.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        throw new NotSupportedException($"Type {type.Name} is not supported for measurement parsing.");
    }

    private static Calculator<T> GetCalculator<T>()
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

        throw new NotSupportedException($"Type {type.Name} is not supported for measurement parsing.");
    }
}
