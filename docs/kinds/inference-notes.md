# Quantity Kind Inference Notes

This document captures the philosophy, scope, current rule set, intentional omissions, and future candidates for the single‑hop `QuantityKind` inference system.

---

## 1. Philosophy

Inference provides a **single semantic hop** for multiplication or division between exactly two quantity kinds:

- No chaining: Only the direct binary operation presented by the user is considered.
- No exponent pattern solving: Exponents are handled purely at the unit algebra layer, not semantic inference.
- No transitive closure: If A×B→C and C×D→E, the system will not infer A×B×D→E.
- Deterministic and unambiguous: A left/operator/right triple maps to **at most one** result kind.
- Semantic priority over dimensional equivalence: Sharing a physical dimension does not justify an inference rule (e.g., Pressure vs Stress vs ShearStress).

The registry lives in `src/Veggerby.Units/Quantities/QuantityKindInferenceRegistry.cs` and is seeded once at static initialization. All additions must preserve the no‑conflict invariant.

---

## 2. Inclusion Criteria

A proposed rule must satisfy ALL of:

1. Uniqueness: The dimensional result must not already map from another pair yielding a *different* semantic kind.
2. Canonicality: The pair is a widely accepted first‑principles relationship (e.g., Force × Length = Energy, Irradiance × Time = RadiantExposure).
3. Non‑redundancy: Does not merely alias an existing mapping through an alternative but equivalent perspective (e.g., avoiding both Voltage × Current = Power and Current × Resistance = Voltage × Current logic explosion beyond what is already accepted).
4. Domain Clarity: The result introduces or reinforces a distinct semantic role, not just a synonym with different naming convention.
5. Stability: Relationship is invariant across standard conditions (i.e., does not depend on state, material model, or higher‑order constitutive assumptions).

---

## 3. Current Rule Families (Additions Through Advanced3 Phase)

Below are the notable semantic families beyond the original core set.

### 3.1 Structural & Mechanical

- Force / Length → Stiffness
- Length / Force → Compliance
- DampingCoefficient × Length → Impulse  (DampingCoefficient has dimensions N·s/m; Impulse is N·s. This preserves uniqueness without mapping directly to Force.)

### 3.2 Photometry & Radiometry

- Illuminance × Time → LuminousExposure
- Irradiance × Time → RadiantExposure

### 3.3 Transport / Kinematics

- Velocity × Length → Circulation

### 3.4 Nuclear / Radiation

- Radioactivity / Volume → ActivityConcentration
- Fluence / Time → FluenceRate

### 3.5 Thermoelectric / Thermophysical

- Voltage / TemperatureDelta → SeebeckCoefficient
- Strain / TemperatureDelta → CoefficientOfThermalExpansion

### 3.6 Attenuation / Optical Depth

- LinearAttenuationCoefficient × Length → OpticalDepth

Reverse division rules exist for all above where semantically meaningful (e.g., Exposure / Time → Flux Density counterpart; OpticalDepth / Length → LinearAttenuationCoefficient).

---

## 4. Intentional Omissions & Rationale

| Candidate Relationship | Omitted Mapping | Reason |
|------------------------|-----------------|--------|
| Stress × Area = Force | Stress/ShearStress not inferred | Collides dimensionally with Pressure × Area = Force; would create multiple semantic paths to Force. |
| ShearStress × Area = Force | Same as above | Same collision rationale. |
| Vorticity × Length = Velocity | 1/s × m = m/s | Ambiguous with AngularVelocity relationships; vorticity field not treated as a direct kinematic generator. |
| SpecificLatentHeat × Mass = Energy | Not inferred | Shares dimension with SpecificEnthalpy; semantic ambiguity (process vs state). |
| MolarLatentHeat × Amount = Energy | Not inferred | Overlaps with ChemicalPotential × Amount semantics. |
| LinearAttenuationCoefficient × Length = OpticalDepth | (Now ADDED) | Previously deferred; added once deemed uniquely safe. |
| AbsorbedDose / Time = AbsorbedDoseRate | Not inferred | Dimensional collision risk with other energy‑rate per mass constructs (future reserved). |
| EquivalentDose / Time = EquivalentDoseRate | Not inferred | Same as absorbed dose rate. |
| MagneticSusceptibility × MagneticField = Magnetization | Not inferred | Would require modeling magnetization kind & domain state coupling (not present). |
| ElectricSusceptibility × ElectricField = Polarization | Not inferred | Would create partial material model; deferred until broader EM constitutive layer exists. |
| RelativePermittivity derivations | Not inferred | Depends on ε₀ constant; constants not modeled in semantic layer. |
| RelativePermeability derivations | Not inferred | Depends on μ₀ constant; same constraint. |
| HallCoefficient × (CurrentDensity × MagneticField) forms | Not inferred | Multi-variable relation; exceeds binary single-hop rule design. |
| Dimensionless Transport Numbers (Re, Fr, We, Pe, etc.) | Not inferred | Each is a composite *definition* ratio; reverse inference offers little semantic gain and risks accidental collisions. |
| CoefficientOfFriction × NormalForce = FrictionForce | Not inferred | Contact/friction force is context dependent; not a direct universal identity. |
| PoissonRatio × AxialStrain = LateralStrain | Not inferred | Constitutive/multiplicative use is material-model dependent; stays outside single-hop registry. |

