# Performance & Repeatability Gates

This library enforces deterministic equality semantics and guards against performance regressions via CI.

## Repeatability Gate

Two full test runs execute sequentially in the `ci-repeatability` workflow. Any nondeterministic failures (flakes) will surface as a PR failure. All tests share a serialized xUnit collection for `ReductionSettings` ensuring feature flag isolation.

## Benchmark Baseline

Representative micro benchmarks (see `bench/Veggerby.Units.Benchmarks/*Benchmarks.cs`) capture:

- Equality_LazyVsEager_5 / 10 / 25
- UnitEqualityLargeProduct
- DivisionCancellation_Full

The canonical equality algorithm is considered stable when:

- Mean time does not regress more than 1% versus baseline.
- Managed allocation does not increase for any tracked benchmark.

## Updating the Baseline

1. Run benchmarks locally (full suite): `dotnet run --project bench/Veggerby.Units.Benchmarks -c Release -- --exporters markdown`.
2. Locate the GitHub-style report (e.g. `BenchmarkDotNet.Artifacts/results/*report-github.md`).
3. Extract summary: `dotnet script scripts/extract-benchmark-summary.csx -- BenchmarkDotNet.Artifacts/results/EqualityBenchmarks-report-github.md bench/Veggerby.Units.Benchmarks/current-benchmark-summary.json`.
4. Review JSON: `jq . bench/Veggerby.Units.Benchmarks/current-benchmark-summary.json`.
5. Replace baseline at `bench/Veggerby.Units.Benchmarks/Baseline/equality-baseline.json` if acceptable.
6. Commit the updated baseline (include rationale & representative before/after Mean numbers in commit message).

## Adding a Benchmark

Keep the set minimal. Only add cases representing a distinct algorithmic path (e.g. new cancellation form). After adding, update baseline JSON with initial metrics.

## Fast Path Policy

Micro optimizations (e.g. exponent==1 early-outs) must include:

- Targeted test demonstrating semantic invariance.
- Added / updated benchmark demonstrating neutrality or improvement under the 1% threshold.

## Comparator

CI uses a POSIX shell script (`scripts/compare-benchmarks.sh`) to avoid a PowerShell dependency on Linux runners. Local PowerShell comparison remains available (`scripts/compare-benchmarks.ps1`) if you prefer. The comparator expects the consolidated benchmark project output (no legacy `benchmarks/` directory).

## Diagnostics

`DEBUG` builds log canonical factor mismatches when structural forms differ but are canonically equal, aiding investigation if regressions appear. If a suspected regression occurs, re-run benchmarks twice to rule out noise before updating the baseline.
