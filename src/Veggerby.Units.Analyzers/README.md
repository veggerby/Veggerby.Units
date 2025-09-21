# Veggerby.Units.Analyzers

Roslyn analyzers for **Veggerby.Units** providing early feedback on unsafe or ambiguous unit usage.

These analyzers complement the core library’s deterministic dimensional algebra by surfacing issues directly in the IDE / CI before runtime.

## Install

Add the analyzer package alongside the core library. Optionally add the code fixes package for IDE quick actions:

```xml
<ItemGroup>
  <PackageReference Include="Veggerby.Units" Version="$(VeggerbyUnitsVersion)" />
  <PackageReference Include="Veggerby.Units.Analyzers" Version="$(VeggerbyUnitsAnalyzersVersion)" PrivateAssets="all" />
  <PackageReference Include="Veggerby.Units.CodeFixes" Version="$(VeggerbyUnitsCodeFixesVersion)" PrivateAssets="all" />
</ItemGroup>
```

`PrivateAssets="all"` keeps analyzers/code fixes from flowing transitively to downstream consumers unless you intend that.

If you only want diagnostics in CI (no IDE code actions) omit the `Veggerby.Units.CodeFixes` reference.

## Rules

| Id | Title | Severity | Description |
|----|-------|----------|-------------|
| VUNITS001 | Incompatible unit addition/subtraction | Error | Flags `+` / `-` between measurements whose units differ (no implicit conversion performed). |
| VUNITS002 | Ambiguous unit formatting | Info | Warns when calling `ToString()` / `Format()` on a measurement whose unit symbol is ambiguous without specifying `UnitFormat`. |

### VUNITS001 – Incompatible unit arithmetic

Disallows adding or subtracting measurements unless their units are structurally identical. Convert explicitly first if needed.

```csharp
var length = new Int32Measurement(5, Unit.SI.m);
var time = new Int32Measurement(3, Unit.SI.s);
var bad = length + time; // VUNITS001
```

### VUNITS002 – Ambiguous unit formatting

Some symbols (e.g. `J`, `Pa`, `W`, `H`) map to multiple quantity meanings. Use an explicit `UnitFormat` (e.g. `UnitFormat.Qualified`) to clarify output.

```csharp
var energy = new Int32Measurement(1, Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s));
var s1 = energy.ToString();          // VUNITS002
var s2 = energy.Format(UnitFormat.Qualified); // OK
```

## Code Fix Support

Install `Veggerby.Units.CodeFixes` to enable:

* VUNITS001: Adds `.ConvertTo(left.Unit)` on the right operand of `+` / `-`.
* VUNITS002: Appends `UnitFormat.Qualified` to formatting invocation.

Both fixes support Fix All in Document / Project / Solution.

## Suppression

Use standard pragma or `GlobalSuppressions.cs` when you intentionally violate a rule (rare; prefer fixing instead).

```csharp
#pragma warning disable VUNITS002
var s = energy.ToString();
#pragma warning restore VUNITS002
```

## Roadmap

* Enrich unit extraction for complex expression flows.
* Direct consumption of shared ambiguity registry (when public hook is finalized).
* Additional rules: affine misuse in arithmetic, redundant unit multiplications, dead dimension factors.

## Contributing

1. Clone the repo.
2. Build & test: `dotnet test` (includes analyzer tests).
3. Add or update rule docs under `docs/analyzers/` before submitting PRs.

## License

MIT – see repository root `LICENSE` file.
