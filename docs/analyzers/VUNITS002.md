# VUNITS002: Ambiguous unit formatting without explicit format

| Property | Value |
|----------|-------|
| **Category** | Clarity |
| **Severity** | Info |
| **Enabled by default** | Yes |
| **Code Fix** | Yes (append `UnitFormat.Qualified`) |

## Summary

Flags calls to `ToString()` / `Format()` on a measurement (or unit-aware object) that produce an ambiguous symbol (`J`, `Pa`, `W`, `H`) without specifying an explicit `UnitFormat`. These symbols map to multiple quantity kinds and unqualified output may hide semantic intent.

## Motivation

Certain SI derived symbols are polysemous:

- `J` = Energy, Work, Heat, Torque (dimensionally N·m)
- `Pa` = Pressure, Stress
- `W` = Power, RadiantFlux
- `H` = Inductance, MagneticFieldStrength

Relying on contextual guesswork (variable names, surrounding comments) reduces readability and increases maintenance cost. Explicit formatting (Qualified or Mixed with qualification) disambiguates intent.

## Detected Patterns

```csharp
var energy = new Int32Measurement(5, Unit.SI.J);
Console.WriteLine(energy.ToString()); // VUNITS002

var pressure = GetPressure();
Log(pressure.ToString()); // VUNITS002
```

Static `Format` pattern:

```csharp
UnitFormatter.Format(value, unit); // VUNITS002 when unit symbol ambiguous
```

## Non-Issues

Explicit format supplied:

```csharp
energy.ToString(UnitFormat.Qualified); // OK
energy.ToString(UnitFormat.Mixed);      // OK (will qualify if kind provided)
```

Non-ambiguous symbols:

```csharp
var force = new Int32Measurement(2, Unit.SI.N);
force.ToString(); // OK (N unique)
```

## Code Fix

Appends `UnitFormat.Qualified` to the invocation argument list if missing:

```csharp
energy.ToString();            // before
energy.ToString(UnitFormat.Qualified); // after
```

For a static `Format(value, unit)` call, the fix transforms to:

```csharp
UnitFormatter.Format(value, unit, UnitFormat.Qualified);
```

## Limitations

- Heuristic symbol inference: analyzer resolves `unit.Symbol` when accessible; if symbol cannot be statically determined (e.g., dynamic flow through a variable of type `Unit` set via reflection), no diagnostic.
- Does not attempt to infer or attach a specific `QuantityKind`; it only enforces explicit formatting to prompt user choice.

## Suppression Guidance

Prefer fixing by adding an explicit format. If you intentionally rely on implicit context (rarely justified), you may suppress locally:

```csharp
#pragma warning disable VUNITS002
Console.WriteLine(energy.ToString());
#pragma warning restore VUNITS002
```

Document why the suppression is safe.

## Rationale

Maintains semantic transparency. Output like `5 J` without context could represent energy, work done, torque, or heat. Qualified formatting (`5 J (Energy)`) removes ambiguity and aids logging, telemetry parsing, and code reviews.

## Related

- `VUNITS001` (additive unit mismatch) – orthogonal dimensional correctness rule.
- `AmbiguityRegistry` – authoritative list of ambiguous symbols.
