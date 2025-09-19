using BenchmarkDotNet.Running;

using Veggerby.Units.Benchmarks;

// BenchmarkDotNet entrypoint. When no arguments are supplied run a smoke subset (fastest reduction case).
var switcher = new BenchmarkSwitcher(
[
    typeof(UnitReductionBenchmarks),
    typeof(DimensionReductionBenchmarks),
    typeof(ComplexDimensionReductionBenchmarks),
    typeof(EqualityBenchmarks),
    typeof(DivisionCancellationBenchmarks),
    typeof(PowerDistributionBenchmarks),
]);
var actualArgs = args.Length == 0 ? ["--filter", "*MultiplyThenDivideCancellation*"] : args;
return switcher.Run(actualArgs).Any(s => s.HasCriticalValidationErrors) ? 1 : 0;