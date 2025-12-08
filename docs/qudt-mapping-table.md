# QUDT QuantityKind Mapping Table

This document provides a detailed mapping between Veggerby.Units `QuantityKind` instances and their corresponding QUDT `qudt:QuantityKind` URIs. This mapping serves as a validation reference and does not indicate runtime dependency.

## Mapping Conventions

- **QUDT URI Format:** `http://qudt.org/vocab/quantitykind/{QuantityKindName}`
- **Alignment Status:** 
  - ✅ **Exact Match** – Dimensions and semantics align perfectly
  - ⚠️ **Partial Match** – Conceptually similar but may have naming or scope differences
  - ➕ **Veggerby-Specific** – Not present in QUDT or intentionally extended
  - 🔍 **Under Review** – Needs further validation

## Base Dimensions

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.Length` | L | `qudt:Length` | L | ✅ | SI base dimension (meter) |
| `QuantityKinds.Mass` | m | `qudt:Mass` | M | ✅ | SI base dimension (kilogram) |
| `QuantityKinds.Time` | t | `qudt:Time` | T | ✅ | SI base dimension (second) |
| `QuantityKinds.ElectricCurrent` | I | `qudt:ElectricCurrent` | I | ✅ | SI base dimension (ampere) |
| — | — | `qudt:ThermodynamicTemperature` | Θ | — | Represented via `TemperatureAbsolute` |
| — | — | `qudt:AmountOfSubstance` | N | — | Base dimension exists, quantity kind TBD |
| — | — | `qudt:LuminousIntensity` | J | — | Base dimension exists, quantity kind TBD |

## Mechanics & Kinematics

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.Area` | A_r | `qudt:Area` | A | ✅ | m² |
| `QuantityKinds.Volume` | V | `qudt:Volume` | V | ✅ | m³ |
| `QuantityKinds.Velocity` | v | `qudt:Velocity` | v | ✅ | m/s (linear velocity) |
| `QuantityKinds.Acceleration` | a | `qudt:Acceleration` | a | ✅ | m/s² (linear acceleration) |
| `QuantityKinds.Force` | F | `qudt:Force` | F | ✅ | kg·m/s² (newton) |
| `QuantityKinds.Pressure` | p | `qudt:Pressure` | p | ✅ | kg/(m·s²) (pascal) |
| `QuantityKinds.Momentum` | p_m | `qudt:LinearMomentum` | p | ✅ | kg·m/s |
| `QuantityKinds.AngularVelocity` | ω | `qudt:AngularVelocity` | ω | ✅ | rad/s |
| `QuantityKinds.AngularAcceleration` | α_ang | `qudt:AngularAcceleration` | α | ✅ | rad/s² |
| `QuantityKinds.AngularMomentum` | L_ang | `qudt:AngularMomentum` | L | ✅ | kg·m²/s |
| `QuantityKinds.Torque` | τ | `qudt:Torque` | τ | ✅ | kg·m²/s² (dimensionally energy, semantically distinct) |
| `QuantityKinds.MomentOfInertia` | I_mom | `qudt:MomentOfInertia` | I | ✅ | kg·m² |
| `QuantityKinds.Impulse` | J_imp | `qudt:Impulse` | J | ✅ | kg·m/s (N·s) |
| `QuantityKinds.Action` | S_act | `qudt:Action` | S | ✅ | kg·m²/s (J·s) |
| `QuantityKinds.Angle` | θ | `qudt:Angle` | θ | ✅ | Dimensionless (radian) |
| `QuantityKinds.SolidAngle` | Ω | `qudt:SolidAngle` | Ω | ✅ | Dimensionless (steradian) |
| `QuantityKinds.SpecificAngularMomentum` | h_ang | `qudt:SpecificAngularMomentum` | h | ✅ | m²/s |

