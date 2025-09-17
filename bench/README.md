# Benchmark Suite

Consolidated BenchmarkDotNet project for Veggerby.Units lives under `bench/Veggerby.Units.Benchmarks`.

## Projects

- `Veggerby.Units.Benchmarks` – full suite (reduction, equality, power, division cancellation, dimension scenarios). When no filter is supplied a fast smoke subset runs.

## Running

Full suite (markdown + GitHub report exporter):

```bash
dotnet run --project bench/Veggerby.Units.Benchmarks -c Release -- --exporters markdown
```

Smoke (default when no args):

```bash
dotnet run --project bench/Veggerby.Units.Benchmarks -c Release
```

Filter examples:

```bash
# Only equality micro benchmarks
dotnet run --project bench/Veggerby.Units.Benchmarks -c Release -- --filter *EqualityBenchmarks*
# Only division cancellation
dotnet run --project bench/Veggerby.Units.Benchmarks -c Release -- --filter *DivisionCancellationBenchmarks*
```

## Performance Gating

Baseline JSON for gating: `bench/Veggerby.Units.Benchmarks/Baseline/equality-baseline.json`.

After a benchmark run, extract the summary into JSON with:

```bash
dotnet script scripts/extract-benchmark-summary.csx -- BenchmarkDotNet.Artifacts/results/EqualityBenchmarks-report-github.md bench/Veggerby.Units.Benchmarks/current-benchmark-summary.json
```

Compare against baseline locally (shell comparator):

```bash
./scripts/compare-benchmarks.sh bench/Veggerby.Units.Benchmarks/Baseline/equality-baseline.json bench/Veggerby.Units.Benchmarks/current-benchmark-summary.json 1.0
```

## Adding Benchmarks

Keep them focused and minimal; prefer extending existing categories. Update the baseline only when the new benchmark is intended for gating and after verifying stability across several runs.
