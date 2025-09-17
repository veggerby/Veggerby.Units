# Veggerby.Units

<!-- Badges -->
[![CI](https://github.com/veggerby/Veggerby.Units/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/veggerby/Veggerby.Units/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Veggerby.Units.svg)](https://www.nuget.org/packages/Veggerby.Units/)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Coverage](https://img.shields.io/codecov/c/github/veggerby/Veggerby.Units?token=)](https://codecov.io/gh/veggerby/Veggerby.Units)
[![Static Analysis](https://img.shields.io/badge/analysis-style%20rules-blueviolet)](.editorconfig)
[![.NET](https://img.shields.io/badge/.NET-9.0-informational)](https://dotnet.microsoft.com/)

> A focused .NET library for strongly‑typed measurements, deterministic dimensional reduction, and algebra over physical units.

## Why this library?

Most unit libraries wrap numbers. Veggerby.Units models algebra: composite unit expressions, cancellation, exponent distribution, and canonical equality.

## Highlights

* Unit system interoperability (SI ↔ Imperial) with strict dimensional safety.
* Order‑independent equality: (m*s)^2 == m^2*s^2, m*s/s == m, A*B == B*A.
* Deterministic canonical factor multiset normalization for product / division / power.
* Automatic cancellation & power aggregation in operator chains (low allocation fast paths).
* Metric prefixes (yocto → yotta) as first‑class multiplicative factors.
* Measurement arithmetic (generic numeric backends: int, double, decimal) with safe conversions.
* Affine temperature conversion support (°C ↔ K).
* Benchmark‑guarded performance (≤1% regression gate on equality micro benchmarks).
* Planned: parsing (string → expression tree), richer physical property taxonomy.

## Quick start

```csharp
using Veggerby.Units;

var distance = new DoubleMeasurement(5, Prefix.k * Unit.SI.m);    // 5 km
var time     = new DoubleMeasurement(30, Unit.SI.s);              // 30 s
var speed    = distance / time;                                   // ≈ 0.166666 km/s
var speedMS  = speed.ConvertTo(Unit.SI.m / Unit.SI.s);            // ≈ 166.666 m/s
```

More examples: `docs/capabilities.md`.

## Core concepts

| Concept | Summary |
|---------|---------|
| Unit | Structural node: basic, derived, product, division, power, prefixed, scaled. |
| Dimension | Physical basis (L, M, T, I, Θ, J, N, …) enforcing homogeneous addition. |
| Measurement&lt;T&gt; | Numeric value + Unit + arithmetic strategy (`Calculator<T>`). |
| Prefix | Metric 10^n factor (k, m, μ …). |
| Reduction | Re-association + cancellation + exponent aggregation to canonical form. |
| Canonical equality | Factor multiset comparison immune to authoring order & lazy power shape. |

### Joule decomposition (example)

```text
1 J
├─ composition = N * m
│  └─ N = kg * m / s^2
└─ dimension = M * L^2 / T^2
```

Adding metres to seconds or converting velocity to mass raises an exception.

## Canonical equality strategy

1. Flatten product trees.
2. Encode division as negative exponents.
3. Multiply nested power exponents ( (A^m)^n -> A^(m·n) ).
4. Distribute (Product)^n forms during equality (pre-normalization) for deterministic accumulation.
5. Compare exponent maps; fallback structural leaf match if references differ.

Guarantees: order independence, lazy vs eager parity, no early false negatives.

## Extensibility

```csharp
var foot = new ScaleUnit("ft", Unit.SI.m, 0.3048);      // 1 ft = 0.3048 m
var span = new DoubleMeasurement(6, foot);               // 6 ft
var metres = span.ConvertTo(Unit.SI.m);                  // 1.8288 m

var newton = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);   // kg·m/s^2
```

All reduction / equality logic reuses core algorithms—no extra wiring.

## Performance & benchmarks

Benchmarks: `bench/Veggerby.Units.Benchmarks` (see `docs/performance.md`).

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks
```

Equality micro benchmarks baseline enforces ≤1% mean regression & zero new allocation.

Filter examples:

```bash
dotnet run -c Release --project bench/Veggerby.Units.Benchmarks -- --filter *EqualityBenchmarks*
```

## Feature flags (advanced)

`Veggerby.Units.Reduction.ReductionSettings`:

| Flag | Default | Purpose |
|------|---------|---------|
| `EqualityNormalizationEnabled` | true | Canonical factor multiset equality path. |
| `LazyPowerExpansion` | false | Leaves (Product)^n unexpanded until needed (still equal). |
| `UseFactorVector` | false | Cached canonical factor vectors for some composites. |
| `UseExponentMapForReduction` | false | Exponent map based reduce path (A/B). |

Toggle only in benchmark / test contexts.

## Roadmap

* Parsing (string → expression tree)
* Additional systems (CGS, US customary variants)
* More numeric types (BigInteger, arbitrary precision) beyond current int/double/decimal
* Property classification (Energy vs Work vs Heat) atop dimensions

## Temperature (Affine Units)

Temperature units with offsets (°C, °F) are modelled as affine units over absolute Kelvin. Rules:

* Direct conversions supported: C ↔ K, F ↔ K, C ↔ F.
* Affine units cannot be multiplied, divided, prefixed, or raised to powers > 1 (these operations are not linear with offsets) – attempting this throws `UnitException`.
* Equality and comparison work as for other units (values compared after alignment via Kelvin base).

Helper factories: `Temperature.Celsius(25)`, `Temperature.Fahrenheit(77)`, `Temperature.Kelvin(300)`.

### Temperature Semantics (Absolute vs Delta)

The semantic quantity layer further distinguishes absolute temperatures from temperature differences to prevent misuse:

* `TemperatureAbsolute` (T_abs): affine (K, °C, °F). Direct `+` / `-` between absolutes is blocked.
* `TemperatureDelta` (ΔT): linear differences (canonical Kelvin scale). Free additive arithmetic.

APIs:

```csharp
var t20C = TemperatureQuantity.Absolute(20.0, Unit.SI.C);
var d5K  = TemperatureQuantity.Delta(5.0);     // 5 K difference
var t25C = TemperatureOps.AddDelta(t20C, d5K); // 25 °C
var d10F = TemperatureQuantity.DeltaF(10.0);   // 10 °F -> 5.555... K
```

See `docs/quantities.md` (Temperature Semantics) for rationale and usage.

## References

* Dimensional analysis – <https://en.wikipedia.org/wiki/Dimensional_analysis>
* SI units – <https://en.wikipedia.org/wiki/International_System_of_Units>
* Nondimensionalization – <https://en.wikipedia.org/wiki/Nondimensionalization>

---
Docs index (see also `TryConvertTo` and decimal support in capabilities):

* Capabilities: `docs/capabilities.md`
* Reduction architecture: `docs/reduction_architecture.md`
* Reduction pipeline narrative: `docs/reduction-pipeline.md`
* Performance guide: `docs/performance.md`
