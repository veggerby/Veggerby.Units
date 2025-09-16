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
}