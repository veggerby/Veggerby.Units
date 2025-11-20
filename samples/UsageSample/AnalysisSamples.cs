using System;

using Veggerby.Units;
using Veggerby.Units.Analysis;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Samples;

/// <summary>
/// Demonstrates the use of the Dimension Analysis and Validation Framework.
/// </summary>
public static class AnalysisSamples
{
    /// <summary>
    /// Runs all dimension analysis examples.
    /// </summary>
    public static void RunExamples()
    {
        Console.WriteLine("=== Veggerby.Units Dimension Analysis Examples ===\n");

        Example1_UnderstandComplexUnit();
        Example2_ValidateExpectedDimension();
        Example3_ExplainMismatch();
        Example4_FindDimensionalErrors();
        Example5_BatchValidation();
    }

    /// <summary>
    /// Example 1: Understand a complex unit by decomposing it to base dimensions.
    /// </summary>
    private static void Example1_UnderstandComplexUnit()
    {
        Console.WriteLine("Example 1: Understand a complex unit");
        Console.WriteLine("-------------------------------------");

        var unit = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3);
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);

        Console.WriteLine($"Unit: {unit.Symbol}");
        Console.WriteLine($"Human-readable: {breakdown.HumanReadable}");
        Console.WriteLine($"Symbolic form: {breakdown.SymbolicForm}");
        Console.WriteLine($"Dimension exponents:");
        foreach (var kvp in breakdown.Exponents)
        {
            Console.WriteLine($"  {kvp.Key.Symbol} (/{kvp.Key.Name}/): {kvp.Value}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Example 2: Validate that a unit matches an expected dimension.
    /// </summary>
    private static void Example2_ValidateExpectedDimension()
    {
        Console.WriteLine("Example 2: Validate expected dimension");
        Console.WriteLine("---------------------------------------");

        var pressure = new DoubleMeasurement(101325, Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)));
        var expectedDimension = Dimension.Mass / (Dimension.Length * (Dimension.Time ^ 2));

        var result = DimensionAnalyzer.Validate(pressure.Unit, expectedDimension);
        Console.WriteLine($"Validating pressure unit: {pressure.Unit.Symbol}");
        Console.WriteLine($"Is valid: {result.IsValid}");
        Console.WriteLine($"Message: {result.Message}");
        Console.WriteLine();
    }

    /// <summary>
    /// Example 3: Explain why units have incompatible dimensions.
    /// </summary>
    private static void Example3_ExplainMismatch()
    {
        Console.WriteLine("Example 3: Explain dimension mismatch");
        Console.WriteLine("--------------------------------------");

        var distance = Unit.SI.m;
        var velocity = Unit.SI.m / Unit.SI.s;

        var explanation = DimensionAnalyzer.ExplainMismatch(distance, velocity);
        Console.WriteLine($"Comparing: {distance.Symbol} vs {velocity.Symbol}");
        Console.WriteLine($"Explanation: {explanation.Explanation}");
        Console.WriteLine("Suggestions:");
        foreach (var suggestion in explanation.Suggestions)
        {
            Console.WriteLine($"  - {suggestion}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Example 4: Find dimensional errors with correction suggestions.
    /// </summary>
    private static void Example4_FindDimensionalErrors()
    {
        Console.WriteLine("Example 4: Find and suggest corrections");
        Console.WriteLine("----------------------------------------");

        var actual = Unit.SI.m;
        var expected = Dimension.Length / Dimension.Time; // Should be velocity

        var suggestions = DimensionAnalyzer.SuggestCorrections(actual, expected);
        Console.WriteLine($"Actual unit: {actual.Symbol} (dimension: {DimensionAnalyzer.DecomposeToBase(actual).SymbolicForm})");
        Console.WriteLine($"Expected dimension: L·T⁻¹ (velocity)");
        Console.WriteLine("Correction suggestions:");
        foreach (var suggestion in suggestions)
        {
            Console.WriteLine($"  - {suggestion.Description}");
            Console.WriteLine($"    Transformation: {suggestion.Transformation}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Example 5: Batch validation of measurement collections.
    /// </summary>
    private static void Example5_BatchValidation()
    {
        Console.WriteLine("Example 5: Batch validation");
        Console.WriteLine("----------------------------");

        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "Temperature", Dimension.ThermodynamicTemperature },
            { "Pressure", Dimension.Mass / (Dimension.Length * (Dimension.Time ^ 2)) },
            { "Volume", Dimension.Length ^ 3 }
        };

        var validator = new MeasurementValidator(schema);

        var measurements = new System.Collections.Generic.Dictionary<string, Measurement<double>>
        {
            { "Temperature", new DoubleMeasurement(273.15, Unit.SI.K) },
            { "Pressure", new DoubleMeasurement(101325, Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2))) },
            { "Volume", new DoubleMeasurement(1.0, Unit.SI.m ^ 3) }
        };

        var report = validator.ValidateCollection(measurements);
        Console.WriteLine($"Total items: {report.TotalItems}");
        Console.WriteLine($"Valid items: {report.ValidItems}");
        Console.WriteLine($"Invalid items: {report.InvalidItems}");

        if (report.Failures.Count > 0)
        {
            Console.WriteLine("Failures:");
            foreach (var failure in report.Failures)
            {
                Console.WriteLine($"  - {failure.ItemKey}: {failure.Reason}");
            }
        }
        else
        {
            Console.WriteLine("All measurements validated successfully!");
        }
        Console.WriteLine();
    }
}
