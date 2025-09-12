<!--
GitHub Copilot / AI Contributor Instructions for the Veggerby.Units codebase.
These rules are binding for any AI-generated contribution.
-->

# Veggerby.Units – AI Contribution Guidelines

Veggerby.Units is a .NET library for defining, composing, converting and comparing physical units and typed measurements. All AI assistance must produce code that is correct, deterministic, minimal in unnecessary abstraction, and consistent with existing operator semantics and reduction logic.

---
## 1. Non‑Negotiable Principles
1. Determinism over cleverness (no hidden randomness in unit math or conversions).
2. Clarity over abstraction bloat (new types only when they add clear semantic or safety value).
3. Arithmetic correctness: operator overloads must preserve invariants (commutativity where expected, associativity for multiplication, dimensional consistency for + / -).
4. Dimension integrity: never allow addition/subtraction of incompatible dimensions; always throw `UnitException` early.
5. Public surface stable & documented (XML docs for all public types/members).

---
## 2. Style & Formatting
Follow `.editorconfig` exactly.

Mandatory:
- File-scoped namespaces only (`namespace Veggerby.Units;`).
- Namespace path must mirror folder structure.
- Full curly braces for every control statement (even single line).
- Nullable reference types honored; no suppressions without justification.
- Private fields: `_camelCase`; public members: PascalCase.
- Remove unused `using` directives; order consistently (System first if added, then project namespaces).
- Keep vertical space for readability (especially in tests: separate Arrange / Act / Assert blocks).
- No banner comments, generated disclaimers, or humorous prose.

Preferred:
- Use expression-bodied members only when they materially improve clarity.
- Keep operators small; extract helpers only if reused or complex.

Prohibited:
- Region folding (`#region`) – not used in this codebase.
- Blocking on async (`.Result`, `.Wait()`). (Currently the library is synchronous; future async additions must follow this rule.)

---
## 3. Architectural Boundaries
- Core abstractions: `Unit`, `Measurement<T>`, `Dimension`, `Prefix` plus concrete composites (`ProductUnit`, `DivisionUnit`, `PowerUnit`, `PrefixedUnit`).
- Reduction & algebraic normalization lives in `OperationUtility` and related visitor types—do not duplicate simplification logic elsewhere.
- `Unit` operator overloads must:
  - Short-circuit `Unit.None` appropriately.
  - Maintain dimensional correctness (only + and - enforce equality of dimensions & identity of units).
  - Use `OperationUtility` for rearrangement / reduction before allocating new composite units.
- Do not leak internal normalization strategies through public APIs.
- Avoid mutable global/static state other than well-defined singletons like `Unit.SI` and `Unit.Imperial` (these must stay immutable after construction).

---
## 4. Semantics & Invariants
- Equality (`==`, `!=`) for `Unit` must compare structural & dimensional equivalence via existing `Equals` implementation (already delegates to reduction-based equality). Do not re-implement structural matching ad hoc.
- Addition/Subtraction of `Unit` return the common unit instance (left) when identical; otherwise throw.
- Addition/Subtraction of `Measurement<T>` sum/subtract numeric values only when units are identical (reference or structural equality via `==`).
- Multiplication/Division must attempt reduction (e.g., cancelling reciprocal factors, merging powers) before constructing a new composite.
- Power expansion for negative exponents: `u ^ -n` => `1 / (u ^ n)` must remain allocation-minimal (reuse existing logic).
- Prefix application: ensure invalid numeric factors throw `PrefixException`.

---
## 5. Testing Requirements
All new code paths must be covered by xUnit tests under `test/Veggerby.Units.Tests`.

Rules:
- Use `AwesomeAssertions` exclusively for assertions (no `Assert.*` except `[Fact]` attribute usage).
- Follow Arrange / Act / Assert comment sections exactly (`// Arrange`, `// Act`, `// Assert`).
- Cover for each new operator / logic branch:
  - Happy path (expected reduction / conversion).
  - Edge cases: `Unit.None`, identity exponent (0/1), negative exponents, mixed prefix + base, reciprocal cancellation.
  - Exception path: incompatible dimensions for + / -, invalid prefix factor, null comparison semantics.
