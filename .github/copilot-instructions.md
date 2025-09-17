# Copilot Instructions – Veggerby.Units

These rules are binding for any AI-generated code in this repo.
**Follow exactly.**

---

## 1. Purpose & Framing

You are contributing to **Veggerby.Units**, a focused .NET library for strongly-typed measurements, deterministic dimensional reduction, and algebra over physical units.

### What this library is
- A **structural algebra engine** for units: products, divisions, powers, prefixes, and **deterministic reduction** (cancellation + exponent aggregation).
- A **typed measurement** layer (`Measurement<T>`) that performs safe arithmetic and conversions across **SI ↔ Imperial** (and custom scale units).
- A **semantic (quantity) layer** (Energy vs Torque, Absolute vs Delta Temperature, etc.) that prevents meaning drift even when dimensions match.

### What this library is not
- Not a UI/formatting helper, not a physics simulator, and not a string parser (parsing is **planned**, not present).
- Not a place for clever abstractions that hide math or add global state.
- Not a silent coercion factory: **incompatible additions/subtractions must throw**.

### Mental model (authoritative)
- **Equality is canonical and order-independent.** `(m*s)^2 == m^2*s^2`, `A*B == B*A`, and division is negative exponents.
- **Reduction is centralized.** Only use `OperationUtility` for rearrangement/reduction; do not duplicate simplification logic.
- **Operator chains minimize allocations.** Prefer fast paths and reuse over LINQ or temporary composites.
- **Dimension integrity is sacrosanct.** `+`/`-` require identical units (or convert measurements first). Otherwise: throw `UnitException`.
- **Quantities add meaning atop dimensions.** Same dimensions (J) can be different kinds (Energy vs Torque) and **must not** add together.

### Temperature rules (affine units)
- °C/°F/K as **absolute** temperatures (affine over Kelvin) support **direct conversions and comparisons** only.
- Affine units **cannot** be multiplied/divided/prefixed or raised to power > 1 — doing so **throws**.
- Distinguish **Absolute** vs **Delta**:
  - `Absolute – Absolute → Delta`
  - `Absolute ± Delta → Absolute`
  - `Delta ± Delta → Delta`
  - Absolute scaling by scalar is **not** allowed; Delta scaling is allowed.

### Quantity semantics (when present)
- Same-kind `+`/`-` only when allowed by the kind.
- Multiplication/Division: only via **explicit inference rules** or if one side is dimensionless. No transitive inference chains.
- Angle remains dimensionless but does **not** infect other kinds without an explicit rule.

### Copilot’s default behavior
1. **Prefer correctness and determinism** over abstraction or micro-optimizations unless guided by existing patterns.
2. **Use existing utilities** (`OperationUtility`, equality normalization) before allocating new composites.
3. **Throw early** on dimension or kind violations; never coerce silently.
4. **Keep public APIs documented** (XML docs) and stable.
5. **Follow the style guide** and `.editorconfig` exactly (spaces, braces, naming, spacing).

### Safe patterns (do this)
- Use `OperationUtility` to cancel reciprocals, merge powers, and normalize products before returning.
- Ensure `u ^ -n` → `1 / (u ^ n)` with minimal allocations.
- For measurements, convert to a common unit before comparison; fail fast on incompatible conversions.
- Cover new branches with **xUnit + AwesomeAssertions** tests (happy, edge, exception).

### Unsafe patterns (never do this)
- ❌ Adding `Measurement<T>` with different units without converting.
- ❌ Adding quantities of different kinds (e.g., Energy + Torque).
- ❌ Multiplying/dividing affine temperatures or prefixing them.
- ❌ Duplicating reduction logic outside `OperationUtility`.
- ❌ Introducing global mutable state or hidden randomness.

> **TL;DR for Copilot:** Model the math, reduce deterministically, throw on ambiguity, and keep semantics explicit.
> If a behavior isn’t clearly supported by an existing pattern or rule, **don’t invent one**—raise, document, or propose an explicit rule.

---

## 2. Core Principles
1. Determinism > cleverness (no hidden randomness).
2. Clarity > abstraction bloat (new types only if semantically necessary).
3. Arithmetic correctness: preserve invariants (commutativity, associativity, dimensional consistency).
4. Dimension integrity: disallow `+`/`-` across incompatible dimensions → throw `UnitException`.
5. Stable, documented public surface (XML docs required).

---