---

## 5. Conflict Avoidance Strategy

1. Search for existing rules producing the same result kind; reject if a new mapping introduces a second semantic path that reduces clarity.
2. Reject if multiple plausible target kinds share identical dimensions (e.g., Pressure/Stress/ShearStress). Pick one canonical representative (Pressure) and leave the rest inert.
3. Prefer mapping to *process-like* kinds (Exposure, Impulse) where intermediate accumulation semantics are clear.
4. Material or constitutive coefficients are only mapped when they transform directly with a universally accepted pair (e.g., SeebeckCoefficient & CoefficientOfThermalExpansion). Others (susceptibilities) withheld pending richer model context.

---

## 6. Future Candidate Rules (Pending Uniqueness Validation)

| Candidate | Potential Rule | Preconditions Before Acceptance |
|-----------|----------------|---------------------------------|
| AbsorbedDoseRate | AbsorbedDose / Time → AbsorbedDoseRate | Ensure no conflicting m^2·s^-3 semantic kind emerges. |
| EquivalentDoseRate | EquivalentDose / Time → EquivalentDoseRate | Same uniqueness check as absorbed dose rate. |
| Magnetization (if added) | MagneticSusceptibility × MagneticField → Magnetization | Introduce explicit Magnetization kind; verify no collisions. |
| Polarization (if added) | ElectricSusceptibility × ElectricField → Polarization | Add Polarization kind + ensure uniqueness. |
| OpticalDepth Accumulation | LinearAttenuationCoefficient × PathSegment → OpticalDepth (already covered generically via Length) | Possibly add clarifying doc example instead of new rule. |
| Dose Accumulation | DoseRate × Time → Dose (reverse of rate) | Requires a canonical single dose kind separate from AbsorbedDose to avoid ambiguity. |
| Frictional Work (contextual) | FrictionForce × Displacement → Energy | Would depend on explicit FrictionForce semantic; out of scope currently. |

Any future adoption must: (a) pass uniqueness test in registry, (b) add forward & reverse tests, (c) document rationale and any explicitly *not* added related variants.

---

## 7. Contribution Checklist for New Inference Rule

1. Justify uniqueness (list potentially colliding kinds & explain disqualification).
2. Add rule(s) in `QuantityKindInferenceRegistry.Seed()` in a clearly grouped section.
3. Add forward inference test and both reverse division tests if symmetric.
4. Add omission test(s) for near-miss ambiguous candidates (if applicable).
5. Update this document (Section 3 or 4 or 6 accordingly).
6. Run full test suite; ensure zero conflicts under `StrictConflictDetection`.

---

## 8. Non-Goals

- Multi-variable derivations (e.g., three-factor definitions).
- Contextual / stateful constitutive modeling (stress–strain law, transport closure parameters).
- Constants (ε₀, μ₀, k_B, R) integration at semantic inference level.

---

## 9. Summary

The inference registry deliberately stays **minimal, explicit, and collision-free**. Additions favor clarity and teaching value over breadth. When in doubt—omit, document here, and revisit after a motivating use case is demonstrated.
