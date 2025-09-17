# Changelog

All notable changes to this project will be documented in this file.

The format roughly follows Keep a Changelog, but categories are tailored to this library's focus on semantic quantity safety and deterministic reduction.

This project adheres to [Semantic Versioning](https://semver.org/) (pre-1.0: minor versions may still include breaking adjustments; see Notes).

## [Unreleased]

### Added

- Quantity semantic guard: Angle no longer silently behaves as generic scalar; participates only via explicit rules (e.g. Torque × Angle → Energy).
- New quantity kinds: Power, Force, Pressure, Volume, Area, Velocity, Acceleration, Momentum, EnergyDensity, SpecificHeatCapacity, SpecificEntropy, plus base Length, Time, Mass.
- Inference rules (single-step only):
  - Power × Time ↔ Energy
  - Force × Length ↔ Energy
  - Pressure × Volume ↔ Energy
  - Pressure × Area ↔ Force
  - Torque × Angle ↔ Energy
  - Entropy × TemperatureAbsolute ↔ Energy
  - Energy ÷ TemperatureAbsolute → Entropy; Energy ÷ Entropy → TemperatureAbsolute
  - Energy ÷ Angle → Torque; Energy ÷ Torque → Angle
- Comparison operators for `Quantity<T>` (same-kind only). Cross-kind comparisons throw early.
- Non-throwing arithmetic: `TryAdd`, `TrySubtract`, `TryMultiply`, `TryDivide` return false instead of throwing on semantic violations.
- Inference registry lifecycle: `QuantityKindInferenceRegistry.Seal()` and `IsSealed` to lock semantic rule set.
- Temperature mean helper: `TemperatureMean.Mean(...)` for safe averaging of absolute temperatures via Kelvin base conversion.
- Documentation: Expanded `docs/quantities.md` with comparison semantics, Try* APIs, sealing rationale, temperature mean usage; updated `quantity-kinds.md` list; README examples for inferred work (P·t, p·V) and semantic gotchas (Energy vs Torque).
- Open semantic tag system (`QuantityKindTag`) replacing closed enum categories; canonical tag instances via `QuantityKindTag.Get(name)`.
- Work and Heat quantity kinds (path energy transfers) + energy transfer aggregation helper `EnergyBalance` (distinct from state functions).
- Tag-based classification for built-in kinds (`Energy.StateFunction`, `Energy.PathFunction`, `Domain.Thermodynamic`, `Domain.Mechanical`, `Form.Dimensionless`).
- Registry behavior tests enhancing semantic inference safety (conflict detection and overwrite scenarios).

### Changed

- Helper parity: `Quantity<T>.Add` / `Subtract` now mirror operator semantics exactly (breaking if prior unsafe cross-kind reliance existed).

- Internal Add/Sub branching centralized; reduced potential for future drift between operators and helpers.
- README, `docs/quantities.md`, and `docs/quantity-kinds.md` updated to reflect tag system (enum removed) and new Work/Heat semantics.
- Built-in kinds now explicitly constructed with tag sets (deterministic metadata snapshot).

### Fixed

- Prevented inadvertent absolute temperature scaling by treating absolute kinds as point-like in scalar fallback logic.
- Ensured Angle does not erode semantic meaning through dimensionless fallback.

### Performance

- No regression: existing equality / reduction benchmarks unaffected (allocation profile unchanged). (Informal validation; formal gate maintained via benchmarks.)

### Notes

- All inference remains single-hop: no transitive chaining introduced (design invariant preserved).
- Registry sealing recommended after application startup to guarantee deterministic semantics.
- Quantity kind categorization is now fully open-ended; downstream libraries can introduce arbitrary domain tags without core modification.

### Removed

- Legacy `QuantityKindCategory` enum and associated constructor/property (superseded by open tag model).

## [0.1.1] - 2025-09-17

### 0.1.1 Added

- Included README and icon assets in NuGet package (improved package metadata & visual identity).

### 0.1.1 Notes

- No functional code changes; packaging/content only.

## [0.1.0] - 2025-09-17

### 0.1.0 Added

- Core unit algebra engine (product/division/power reduction, canonical equality).
- Measurement&lt;T&gt; with int/double/decimal numeric backends and unit-safe arithmetic.
- SI + Imperial unit systems with conversion (including affine temperatures C/F/K).
- Prefix system (yocto ↔ yotta) and scale / derived unit support.
- Reduction settings feature flags (equality normalization, lazy power expansion, factor vector, exponent map path).
- Initial documentation set (README, capabilities, performance notes) and benchmark suite.

### 0.1.0 Notes

- Pre-semantic layer (no quantity kinds yet); additions after this version introduce semantic disambiguation.

---

[Unreleased]: https://github.com/veggerby/Veggerby.Units/compare/v0.1.1...HEAD
[0.1.1]: https://github.com/veggerby/Veggerby.Units/compare/v0.1.0...v0.1.1
[0.1.0]: https://github.com/veggerby/Veggerby.Units/releases/tag/v0.1.0
