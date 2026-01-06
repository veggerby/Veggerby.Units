# Repository Guidelines

## Project Structure & Module Organization
- `src/Veggerby.Units`: core library (units, dimensions, reduction logic, formatting).
- `test/Veggerby.Units.Tests`: xUnit tests with AwesomeAssertions.
- `bench/Veggerby.Units.Benchmarks`: BenchmarkDotNet performance suite.
- `samples/`: sample apps (analyzers, usage).
- `docs/`: architecture, formatting, and domain documentation.
- Root config: `Veggerby.Units.sln`, `Directory.Build.props`, `Directory.Packages.props`, `.editorconfig`.

## Build, Test, and Development Commands
- `dotnet restore`: restore NuGet packages.
- `dotnet build --configuration Release`: build the solution (target .NET 9).
- `dotnet test test/Veggerby.Units.Tests --configuration Release`: run tests.
- `dotnet run -c Release --project bench/Veggerby.Units.Benchmarks`: run benchmarks.
- `dotnet format`: apply repo formatting rules before committing.

## Coding Style & Naming Conventions
- Indentation: 4 spaces; tabs forbidden; CRLF line endings.
- File-scoped namespaces, full braces on all control flow.
- Prefer clarity in expressions; add parentheses in mixed arithmetic.
- Naming: private fields `_camelCase`, constants `PascalCase`.
- Tests: use `AwesomeAssertions` (no `Assert.`) and Arrange/Act/Assert comments with a blank line before each section.
- See `docs/style-formatting.md` for authoritative rules.

## Testing Guidelines
- Frameworks: xUnit + AwesomeAssertions.
- Naming: `GivenCondition_WhenAction_ThenOutcome` (see `docs/style-formatting.md` template).
- Coverage: keep test count and coverage stable or increased; add tests for new logic and edge cases.
- Run: `dotnet test test/Veggerby.Units.Tests --configuration Release`.

## Commit & Pull Request Guidelines
- Commit messages are short, imperative; common prefixes include `chore:` and `fix:` and PR numbers in parentheses.
- Keep PRs focused; discuss larger changes in an issue first.
- PR description should include: Problem / Approach / Tests Added / Risk & Mitigation.
- Call out any behavioral change in public operators or APIs.

## Performance & Safety Notes
- Avoid LINQ in hot operator paths unless justified.
- Preserve unit/dimension invariants; mismatched dimensions must throw `UnitException`.
- For benchmark work, use Release builds and keep results reproducible (see `docs/performance.md`).
