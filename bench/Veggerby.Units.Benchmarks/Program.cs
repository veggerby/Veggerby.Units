using BenchmarkDotNet.Running;

using Veggerby.Units.Benchmarks;

// BenchmarkDotNet entrypoint. When no arguments are supplied we run a focused quick set; can be expanded later.
var switcher = new BenchmarkSwitcher(new[]
{
    typeof(UnitReductionBenchmarks),
    typeof(DimensionReductionBenchmarks),
    typeof(ComplexDimensionReductionBenchmarks),
});

return switcher.Run(args).Any(s => s.HasCriticalValidationErrors) ? 1 : 0;