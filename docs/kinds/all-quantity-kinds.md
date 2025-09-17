# All Quantity Kinds (Authoritative Listing)

This document enumerates every `QuantityKind` currently defined in the library (reflection-equivalent snapshot). It complements `quantity-kinds.md` (curated subset) by listing extended electromagnetic, transport, radiation, material, and thermodynamic kinds.

Columns:

- Name – Identifier used in code (static field name value).
- Symbol – Conventional symbol (where applicable).
- Canonical Unit – Unit expression used internally (before any reduction when constructed).
- Dimension (Base Form) – Expanded base units (kg, m, s, A, K, cd, mol) with exponents.
- + / - – Whether same‑kind direct addition / subtraction is allowed.
- Δ Result – Difference result kind when subtracting two point (absolute) kinds (otherwise —).
- Tags – All tags attached (governance roots described in `tags.md`).

> NOTE: Only `TemperatureAbsolute` is modeled as a point kind (no direct + / -). All others listed allow direct addition & subtraction.

## Legend

- `·` multiplication, `/` division, `^` exponent, negative exponents expressed via division form for readability.
- Dimension expansions are simplified (e.g. J rewritten as kg·m^2/s^2). Identical dimensions across different kinds intentionally prevent cross‑kind addition.

---

## Core Energy / Thermodynamic State & Path Functions

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| Energy | E | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction |
| InternalEnergy | U | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| Enthalpy | H | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| GibbsFreeEnergy | G | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| HelmholtzFreeEnergy | A | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy, Energy.StateFunction, Domain.Thermodynamic |
| Work | Wk | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy.PathFunction, Domain.Mechanical, Energy |
| Heat | Q | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Energy.PathFunction, Domain.Thermodynamic, Energy |
| Entropy | S | kg·m^2/(s^2·K) | kg·m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| HeatCapacity | C_p | kg·m^2/(s^2·K) | kg·m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| ChemicalPotential | μ | kg·m^2/(s^2·mol) | kg·m^2/(s^2·mol) | Yes | Yes | — | Domain.Thermodynamic |

## Mechanical / Kinematic / Geometric

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| Torque | τ | kg·m^2/s^2 | kg·m^2/s^2 | Yes | Yes | — | Domain.Mechanical |
| Angle | θ | 1 | 1 | Yes | Yes | — | Form.Dimensionless, Domain.Mechanical |
| Length | L | m | m | Yes | Yes | — | — |
| Area | A_r | m^2 | m^2 | Yes | Yes | — | — |
| Volume | V | m^3 | m^3 | Yes | Yes | — | — |
| Time | t | s | s | Yes | Yes | — | — |
| Mass | m | kg | kg | Yes | Yes | — | — |
| Velocity | v | m/s | m/s | Yes | Yes | — | — |
| Acceleration | a | m/s^2 | m/s^2 | Yes | Yes | — | — |
| Momentum | p_m | kg·m/s | kg·m/s | Yes | Yes | — | — |
| Power | P | kg·m^2/s^3 (J/s) | kg·m^2/s^3 | Yes | Yes | — | — |
| Force | F | kg·m/s^2 | kg·m/s^2 | Yes | Yes | — | — |
| Pressure | p | kg/(m·s^2) | kg/(m·s^2) | Yes | Yes | — | — |
| EnergyDensity | u | kg/(m·s^2) | kg/(m·s^2) | Yes | Yes | — | — |
| SpecificHeatCapacity | c_p | m^2/(s^2·K) | m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| SpecificEntropy | s | m^2/(s^2·K) | m^2/(s^2·K) | Yes | Yes | — | Domain.Thermodynamic |
| SolidAngle | Ω_s | 1 (sr) | 1 | Yes | Yes | — | Domain.Geometry |
| AngularVelocity | ω | 1/s | s^-1 | Yes | Yes | — | Domain.Kinematics |
| AngularAcceleration | α_ang | 1/s^2 | s^-2 | Yes | Yes | — | Domain.Kinematics |
| AngularMomentum | L_ang | kg·m^2/s | kg·m^2/s | Yes | Yes | — | Domain.Mechanics |
| MomentOfInertia | I_m | kg·m^2 | kg·m^2 | Yes | Yes | — | Domain.Mechanics |
| Jerk | j | m/s^3 | m·s^-3 | Yes | Yes | — | Domain.Kinematics |
| Wavenumber | k | 1/m | m^-1 | Yes | Yes | — | Domain.Geometry |
| Curvature | κ | 1/m | m^-1 | Yes | Yes | — | Domain.Geometry |
| Strain | ε | 1 | 1 | Yes | Yes | — | Domain.Material |
| StrainRate | ε_dot | 1/s | s^-1 | Yes | Yes | — | Domain.Material, Domain.Transport |
| Impulse | J_imp | kg·m/s | kg·m/s | Yes | Yes | — | Domain.Mechanics |
| Action | S_act | kg·m^2/s | kg·m^2/s | Yes | Yes | — | Domain.Mechanics |
| SpecificAngularMomentum | h_ang | m^2/s | m^2/s | Yes | Yes | — | Domain.Mechanics |
| Torsion | τ_tor | 1/m | m^-1 | Yes | Yes | — | Domain.Geometry |

