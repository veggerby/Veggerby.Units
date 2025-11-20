using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Provides comprehensive dimension analysis and validation capabilities for units and measurements.
/// All methods are pure functions (thread-safe) and leverage existing dimension infrastructure.
/// </summary>
public static class DimensionAnalyzer
{
    /// <summary>
    /// Decomposes a unit into its base dimensional components with exponents.
    /// </summary>
    /// <param name="unit">The unit to decompose.</param>
    /// <returns>A breakdown of the unit's dimensional structure.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="unit"/> is null.</exception>
    public static DimensionBreakdown DecomposeToBase(Unit unit)
    {
        if (unit is null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        var dimension = unit.Dimension;
        var exponents = ExtractBaseDimensionExponents(dimension);
        var humanReadable = GenerateHumanReadable(exponents);
        var symbolicForm = GenerateSymbolicForm(exponents);

        return new DimensionBreakdown(dimension, exponents, humanReadable, symbolicForm);
    }

    /// <summary>
    /// Checks if two units are dimensionally equivalent (have the same dimension).
    /// </summary>
    /// <param name="left">The first unit to compare.</param>
    /// <param name="right">The second unit to compare.</param>
    /// <returns>True if both units have the same dimension; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public static bool AreDimensionallyEquivalent(Unit left, Unit right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        return left.Dimension == right.Dimension;
    }

    /// <summary>
    /// Validates that a unit matches an expected dimension.
    /// </summary>
    /// <param name="unit">The unit to validate.</param>
    /// <param name="expectedDimension">The dimension expected for the unit.</param>
    /// <returns>A validation result indicating success or failure with details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public static ValidationResult Validate(Unit unit, Dimension expectedDimension)
    {
        if (unit is null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        if (expectedDimension is null)
        {
            throw new ArgumentNullException(nameof(expectedDimension));
        }

        var actualDimension = unit.Dimension;
        var isValid = actualDimension == expectedDimension;

        var message = isValid
            ? $"Valid: unit has dimension {GetDimensionDescription(actualDimension)} matching expected dimension"
            : $"Invalid: unit has dimension {GetDimensionDescription(actualDimension)} but expected {GetDimensionDescription(expectedDimension)}";

        return new ValidationResult(isValid, message, actualDimension, expectedDimension);
    }

    /// <summary>
    /// Explains why two units have incompatible dimensions.
    /// </summary>
    /// <param name="left">The first unit.</param>
    /// <param name="right">The second unit.</param>
    /// <returns>A detailed explanation of the dimension mismatch.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public static DimensionMismatchExplanation ExplainMismatch(Unit left, Unit right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        var leftDimension = left.Dimension;
        var rightDimension = right.Dimension;

        var leftBreakdown = ExtractBaseDimensionExponents(leftDimension);
        var rightBreakdown = ExtractBaseDimensionExponents(rightDimension);

        var explanation = GenerateMismatchExplanation(leftBreakdown, rightBreakdown, left, right);
        var suggestions = GenerateCorrectionSuggestions(leftBreakdown, rightBreakdown);

        return new DimensionMismatchExplanation(left, right, leftDimension, rightDimension, explanation, suggestions);
    }

    /// <summary>
    /// Suggests corrections for common dimensional errors.
    /// </summary>
    /// <param name="actual">The actual unit.</param>
    /// <param name="expected">The expected dimension.</param>
    /// <returns>A collection of correction suggestions.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public static IEnumerable<CorrectionSuggestion> SuggestCorrections(Unit actual, Dimension expected)
    {
        if (actual is null)
        {
            throw new ArgumentNullException(nameof(actual));
        }

        if (expected is null)
        {
            throw new ArgumentNullException(nameof(expected));
        }

        var actualBreakdown = ExtractBaseDimensionExponents(actual.Dimension);
        var expectedBreakdown = ExtractBaseDimensionExponents(expected);

        return GenerateCorrections(actualBreakdown, expectedBreakdown, actual);
    }

    /// <summary>
    /// Analyzes dimensional homogeneity of an expression (checks if all units share the same dimension).
    /// </summary>
    /// <param name="terms">The units to analyze.</param>
    /// <returns>A report on the homogeneity of the units.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="terms"/> is null.</exception>
    public static HomogeneityReport AnalyzeHomogeneity(params Unit[] terms)
    {
        if (terms is null)
        {
            throw new ArgumentNullException(nameof(terms));
        }

        if (terms.Length == 0)
        {
            return new HomogeneityReport(true, Array.Empty<Unit>(), "No units to analyze");
        }

        var firstDimension = terms[0].Dimension;
        var isHomogeneous = terms.All(u => u.Dimension == firstDimension);

        var summary = isHomogeneous
            ? $"All {terms.Length} unit(s) share dimension {GetDimensionDescription(firstDimension)}"
            : $"Units have mixed dimensions: {string.Join(", ", terms.Select(u => $"{u.Symbol} ({GetDimensionDescription(u.Dimension)})"))}";

        return new HomogeneityReport(isHomogeneous, terms, summary);
    }

