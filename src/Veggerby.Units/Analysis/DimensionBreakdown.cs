using System.Collections.Generic;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents the decomposition of a unit into its base dimensional components with exponents.
/// </summary>
/// <param name="Dimension">The physical dimension of the analyzed unit.</param>
/// <param name="Exponents">Dictionary mapping basic dimensions to their exponents (e.g., L^1, M^1, T^-2).</param>
/// <param name="HumanReadable">Human-readable representation (e.g., "mass × length / time²").</param>
/// <param name="SymbolicForm">Symbolic mathematical representation (e.g., "M·L·T⁻²").</param>
public record DimensionBreakdown(
    Dimension Dimension,
    Dictionary<BasicDimension, int> Exponents,
    string HumanReadable,
    string SymbolicForm);
