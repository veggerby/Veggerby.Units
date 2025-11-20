using System.Collections.Generic;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents a single validation failure in a batch validation operation.
/// </summary>
/// <param name="ItemKey">Identifier for the item that failed validation.</param>
/// <param name="ActualUnit">The actual unit found.</param>
/// <param name="ExpectedDimension">The dimension that was expected.</param>
/// <param name="Reason">Detailed reason for the validation failure.</param>
public record ValidationFailure(
    string ItemKey,
    Unit ActualUnit,
    Dimension ExpectedDimension,
    string Reason);

/// <summary>
/// Represents the complete result of a batch validation operation.
/// </summary>
/// <param name="TotalItems">Total number of items validated.</param>
/// <param name="ValidItems">Number of items that passed validation.</param>
/// <param name="InvalidItems">Number of items that failed validation.</param>
/// <param name="Failures">Detailed list of validation failures.</param>
public record ValidationReport(
    int TotalItems,
    int ValidItems,
    int InvalidItems,
    List<ValidationFailure> Failures);