## Temperature (Affine vs Linear)

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| TemperatureDelta | ΔT | K | K | Yes | Yes | — | — |
| TemperatureAbsolute | T_abs | K | K | No | No | TemperatureDelta | Domain.Thermodynamic |

## Electromagnetic

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| ElectricCurrent | I | A | A | Yes | Yes | — | Domain.Electromagnetic |
| Frequency | Hz | 1/s | s^-1 | Yes | Yes | — | Domain.Electromagnetic |
| ElectricCharge | q | A·s | A·s | Yes | Yes | — | Domain.Electromagnetic |
| Voltage | V | kg·m^2/(s^3·A) | kg·m^2/(s^3·A) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricResistance | Ω | kg·m^2/(s^3·A^2) | kg·m^2/(s^3·A^2) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricConductance | S | s^3·A^2/(kg·m^2) | s^3·A^2/(kg·m^2) | Yes | Yes | — | Domain.Electromagnetic |
| Capacitance | F | s^4·A^2/(kg·m^2) | s^4·A^2/(kg·m^2) | Yes | Yes | — | Domain.Electromagnetic |
| Inductance | H | kg·m^2/(s^2·A^2) | kg·m^2/(s^2·A^2) | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFlux | Wb | kg·m^2/(s^2·A) | kg·m^2/(s^2·A) | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFluxDensity | T | kg/(s^2·A) | kg/(s^2·A) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricFieldStrength | E_f | kg·m/(s^3·A) | kg·m/(s^3·A) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricChargeDensity | ρ_q | A·s/m^3 | A·s/m^3 | Yes | Yes | — | Domain.Electromagnetic |
| ElectricCurrentDensity | J_d | A/m^2 | A/m^2 | Yes | Yes | — | Domain.Electromagnetic |
| MagneticFieldStrength | H | A/m | A/m | Yes | Yes | — | Domain.Electromagnetic |
| Magnetization | M_mag | A/m | A/m | Yes | Yes | — | Domain.Electromagnetic |
| ElectricDisplacement | D | A·s/m^2 | A·s/m^2 | Yes | Yes | — | Domain.Electromagnetic |
| MagneticVectorPotential | A_vec | kg·m/(s^2·A) | kg·m/(s^2·A) | Yes | Yes | — | Domain.Electromagnetic |
| Permittivity | ε | A^2·s^4/(kg·m^3) | A^2·s^4/(kg·m^3) | Yes | Yes | — | Domain.Electromagnetic |
| Permeability | μ | kg·m/(A^2·s^2) | kg·m/(A^2·s^2) | Yes | Yes | — | Domain.Electromagnetic |
| Impedance | Z | kg·m^2/(A^2·s^3) | kg·m^2/(A^2·s^3) | Yes | Yes | — | Domain.Electromagnetic |
| Admittance | Y | A^2·s^3/(kg·m^2) | A^2·s^3/(kg·m^2) | Yes | Yes | — | Domain.Electromagnetic |
| ChargeMobility | μ_e | m^2/(V·s) | A·s^2/kg | Yes | Yes | — | Domain.Electromagnetic |
| ElectricalConductivity | σ_cond | S/m | A^2·s^3/(kg·m^3) | Yes | Yes | — | Domain.Electromagnetic |
| ElectricalResistivity | ρ_res | Ω·m | kg·m^3/(A^2·s^3) | Yes | Yes | — | Domain.Electromagnetic |
| SurfaceChargeDensity | σ_q | C/m^2 | A·s/m^2 | Yes | Yes | — | Domain.Electromagnetic |
| LineChargeDensity | λ_q | C/m | A·s/m | Yes | Yes | — | Domain.Electromagnetic |
| SurfaceCurrentDensity | K_s | A/m | A/m | Yes | Yes | — | Domain.Electromagnetic |
| ElectricDipoleMoment | p_e | C·m | A·s·m | Yes | Yes | — | Domain.Electromagnetic |
| MagneticDipoleMoment | m_dip | A·m^2 | A·m^2 | Yes | Yes | — | Domain.Electromagnetic |
| Polarization | P_pol | C/m^2 | A·s/m^2 | Yes | Yes | — | Domain.Electromagnetic |
| BoundCurrentDensity | J_b | A/m^2 | A/m^2 | Yes | Yes | — | Domain.Electromagnetic |
| OpticalPathLength | L_opt | m | m | Yes | Yes | — | Domain.Optics |
| RefractiveIndex | n_refr | 1 | 1 | Yes | Yes | — | Domain.Optics, Form.Dimensionless |

