# Performance & Benchmarks

This document explains how to run and interpret the Veggerby.Units benchmark suite.

## Projects

Benchmarks live in `bench/Veggerby.Units.Benchmarks` and use BenchmarkDotNet.

Included benchmark classes:

* `UnitReductionBenchmarks` – core unit algebra scenarios
* `DimensionReductionBenchmarks` – dimension algebra parallels
* `ComplexDimensionReductionBenchmarks` – stress chained cancellation & associativity

## Quick Start

Run all benchmarks (Release build recommended):

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks
```

Run a filtered subset (wildcards supported):

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks -- --filter *NestedDivision*
```

Run only cancellation methods in all classes:

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks -- --filter *MultiplyThenDivideCancellation*
```

List all benchmarks without running:

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks -- --list flat
```

## CI Smoke

The workflow `.github/workflows/benchmark-smoke.yml` executes a single fast benchmark (`*ReductionBenchmarks.MultiplyThenDivideCancellation*`) to catch regressions quickly without incurring the full suite cost.

## Adding New Benchmarks

1. Create a new `*.cs` file in the benchmark project namespace `Veggerby.Units.Benchmarks`.
2. Add `[MemoryDiagnoser]` (and optionally `[BenchmarkCategory("reduction")]`).
3. Register the type in `Program.cs` switcher array.
4. Keep per-operation logic minimal; avoid including I/O or random number generation.

## Measuring Changes

When evaluating an optimization:

1. Run the baseline commit: export results (`BenchmarkDotNet.Artifacts/results/*.json`).
2. Apply changes; re-run same filter.
3. Compare Means + Allocated (B) columns. Improvements should exceed natural variance (look for non-overlapping confidence intervals).

## Interpreting Results

Key columns:

* Mean – average execution time per operation
* Error / StdDev – variability (lower is better)
* Allocated – bytes allocated per operation (critical for high‑frequency algebra calls)

## Known Hot Paths

Current hotspots are typically in:

* `OperationUtility.ReduceDivision`
* `OperationUtility.ReduceMultiplication`
* `OperationUtility.ExpandPower`

## Optimization Policy

* Maintain determinism; no caching unless immutable and thread-safe.
* Avoid micro-optimizations that reduce clarity unless benchmark data shows ≥10% improvement in both time and allocation.
* Revert speculative changes that hurt readability without measurable gain.

## ExponentMap Prototype

`ExponentMap<T>` exists as a prototype but is not integrated after an attempted in-place swap caused recursion complexity. Future safe adoption would require localized usage guarded by thorough tests and profiling evidence.

## Troubleshooting

If benchmarks fail with validation errors, ensure:

* Release build is used
* No debugger attached
* Deterministic environment (close other CPU intensive tasks)

---
For further performance exploration, consider adding a dedicated equality benchmark focusing on `OperationUtility.Equals` with large synthetic products.
