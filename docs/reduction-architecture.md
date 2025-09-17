# Reduction Architecture

This document describes the internal structural normalisation pipeline used by Veggerby.Units to ensure that
algebraically equivalent unit (and dimension) expressions converge to a canonical representation.

## Goals

* Deterministic structural equality and hashing
* Minimal allocation during operator chains
* Early cancellation of reciprocal factors
* Stable ordering that ignores authoring order of commutative operations

## Core Abstractions

| Abstraction | Purpose |
|-------------|---------|
| `IOperand` | Marker for anything reducible (unit or dimension composite) |
| `IProductOperation` | Commutative multiplicative group of operands |
| `IDivisionOperation` | Represents a single division with explicit dividend / divisor |
| `IPowerOperation` | Represents exponentiation (positive exponent in this layer) |

All algorithms operate against these interfaces so Units and Dimensions share identical reduction semantics.

## Normalisation Steps

1. Reassociate multiplication to hoist embedded division: `A * (B / C)` → `(A * B) / C`.
2. Collapse nested division: `(A/B)/(C/D)` → `(A * D) / (B * C)`.
3. Linearize multiplication: flatten nested product trees.
4. Aggregate duplicate factors: `A * A * A * B` → `A^3 * B`.
5. Cancel factors across division: `(A * C)/(A * B)` → `C / B`.
6. Expand powers over composites: `(A*B)^n` → `A^n * B^n`; `(A/B)^n` → `A^n / B^n`; `(A^m)^n` → `A^(m*n)`.

These steps are applied opportunistically by operator overloads; no single master pipeline runs every step unconditionally.

## Structural Equality (Canonical Form)

`OperationUtility.Equals` now uses a canonical factor multiset normalisation for all algebraic nodes (product / division / power) when `ReductionSettings.EqualityNormalizationEnabled` is `true` (default):

1. Recursively accumulate factors into `base -> exponent` (products sum, divisions subtract, powers multiply exponents).
2. Remove zero entries; ordering is irrelevant to correctness (dictionary lookup); a stable ordering is only used for diagnostics.
3. Compare exponent values, performing structural leaf comparison if references differ.

This eliminates dependence on operand authoring order and lazy power distribution. Legacy hash/sort based product comparison (`EqualityUsesMap` / sort+zip) remains only for diagnostic A/B testing and is bypassed under normalization.

Hash codes still derive from reduced structural form to preserve consistency with equality.

## Complexity Overview

| Operation | Complexity (n = operand count) |
|-----------|--------------------------------|
| Reassociate multiplication | O(n) inspection |
| Reassociate division | O(1) pattern matching |
| Reduce multiplication | O(n) flatten + group |
| Reduce division | O(n log n) (group + cancellation) |
| Expand power | O(n) for composite base, O(1) otherwise |
| Equality (algebraic, normalized) | O(n + k) expected (hash map accumulation + structural leaf matches) |
| Equality (legacy product path) | O(n log n) due to ordering |

## Thread Safety

All operations are pure and allocate only transient collections. No shared mutable state is used.

## Design Choices

* Canonical normalization introduced for determinism; legacy ordering retained only for fallback/experimentation.
* LINQ remains in non-hot paths; normalization uses explicit recursion + dictionary to minimize allocations.

## Future Extensions

* Potential pooling of the temporary factor dictionaries or custom lightweight map to further reduce allocations.
* Specialized small-vector optimization for very small products (n<=3) if benchmark-supported.
