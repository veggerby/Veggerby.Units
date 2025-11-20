using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents the result of validating a unit against an expected dimension.
/// </summary>
/// <param name="IsValid">Indicates whether the unit matches the expected dimension.</param>
/// <param name="Message">Descriptive message explaining the validation result.</param>
/// <param name="ActualDimension">The actual dimension of the validated unit.</param>
/// <param name="ExpectedDimension">The dimension that was expected.</param>
public record ValidationResult(
    bool IsValid,
    string Message,
    Dimension ActualDimension,
    Dimension ExpectedDimension);
