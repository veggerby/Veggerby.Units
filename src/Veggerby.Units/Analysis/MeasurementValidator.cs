using System;
using System.Collections.Generic;
using System.Linq;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Provides batch validation capabilities for collections of measurements.
/// </summary>
public class MeasurementValidator
{
    private readonly Dictionary<string, Dimension> _schema;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementValidator"/> class.
    /// </summary>
    /// <param name="schema">Dictionary mapping item keys to expected dimensions.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="schema"/> is null.</exception>
    public MeasurementValidator(Dictionary<string, Dimension> schema)
    {
        _schema = schema ?? throw new ArgumentNullException(nameof(schema));
    }

    /// <summary>
    /// Validates a collection of measurements against the configured schema.
    /// </summary>
    /// <typeparam name="T">The numeric type of the measurements.</typeparam>
    /// <param name="measurements">Dictionary mapping keys to measurements.</param>
    /// <returns>Validation report with detailed results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="measurements"/> is null.</exception>
    public ValidationReport ValidateCollection<T>(Dictionary<string, Measurement<T>> measurements)
        where T : IComparable
    {
        if (measurements is null)
        {
            throw new ArgumentNullException(nameof(measurements));
        }

        var failures = new List<ValidationFailure>();
        var totalItems = 0;
        var validItems = 0;

        foreach (var kvp in measurements)
        {
            totalItems++;
            var key = kvp.Key;
            var measurement = kvp.Value;

            if (!_schema.TryGetValue(key, out var expectedDimension))
            {
                failures.Add(new ValidationFailure(
                    key,
                    measurement.Unit,
                    Dimension.None,
                    $"Item '{key}' not found in validation schema"
                ));
                continue;
            }

            var actualDimension = measurement.Unit.Dimension;
            if (actualDimension != expectedDimension)
            {
                var breakdown = DimensionAnalyzer.DecomposeToBase(measurement.Unit);
                var expectedBreakdown = DimensionAnalyzer.DecomposeToBase(
                    new HelperUnit(expectedDimension));

                failures.Add(new ValidationFailure(
                    key,
                    measurement.Unit,
                    expectedDimension,
                    $"Dimension mismatch: expected {expectedBreakdown.SymbolicForm}, got {breakdown.SymbolicForm}"
                ));
            }
            else
            {
                validItems++;
            }
        }

        return new ValidationReport(
            totalItems,
            validItems,
            failures.Count,
            failures
        );
    }

    /// <summary>
    /// Validates a collection of units against the configured schema.
    /// </summary>
    /// <param name="units">Dictionary mapping keys to units.</param>
    /// <returns>Validation report with detailed results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="units"/> is null.</exception>
    public ValidationReport ValidateUnits(Dictionary<string, Unit> units)
    {
        if (units is null)
        {
            throw new ArgumentNullException(nameof(units));
        }

        var failures = new List<ValidationFailure>();
        var totalItems = 0;
        var validItems = 0;

        foreach (var kvp in units)
        {
            totalItems++;
            var key = kvp.Key;
            var unit = kvp.Value;

            if (!_schema.TryGetValue(key, out var expectedDimension))
            {
                failures.Add(new ValidationFailure(
                    key,
                    unit,
                    Dimension.None,
                    $"Item '{key}' not found in validation schema"
                ));
                continue;
            }

            var actualDimension = unit.Dimension;
            if (actualDimension != expectedDimension)
            {
                var breakdown = DimensionAnalyzer.DecomposeToBase(unit);
                var expectedBreakdown = DimensionAnalyzer.DecomposeToBase(
                    new HelperUnit(expectedDimension));

                failures.Add(new ValidationFailure(
                    key,
                    unit,
                    expectedDimension,
                    $"Dimension mismatch: expected {expectedBreakdown.SymbolicForm}, got {breakdown.SymbolicForm}"
                ));
            }
            else
            {
                validItems++;
            }
        }

        return new ValidationReport(
            totalItems,
            validItems,
            failures.Count,
            failures
        );
    }

    // Helper class for dimension analysis
    private class HelperUnit : Unit
    {
        private readonly Dimension _dimension;

        public HelperUnit(Dimension dimension)
        {
            _dimension = dimension;
        }

        public override string Symbol => _dimension.Symbol;
        public override string Name => _dimension.Name;
        public override UnitSystem System => UnitSystem.None;
        public override Dimension Dimension => _dimension;
    }
}
