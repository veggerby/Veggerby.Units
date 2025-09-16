using BenchmarkDotNet.Attributes;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("equality")]
public class EqualityBenchmarks
{
    private Unit? _largeLeftUnit;
    private Unit? _largeRightUnit;
    private Dimension? _largeLeftDimension;
    private Dimension? _largeRightDimension;

    [Params(false, true)]
    public bool UseExponentMap;

    [GlobalSetup]
    public void Setup()
    {
        ReductionSettings.UseExponentMapForReduction = UseExponentMap;

        var a = Unit.SI.m; var b = Unit.SI.s; var c = Unit.SI.kg;
        _largeLeftUnit = (((a * b * c) ^ 2) * (b * c / a)) / c;
        _largeRightUnit = ((b * c / a) * ((a * b * c) ^ 2)) / c;

        var L = Dimension.Length; var T = Dimension.Time; var M = Dimension.Mass;
        _largeLeftDimension = (((L * T * M) ^ 3) * (T * M / L)) / M;
        _largeRightDimension = ((T * M / L) * ((L * T * M) ^ 3)) / M;
    }

    [Benchmark]
    public bool UnitEqualityLargeProduct() => OperationUtility.Equals(_largeLeftUnit!, _largeRightUnit!);

    [Benchmark]
    public bool DimensionEqualityLargeProduct() => OperationUtility.Equals(_largeLeftDimension!, _largeRightDimension!);
}