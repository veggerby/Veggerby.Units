using BenchmarkDotNet.Attributes;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
public class ComplexDimensionReductionBenchmarks
{
    private readonly Dimension _l = Dimension.Length; // L
    private readonly Dimension _t = Dimension.Time;   // T
    private readonly Dimension _m = Dimension.Mass;   // M

    [Benchmark]
    public Dimension ChainedCancellationAndPowers()
    {
        // (((L*T)/T) * (M / (L/M))) ^ 2 => ((L) * (M / (L/M)))^2 => (L * (M * M / L))^2 => (M^2)^2 => M^4
        return (((_l * _t) / _t) * (_m / (_l / _m))) ^ 2;
    }

    [Benchmark]
    public Dimension DeeplyNestedAssociativity()
    {
        // (((L / (T / M)) / (M / (L / T)))) => normalize
        return ((_l / (_t / _m)) / (_m / (_l / _t)));
    }
}