## Luminous / Optical (Photometric)

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| LuminousFlux | Φ_v (lm) | cd·sr | cd | Yes | Yes | — | Domain.Optics |
| LuminousIntensity | I_v | cd | cd | Yes | Yes | — | Domain.Optics |
| Luminance | L_v | cd/m^2 | cd·m^-2 | Yes | Yes | — | Domain.Optics |
| Illuminance | E_v (lx) | cd·sr/m^2 | cd·m^-2 | Yes | Yes | — | Domain.Optics |
| LuminousEfficacy | K_cd | lm/W | cd·sr·s^3/(kg·m^2) | Yes | Yes | — | Domain.Optics |
| Emissivity | ε_emis | 1 | 1 | Yes | Yes | — | Domain.Thermodynamic |
| Absorptivity | α_abs | 1 | 1 | Yes | Yes | — | Domain.Thermodynamic |
| Reflectivity | ρ_refl | 1 | 1 | Yes | Yes | — | Domain.Thermodynamic |
| Transmissivity | τ_trans | 1 | 1 | Yes | Yes | — | Domain.Thermodynamic |

## Radiation / Nuclear

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| Radioactivity | Bq | 1/s | s^-1 | Yes | Yes | — | Domain.Radiation |
| AbsorbedDose | Gy | m^2/s^2 | m^2/s^2 | Yes | Yes | — | Domain.Radiation |
| DoseEquivalent | Sv | m^2/s^2 | m^2/s^2 | Yes | Yes | — | Domain.Radiation |
| RadiantFlux | Φ_e | kg·m^2/s^3 | kg·m^2/s^3 | Yes | Yes | — | Domain.Radiation |
| RadiantIntensity | I_e | kg·m^2/(s^3·sr) | kg·m^2/(s^3) | Yes | Yes | — | Domain.Radiation |
| Irradiance | E_e | kg/s^3 | kg·s^-3 | Yes | Yes | — | Domain.Radiation |
| Radiance | L_e | kg/(s^3·sr) | kg·s^-3 | Yes | Yes | — | Domain.Radiation |
| RadiationExposure | X | A·s/kg | A·s/kg | Yes | Yes | — | Domain.Radiation |
| SpectralRadiance | L_λ | kg/(s^3·m·sr) | kg·s^-3·m^-1 | Yes | Yes | — | Domain.Radiation |
| SpectralIrradiance | E_λ | kg/(s^3·m) | kg·s^-3·m^-1 | Yes | Yes | — | Domain.Radiation |
| SpectralFlux | Φ_λ | kg·m/(s^3·m) | kg·s^-3 | Yes | Yes | — | Domain.Radiation |

