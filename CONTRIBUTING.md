# Contributing to Veggerby.Units

Thanks for your interest in contributing to Veggerby.Units.
This library focuses on precise, type-safe composition, comparison and conversion of physical units and measurements. Every thoughtful improvement—tests, docs, bug fixes, or new unit definitions—helps strengthen correctness and trust.

---

## 🚀 Ways to Contribute

There are many ways you can help:

- **Report bugs**: Open an issue with clear reproduction steps (ideally include failing test outline).
- **Propose enhancements**: New units, prefixes, or safe APIs—open an issue to discuss first.
- **Improve documentation**: Clarify operator semantics, dimensional reduction, or examples.
- **Add tests**: Especially for edge cases (prefix combinations, reduction of composite units, negative exponents, cancellation scenarios).
- **Submit code**: Focus on determinism, minimal allocations, and preserving invariants.

---

## 📋 Guidelines

Please follow these basic guidelines to keep everything smooth:

1. **Discuss Larger Changes First**  
   For anything beyond a small bug fix or new simple unit, open an issue to align on scope (prevents churn and duplicate effort).

2. **Keep PRs Focused**  
   Single-purpose PRs are faster to review and less risky.

3. **Always Add/Update Tests**  
   Any new logic (branch, operator path, failure mode) must be covered. Use `AwesomeAssertions` and the Arrange / Act / Assert commenting convention.

4. **Match Style & Semantics**  
   - File-scoped namespaces only.
   - Full curly braces always.
   - No LINQ in hot operator paths unless justified.
   - Preserve existing operator semantics (see `Unit`, `Measurement<T>` and `OperationUtility`).
   - Avoid introducing abstraction layers unless they remove duplication or enforce safety.

5. **Explain the Why**  
   PR description should include: Problem / Approach / Tests Added / Risk & Mitigation.

6. **No Silent Behavioral Changes**  
   If a public operator or method changes semantics, call it out explicitly and justify.

7. **Dimension & Unit Integrity**  
   Never coerce mismatched dimensions; addition/subtraction must throw `UnitException`.

---

## 🛠 Local Setup

- Clone the repository.
- Build the solution (`Veggerby.Units.sln`) using .NET 9 (primary target) — older TFMs may exist for compatibility.
- Run the tests (`Veggerby.Units.Tests`) before pushing.
- Keep test count and coverage stable or increased.

```bash
dotnet restore
dotnet build --configuration Release
dotnet test test/Veggerby.Units.Tests --configuration Release
```

Optional with coverage (locally):

```bash
dotnet test test/Veggerby.Units.Tests \
   --collect:"XPlat Code Coverage" \
   -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura
```

---

## 🧩 Project Structure (Overview)

| Folder | Purpose |
|:-------|:--------|
| `src/Veggerby.Units` | Core library: units, dimensions, prefixes, reduction logic |
| `test/Veggerby.Units.Tests` | xUnit tests (AwesomeAssertions) |
| `docs/` | Supplemental documentation (capabilities, usage examples) |
| `.github/` | CI workflows and contributor guidelines |

---

## 🛡️ Code of Conduct

Be constructive and respectful. Assume good intent. Technical critique is welcome; personal criticism is not.

---

## ✅ Quick PR Checklist

- [ ] Builds clean (`dotnet build`)
- [ ] Tests added/updated & passing (`dotnet test`)
- [ ] No `Assert.` usage in tests (use AwesomeAssertions)
- [ ] Public APIs documented (XML summaries)
- [ ] Operator semantics preserved (compare existing tests)
- [ ] No unintended allocation regressions (esp. in `Unit` operator paths)
- [ ] README/docs updated if user-facing

Thanks for helping make Veggerby.Units more robust.
