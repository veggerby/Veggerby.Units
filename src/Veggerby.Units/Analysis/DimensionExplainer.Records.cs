using System.Collections.Generic;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents a single step in the derivation of a unit's dimension.
/// </summary>
/// <param name="Operation">Description of the operation performed (e.g., "Expand Newton: kg·m/s²").</param>
/// <param name="Intermediate">The intermediate dimension after this step.</param>
/// <param name="Explanation">Human-readable reasoning for this step.</param>
public record DerivationStep(
    string Operation,
    Dimension Intermediate,
    string Explanation);

/// <summary>
/// Represents the complete derivation path from a unit to its base dimensions.
/// </summary>
/// <param name="OriginalUnit">The unit being analyzed.</param>
/// <param name="Steps">Ordered list of derivation steps.</param>
/// <param name="FinalDimension">The final dimension after all derivations.</param>
public record DerivationSteps(
    Unit OriginalUnit,
    List<DerivationStep> Steps,
    Dimension FinalDimension);

/// <summary>
/// Represents a single step in a conversion path between units.
/// </summary>
/// <param name="Description">Human-readable description of the conversion step.</param>
/// <param name="IntermediateUnit">The intermediate unit after this conversion step.</param>
public record ConversionStep(
    string Description,
    Unit IntermediateUnit);

/// <summary>
/// Represents the complete path for converting between two units.
/// </summary>
/// <param name="IsPossible">Indicates whether conversion is dimensionally possible.</param>
/// <param name="Steps">Ordered list of conversion steps if possible.</param>
/// <param name="ScaleFactor">The total scale factor for the conversion (1.0 if dimensions don't match).</param>
/// <param name="Explanation">Human-readable explanation of why conversion is or isn't possible.</param>
public record ConversionPath(
    bool IsPossible,
    List<ConversionStep> Steps,
    double ScaleFactor,
    string Explanation);
