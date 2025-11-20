using System.Collections.Generic;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Provides a detailed explanation of why two units have incompatible dimensions.
/// </summary>
/// <param name="Left">The left unit in the comparison.</param>
/// <param name="Right">The right unit in the comparison.</param>
/// <param name="LeftDimension">The dimension of the left unit.</param>
/// <param name="RightDimension">The dimension of the right unit.</param>
/// <param name="Explanation">Human-readable explanation of the mismatch.</param>
/// <param name="Suggestions">Collection of suggestions for correcting the mismatch.</param>
public record DimensionMismatchExplanation(
    Unit Left,
    Unit Right,
    Dimension LeftDimension,
    Dimension RightDimension,
    string Explanation,
    IEnumerable<string> Suggestions);