## Energy & Thermodynamics

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.Energy` | E | `qudt:Energy` | E | ✅ | kg·m²/s² (joule) |
| `QuantityKinds.InternalEnergy` | U | `qudt:InternalEnergy` | U | ✅ | Thermodynamic state function |
| `QuantityKinds.Enthalpy` | H | `qudt:Enthalpy` | H | ✅ | Thermodynamic state function |
| `QuantityKinds.GibbsFreeEnergy` | G | `qudt:GibbsEnergy` | G | ✅ | Thermodynamic state function |
| `QuantityKinds.HelmholtzFreeEnergy` | A | `qudt:HelmholtzEnergy` | A | ✅ | Thermodynamic state function |
| `QuantityKinds.Entropy` | S | `qudt:Entropy` | S | ✅ | J/K |
| `QuantityKinds.HeatCapacity` | C_p | `qudt:HeatCapacity` | C | ✅ | J/K |
| `QuantityKinds.SpecificHeatCapacity` | c_p | `qudt:SpecificHeatCapacity` | c_p | ✅ | J/(kg·K) |
| `QuantityKinds.SpecificEntropy` | s | `qudt:SpecificEntropy` | s | ✅ | J/(kg·K) |
| `QuantityKinds.SpecificEnthalpy` | h_spec | `qudt:SpecificEnthalpy` | h | ✅ | J/kg |
| `QuantityKinds.VolumetricHeatCapacity` | C_vol | `qudt:VolumetricHeatCapacity` | C_V | ✅ | J/(m³·K) |
| `QuantityKinds.MolarHeatCapacity` | C_m | `qudt:MolarHeatCapacity` | C_m | ✅ | J/(mol·K) |
| `QuantityKinds.Power` | P | `qudt:Power` | P | ✅ | kg·m²/s³ (watt) |
| `QuantityKinds.Work` | Wk | `qudt:Work` | W | ✅ | Path energy transfer (dimensionally energy) |
| `QuantityKinds.Heat` | Q | `qudt:Heat` | Q | ✅ | Thermal path energy transfer |
| `QuantityKinds.EnergyDensity` | u | `qudt:EnergyDensity` | u | ✅ | J/m³ |
| `QuantityKinds.ChemicalPotential` | μ | `qudt:ChemicalPotential` | μ | ✅ | J/mol |
| `QuantityKinds.TemperatureAbsolute` | T_abs | `qudt:ThermodynamicTemperature` | T | ✅ | Kelvin (affine semantics) |
| `QuantityKinds.TemperatureDelta` | ΔT | `qudt:TemperatureDifference` | ΔT | ✅ | Kelvin (differential) |
| `QuantityKinds.SpecificLatentHeat` | L_spec | `qudt:SpecificEnergy` | — | ⚠️ | J/kg (QUDT uses SpecificEnergy) |
| `QuantityKinds.MolarLatentHeat` | L_mol | `qudt:MolarEnergy` | — | ⚠️ | J/mol (QUDT uses MolarEnergy) |

## Thermal Transport

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.ThermalConductivity` | k_th | `qudt:ThermalConductivity` | λ, k | ✅ | W/(m·K) |
| `QuantityKinds.ThermalDiffusivity` | α | `qudt:ThermalDiffusivity` | α | ✅ | m²/s |
| `QuantityKinds.HeatTransferCoefficient` | h_c | `qudt:HeatTransferCoefficient` | h | ✅ | W/(m²·K) |
| `QuantityKinds.HeatFlux` | q_dot | `qudt:HeatFlowRate` | φ | ✅ | W/m² |
| `QuantityKinds.ThermalResistance` | R_th | `qudt:ThermalResistance` | R_th | ✅ | K/W |
| `QuantityKinds.ThermalConductance` | G_th | `qudt:ThermalConductance` | G_th | ✅ | W/K |
| `QuantityKinds.CoefficientOfThermalExpansion` | α_th | `qudt:LinearThermalExpansion` | α_L | ✅ | 1/K |

