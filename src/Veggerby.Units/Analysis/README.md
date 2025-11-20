# Dimension Analysis and Validation Framework

This directory contains the comprehensive dimension analysis and validation framework for Veggerby.Units. The framework provides deep insights into unit expressions, detects semantic errors early, and helps users understand complex dimensional relationships.

## Overview

The Analysis namespace provides tools for:
- Decomposing units into base dimensional components
- Validating units against expected dimensions
- Explaining dimensional mismatches
- Suggesting corrections for dimensional errors
- Batch validation of measurement collections
- Step-by-step derivation tracing

## Core Components

### DimensionAnalyzer

The main static class providing dimension analysis capabilities:

```csharp
// Decompose unit to base dimensions
var breakdown = DimensionAnalyzer.DecomposeToBase(unit);
Console.WriteLine(breakdown.SymbolicForm);     // e.g., "M·L·T⁻²"
Console.WriteLine(breakdown.HumanReadable);     // e.g., "mass × length / time²"

// Check dimensional equivalence
bool equivalent = DimensionAnalyzer.AreDimensionallyEquivalent(Unit.SI.m, Unit.Imperial.ft);
// Returns: true (both are length)

// Validate against expected dimension
var result = DimensionAnalyzer.Validate(unit, expectedDimension);
Console.WriteLine(result.Message);

// Explain dimension mismatches
var explanation = DimensionAnalyzer.ExplainMismatch(distance, velocity);
Console.WriteLine(explanation.Explanation);
foreach (var suggestion in explanation.Suggestions)
{
    Console.WriteLine($"  - {suggestion}");
}

// Suggest corrections
var suggestions = DimensionAnalyzer.SuggestCorrections(actual, expected);

// Analyze homogeneity
var report = DimensionAnalyzer.AnalyzeHomogeneity(unit1, unit2, unit3);
Console.WriteLine(report.Summary);
```

### DimensionExplainer

Provides step-by-step explanations of dimensional derivations and conversions:

```csharp
// Explain derivation
var derivation = DimensionExplainer.ExplainDerivation(unit);
foreach (var step in derivation.Steps)
{
    Console.WriteLine($"{step.Operation}: {step.Explanation}");
}

// Explain conversion possibility
var path = DimensionExplainer.ExplainConversion(fromUnit, toUnit);
Console.WriteLine($"Conversion possible: {path.IsPossible}");
Console.WriteLine(path.Explanation);
```

### MeasurementValidator

Batch validation for collections of measurements:

```csharp
// Define schema
var schema = new Dictionary<string, Dimension>
{
    { "Temperature", Dimension.ThermodynamicTemperature },
    { "Pressure", Dimension.Mass / (Dimension.Length * (Dimension.Time ^ 2)) },
    { "Volume", Dimension.Length ^ 3 }
};

var validator = new MeasurementValidator(schema);

// Validate collection
var report = validator.ValidateCollection(measurements);
Console.WriteLine($"Valid: {report.ValidItems}/{report.TotalItems}");

foreach (var failure in report.Failures)
{
    Console.WriteLine($"{failure.ItemKey}: {failure.Reason}");
}
```

## Records

### DimensionBreakdown
Represents the decomposition of a unit into base dimensions:
- `Dimension` - The physical dimension
- `Exponents` - Dictionary mapping basic dimensions to exponents
- `HumanReadable` - Human-readable form (e.g., "mass × length / time²")
- `SymbolicForm` - Symbolic mathematical form (e.g., "M·L·T⁻²")

### ValidationResult
Result of validating a unit:
- `IsValid` - Whether validation passed
- `Message` - Descriptive message
- `ActualDimension` - Actual dimension found
- `ExpectedDimension` - Expected dimension

### DimensionMismatchExplanation
Detailed explanation of dimension mismatches:
- `Left`, `Right` - The units being compared
- `LeftDimension`, `RightDimension` - Their dimensions
- `Explanation` - Human-readable explanation
- `Suggestions` - Collection of correction suggestions

### ValidationReport
Result of batch validation:
- `TotalItems` - Total items validated
- `ValidItems` - Number that passed
- `InvalidItems` - Number that failed
- `Failures` - List of detailed failures

## Usage Examples

See `samples/UsageSample/AnalysisSamples.cs` for comprehensive examples including:
1. Understanding complex units
2. Validating expected dimensions
3. Explaining dimension mismatches
4. Finding and correcting dimensional errors
5. Batch validation of measurement collections

## Design Principles

- **Pure functions**: All analysis methods are thread-safe and side-effect free
- **Separation of concerns**: Analysis logic is separate from core unit algebra
- **Leverages existing infrastructure**: Uses existing `Dimension` and `Unit` classes
- **Minimal changes**: No modifications to core library code
- **Comprehensive**: Covers decomposition, validation, explanation, and correction

## Implementation Notes

- Handles implicit string conversion operator on `Dimension` class correctly
- Uses explicit type checks instead of switch expressions to avoid conversion
- Provides placeholder for `FindUnitsWithDimension()` (requires unit registry)
- All public APIs have comprehensive XML documentation
- Follows repository coding standards (.editorconfig compliant)

## Testing

The framework includes 54 comprehensive tests covering:
- Happy paths for all functionality
- Edge cases and boundary conditions
- Null argument handling
- Dimension mismatch scenarios
- Batch validation scenarios

All tests pass, and no existing tests were broken (660 total tests passing).

## Future Enhancements

Potential future improvements (out of current scope):
- Unit registry for complete `FindUnitsWithDimension()` implementation
- Integration with parser (Issue #21) for expression analysis
- Educational tutor with practice problem generation
- Support for custom dimension types
- Localization of human-readable messages
