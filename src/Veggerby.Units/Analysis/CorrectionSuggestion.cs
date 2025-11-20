namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents a suggested correction for a dimensional error.
/// </summary>
/// <param name="Description">Human-readable description of the correction.</param>
/// <param name="CorrectedUnit">The corrected unit, if applicable.</param>
/// <param name="Transformation">Description of the transformation needed (e.g., "multiply by time").</param>
public record CorrectionSuggestion(
    string Description,
    Unit CorrectedUnit,
    string Transformation);
