# Changelog

All notable changes to this project will be documented in this file.

The format roughly follows Keep a Changelog, but categories are tailored to this library's focus on semantic quantity safety and deterministic reduction.

This project adheres to [Semantic Versioning](https://semver.org/) (pre-1.0: minor versions may still include breaking adjustments; see Notes).

## [Unreleased]

### Added

- Quantity semantic guard: Angle no longer silently behaves as generic scalar; participates only via explicit rules (e.g. Torque × Angle → Energy).
- New quantity kinds: Power, Force, Pressure, Volume, Area, Velocity, Acceleration, Momentum, EnergyDensity, SpecificHeatCapacity, SpecificEntropy, plus base Length, Time, Mass.
- Electromagnetic quantity kinds: Charge, Current, Voltage, Resistance, Conductance, Capacitance, Inductance, MagneticFlux, MagneticFluxDensity, ElectricFieldStrength, MagneticFieldStrength, Permittivity, Permeability, ElectricDipoleMoment, MagneticDipoleMoment, ElectricPotentialEnergy, PowerSpectralDensity, ChargeDensity.
- Imperial & SI unit extensions: fathom, cable, nautical mile, psi, pound-force, foot-pound, horsepower, radian, steradian, agricultural volumes (peck, bushel, barrel, tun), specialized weight units, square inch/foot/yard/mile area units.
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
- Electromagnetic inference rules: Current × Time ↔ Charge; Voltage × Charge ↔ Energy; Current × Voltage ↔ Power.
- Non-throwing arithmetic: `TryAdd`, `TrySubtract`, `TryMultiply`, `TryDivide` return false instead of throwing on semantic violations.
- Inference registry lifecycle: `QuantityKindInferenceRegistry.Seal()` and `IsSealed` to lock semantic rule set.
- Temperature mean helper: `TemperatureMean.Mean(...)` for safe averaging of absolute temperatures via Kelvin base conversion.
- Documentation: Expanded `docs/quantities.md` with comparison semantics, Try* APIs, sealing rationale, temperature mean usage; updated `quantity-kinds.md` list; README examples for inferred work (P·t, p·V) and semantic gotchas (Energy vs Torque).
- Open semantic tag system (`QuantityKindTag`) replacing closed enum categories; canonical tag instances via `QuantityKindTag.Get(name)`.
- Electromagnetic canonical unit documentation and corrections (Resistance, Conductance, Capacitance, Inductance) aligned with SI derivations.
- Work and Heat quantity kinds (path energy transfers) + energy transfer aggregation helper `EnergyBalance` (distinct from state functions).
- Tag-based classification for built-in kinds (`Energy.StateFunction`, `Energy.PathFunction`, `Domain.Thermodynamic`, `Domain.Mechanical`, `Form.Dimensionless`).
- Registry behavior tests enhancing semantic inference safety (conflict detection and overwrite scenarios).

#### Advanced Extensions (Second Wave)

- Mechanics / Dynamics: Impulse, Action, SpecificAngularMomentum, Torsion.
- Thermal & Transport: VolumetricHeatCapacity, SpecificWeight.
- Optical / Radiative: Emissivity, Absorptivity, Reflectivity, Transmissivity, SpectralRadiance, SpectralIrradiance, SpectralFlux, OpticalPathLength, RefractiveIndex.
- Fluid / Dimensionless Groups: ReynoldsNumber, PrandtlNumber, NusseltNumber, SchmidtNumber, SherwoodNumber, BiotNumber.
- Chemistry / Material: PartialPressure, Activity, ActivityCoefficient, MassAttenuationCoefficient, HenrysConstant.
- Electromagnetic Advanced: ElectricalConductivity, ElectricalResistivity, SurfaceChargeDensity, LineChargeDensity, SurfaceCurrentDensity, Polarization, BoundCurrentDensity.

#### Additional Inference Rules (Second Wave)

- Force × Time ↔ Impulse; Energy × Time ↔ Action; AngularMomentum ÷ Mass → SpecificAngularMomentum.
- HeatCapacity ÷ Volume → VolumetricHeatCapacity; Force ÷ Volume → SpecificWeight.
- Radiance ÷ Length → SpectralRadiance; Irradiance ÷ Length → SpectralIrradiance; RadiantFlux ÷ Length → SpectralFlux.
- Pressure × MoleFraction ↔ PartialPressure.
- ActivityCoefficient × MoleFraction ↔ Activity.
- Conductivity × ElectricFieldStrength → ElectricCurrentDensity (and related resistivity forms).

#### Deferred / Omitted (Rationale)

- ThermalEffusivity: Requires fractional exponents (sqrt of conductivity·density·heat capacity) not currently modeled; postponed until fractional exponent support is introduced to unit algebra.
- General RateConstant (k): Order-dependent dimensionality (e.g., molarity exponents vary). Library avoids introducing a polymorphic kind whose dimension changes with reaction order—users should model specific forms explicitly when needed.
- HenrysConstant inference: Multiplicative mapping with MoleFraction intentionally omitted to preserve unambiguous PartialPressure ÷ MoleFraction → Pressure rule (conflict surfaced during registry sealing). HenrysConstant remains a kind for value storage without inference participation.

#### Extended Semantic Surface (this iteration)

- Extensive new quantity kinds grouped by domain:
  - Geometry / Kinematics: SolidAngle, AngularVelocity, AngularAcceleration, AngularMomentum, MomentOfInertia, Jerk, Wavenumber, Curvature, Strain, StrainRate.
  - Flow & Rates: VolumetricFlowRate, MassFlowRate, MolarFlowRate.
  - Materials & Mechanics: YoungsModulus, ShearModulus, BulkModulus, Compressibility, LinearMassDensity, ArealMassDensity, NumberDensity, ArealNumberDensity, Porosity.
  - Thermal: ThermalResistance, ThermalConductance, HeatTransferCoefficient, MolarHeatCapacity, SpecificEnthalpy.
  - Electromagnetics (additions): Magnetization, ElectricDisplacement, MagneticVectorPotential, Impedance, Admittance, ChargeMobility.
  - Radiometry & Photometry: RadiantFlux, RadiantIntensity, Irradiance, Radiance, RadiationExposure, Luminance, LuminousIntensity, LuminousEfficacy.
  - Chemistry & Transport: DiffusionCoefficient, ReactionRate, MoleFraction, MassFraction, OsmoticPressure.
  - Generic / Dimensionless: DimensionlessRatio (explicit semantic ratio discriminator).
- Factory helpers added for all new kinds (`QuantityFactories.Additional`).
- New single-step inference rules (preserving no multi-hop expansion):
  - AngularVelocity × Time → Angle; Angle ÷ Time → AngularVelocity.
  - AngularAcceleration × Time → AngularVelocity.
  - Jerk × Time → Acceleration.
  - VolumetricFlowRate × Time → Volume; MassFlowRate × Time → Mass; MolarFlowRate × Time → CatalyticActivity.
  - ThermalConductance × TemperatureDelta → Power; Power × ThermalResistance → TemperatureDelta.
  - Corresponding inverse (division) rules for each of the above where semantically unambiguous.

#### Developer Tooling / Safety

- Static initialization safety: new kinds avoid cross-kind field references by expressing canonical units directly in base SI algebra (prevents partial static ordering faults observed during development).

### Changed

- Helper parity: `Quantity<T>.Add` / `Subtract` now mirror operator semantics exactly (breaking if prior unsafe cross-kind reliance existed).

- Internal Add/Sub branching centralized; reduced potential for future drift between operators and helpers.
- README, `docs/quantities.md`, and `docs/quantity-kinds.md` updated to reflect tag system (enum removed), Work/Heat semantics, electromagnetic kinds, canonical corrections, and inference registry sealing guidance.
- Built-in kinds now explicitly constructed with tag sets (deterministic metadata snapshot).

### Fixed

- Prevented inadvertent absolute temperature scaling by treating absolute kinds as point-like in scalar fallback logic.
- Ensured Angle does not erode semantic meaning through dimensionless fallback.
- Corrected electromagnetic canonical unit exponents (R, G, C, L) ensuring dimensional consistency.
- Pressure canonical unit explicitly defined to avoid rare reduction anomaly in composite expressions.

### Performance

- No regression: existing equality / reduction benchmarks unaffected (allocation profile unchanged). (Informal validation; formal gate maintained via benchmarks.)

### Notes

- All inference remains single-hop: no transitive chaining introduced (design invariant preserved).
- Registry sealing recommended after application startup to guarantee deterministic semantics; internal test-only reset helper used for isolation.
- Electromagnetic canonical corrections are definition/document accuracy updates; verify any cached dimension manifests.
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
