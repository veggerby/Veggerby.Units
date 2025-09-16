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

## Structural Equality

`OperationUtility.Equals` performs a shape comparison after ordering product operands by hash code. This affords:

* Commutative insensitivity for products
* Direct structural recursion for division and power
* Prefix + base pairing for prefixed units

Hash codes for composed units rely on reduced form to maintain consistency with equality.

## Complexity Overview

| Operation | Complexity (n = operand count) |
|-----------|--------------------------------|
| Reassociate multiplication | O(n) inspection |
| Reassociate division | O(1) pattern matching |
| Reduce multiplication | O(n) flatten + group |
| Reduce division | O(n log n) (group + cancellation) |
| Expand power | O(n) for composite base, O(1) otherwise |
| Equality (product) | O(n log n) due to ordering |

## Thread Safety

All operations are pure and allocate only transient collections. No shared mutable state is used.

## Design Choices

* Avoided LINQ in some critical spots would marginally reduce allocations but current clarity favored; revisit if profiling shows hotspots.
* Hash ordering during equality ensures stable comparison independent of initial authoring order.

## Future Extensions

* Potential introduction of exponent map structure to reduce intermediate allocations in heavy algebra scenarios.
* Consider specialized small-vector optimization for very frequent binary products.