    /// <summary>
    /// Finds all registered units matching a specific dimension.
    /// Note: This method currently returns an empty collection as the unit registry is not exposed.
    /// Future enhancement: Expose unit registry for comprehensive unit discovery.
    /// </summary>
    /// <param name="dimension">The dimension to search for.</param>
    /// <returns>Collection of units with the specified dimension.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="dimension"/> is null.</exception>
    public static IEnumerable<Unit> FindUnitsWithDimension(Dimension dimension)
    {
        if (dimension is null)
        {
            throw new ArgumentNullException(nameof(dimension));
        }

        // Note: Unit registry is not currently exposed in the library.
        // This would require access to UnitSystem registrations or a global unit catalog.
        // For now, return empty collection as a placeholder.
        return Enumerable.Empty<Unit>();
    }

    // Private helper methods

    private static Dictionary<BasicDimension, int> ExtractBaseDimensionExponents(Dimension dimension)
    {
        var exponents = new Dictionary<BasicDimension, int>();

        if (dimension is null || dimension is NullDimension)
        {
            return exponents;
        }

        AccumulateDimensionExponents(dimension, exponents, 1);

        return exponents;
    }

    private static void AccumulateDimensionExponents(Dimension dimension, Dictionary<BasicDimension, int> exponents, int multiplier)
    {
        // Explicit type checks to avoid implicit string conversion
        if (dimension is BasicDimension basic)
        {
            exponents[basic] = exponents.TryGetValue(basic, out var current) ? current + multiplier : multiplier;
        }
        else if (dimension is ProductDimension product && product is IProductOperation productOp)
        {
            foreach (var operand in productOp.Operands)
            {
                if (operand is Dimension dim)
                {
                    AccumulateDimensionExponents(dim, exponents, multiplier);
                }
            }
        }
        else if (dimension is DivisionDimension division && division is IDivisionOperation divOp)
        {
            if (divOp.Dividend is Dimension dividend)
            {
                AccumulateDimensionExponents(dividend, exponents, multiplier);
            }

            if (divOp.Divisor is Dimension divisor)
            {
                AccumulateDimensionExponents(divisor, exponents, -multiplier);
            }
        }
        else if (dimension is PowerDimension power && power is IPowerOperation powerOp)
        {
            if (powerOp.Base is Dimension baseDim)
            {
                AccumulateDimensionExponents(baseDim, exponents, multiplier * powerOp.Exponent);
            }
        }
        // NullDimension and other cases: no exponents to add
    }

    private static string GenerateHumanReadable(Dictionary<BasicDimension, int> exponents)
    {
        if (exponents.Count == 0)
        {
            return "dimensionless";
        }

        var numerator = new List<string>();
        var denominator = new List<string>();

        foreach (var kvp in exponents.OrderBy(kv => kv.Key.Symbol))
        {
            var name = kvp.Key.Name;
            var exp = kvp.Value;

            if (exp > 0)
            {
                numerator.Add(exp == 1 ? name : $"{name}^{exp}");
            }
            else if (exp < 0)
            {
                denominator.Add(Math.Abs(exp) == 1 ? name : $"{name}^{Math.Abs(exp)}");
            }
        }

        if (denominator.Count == 0)
        {
            return string.Join(" × ", numerator);
        }

        if (numerator.Count == 0)
        {
            return "1 / " + (denominator.Count == 1 ? denominator[0] : $"({string.Join(" × ", denominator)})");
        }

        return $"{string.Join(" × ", numerator)} / {(denominator.Count == 1 ? denominator[0] : $"({string.Join(" × ", denominator)})")}";
    }

    private static string GenerateSymbolicForm(Dictionary<BasicDimension, int> exponents)
    {
        if (exponents.Count == 0)
        {
            return "1";
        }

        var parts = new List<string>();

        foreach (var kvp in exponents.OrderBy(kv => kv.Key.Symbol))
        {
            var symbol = kvp.Key.Symbol;
            var exp = kvp.Value;

            if (exp == 1)
            {
                parts.Add(symbol);
            }
            else if (exp != 0)
            {
                parts.Add($"{symbol}{FormatExponent(exp)}");
            }
        }

        return parts.Count > 0 ? string.Join("·", parts) : "1";
    }

