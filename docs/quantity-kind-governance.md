# Quantity Kind Governance & Symbol Policy

This document codifies the governance rules enforced by the test suite for `QuantityKind` definitions.

## Enforcement Goals

Keep semantics explicit and unambiguous without forcing unnecessary churn on widely accepted physics symbols.

## Rules (Tested)

1. Name Uniqueness: Every `QuantityKind.Name` must be globally unique.
2. Symbol Collision Rule (Relaxed):
   - Allowed: Reuse of a symbol (e.g. `V` for `Voltage` and `Volume`) when the underlying canonical units (after reduction) differ.
   - Forbidden (Hard Fail): Two distinct kinds share both the same `Symbol` AND the same reduced canonical unit signature (making them indistinguishable when formatted as value+symbol alone).
3. Dimensionless Tagging: Any kind whose canonical unit is `Unit.None` must include the tag `FormDimensionless`.
4. Metadata Integrity: `Name`, `Symbol`, and `Tags` must be non-null/non-empty.

## Rationale

Physics and engineering disciplines frequently overload single-letter symbols contextually. Enforcing global symbol uniqueness would:

- Break user expectation (standard notation familiarity).
- Generate needless breaking changes and semantic suffix noise (e.g. `V_e`, `S_cond`, etc.).

Instead we guard against *true ambiguity*—cases where two different semantic kinds would render identically in plain formatted output: same symbol and identical underlying dimension/unit expression.

## Examples

| Allowed Overlap | Reason |
|-----------------|--------|
| Voltage (V), Volume (V) | Different dimensions (J/C vs m^3). |
| Entropy (S), ElectricConductance (S) | Different dimensions (J/K vs A^2·s^3/(kg·m^2)). |
| Inductance (H), Enthalpy (H) | Different dimensions (kg·m^2/(s^2·A^2) vs kg·m^2/s^2). |

| Forbidden (Would Fail) | Reason |
|------------------------|--------|
| Two distinct energy-like kinds both named different but symbol `E` and canonical unit J | Ambiguous formatting (value + E). |
| Two dimensionless ratios both `Fo` (same symbol, dimensionless) | Ambiguous unless one is renamed or further qualified. |

## Soft Diagnostics

The governance test emits (debug) a list of overlapping symbols that are allowed under current rules. This is informational—use it during review to decide whether future disambiguation is desirable (e.g., in formatting layers offering optional qualified symbols or domain-aware rendering).

## Future Extensibility

Potential future safeguards (not yet enforced):

- Optional analyzer suggesting domain scoping for newly added overlaps involving more than N distinct kinds.
- Formatting API extension: `Format(options => options.WithQualifiedSymbols())` to output `V(vol)` vs `V(volt)` when ambiguity matters.

## Contributing New Kinds

Before adding a new `QuantityKind`:

1. Confirm a semantically distinct need (avoid redundant synonyms).
2. Reuse existing dimensions—do not duplicate a kind just to change symbol style.
3. If introducing a symbol already in use with identical canonical unit, choose a differentiated symbol (e.g. suffix, subscript form) or raise a design discussion.
4. Add `FormDimensionless` tag for pure ratios / nondimensional groups.
5. Run the test suite to validate governance.

## Test File Reference

Implemented in: `QuantityKindGovernanceTests` under `test/Veggerby.Units.Tests`.

---

This policy balances pragmatic notation with unambiguous computational modeling. If a scenario arises that challenges these constraints, open a discussion before tightening or relaxing the rules further.