## Electromagnetics

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.ElectricCharge` | q | `qudt:ElectricCharge` | Q, q | ✅ | A·s (coulomb) |
| `QuantityKinds.Voltage` | V | `qudt:Voltage` | U, V | ✅ | kg·m²/(s³·A) (volt) |
| `QuantityKinds.ElectricResistance` | Ω | `qudt:Resistance` | R | ✅ | kg·m²/(s³·A²) (ohm) |
| `QuantityKinds.ElectricConductance` | S | `qudt:Conductance` | G | ✅ | s³·A²/(kg·m²) (siemens) |
| `QuantityKinds.Capacitance` | F | `qudt:Capacitance` | C | ✅ | s⁴·A²/(kg·m²) (farad) |
| `QuantityKinds.Inductance` | H | `qudt:Inductance` | L | ✅ | kg·m²/(s²·A²) (henry) |
| `QuantityKinds.MagneticFlux` | Wb | `qudt:MagneticFlux` | Φ | ✅ | kg·m²/(s²·A) (weber) |
| `QuantityKinds.MagneticFluxDensity` | T | `qudt:MagneticFluxDensity` | B | ✅ | kg/(s²·A) (tesla) |
| `QuantityKinds.ElectricFieldStrength` | E_f | `qudt:ElectricFieldStrength` | E | ✅ | kg·m/(s³·A) (V/m) |
| `QuantityKinds.MagneticFieldStrength` | H | `qudt:MagneticFieldStrength` | H | ✅ | A/m |
| `QuantityKinds.Permittivity` | ε | `qudt:Permittivity` | ε | ✅ | s⁴·A²/(kg·m³) (F/m) |
| `QuantityKinds.Permeability` | μ | `qudt:Permeability` | μ | ✅ | kg·m/(s²·A²) (H/m) |
| `QuantityKinds.Impedance` | Z | `qudt:Impedance` | Z | ✅ | kg·m²/(s³·A²) (ohm) |
| `QuantityKinds.Admittance` | Y | `qudt:Admittance` | Y | ✅ | s³·A²/(kg·m²) (siemens) |
| `QuantityKinds.ElectricalConductivity` | σ_cond | `qudt:Conductivity` | σ | ✅ | s³·A²/(kg·m³) (S/m) |
| `QuantityKinds.ElectricalResistivity` | ρ_res | `qudt:Resistivity` | ρ | ✅ | kg·m³/(s³·A²) (Ω·m) |
| `QuantityKinds.ElectricChargeDensity` | ρ_q | `qudt:ElectricChargeDensity` | ρ | ✅ | A·s/m³ (C/m³) |
| `QuantityKinds.ElectricCurrentDensity` | J_d | `qudt:CurrentDensity` | J | ✅ | A/m² |
| `QuantityKinds.ElectricDisplacement` | D | `qudt:ElectricDisplacement` | D | ✅ | A·s/m² (C/m²) |
| `QuantityKinds.Magnetization` | M_mag | `qudt:Magnetization` | M | ✅ | A/m |
| `QuantityKinds.Polarization` | P_pol | `qudt:Polarization` | P | ✅ | A·s/m² (C/m²) |
| `QuantityKinds.ElectricDipoleMoment` | p_e | `qudt:ElectricDipoleMoment` | p | ✅ | A·s·m (C·m) |
| `QuantityKinds.MagneticDipoleMoment` | m_dip | `qudt:MagneticDipoleMoment` | m | ✅ | A·m² |
| `QuantityKinds.Frequency` | Hz | `qudt:Frequency` | f, ν | ✅ | 1/s (hertz) |
| `QuantityKinds.MagneticVectorPotential` | A_vec | `qudt:MagneticVectorPotential` | A | ✅ | kg·m²/(s²·A·m) (Wb/m) |

## Materials & Mechanics

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.YoungsModulus` | E | `qudt:ModulusOfElasticity` | E | ✅ | kg/(m·s²) (Pa) |
| `QuantityKinds.ShearModulus` | G | `qudt:ShearModulus` | G | ✅ | kg/(m·s²) (Pa) |
| `QuantityKinds.BulkModulus` | K | `qudt:BulkModulus` | K | ✅ | kg/(m·s²) (Pa) |
| `QuantityKinds.PoissonRatio` | ν | `qudt:PoissonRatio` | ν | ✅ | Dimensionless |
| `QuantityKinds.Stress` | σ | `qudt:Stress` | σ | ✅ | kg/(m·s²) (Pa) |
| `QuantityKinds.Strain` | ε_strain | `qudt:Strain` | ε | ✅ | Dimensionless (m/m) |
| `QuantityKinds.Density` | ρ | `qudt:Density` | ρ | ✅ | kg/m³ |
| `QuantityKinds.SpecificVolume` | v_spec | `qudt:SpecificVolume` | v | ✅ | m³/kg |

