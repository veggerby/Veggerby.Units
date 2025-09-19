# VUNITS001: Incompatible units in additive operation

| Property | Value |
|----------|-------|
| **Category** | Correctness |
| **Severity** | Error |
| **Enabled by default** | Yes |
| **Code Fix** | Yes (wrap RHS with `ConvertTo(left.Unit)`) |

## Summary

Reports when two `Measurement<T>` (or related) operands in `+` or `-` have different `Unit` instances, which would silently produce dimensionally invalid arithmetic if allowed. The library contract requires identical units for additive operations; conversions must be explicit.

## Motivation

Dimensional addition/subtraction is only valid when magnitudes are expressed in the *same concrete unit*. Allowing mixed units encourages subtle mistakes (e.g. meters + kilometers) that depend on implicit conversion rules and can mask logic errors. Enforcing explicit conversion keeps intent obvious and prevents accidental mixing of unrelated dimensions that happen to share structure after reduction.

## Detected Patterns

```csharp
var a = new Int32Measurement(5, Unit.SI.m);
var b = new Int32Measurement(20, Unit.SI.km);
var c = a + b; // VUNITS001
```

Also flagged when constructed inline:

```csharp
var c = new Int32Measurement(5, Unit.SI.m) + new Int32Measurement(2, Unit.SI.cm); // VUNITS001
```

Or via member access to a `.Unit` property:

```csharp
Measurement<int> left = GetDistance();      // left.Unit == m
Measurement<int> right = GetAltitude();     // right.Unit == ft
var sum = left + right; // VUNITS001
```

## Non-Issues

Operands with identical unit references:

```csharp
var a = new Int32Measurement(5, Unit.SI.m);
var b = new Int32Measurement(12, Unit.SI.m);
var c = a + b; // OK
```

Chained additions already fixed:

```csharp
var c = a + b.ConvertTo(a.Unit); // OK
```

## Code Fix

Applies an explicit RHS conversion to the LHS unit:

```csharp
var c = a + b; // before
var c = a + b.ConvertTo(a.Unit); // after
```

If the operation is subtraction, the same pattern is applied (`left - right.ConvertTo(left.Unit)`).

The fix avoids duplication when the RHS is already a `ConvertTo(...)` invocation.


## Limitations

- Analyzer uses semantic model to resolve the `Unit` from constructor second argument or `.Unit` property. Extremely dynamic flows (e.g. passing through `object`) will not be flagged.
- Does not currently attempt operand *swapping* (i.e., converting the left to right's unit). Deterministic choice keeps diffs predictable.

## Suppression Guidance

If you intentionally compare different units *after external normalization*, convert both to a common unit first:

```csharp
var total = a.ConvertTo(Unit.SI.m) + b.ConvertTo(Unit.SI.m); // clear & self-explanatory
```

Suppressing the diagnostic (e.g. `#pragma warning disable VUNITS001`) is discouraged; explicit conversion communicates intent.


## Rationale

Invariant preservation: additive closure is only guaranteed inside a single concrete unit representation. Making conversion *visible* maintains transparency and tests can assert the unit chosen.

## Related

- `Measurement<T>.ConvertTo(Unit target)`
- `VUNITS002` (format ambiguity) – orthogonal; a different semantic concern.
