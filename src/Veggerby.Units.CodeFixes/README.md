# Veggerby.Units.CodeFixes

Code fix providers for the `Veggerby.Units.Analyzers` diagnostics.

This package supplies IDE (Roslyn) code actions that complement the analyzer warnings/errors:

| Diagnostic | Code Fix Action | Effect |
|------------|-----------------|--------|
| VUNITS001  | "Convert right operand to left unit" | Inserts `.ConvertTo(left.Unit)` on the right-hand side of a `+` / `-` operation between differing unit measurements. |
| VUNITS002  | "Specify UnitFormat.Qualified" | Adds `UnitFormat.Qualified` argument to measurement formatting calls lacking explicit format. |

## When to Install

Install this package if you want automated fixes in Visual Studio / Rider / VS Code C# extension. The analyzers package (`Veggerby.Units.Analyzers`) is still required to produce diagnostics; this package only contributes fixes.

In most solutions reference both:

```xml
<ItemGroup>
  <PackageReference Include="Veggerby.Units" Version="$(VeggerbyUnitsVersion)" />
  <PackageReference Include="Veggerby.Units.Analyzers" Version="$(VeggerbyUnitsAnalyzersVersion)" PrivateAssets="all" />
  <PackageReference Include="Veggerby.Units.CodeFixes" Version="$(VeggerbyUnitsCodeFixesVersion)" PrivateAssets="all" />
</ItemGroup>
```

`PrivateAssets="all"` prevents re‑distribution of analyzer/code fix assets to downstream consumers unless explicitly desired.

## Usage Examples

### VUNITS001

Before:

```csharp
var len = new DoubleMeasurement(5, Unit.SI.m);
var time = new DoubleMeasurement(3, Unit.SI.s);
var bad = len + time; // diagnostic VUNITS001
```

Fix applies:

```csharp
var fixedValue = len + time.ConvertTo(len.Unit);
```

### VUNITS002

Before:

```csharp
var energy = new DoubleMeasurement(1, Unit.SI.kg * Unit.SI.m * Unit.SI.m / (Unit.SI.s ^ 2));
var s = energy.ToString(); // diagnostic VUNITS002
```

Fix applies:

```csharp
var s2 = energy.Format(UnitFormat.Qualified);
```

## Relationship to Analyzer Package

The analyzers assembly is intentionally kept free of `Microsoft.CodeAnalysis.Workspaces` to satisfy RS1038 guidance. Code fixes depend on Workspaces APIs and therefore live in this separate package. Both share internal diagnostic descriptors via `InternalsVisibleTo`.

## Versioning

Code fixes follow the same version number as analyzers. A new analyzer rule may ship without an immediate fix; in that case a later version adds the fix while maintaining semantic versioning guidelines.

## Limitations

* Only explicit, safe transformations are offered. No speculative dimensional coercions are applied.
* Fix all in solution is supported (batch fixer) but still validates each occurrence.

## License

MIT – see root repository LICENSE.