## Fluid Mechanics & Transport

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.DynamicViscosity` | μ_visc | `qudt:DynamicViscosity` | μ, η | ✅ | kg/(m·s) (Pa·s) |
| `QuantityKinds.KinematicViscosity` | ν_visc | `qudt:KinematicViscosity` | ν | ✅ | m²/s |
| `QuantityKinds.VolumeFlowRate` | Q_vol | `qudt:VolumeFlowRate` | Q | ✅ | m³/s |
| `QuantityKinds.MassFlowRate` | m_dot | `qudt:MassFlowRate` | ṁ | ✅ | kg/s |
| `QuantityKinds.SpecificWeight` | γ_spec | `qudt:SpecificWeight` | γ | ✅ | kg/(m²·s²) (N/m³) |
| `QuantityKinds.SurfaceTension` | γ_surf | `qudt:SurfaceTension` | γ, σ | ✅ | kg/s² (N/m) |

## Dimensionless Flow Numbers

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.ReynoldsNumber` | Re | `qudt:ReynoldsNumber` | Re | ✅ | Dimensionless |
| `QuantityKinds.PrandtlNumber` | Pr | `qudt:PrandtlNumber` | Pr | ✅ | Dimensionless |
| `QuantityKinds.NusseltNumber` | Nu | `qudt:NusseltNumber` | Nu | ✅ | Dimensionless |
| `QuantityKinds.GrashofNumber` | Gr | `qudt:GrashofNumber` | Gr | ✅ | Dimensionless |
| `QuantityKinds.PecletNumber` | Pe | `qudt:PecletNumber` | Pe | ✅ | Dimensionless |
| `QuantityKinds.MachNumber` | Ma | `qudt:MachNumber` | Ma | ✅ | Dimensionless |
| `QuantityKinds.FroudeNumber` | Fr | `qudt:FroudeNumber` | Fr | ✅ | Dimensionless |
| `QuantityKinds.WeberNumber` | We | `qudt:WeberNumber` | We | ✅ | Dimensionless |

