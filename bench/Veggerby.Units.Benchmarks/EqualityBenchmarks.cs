using BenchmarkDotNet.Attributes;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("equality")]
public class EqualityBenchmarks
{
    private const int Ops = 500; // amplify micro equality operations per invocation for gating precision
    private Unit? _largeLeftUnit;
    private Unit? _largeRightUnit;
    private Dimension? _largeLeftDimension;
    private Dimension? _largeRightDimension;
    private Unit? _lazyPowerUnit5;
    private Unit? _eagerPowerUnit5;
    private Unit? _lazyPowerUnit10;
    private Unit? _eagerPowerUnit10;
    private Unit? _lazyPowerUnit25;
    private Unit? _eagerPowerUnit25;

    [Params(false, true)]
    public bool UseExponentMap;

    [Params(false, true)]
    public bool UseFactorVector;

    [GlobalSetup]
    public void Setup()
    {
        ReductionSettings.UseExponentMapForReduction = UseExponentMap;
        ReductionSettings.UseFactorVector = UseFactorVector;
        ReductionSettings.EqualityNormalizationEnabled = true; // ensure canonical path for benchmark
        ReductionSettings.LazyPowerExpansion = true; // exercise lazy vs eager equivalence

        var a = Unit.SI.m; var b = Unit.SI.s; var c = Unit.SI.kg;
        _largeLeftUnit = (((a * b * c) ^ 2) * (b * c / a)) / c;
        _largeRightUnit = ((b * c / a) * ((a * b * c) ^ 2)) / c;

        var L = Dimension.Length; var T = Dimension.Time; var M = Dimension.Mass;
        _largeLeftDimension = (((L * T * M) ^ 3) * (T * M / L)) / M;
        _largeRightDimension = ((T * M / L) * ((L * T * M) ^ 3)) / M;

        // Build products with N factors then store lazy vs eager exponentiations for N=5,10,25
        _lazyPowerUnit5 = BuildLazyVsEager(5, out _eagerPowerUnit5);
        _lazyPowerUnit10 = BuildLazyVsEager(10, out _eagerPowerUnit10);
        _lazyPowerUnit25 = BuildLazyVsEager(25, out _eagerPowerUnit25);
    }

    [Benchmark(OperationsPerInvoke = Ops)]
    public bool UnitEqualityLargeProduct() => OperationUtility.Equals(_largeLeftUnit!, _largeRightUnit!);

    [Benchmark(OperationsPerInvoke = Ops)]
    public bool DimensionEqualityLargeProduct()
    {
        // Keep single evaluation (dimension equality is heavier); not loop-amplified intentionally to avoid skew.
        return OperationUtility.Equals(_largeLeftDimension!, _largeRightDimension!);
    }

    [Benchmark(OperationsPerInvoke = Ops)]
    public bool Equality_LazyVsEager_5()
    {
        bool last = false;
        for (int i = 0; i < Ops; i++) { last = OperationUtility.Equals(_lazyPowerUnit5!, _eagerPowerUnit5!); }
        return last;
    }

    [Benchmark(OperationsPerInvoke = Ops)]
    public bool Equality_LazyVsEager_10()
    {
        bool last = false;
        for (int i = 0; i < Ops; i++) { last = OperationUtility.Equals(_lazyPowerUnit10!, _eagerPowerUnit10!); }
        return last;
    }

    [Benchmark(OperationsPerInvoke = Ops)]
    public bool Equality_LazyVsEager_25()
    {
        bool last = false;
        for (int i = 0; i < Ops; i++) { last = OperationUtility.Equals(_lazyPowerUnit25!, _eagerPowerUnit25!); }
        return last;
    }

    private static Unit BuildLazyVsEager(int n, out Unit eager)
    {
        // Simple repeating pattern of SI base units to reach n multiplicative factors
        var pattern = new[] { Unit.SI.m, Unit.SI.s, Unit.SI.kg, Unit.SI.A };
        Unit product = pattern[0];
        for (int i = 1; i < n; i++)
        {
            product *= pattern[i % pattern.Length];
        }
        // eager distribution disabled by toggling lazy flag temporarily
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        ReductionSettings.LazyPowerExpansion = false;
        eager = product ^ 3; // distributed
        ReductionSettings.LazyPowerExpansion = true;
        var lazy = product ^ 3; // power-of-product
        ReductionSettings.LazyPowerExpansion = originalLazy; // restore
        return lazy;
    }
}