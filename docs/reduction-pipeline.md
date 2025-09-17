# Reduction & Equality Pipeline

This document describes the internal normalization and equality algorithm used by `Veggerby.Units` after the deterministic lazy power expansion work (Steps 1–8).

## Goals

* Deterministic structural equality regardless of operand construction order.
* Order-agnostic handling of products, divisions, and powers (including lazy `(Product)^n` forms).
* Minimal allocations: factor accumulation performed with simple dictionaries (not full AST cloning).
* Idempotent equality: two consecutive equality calls must always return the same result.

## Canonical Equality Algorithm

For any pair of operands `(left, right)`:

1. Quick reference / null checks.
2. If either operand is algebraic (Product / Division / Power) and `ReductionSettings.EqualityNormalizationEnabled` is `true` (default):
   1. Recursively accumulate factors for each side into a multiset `base -> exponent`:
      * Product: sum exponents of each operand.
      * Division: dividend exponents positive; divisor exponents negative.
      * Power: multiply nested exponent into accumulated value.
      * Leaf (basic / prefixed / scale): add +/-1.
   2. Remove zero exponent entries.
   3. Compare the multisets for structural base equality and identical exponents (searching structurally if references differ).
3. If normalization disabled, optionally canonicalize `(Product)^n` forms when lazy power expansion is on (legacy path).
4. Fall back to structural comparison for non‑algebraic leaves only.

This guarantees `(A*B)^n` equals `A^n * B^n` (eager) without expanding the lazy form.

## Normalization Idempotence

Running factor accumulation multiple times over the same operand produces identical `(symbol, exponent)` ordered output. Tests assert this for diagnostic stability.

## Flags

| Flag | Default | Purpose |
| ---- | ------- | ------- |
| `LazyPowerExpansion` | false | Defer distribution of powers over products/divisions. |
| `EqualityNormalizationEnabled` | true | Activate canonical multiset equality for algebraic nodes. |
| `UseFactorVector` | false | Opt-in cached factor vectors on composite nodes (allocation reduction). |
| `EqualityUsesMap` | false (legacy) | Previous hash-bucket product equality; superseded by normalization path. |

`EqualityNormalizationEnabled` supersedes `EqualityUsesMap` and structural zip ordering for algebraic nodes.

## Determinism Guarantees

* Canonical factor accumulation is purely functional given the operand graph.
* No global caches mutate during equality evaluation (factor vectors are per-instance lazy caches gated by `UseFactorVector`).
* Tests cover:
  * Lazy vs eager power parity (`(m*s*kg*m)^5` vs `m^10 s^5 kg^5`).
  * Adversarial operand orderings.
  * Idempotent consecutive equality calls.
  * Cache independence (cold vs warm).
  * Property-based fuzz over random bags and exponents in `[-5,5] \ {0}`.

## Benchmarks

Additional benchmarks (`EqualityBenchmarks`) include lazy vs eager cases for factor counts N=5,10,25. The canonical path must stay within 1% time regression relative to prior structural approach and should not increase allocations.

## Failure Diagnostics

`OperationUtility.TryGetCanonicalFactorsForDiagnostics` exposes a stable, ordered `(symbol, exponent)` list for test failure dumps, ensuring reproducible diffs.

## Future Work

* Potential pooling of temporary dictionaries in equality (micro-optimization) if profiling justifies.
* Extension of factor caching to carry dimension + unit combined canonical keys (requires design).

## Summary

Equality now depends solely on canonical factor multisets; ordering, nesting, and lazy power distribution no longer influence outcomes. This removes prior flakiness and simplifies reasoning about composite equivalence.