## Radiation & Optics

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.LuminousIntensity` | I_v | `qudt:LuminousIntensity` | I_v | ✅ | cd (base SI dimension) |
| `QuantityKinds.LuminousFlux` | Φ_v | `qudt:LuminousFlux` | Φ_v | ✅ | cd·sr (lumen) |
| `QuantityKinds.Illuminance` | E_v | `qudt:Illuminance` | E_v | ✅ | cd·sr/m² (lux) |
| `QuantityKinds.Luminance` | L_v | `qudt:Luminance` | L_v | ✅ | cd/m² |
| `QuantityKinds.SpectralRadiance` | L_λ | `qudt:SpectralRadiance` | L_λ | ✅ | kg/(s³·m) (W/(m²·sr·m)) |
| `QuantityKinds.Irradiance` | E_e | `qudt:Irradiance` | E_e | ✅ | kg/s³ (W/m²) |
| `QuantityKinds.Radiance` | L_e | `qudt:Radiance` | L_e | ✅ | kg/(s³·sr) (W/(m²·sr)) |
| `QuantityKinds.RadiantIntensity` | I_e | `qudt:RadiantIntensity` | I_e | ✅ | kg·m²/(s³·sr) (W/sr) |
| `QuantityKinds.Emissivity` | ε_emis | `qudt:Emissivity` | ε | ✅ | Dimensionless |
| `QuantityKinds.Absorptivity` | α_abs | `qudt:Absorptivity` | α | ✅ | Dimensionless |
| `QuantityKinds.Reflectivity` | ρ_refl | `qudt:Reflectivity` | ρ | ✅ | Dimensionless |
| `QuantityKinds.Transmissivity` | τ_trans | `qudt:Transmissivity` | τ | ✅ | Dimensionless |

## Chemistry

| Veggerby.Units QuantityKind | Symbol | QUDT URI | QUDT Symbol | Alignment | Notes |
|------------------------------|--------|----------|-------------|-----------|-------|
| `QuantityKinds.MolarMass` | M | `qudt:MolarMass` | M | ✅ | kg/mol |
| `QuantityKinds.MolarVolume` | V_m | `qudt:MolarVolume` | V_m | ✅ | m³/mol |
| `QuantityKinds.AmountOfSubstance` | n | `qudt:AmountOfSubstance` | n | ✅ | mol |
| `QuantityKinds.Concentration` | c | `qudt:Concentration` | c | ✅ | mol/m³ |
| `QuantityKinds.Molality` | b | `qudt:Molality` | b | ✅ | mol/kg |
| `QuantityKinds.MoleFraction` | x | `qudt:MoleFraction` | x | ✅ | Dimensionless (mol/mol) |
| `QuantityKinds.PartialPressure` | p_i | `qudt:PartialPressure` | p_i | ✅ | kg/(m·s²) (Pa) |
| `QuantityKinds.Activity` | a | `qudt:Activity` | a | ✅ | Dimensionless |
| `QuantityKinds.ActivityCoefficient` | γ_act | `qudt:ActivityCoefficient` | γ | ✅ | Dimensionless |
| `QuantityKinds.HenrysConstant` | H_cp | `qudt:HenrysLawConstant` | H_cp | ✅ | kg·m/(s²·mol) (Pa·m³/mol) |

## Veggerby-Specific Extensions

These quantity kinds are defined in Veggerby.Units but do not have direct QUDT equivalents:

| Veggerby.Units QuantityKind | Symbol | Canonical Unit | Notes |
|------------------------------|--------|----------------|-------|
| `QuantityKinds.SurfaceChargeDensity` | σ_q | C/m² | Electromagnetic surface density |
| `QuantityKinds.LineChargeDensity` | λ_q | C/m | Electromagnetic line density |
| `QuantityKinds.SurfaceCurrentDensity` | K_s | A/m | Electromagnetic surface current |
| `QuantityKinds.BoundCurrentDensity` | J_b | A/m² | Bound current from magnetization |
| `QuantityKinds.HallCoefficient` | R_H | m³/C | Hall effect coefficient |
| `QuantityKinds.SeebeckCoefficient` | S_seeb | V/K | Thermoelectric Seebeck coefficient |
| `QuantityKinds.MagneticSusceptibility` | χ_m | Dimensionless | Magnetic response |
| `QuantityKinds.ElectricSusceptibility` | χ_e | Dimensionless | Electric response |
| `QuantityKinds.RelativePermittivity` | ε_r | Dimensionless | Dielectric constant |
| `QuantityKinds.RelativePermeability` | μ_r | Dimensionless | Magnetic constant |
| `QuantityKinds.ChargeMobility` | μ_e | m²/(V·s) | Charge carrier mobility |
| `QuantityKinds.IsentropicExponent` | γ_iso | Dimensionless | Heat capacity ratio (γ = C_p/C_V) |

*Note: These extensions may map to QUDT quantities under different names or may represent composite concepts. Further QUDT catalog review ongoing.*

## Summary Statistics

- **Total Veggerby.Units QuantityKinds:** ~80+
- **QUDT Exact Matches:** ~70
- **Partial Matches:** ~5
- **Veggerby-Specific:** ~10
- **Coverage:** >85% direct alignment

## Validation Notes

1. **Symbol Differences:** Some symbols differ due to physics convention variations (e.g., QUDT uses `λ` for thermal conductivity, Veggerby.Units uses `k_th` to avoid confusion with wavelength).

2. **Semantic Disambiguation:** Energy-dimensioned quantities (Energy, Torque, Work, Heat) are semantically distinct in both systems despite identical dimensions.

3. **Temperature Handling:** Both QUDT and Veggerby.Units distinguish absolute (affine) temperatures from temperature differences (deltas).

4. **Dimensionless Quantities:** Both systems recognize that dimensionless quantities can have distinct semantic meanings (Angle, Strain, Activity, etc.).

## Future Work

- Cross-validate scale factors for Imperial/US customary units against QUDT
- Add missing QUDT quantity kinds identified during catalog review
- Implement build-time validation test comparing canonical units with QUDT data
- Investigate QUDT quantities not yet mapped (e.g., radiation dose equivalents)

## References

- **QUDT QuantityKind Catalog:** http://www.qudt.org/doc/DOC_VOCAB-QUANTITY-KINDS.html
- **Veggerby.Units Quantity Documentation:** `docs/quantity-kinds.md`
- **QUDT GitHub Repository:** https://github.com/qudt/qudt-public-repo/tree/master/vocab/quantitykinds