## 3. Style & Formatting
- Follow `.editorconfig` **exactly**.
- File-scoped namespaces only (`namespace Veggerby.Units;`).
- Braces mandatory for all control flow, even single lines.
- Spaces only (4 per indent). No tabs, no trailing whitespace.
- Usings: `System` first, blank line, then others. Keep outside namespace.
- Private fields: `_camelCase`. Public: PascalCase. Constants: PascalCase.
- Remove unused usings; no `#region`.
- Expression-bodied members only if trivially clearer.
- Parentheses for clarity in mixed expressions.
- Tests: strict `// Arrange`, `// Act`, `// Assert` sections with one blank line before each.

**Example operator:**
```csharp
public static Unit operator *(Unit left, Unit right)
{
    if (left == Unit.None)
    {
        return right;
    }

    if (right == Unit.None)
    {
        return left;
    }

    if (IsAffine(left) || IsAffine(right))
    {
        throw new UnitException(left, right);
    }

    return OperationUtility.RearrangeMultiplication(
        x => x.Multiply((a, b) => a * b, Unit.None),
        (x, y) => x / y,
        left, right)
    ?? OperationUtility.ReduceMultiplication(
        x => x.Multiply((a, b) => a * b, Unit.None),
        (x, y) => x ^ y,
        left, right)
    ?? Unit.Multiply(left, right);
}
````

---

## 4. Architecture Boundaries

* Core abstractions: `Unit`, `Measurement<T>`, `Dimension`, `Prefix`, composites (`ProductUnit`, `DivisionUnit`, `PowerUnit`, `PrefixedUnit`).
* Algebraic normalization lives in `OperationUtility` – do not duplicate.
* `Unit` operators must:

  * Short-circuit `Unit.None`.
  * Validate dimension equality for `+`/`-`.
  * Use `OperationUtility` for reduction.
* No mutable global state beyond immutable singletons (`Unit.SI`, `Unit.Imperial`).

---

## 5. Semantics & Invariants

* Equality (`==`, `!=`): must use `Equals` (structural + dimensional).
* `+`/`-` on units: return left if identical, else throw.
* `+`/`-` on measurements: allowed only if units identical.
* `*` and `/`: must attempt reduction (cancel reciprocals, merge powers).
* Power: `u ^ -n` → `1 / (u ^ n)` with minimal allocations.
* Prefixes: invalid numeric factors throw `PrefixException`.

---

## 6. Testing

* Framework: xUnit + AwesomeAssertions. No raw `Assert.*` except `[Fact]`.
* Every new branch tested: happy path, edge cases, exceptions.
* Tests must be deterministic, no randomness.

Template:

```csharp
[Fact]
public void GivenCondition_WhenAction_ThenOutcome()
{
    // Arrange
    var m1 = new Int32Measurement(2, Unit.SI.m);
    var m2 = new Int32Measurement(3, Unit.SI.m);

    // Act
    var sum = m1 + m2;

    // Assert
    sum.Value.Should().Be(5);
}
```

---

## 7. Performance

* Minimize allocations in operator chains (`*`, `/`, `^`).
* Prefer explicit loops/utilities; no LINQ in hot paths.
* Don’t add caching unless safe (immutable or eviction defined).

---

## 8. Documentation

* XML docs for all public types/members.
* Document invariants in `<remarks>` when non-trivial.
* Update `README.md`/`docs/` for new public features.

---

## 9. Dependency Policy

* Keep dependencies minimal.
* Tests: only xUnit + AwesomeAssertions. No mocking libraries.

---

## 10. Forbidden Patterns

🚫 Raw `Assert.*` in tests.
🚫 Hidden implicit conversions changing dimension meaning.
🚫 Global mutable state (beyond defined singletons).
🚫 Silent coercion in `+`/`-` (must throw).
🚫 Tabs, missing braces, trailing whitespace.

---

## 11. PR Checklist

Before PR:

* [ ] `dotnet build` passes.
* [ ] `dotnet test` all green.
* [ ] New code covered by tests (happy/edge/exception).
* [ ] No analyzer/style warnings.
* [ ] XML docs added.
* [ ] Operator semantics preserved.
* [ ] Docs updated if public behavior changed.

---

## 12. Suitable Tasks

✅ Add reciprocal cancellation tests.
✅ Optimize multiplication operator depth.
✅ Add km²↔m² conversion helper + tests.
✅ Document/test negative exponent behavior.

❌ “Make multiplication faster” (too vague).
❌ Add external dependencies for syntactic sugar.

---

## 13. Missing Info

* If unclear: follow existing `OperationUtility` patterns.
* Document assumptions in PR description.
* Suggest bigger refactors separately (not inline).

---

## 14. Safety

* No user input parsing yet; validate if added.
* No logging of internals unless behind explicit diagnostic flag.

---

**Final rule:** Favor *small, verifiable changes*. If a new abstraction doesn’t reduce duplication or clarify semantics, **don’t add it**.
