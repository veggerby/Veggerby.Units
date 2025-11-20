using System;
using System.Collections.Generic;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Provides detailed explanations of dimensional derivations and conversions.
/// </summary>
public static class DimensionExplainer
{
    /// <summary>
    /// Generates step-by-step derivation of a unit's dimension.
    /// </summary>
    /// <param name="unit">The unit to analyze.</param>
    /// <returns>Complete derivation showing how the unit's dimension is derived.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="unit"/> is null.</exception>
    public static DerivationSteps ExplainDerivation(Unit unit)
    {
        if (unit is null)
        {
            throw new ArgumentNullException(nameof(unit));
        }

        var steps = new List<DerivationStep>();
        var finalDimension = unit.Dimension;

        // Add initial step
        steps.Add(new DerivationStep(
            $"Starting with unit: {unit.Symbol}",
            finalDimension,
            $"Unit '{unit.Symbol}' has dimension {GetDimensionSymbol(finalDimension)}"
        ));

        // If it's a derived unit, show the breakdown
        if (unit is DerivedUnit derived)
        {
            steps.Add(new DerivationStep(
                $"Derived unit definition",
                finalDimension,
                $"'{unit.Symbol}' is defined in terms of base units"
            ));
        }

        // Add final decomposition step
        var breakdown = DimensionAnalyzer.DecomposeToBase(unit);
        if (breakdown.Exponents.Count > 0)
        {
            steps.Add(new DerivationStep(
                "Final dimension breakdown",
                finalDimension,
                $"Dimension: {breakdown.SymbolicForm} = {breakdown.HumanReadable}"
            ));
        }

        return new DerivationSteps(unit, steps, finalDimension);
    }

    /// <summary>
    /// Explains how to convert between dimensions (if possible).
    /// </summary>
    /// <param name="from">The source unit.</param>
    /// <param name="to">The target unit.</param>
    /// <returns>Conversion path with explanation of possibility and scale factor.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public static ConversionPath ExplainConversion(Unit from, Unit to)
    {
        if (from is null)
        {
            throw new ArgumentNullException(nameof(from));
        }

        if (to is null)
        {
            throw new ArgumentNullException(nameof(to));
        }

        var steps = new List<ConversionStep>();
        var fromDimension = from.Dimension;
        var toDimension = to.Dimension;

        // Check if conversion is dimensionally possible
        var isPossible = fromDimension == toDimension;

        if (!isPossible)
        {
            var explanation = $"Conversion not possible: '{from.Symbol}' has dimension {GetDimensionSymbol(fromDimension)} " +
                            $"while '{to.Symbol}' has dimension {GetDimensionSymbol(toDimension)}. " +
                            "Units must have the same dimension to convert between them.";

            return new ConversionPath(false, steps, 1.0, explanation);
        }

        // Conversion is possible
        var successExplanation = $"Conversion possible: both '{from.Symbol}' and '{to.Symbol}' have dimension {GetDimensionSymbol(fromDimension)}. " +
                                "Apply appropriate scale factor.";

        steps.Add(new ConversionStep(
            $"Convert from {from.Symbol} to {to.Symbol}",
            to
        ));

        // Note: Actual scale factor calculation would require access to conversion logic
        // For now, we indicate it's possible but don't calculate the exact factor
        return new ConversionPath(true, steps, 1.0, successExplanation);
    }

    // Private helper methods

    private static string GetDimensionSymbol(Dimension dimension)
    {
        var breakdown = DimensionAnalyzer.DecomposeToBase(new NullUnit { DimensionOverride = dimension });
        return breakdown.SymbolicForm;
    }

    // Helper class for internal use
    private class NullUnit : Unit
    {
        public override string Symbol => string.Empty;
        public override string Name => string.Empty;
        public override UnitSystem System => UnitSystem.None;
        public Dimension DimensionOverride { get; init; }
        public override Dimension Dimension => DimensionOverride ?? Dimensions.Dimension.None;
    }
}
