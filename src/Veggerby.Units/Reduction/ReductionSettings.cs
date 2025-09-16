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
    /// When true product equality uses a hash-bucket multiset comparison instead of the existing
    /// sort+zip path. Experimental; default off until benchmarks prove benefit.
    /// </summary>
    public static bool EqualityUsesMap { get; set; } = false;

    /// <summary>
    /// When true division reduction uses a single-pass exponent accumulation (map-based) regardless of the
    /// generic ExponentMap reduction flag. Experimental; default off.
    /// </summary>
    public static bool DivisionSinglePass { get; set; } = false;
}