- Conversions: test round numeric factors (e.g. km→m, ft→m) and composite conversions (e.g. km/s to m/s) where added.
- Guard regressions on inequality by including explicit `==` and `!=` symmetry tests for new composite structures.
- No external dependencies; keep everything in-memory and deterministic.

Patterns:
```csharp
// Arrange
var left = new DoubleMeasurement(3.5, Prefix.k * Unit.SI.m);

// Act
var metres = left.ConvertTo(Unit.SI.m);

// Assert
((double)metres).Should().Be(3500d);
```

Exception example:
```csharp
// Arrange
var a = new DoubleMeasurement(1, Unit.SI.m);
var b = new DoubleMeasurement(2, Unit.SI.s);

// Act
var act = () => _ = a + b;

// Assert
act.Should().Throw<UnitException>();
```

---
## 6. Performance Considerations
- Operator chains (`*`, `/`, `^`) should minimize intermediate allocations—prefer using existing `OperationUtility` helpers before constructing new composite unit instances.
- Avoid LINQ in hot operator paths; explicit loops or existing utilities preferred.
- Do not introduce caching without clear eviction or immutability guarantees.

---
## 7. Documentation
- Every public type and public member: XML `<summary>`; parameters documented; `<remarks>` for non-trivial reduction or comparison logic.
- When adding new composite unit behaviors, document invariants (e.g. commutativity assumptions, normalization order) succinctly.
- Update `README.md` or `docs/` if adding new public-facing capabilities (e.g. new unit systems, extension methods for conversion, or new numeric measurement types).

---
## 8. Dependency Policy
- Keep dependencies minimal. No additional assertion/mocking libraries beyond what is already in the test project (currently only xUnit + AwesomeAssertions). Avoid adding mocking frameworks unless absolutely necessary (prefer direct construction over mocks—current code has no interfaces needing heavy mocking).

---
## 9. Forbidden Patterns
🚫 Raw `Assert.*` usage in tests (except `[Fact]`).
🚫 Hidden implicit conversions that alter dimensional meaning silently.
🚫 Inconsistent inequality logic (never reintroduce the previous `!=` regression where identical references were treated as unequal).
🚫 Global mutable state beyond defined static readonly singletons.
🚫 Silent unit coercion during addition/subtraction (must throw on mismatch).

---
## 10. Pull Request Checklist (AI Contributions)
Before submitting AI-generated changes:
- [ ] Build passes (`dotnet build`).
- [ ] All tests green (`dotnet test`).
- [ ] New logic covered by tests (happy + edge + exception paths).
- [ ] No remaining `Assert.` usages.
- [ ] Public APIs documented (XML docs).
- [ ] No analyzer/style warnings introduced.
- [ ] Operator semantics preserved (run existing measurement & unit tests to confirm).
- [ ] README/docs updated if user-visible change.

---
## 11. Suitable Task Examples
- "Add tests covering reciprocal cancellation in chained division and multiplication."
- "Optimize multiplication operator to reduce nested ProductUnit depth; add tests."
- "Add conversion helper for squared units (e.g. km^2 to m^2) with tests."
- "Document and test negative exponent handling for prefixed units."

Unsuitable (needs clarification first):
- Vague performance requests ("make multiplication faster") without identified hotspot.
- Adding external dependencies for marginal syntactic sugar.

---
## 12. Failure Handling & Assumptions
When information is missing:
1. Infer the minimal implementation consistent with current patterns (e.g. follow existing `OperationUtility` style).
2. Mark assumptions clearly in PR description.
3. Provide follow-up list if a broader refactor would be beneficial but out of current scope.

---
## 13. Security & Safety
- No user input parsing presently; if introducing any, validate and constrain.
- Do not log internal structural decomposition of units unless behind explicit diagnostic flag (none currently exist—add one only with design discussion).

---
## 14. Final Reminder
Favor small, verifiable changes. If a proposed abstraction does not directly reduce duplication or clarify semantics (units, dimensions, reduction), do not introduce it.

---
End of Veggerby.Units guidelines.
