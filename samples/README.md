# Samples

This folder contains minimal projects demonstrating how to use Veggerby.Units and its companion analyzer / code fix packages.

## Projects

| Sample | Purpose |
|--------|---------|
| `UsageSample` | Core library usage: unit algebra, measurements, conversion, prefixes, temperatures, quantities. |
| `AnalyzerSample` | Shows analyzer diagnostics (VUNITS001, VUNITS002) and how code fixes apply. |

## Running

Build all samples:

```bash
dotnet build samples/UsageSample
 dotnet build samples/AnalyzerSample
```

Run usage sample:

```bash
dotnet run --project samples/UsageSample
```

Analyzer sample intentionally contains code that triggers diagnostics. In an IDE you can apply fixes:

* VUNITS001: invoke quick actions on the `length + time` expression → Convert right operand.
* VUNITS002: invoke quick actions on `energy.ToString()` → Specify UnitFormat.Qualified.

To see both diagnostics in CLI build without failing the whole build, you can temporarily lower severity via an `.editorconfig` override if desired.

## Editing

Use these samples as a starting point for integration or as reproducible test beds when filing issues.
