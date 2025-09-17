namespace Veggerby.Units.Reduction;

/// <summary>
/// Internal feature flags controlling experimental reduction paths. Default values favour the
/// stable LINQ based implementations. Only tests / benchmarks should toggle these.
/// </summary>
internal static class ReductionSettings
{
    /// <summary>
    /// When true <see cref="OperationUtility.ReduceMultiplication"/> and <see cref="OperationUtility.ReduceDivision"/>
    /// will use the pooled <see cref="ExponentMap{T}"/> implementation instead of LINQ GroupBy.
    /// </summary>
    public static bool UseExponentMapForReduction { get; set; } = false;

    /// <summary>
    /// When true division reduction uses a single-pass exponent accumulation (map-based) regardless of the
    /// generic ExponentMap reduction flag. Experimental; default off.
    /// </summary>
    public static bool DivisionSinglePass { get; set; } = false;

    /// <summary>
    /// Enables allocation of and caching of canonical factor vectors on composite units/dimensions for
    /// faster equality and downstream reductions. Experimental; default off.
    /// </summary>
    public static bool UseFactorVector { get; set; } = false;

    /// <summary>
    /// When enabled, power expansion over products/divisions is deferred (lazy) unless exponent is negative
    /// or further reduction requires distribution. Experimental; default off.
    /// </summary>
    public static bool LazyPowerExpansion { get; set; } = false;

    /// <summary>
    /// When enabled (default) algebraic operand equality (product/division/power) performs canonical factor
    /// normalisation first (base -> exponent multiset) and compares multisets ignoring ordering and structural
    /// representation differences (e.g. (A*B)^n vs A^n*B^n). Disabling reverts to legacy structural paths.
    /// </summary>
    public static bool EqualityNormalizationEnabled { get; set; } = true;
}