    private static string FormatExponent(int exponent)
    {
        if (exponent == 1)
        {
            return string.Empty;
        }

        var superscript = exponent.ToString()
            .Replace("0", "⁰")
            .Replace("1", "¹")
            .Replace("2", "²")
            .Replace("3", "³")
            .Replace("4", "⁴")
            .Replace("5", "⁵")
            .Replace("6", "⁶")
            .Replace("7", "⁷")
            .Replace("8", "⁸")
            .Replace("9", "⁹")
            .Replace("-", "⁻");

        return superscript;
    }

    private static string GetDimensionDescription(Dimension dimension)
    {
        var exponents = ExtractBaseDimensionExponents(dimension);
        return GenerateSymbolicForm(exponents);
    }

    private static string GenerateMismatchExplanation(
        Dictionary<BasicDimension, int> left,
        Dictionary<BasicDimension, int> right,
        Unit leftUnit,
        Unit rightUnit)
    {
        var leftSymbolic = GenerateSymbolicForm(left);
        var rightSymbolic = GenerateSymbolicForm(right);
        var leftReadable = GenerateHumanReadable(left);
        var rightReadable = GenerateHumanReadable(right);

        var sb = new StringBuilder();
        sb.Append($"Cannot add units with incompatible dimensions. ");
        sb.Append($"Left unit '{leftUnit.Symbol}' has dimension {leftSymbolic} ({leftReadable}), ");
        sb.Append($"right unit '{rightUnit.Symbol}' has dimension {rightSymbolic} ({rightReadable}). ");

        var differences = FindDimensionDifferences(left, right);
        if (differences.Count > 0)
        {
            sb.Append("Dimensions differ by: ");
            sb.Append(string.Join(", ", differences.Select(d => $"{d.Key.Symbol}{FormatExponent(d.Value)}")));
            sb.Append(".");
        }

        return sb.ToString();
    }

    private static Dictionary<BasicDimension, int> FindDimensionDifferences(
        Dictionary<BasicDimension, int> left,
        Dictionary<BasicDimension, int> right)
    {
        var differences = new Dictionary<BasicDimension, int>();
        var allDimensions = left.Keys.Union(right.Keys);

        foreach (var dim in allDimensions)
        {
            var leftExp = left.TryGetValue(dim, out var le) ? le : 0;
            var rightExp = right.TryGetValue(dim, out var re) ? re : 0;
            var diff = rightExp - leftExp;

            if (diff != 0)
            {
                differences[dim] = diff;
            }
        }

        return differences;
    }

    private static IEnumerable<string> GenerateCorrectionSuggestions(
        Dictionary<BasicDimension, int> actual,
        Dictionary<BasicDimension, int> expected)
    {
        var suggestions = new List<string>();
        var differences = FindDimensionDifferences(actual, expected);

        if (differences.Count == 0)
        {
            return suggestions;
        }

        foreach (var diff in differences)
        {
            if (diff.Value > 0)
            {
                suggestions.Add($"Multiply by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $"^{diff.Value}" : string.Empty)}");
            }
            else if (diff.Value < 0)
            {
                suggestions.Add($"Divide by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $"^{Math.Abs(diff.Value)}" : string.Empty)}");
            }
        }

        return suggestions;
    }

    private static IEnumerable<CorrectionSuggestion> GenerateCorrections(
        Dictionary<BasicDimension, int> actual,
        Dictionary<BasicDimension, int> expected,
        Unit actualUnit)
    {
        var suggestions = new List<CorrectionSuggestion>();
        var differences = FindDimensionDifferences(actual, expected);

        if (differences.Count == 0)
        {
            return suggestions;
        }

        foreach (var diff in differences)
        {
            string description;
            string transformation;

            if (diff.Value > 0)
            {
                description = $"Multiply the unit by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $" raised to power {diff.Value}" : string.Empty)}";
                transformation = $"multiply by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $"^{diff.Value}" : string.Empty)}";
            }
            else
            {
                description = $"Divide the unit by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $" raised to power {Math.Abs(diff.Value)}" : string.Empty)}";
                transformation = $"divide by {diff.Key.Name}{(Math.Abs(diff.Value) > 1 ? $"^{Math.Abs(diff.Value)}" : string.Empty)}";
            }

            // Note: We cannot construct the corrected unit without knowing which BasicUnit to use
            // (e.g., meter vs foot for length), so we pass the original unit for now
            suggestions.Add(new CorrectionSuggestion(description, actualUnit, transformation));
        }

        return suggestions;
    }
}
