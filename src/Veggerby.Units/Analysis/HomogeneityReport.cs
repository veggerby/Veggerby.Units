using System.Collections.Generic;

namespace Veggerby.Units.Analysis;

/// <summary>
/// Represents the result of analyzing dimensional homogeneity across multiple units.
/// </summary>
/// <param name="IsHomogeneous">Indicates whether all analyzed units share the same dimension.</param>
/// <param name="AnalyzedUnits">The units that were analyzed.</param>
/// <param name="Summary">Human-readable summary of the analysis.</param>
public record HomogeneityReport(
    bool IsHomogeneous,
    IEnumerable<Unit> AnalyzedUnits,
    string Summary);
