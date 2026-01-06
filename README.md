# Veggerby.Units

<!-- Badges -->
[![CI](https://github.com/veggerby/Veggerby.Units/actions/workflows/ci-release.yml/badge.svg?branch=main)](https://github.com/veggerby/Veggerby.Units/actions/workflows/ci-release.yml)
[![NuGet](https://img.shields.io/nuget/v/Veggerby.Units.svg)](https://www.nuget.org/packages/Veggerby.Units/)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Coverage](https://img.shields.io/codecov/c/github/veggerby/Veggerby.Units)](https://codecov.io/gh/veggerby/Veggerby.Units)
[![Static Analysis](https://img.shields.io/badge/analysis-style%20rules-blueviolet)](.editorconfig)
[![.NET](https://img.shields.io/badge/.NET-9.0-informational)](https://dotnet.microsoft.com/)

> A focused .NET library for strongly‑typed measurements, deterministic dimensional reduction, and algebra over physical units.

## Why this library?

Most unit libraries wrap numbers. Veggerby.Units models algebra: composite unit expressions, cancellation, exponent distribution, and canonical equality.

## Highlights

* Unit system interoperability (SI ↔ Imperial ↔ CGS) with strict dimensional safety.
* Order‑independent equality: (m*s)^2 == m^2*s^2, m*s/s == m, A*B == B*A.
* Deterministic canonical factor multiset normalization for product / division / power.
* Automatic cancellation & power aggregation in operator chains (low allocation fast paths).
* Metric prefixes (yocto → yotta) as first‑class multiplicative factors.
* Measurement arithmetic (generic numeric backends: int, double, decimal) with safe conversions.
* Affine temperature conversion support (°C ↔ K).
* Benchmark‑guarded performance (≤1% regression gate on equality micro benchmarks).
* Planned: parsing of formatted/qualified strings (string → expression tree for raw unit expressions is already supported).

## Quick start

```csharp
using Veggerby.Units;
using Veggerby.Units.Fluent;      // formatting + Quantity facade
using Veggerby.Units.Fluent.SI;   // SI numeric extensions

var distance = new DoubleMeasurement(5, Prefix.k * Unit.SI.m);    // 5 km
var time     = new DoubleMeasurement(30, Unit.SI.s);              // 30 s
var speed    = distance / time;                                   // ≈ 0.166666 km/s
var speedMS  = speed.ConvertTo(Unit.SI.m / Unit.SI.s);            // ≈ 166.666 m/s

// Fluent equivalents:
var d2 = 5.0.Kilometers();            // 5000 m
var t2 = 30.0.Seconds();              // 30 s
var v2 = d2 / t2;                     // m/s
var vFmt = v2.Format(Formatting.UnitFormat.BaseFactors); // "166.66666666666666 m/s"
```

More examples: `docs/capabilities.md`.

### Analyzer & Code Fix Integration

Add diagnostics (unit mismatch, ambiguous formatting) and IDE quick fixes with the companion packages:

```xml
<ItemGroup>
    <PackageReference Include="Veggerby.Units.Analyzers" Version="$(VeggerbyUnitsAnalyzersVersion)" PrivateAssets="all" />
    <PackageReference Include="Veggerby.Units.CodeFixes" Version="$(VeggerbyUnitsCodeFixesVersion)" PrivateAssets="all" />
</ItemGroup>
```

Omit `Veggerby.Units.CodeFixes` on CI-only projects that do not need IDE code actions.

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

* Parsing of formatted/qualified strings
* Additional systems (CGS, US customary variants)
* More numeric types (BigInteger, arbitrary precision) beyond current int/double/decimal
* (Done) Property classification (Energy vs Work vs Heat) via open tag system

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

## Quantity Semantics & Arithmetic (Energy vs Torque, Temperature Absolutes, etc.)

The *semantic layer* (`QuantityKind` + `Quantity<T>`) disambiguates identical dimensions by meaning (Energy vs Torque both J). It layers on top of pure dimensional algebra – the core reducer stays ignorant of semantics.

Supported quantity arithmetic (summary):

* Same‑kind addition / subtraction only if the kind allows it (`AllowDirectAddition/AllowDirectSubtraction`).
* Point – Point (same absolute kind with a `DifferenceResultKind`) => Delta kind (e.g. AbsoluteTemperature – AbsoluteTemperature → TemperatureDelta).
* Point ± Delta → Point (adding or subtracting a difference from an absolute).
* Delta ± Delta → Delta (fully linear).
* Multiplication / Division: only when an explicit inference rule exists (registry) OR one operand is dimensionless (unitless) – the non‑dimensionless vector‑like kind is preserved. Absolute (point) kinds never survive scalar fallback (must be inferred).
* Scalar scaling by a dimensionless `Measurement<T>` for vector‑like kinds (not for point‑like kinds, e.g. you cannot scale an absolute temperature directly).

Explicit inference rules (seeded):

* Entropy × TemperatureAbsolute ↔ Energy
* Energy ÷ TemperatureAbsolute → Entropy
* Energy ÷ Entropy → TemperatureAbsolute
* Torque × Angle ↔ Energy
* Energy ÷ Angle → Torque; Energy ÷ Torque → Angle

Unsupported / intentionally rejected behaviors:

* Cross‑kind addition / subtraction (Energy + Torque, HeatCapacity + Entropy) – throws immediately even though dimensions may match.
* Automatic transitive inference (no chaining: if A*B=C and C/D=E we do not infer A*B/D=E without explicit rule).
* Implicit preservation of point‑like kinds under scalar multiply/divide (prevents silently scaling absolute temperatures).
* Power / exponentiation at the quantity layer (apply power to underlying Measurement/Unit first, then wrap if semantically meaningful).
* Prefix application at quantity layer (prefixes belong to units; semantics unaffected).
* Dimensionless fallbacks that would blur meaning (Angle currently dimensionless but remains a distinct kind; fallback only preserves the other operand – Angle does not infect unrelated kinds without a rule).
* Silent coercion between energetically equivalent but semantically different forms (InternalEnergy vs Enthalpy) – you must choose proper kind.

Why these constraints: to surface ambiguous intent early, avoid “semantic drift” hiding behind dimensionally valid math, and keep the registry explicit, reviewable, and minimal. If you need a new semantic product/division outcome, register it explicitly (see `QuantityKindInferenceRegistry`).

More detail & extension guidance: `docs/quantities.md` (Inference & Arithmetic sections).

### Tag System (Open Semantic Classification)

Each `QuantityKind` has an extensible set of canonical tags (`QuantityKindTag`) describing semantic facets:

* Examples: `Energy.StateFunction`, `Energy.PathFunction`, `Domain.Thermodynamic`, `Domain.Mechanical`, `Form.Dimensionless`.
* Tags are canonical via `QuantityKindTag.Get(name)` (same name → same instance).
* Tags never affect dimensional algebra or operator legality; they are metadata for policy, filtering, analytics, or UI grouping.

```csharp
var work = QuantityKinds.Work; // Energy.PathFunction, Domain.Mechanical, Energy
bool isPath = work.HasTag("Energy.PathFunction"); // true
```

Custom kind with tags:

```csharp
var Exergy = new QuantityKind(
    name: "Exergy",
    canonicalUnit: QuantityKinds.Energy.CanonicalUnit,
    symbol: "Ex",
    tags: new[]{ QuantityKindTag.Get("Energy"), QuantityKindTag.Get("Energy.StateFunction"), QuantityKindTag.Get("Domain.Thermodynamic") });
```

Guidelines: prefer dotted hierarchical names, reuse existing roots (`Energy`, `Domain`, `Form`), keep tags stable (avoid transient runtime/state flags).

### Conventional Symbol Overlaps

Physics reuses concise symbols across domains. This library preserves conventional single-letter symbols instead of forcing artificial uniqueness. Governance only fails when two different kinds would be indistinguishable (same symbol + identical reduced canonical unit). Representative overlaps:

| Symbol | Kinds | Distinguishing Dimension Examples |
|--------|-------|-----------------------------------|
| V | Voltage (kg·m²/(s³·A)), Volume (m³) | Electrical potential vs geometric extent |
| S | Entropy (kg·m²/(s²·K)), ElectricConductance (s³·A²/(kg·m²)) | Thermal state vs transport ratio |
| H | Enthalpy (kg·m²/s²), Inductance (kg·m²/(s²·A²)), MagneticFieldStrength (A/m) | Energy state, field storage, field intensity |
| F | Force (kg·m/s²), Capacitance (s⁴·A²/(kg·m²)) | Mechanical interaction vs electric storage |
| A | HelmholtzFreeEnergy (kg·m²/s²), Area (m²) | Energy potential vs geometric surface |
| μ | ChemicalPotential (kg·m²/(s²·mol)), Permeability (kg·m/(s²·A²)), ChargeMobility (m²/(V·s)) | Thermodynamic, field medium, carrier transport |

Use the semantic kind (`QuantityKind`)—not just the printed symbol—when correctness depends on meaning. Formatting layers can add additional disambiguation if desired.

### Inferred Multiplicative Examples

```csharp
using Veggerby.Units.Quantities;

var power = Quantity.Power(250.0);     // 250 W
var time  = Quantity.Of(30.0, Unit.SI.s, QuantityKinds.Time); // 30 s (if factory provided else custom)
Quantity<double>.TryMultiply(power, time, out var energy); // true
Console.WriteLine(energy.Kind.Name);   // Energy

var pressure = Quantity.Pressure(101325.0); // 101325 Pa
var volume   = Quantity.Volume(0.01);       // 0.01 m^3
var ok = Quantity<double>.TryMultiply(pressure, volume, out var pvWork); // true
Console.WriteLine(pvWork.Kind.Name);        // Energy
```

### Energy vs Torque (Same Dimension, Different Meaning)

```csharp
var e = Quantity.Energy(12.0);  // 12 J [Energy]
var τ = Quantity.Torque(12.0);  // 12 J [Torque]
bool eq = e == τ;                // false (different kinds)
// e + τ -> throws InvalidOperationException
// e < τ -> throws (comparisons require same kind)
```

### Safe Temperature Mean (Affine Handling)

```csharp
var t1 = TemperatureQuantity.Absolute(25.0, Unit.SI.C);      // 25 °C
var t2 = TemperatureQuantity.Absolute(77.0, Unit.Imperial.F); // 77 °F
var mean = TemperatureMean.Mean(t1, t2);                     // expressed in °C (first unit)
Console.WriteLine(mean);                                     // e.g. 36.111... °C
```

### Try* APIs & Comparisons

```csharp
var f = Quantity.Force(5.0);
var d = Quantity.Length(2.0);
if (Quantity<double>.TryMultiply(f, d, out var work))
{
    // work.Kind == Energy
}

var f2 = Quantity.Force(7.0);
bool less = f < f2; // true (same kind)

// Cross-kind comparison rejects:
// var invalid = f < work; // throws InvalidOperationException
```

### Registry Sealing (Deterministic Semantics)

```csharp
// After all custom rule registrations
QuantityKindInferenceRegistry.Seal();
// Further Register(...) calls now throw, ensuring stable semantic environment.
```

### Cross-kind Addition Failure (Example)

```csharp
using Veggerby.Units;
using Veggerby.Units.Quantities;

var energy = Quantity.Energy(10.0);    // 10 J [Energy]
var torque = Quantity.Torque(3.0);     // 3 J [Torque]

try
{
    var invalid = energy + torque; // different kinds, same dimension (J)
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    // -> Cannot add Energy and Torque.
}
```

This early failure prevents accidentally mixing distinct semantic concepts that share the same physical dimension.

## References

### Standards & Specifications
* **QUDT Ontology** – Conceptual reference for unit definitions and quantity kinds (no runtime dependency): <http://www.qudt.org/>
  * See `docs/qudt-alignment.md` for detailed alignment documentation
  * QUDT Vocabulary: <http://www.qudt.org/doc/DOC_VOCAB-UNITS.html>
* **SI Brochure (BIPM)** – International System of Units (9th edition): <https://www.bipm.org/en/publications/si-brochure/>
* **NIST Guide to SI** – U.S. authority on metric system: <https://www.nist.gov/pml/owm/metric-si/si-units>

### Background
* Dimensional analysis – <https://en.wikipedia.org/wiki/Dimensional_analysis>
* SI units – <https://en.wikipedia.org/wiki/International_System_of_Units>
* Nondimensionalization – <https://en.wikipedia.org/wiki/Nondimensionalization>

---
Docs index (see also `TryConvertTo` and decimal support in capabilities):

* **QUDT Alignment:** `docs/qudt-alignment.md` – Conceptual reference validation
* **QUDT Mapping Table:** `docs/qudt-mapping-table.md` – Detailed quantity kind mappings
* Capabilities: `docs/capabilities.md`
* Reduction architecture: `docs/reduction_architecture.md`
* Reduction pipeline narrative: `docs/reduction-pipeline.md`
* Performance guide: `docs/performance.md`
* Quantity kinds list: `docs/quantity-kinds.md`
* Fluent quickstart: `docs/fluent-quickstart.md`
* Changelog: `CHANGELOG.md` (Unreleased changes)

## Contributing & Formatting

See `CONTRIBUTING.md` for process guidance. Code formatting is enforced by `.editorconfig` and `dotnet format`.

Key rules (authoritative list in `docs/style-formatting.md`):

* ALWAYS use spaces (never tabs) – 4 spaces per indent level.
* Mandatory braces for all control flow blocks (no single-line exceptions).
* Clear parentheses for arithmetic / relational clarity even when precedence would suffice.
* Single blank line between members; Arrange / Act / Assert separation in tests.
* No trailing whitespace; no aligning with extra spaces.

Review `docs/style-formatting.md` before opening a PR.