## Chemistry / Material / Transport

| Name | Symbol | Canonical Unit | Dimension (Base Form) | + | - | Δ Result | Tags |
|------|--------|---------------|-----------------------|---|---|----------|------|
| CatalyticActivity | kat | mol/s | mol·s^-1 | Yes | Yes | — | Domain.Chemistry |
| MassDensity | ρ | kg/m^3 | kg/m^3 | Yes | Yes | — | Domain.Material |
| MassConcentration | ρ_m | kg/m^3 | kg/m^3 | Yes | Yes | — | Domain.Chemistry, Domain.Material |
| SpecificEnergy | e_spec | m^2/s^2 | m^2/s^2 | Yes | Yes | — | Domain.Material |
| SpecificPower | p_spec | m^2/s^3 | m^2/s^3 | Yes | Yes | — | Domain.Material |
| SpecificVolume | v_spec | m^3/kg | m^3/kg | Yes | Yes | — | Domain.Material |
| DynamicViscosity | η | kg/(m·s) | kg/(m·s) | Yes | Yes | — | Domain.Transport |
| KinematicViscosity | ν | m^2/s | m^2/s | Yes | Yes | — | Domain.Transport |
| ThermalConductivity | k_th | kg·m/(s^3·K) | kg·m/(s^3·K) | Yes | Yes | — | Domain.Transport, Domain.Thermodynamic |
| ThermalDiffusivity | α | m^2/s | m^2/s | Yes | Yes | — | Domain.Transport, Domain.Thermodynamic |
| HeatFlux | q_dot | kg/s^3 | kg/s^3 | Yes | Yes | — | Domain.Transport, Energy.PathFunction |
| SurfaceTension | γ | kg/s^2 | kg/s^2 | Yes | Yes | — | Domain.Material |
| MolarMass | M | kg/mol | kg/mol | Yes | Yes | — | Domain.Chemistry |
| MolarVolume | V_m | m^3/mol | m^3/mol | Yes | Yes | — | Domain.Chemistry |
| MolarConcentration | c | mol/m^3 | mol/m^3 | Yes | Yes | — | Domain.Chemistry |
| VolumetricFlowRate | Q_v | m^3/s | m^3·s^-1 | Yes | Yes | — | Domain.Transport |
| MassFlowRate | ṁ | kg/s | kg·s^-1 | Yes | Yes | — | Domain.Transport |
| MolarFlowRate | n_dot | mol/s | mol·s^-1 | Yes | Yes | — | Domain.Transport, Domain.Chemistry |
| YoungsModulus | E | kg/(m·s^2) | kg·m^-1·s^-2 | Yes | Yes | — | Domain.Material, Domain.Mechanics |
| ShearModulus | G_mod | kg/(m·s^2) | kg·m^-1·s^-2 | Yes | Yes | — | Domain.Material, Domain.Mechanics |
| BulkModulus | K_mod | kg/(m·s^2) | kg·m^-1·s^-2 | Yes | Yes | — | Domain.Material, Domain.Mechanics |
| Compressibility | β_T | m·s^2/kg | m·s^2·kg^-1 | Yes | Yes | — | Domain.Material |
| LinearMassDensity | μ_lin | kg/m | kg·m^-1 | Yes | Yes | — | Domain.Material |
| ArealMassDensity | σ_areal | kg/m^2 | kg·m^-2 | Yes | Yes | — | Domain.Material |
| NumberDensity | n_d | 1/m^3 | m^-3 | Yes | Yes | — | Domain.Material, Domain.Chemistry |
| ArealNumberDensity | n_dA | 1/m^2 | m^-2 | Yes | Yes | — | Domain.Material, Domain.Chemistry |
| Porosity | φ | 1 | 1 | Yes | Yes | — | Domain.Material |
| ThermalResistance | R_th | K/W | K·s^3/(kg·m^2) | Yes | Yes | — | Domain.Thermodynamic |
| ThermalConductance | G_th | W/K | kg·m^2/(s^3·K) | Yes | Yes | — | Domain.Thermodynamic |
| HeatTransferCoefficient | h_c | W/(m^2·K) | kg/(s^3·K) | Yes | Yes | — | Domain.Thermodynamic, Domain.Transport |
| MolarHeatCapacity | C_m | J/(mol·K) | kg·m^2/(s^2·mol·K) | Yes | Yes | — | Domain.Thermodynamic, Domain.Chemistry |
| SpecificEnthalpy | h_spec | m^2/s^2 | m^2·s^-2 | Yes | Yes | — | Domain.Thermodynamic |
| DiffusionCoefficient | D_diff | m^2/s | m^2·s^-1 | Yes | Yes | — | Domain.Transport, Domain.Chemistry |
| ReactionRate | r_rxn | mol/(m^3·s) | mol·m^-3·s^-1 | Yes | Yes | — | Domain.Chemistry |
| MoleFraction | x | 1 | 1 | Yes | Yes | — | Domain.Chemistry |
| MassFraction | w | 1 | 1 | Yes | Yes | — | Domain.Chemistry |
| OsmoticPressure | π_osm | kg/(m·s^2) | kg·m^-1·s^-2 | Yes | Yes | — | Domain.Chemistry, Domain.Thermodynamic |
| DimensionlessRatio | R_dim | 1 | 1 | Yes | Yes | — | Domain.Geometry |
| VolumetricHeatCapacity | C_vol | kg/(m·s^2·K) | kg·m^-1·s^-2·K^-1 | Yes | Yes | — | Domain.Thermodynamic |
| SpecificWeight | γ_spec | kg/(m^2·s^2) | kg·m^-2·s^-2 | Yes | Yes | — | Domain.Transport, Domain.Material |
| PartialPressure | p_i | kg/(m·s^2) | kg·m^-1·s^-2 | Yes | Yes | — | Domain.Chemistry, Domain.Thermodynamic |
| Activity | a | 1 | 1 | Yes | Yes | — | Domain.Chemistry |
| ActivityCoefficient | γ_act | 1 | 1 | Yes | Yes | — | Domain.Chemistry |
| MassAttenuationCoefficient | μ_mass | m^2/kg | m^2·kg^-1 | Yes | Yes | — | Domain.Radiation, Domain.Material |
| HenrysConstant | H_c | Pa·m^3/mol | kg·m/(s^2·mol) | Yes | Yes | — | Domain.Chemistry, Domain.Thermodynamic |
| Reynolds | Re | 1 | 1 | Yes | Yes | — | Domain.Transport |
| Prandtl | Pr | 1 | 1 | Yes | Yes | — | Domain.Transport |
| Nusselt | Nu | 1 | 1 | Yes | Yes | — | Domain.Transport |
| Schmidt | Sc | 1 | 1 | Yes | Yes | — | Domain.Transport |
| Sherwood | Sh | 1 | 1 | Yes | Yes | — | Domain.Transport |
| Biot | Bi | 1 | 1 | Yes | Yes | — | Domain.Transport |

---

## Generation & Maintenance

This file is maintained manually but mirrors what a reflection-based enumeration would produce:

```csharp
var kinds = typeof(Veggerby.Units.Quantities.QuantityKinds)
    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    .Where(f => f.FieldType == typeof(QuantityKind))
    .Select(f => (QuantityKind)f.GetValue(null));
```

When adding a new kind:

1. Define the static field in a partial `QuantityKinds` class.
2. Assign tags (reuse reserved roots; see `tags.md`).
3. Update any relevant curated docs (e.g. `quantity-kinds.md`).
4. Append an entry to this table (group appropriately).
5. Add or adjust inference rules if semantic multiply/divide is meaningful.

> If discrepancies are found between code and this document, code wins—open an issue or PR to reconcile.
