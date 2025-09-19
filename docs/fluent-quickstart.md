# Fluent Quickstart

This guide introduces the Tier 1 fluent ergonomic layer: formatting modes, the `Quantity` facade, and numeric extensions (SI + Imperial subset).

> All features are additive. No semantic / reduction rules changed; this layer only reduces ceremony for common construction & display tasks.

## Namespaces

```csharp
using Veggerby.Units;                 // Core units & Measurement<T>
using Veggerby.Units.Fluent;          // Formatting + Quantity facade
using Veggerby.Units.Fluent.SI;       // SI numeric extensions (Meters(), Joules(), etc.)
using Veggerby.Units.Fluent.Imperial; // Imperial numeric extensions (Feet(), Pounds(), etc.)
```

## Constructing Measurements

### Quantity Facade

```csharp
var energy = Quantity.Energy(12.5);          // 12.5 J (Energy canonical unit)
var force  = Quantity.Force(3, Unit.SI.N);    // 3 N
var accel  = Quantity.Acceleration(9.81);     // 9.81 m/s^2
```

### Numeric Extensions (SI)

```csharp
var distance = 5.0.Kilometers();  // 5000 m (km scaling via factor)
var time     = 30.0.Seconds();    // 30 s
var speed    = distance / time;   // m/s
```

### Numeric Extensions (Imperial)

```csharp
var span  = 6.0.Feet();         // 6 ft
var inch  = 12.0.Inches();      // 12 in
var force = 10.0.PoundForce();  // 10 lbf
var work  = 5.0.FootPounds();   // 5 ft·lb (shares Joule dimension)
```

## Formatting Units & Measurements

Formatting is controlled by `UnitFormat`:

| Mode | Behavior |
|------|----------|
| `BaseFactors` | Raw structural symbol (no derived substitution). |
| `DerivedSymbols` | Exact match replacement using canonical SI derived symbols (N, J, Pa, etc.). No partial substitution. |
| `Mixed` | (Placeholder) Currently identical to `BaseFactors`; future: partial greedy substitution. |
| `Qualified` | Derived substitution + semantic disambiguation (adds kind for ambiguous symbols). |

```csharp
var e = Quantity.Energy(1);
var t = new DoubleMeasurement(1, QuantityKinds.Torque.CanonicalUnit); // same dimension

var j1 = e.Format(UnitFormat.DerivedSymbols);           // "1 J"
var jQ = e.Format(UnitFormat.Qualified, QuantityKinds.Energy);  // "1 J (Energy)"
var τQ = t.Format(UnitFormat.Qualified, QuantityKinds.Torque);  // "1 J (Torque)"
```

Ambiguity detection currently targets Joule (J) for Energy vs Torque. Additional symbols can be added centrally without changing call sites.

## Conversions (Fluent)

Use `In(targetUnit)` for concise conversion:

```csharp
var metres  = 2500.0.Meters();
var km      = metres.In(Unit.SI.m);       // identity (example)
var speedMS = (5.0.Kilometers() / 400.0.Seconds());
var speedM  = speedMS.In(Unit.SI.m / Unit.SI.s);  // ensures composite target structure
```

`In` currently supports `double`, `decimal`, and `int` measurements. Unsupported numeric types throw.

## Energy vs Torque (Qualified Formatting)

```csharp
var energy = Quantity.Energy(12);
var torque = new DoubleMeasurement(12, QuantityKinds.Torque.CanonicalUnit);
energy.Format(UnitFormat.Qualified, QuantityKinds.Energy); //  "12 J (Energy)"
torque.Format(UnitFormat.Qualified, QuantityKinds.Torque); //  "12 J (Torque)"
```

## Imperial Interop Example

```csharp
var lift = 100.0.PoundForce();                  // 100 lbf
var height = 6.0.Feet();                        // 6 ft
var workImperial = lift * height;               // (dimensionally energy)
var workQualified = new DoubleMeasurement(workImperial.Value, workImperial.Unit)
    .Format(UnitFormat.Qualified, QuantityKinds.Torque); // depends on semantic intent
```

## Mixed Mode Placeholder

`Mixed` intentionally returns the base factor expression today (no substitution). This keeps substitution cost predictable. Planned enhancement: minimal factor replacement (e.g., kg·m/s^2·m -> N·m) without recursive search explosion.

## Design Notes

* No partial replacement in `DerivedSymbols` avoids ambiguous intermediate expressions.
* Qualified mode only appends kind when symbol belongs to an ambiguous set.
* Kilometre provided via scaling (`value * 1000 m`) to avoid premature proliferation of prefixed singletons.

## Summary Cheat Sheet

```csharp
1. Quantity facade:   var p = Quantity.Power(750); // 750 W
2. SI numeric:        var f = 9.81.Newtons();
3. Imperial numeric:  var d = 3.0.Miles();
4. Formatting:        p.Format(UnitFormat.DerivedSymbols)  // "750 W"
5. Qualified:         p.Format(UnitFormat.Qualified, QuantityKinds.Power)
6. Conversion:        (5.0.Kilometers() / 300.0.Seconds()).In(Unit.SI.m / Unit.SI.s)
```

> Prefer explicit semantics: choose the correct `QuantityKind` when meaning matters (Energy vs Torque) and use `Qualified` formatting for display disambiguation.
