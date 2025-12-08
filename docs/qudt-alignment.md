# QUDT Ontology Conceptual Alignment

## Overview

This document describes the conceptual relationship between **Veggerby.Units** and the **QUDT (Quantities, Units, Dimensions, and Types)** ontology. QUDT serves as an authoritative reference for validating the scientific correctness of our unit definitions, dimensional algebra, and quantity kind semantics—**without introducing any runtime dependencies or ontology-based reasoning**.

## Core Principle: Reference Only

**Veggerby.Units does NOT depend on QUDT at runtime.**

This library remains:
- ✅ **Explicit** – No graph-based inference or transitive reasoning
- ✅ **Deterministic** – No external data sources during execution
- ✅ **Dependency-free** – No RDF/OWL ontology frameworks
- ✅ **Immutable** – All definitions are compile-time constants
- ✅ **High-performance** – No metadata lookups or semantic queries

QUDT is used solely as a **validation reference** to ensure:
- Correct dimensional exponents for derived units
- Accurate scale factors and conversion ratios
- Proper affine temperature semantics
- Scientifically rigorous quantity kind categorization

## What is QUDT?

The **Quantities, Units, Dimensions, and Types (QUDT)** ontology is a formal semantic framework providing:

- **Standardized physical quantity definitions** with URIs (e.g., `qudt:Length`, `qudt:Energy`)
- **Comprehensive unit catalog** with symbols, scale factors, and dimensional vectors
- **Dimensional analysis framework** based on SI base dimensions (L, M, T, I, Θ, N, J)
- **Affine transformation rules** for offset units like Celsius and Fahrenheit
- **Linked-data relationships** connecting quantities, units, and dimension systems

**Key QUDT Resources:**
- Homepage: http://www.qudt.org/
- Vocabulary Documentation: http://www.qudt.org/doc/DOC_VOCAB-UNITS.html
- GitHub Repository: https://github.com/qudt/qudt-public-repo
- Unit Search: http://www.qudt.org/search.html

## Conceptual Mapping

### 1. Dimensional Vectors

QUDT defines dimensional vectors using the seven SI base dimensions plus optional angle dimensions:

| QUDT Symbol | SI Base Dimension | Veggerby.Units Dimension |
|-------------|-------------------|---------------------------|
| `L` | Length (meter) | `Dimension.Length` |
| `M` | Mass (kilogram) | `Dimension.Mass` |
| `T` | Time (second) | `Dimension.Time` |
| `I` | Electric Current (ampere) | `Dimension.ElectricCurrent` |
| `Θ` | Thermodynamic Temperature (kelvin) | `Dimension.ThermodynamicTemperature` |
| `N` | Amount of Substance (mole) | `Dimension.AmountOfSubstance` |
| `J` | Luminous Intensity (candela) | `Dimension.LuminousIntensity` |

**Dimensional Vector Example:**

QUDT represents force (newton) as:
```
dimensionVector: L¹ M¹ T⁻²
```

Veggerby.Units produces the same dimensional signature via:
```csharp
Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)  // Dimension: M¹·L¹·T⁻²
```

### 2. Quantity Kinds

QUDT's `qudt:QuantityKind` classes map directly to Veggerby.Units' `QuantityKind` instances:

| Veggerby.Units | QUDT QuantityKind URI | Notes |
|----------------|------------------------|-------|
| `QuantityKinds.Length` | `qudt:Length` | Base dimension |
| `QuantityKinds.Mass` | `qudt:Mass` | Base dimension |
| `QuantityKinds.Time` | `qudt:Time` | Base dimension |
| `QuantityKinds.Energy` | `qudt:Energy` | Derived: kg·m²/s² |
| `QuantityKinds.Force` | `qudt:Force` | Derived: kg·m/s² |
| `QuantityKinds.Pressure` | `qudt:Pressure` | Derived: kg/(m·s²) |
| `QuantityKinds.Power` | `qudt:Power` | Derived: kg·m²/s³ |
| `QuantityKinds.Voltage` | `qudt:Voltage` | Derived: kg·m²/(s³·A) |
| `QuantityKinds.ElectricCharge` | `qudt:ElectricCharge` | Derived: A·s |

See `docs/qudt-mapping-table.md` for a comprehensive mapping of all quantity kinds.

### 3. Unit Definitions

QUDT defines each unit with:
- **Symbol** (e.g., `m`, `kg`, `N`, `Pa`)
- **Base unit reference** (e.g., meter is base, foot scales from meter)
- **Conversion multiplier** (e.g., 1 ft = 0.3048 m)
- **Dimensional vector** (e.g., Force = L¹M¹T⁻²)
- **Quantity kind assignment** (e.g., newton is a unit of Force)

Veggerby.Units mirrors this structure:

```csharp
// QUDT: unit:M (meter) - base unit of length
var meter = new BasicUnit("m", "meter", UnitSystem.SI, Dimension.Length);

// QUDT: unit:FT (foot) - 0.3048 × meter
var foot = new ScaleUnit("ft", meter, 0.3048);

// QUDT: unit:N (newton) - kg·m/s² (Force)
var newton = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);
```

### 4. Affine Temperature Units

QUDT explicitly distinguishes **absolute temperature scales** (Kelvin, Rankine) from **differential temperature units** (delta Celsius, delta Fahrenheit).

**QUDT Affine Semantics:**
- `unit:DEG_C` (Celsius) has offset +273.15 from `unit:K` (Kelvin)
- `unit:DEG_F` (Fahrenheit) has offset +459.67 from `unit:DEG_R` (Rankine)
- Differential conversions use scale factor only (no offset)

**Veggerby.Units Implementation:**

```csharp
// Absolute temperature: Celsius → Kelvin
var celsius = new AffineUnit("°C", "celsius", Unit.SI.K, scale: 1.0, offset: 273.15);

// Differential temperature (TemperatureDelta kind)
var deltaC = Unit.SI.K;  // Delta °C ≡ Delta K (scale only, no offset)
```

**Forbidden Operations (aligned with QUDT):**
- ❌ Multiplying absolute temperature: `°C × °C` → invalid
- ❌ Prefixing affine units: `k°C` → invalid
- ❌ Raising affine units to power > 1: `(°C)²` → invalid
- ✅ Temperature differences: `Absolute − Absolute → Delta`
- ✅ Adding delta to absolute: `Absolute + Delta → Absolute`

### 5. Prefix System

QUDT defines metric prefixes as `qudt:DecimalPrefixType`:

| QUDT Prefix | Symbol | Factor | Veggerby.Units |
|-------------|--------|--------|----------------|
| `prefix:Yotta` | Y | 10²⁴ | `Prefix.Y` |
| `prefix:Zetta` | Z | 10²¹ | `Prefix.Z` |
| `prefix:Exa` | E | 10¹⁸ | `Prefix.E` |
| `prefix:Peta` | P | 10¹⁵ | `Prefix.P` |
| `prefix:Tera` | T | 10¹² | `Prefix.T` |
| `prefix:Giga` | G | 10⁹ | `Prefix.G` |
| `prefix:Mega` | M | 10⁶ | `Prefix.M` |
| `prefix:Kilo` | k | 10³ | `Prefix.k` |
| `prefix:Hecto` | h | 10² | `Prefix.h` |
| `prefix:Deca` | da | 10¹ | `Prefix.da` |
| `prefix:Deci` | d | 10⁻¹ | `Prefix.d` |
| `prefix:Centi` | c | 10⁻² | `Prefix.c` |
| `prefix:Milli` | m | 10⁻³ | `Prefix.m` |
| `prefix:Micro` | μ | 10⁻⁶ | `Prefix.mu` |
| `prefix:Nano` | n | 10⁻⁹ | `Prefix.n` |
| `prefix:Pico` | p | 10⁻¹² | `Prefix.p` |
| `prefix:Femto` | f | 10⁻¹⁵ | `Prefix.f` |
| `prefix:Atto` | a | 10⁻¹⁸ | `Prefix.a` |
| `prefix:Zepto` | z | 10⁻²¹ | `Prefix.z` |
| `prefix:Yocto` | y | 10⁻²⁴ | `Prefix.y` |

**Alignment Notes:**
- ✅ All metric prefixes match QUDT factor definitions exactly
- ✅ Binary prefixes (kibi, mebi, etc.) are not part of SI/QUDT and intentionally excluded
- ✅ Prefixes apply multiplicatively to units, not affine temperatures

## Validation Strategy

### Scale Factor Verification

All scale factors in Veggerby.Units have been cross-validated against QUDT canonical values:

**SI Derived Units (sample):**
- ✅ Newton (N): 1 N = 1 kg·m/s² (QUDT: `conversionMultiplier: 1.0`)
- ✅ Joule (J): 1 J = 1 kg·m²/s² (QUDT: `conversionMultiplier: 1.0`)
- ✅ Pascal (Pa): 1 Pa = 1 kg/(m·s²) (QUDT: `conversionMultiplier: 1.0`)
- ✅ Watt (W): 1 W = 1 kg·m²/s³ (QUDT: `conversionMultiplier: 1.0`)

**Imperial/US Customary Units (sample):**
- ✅ Foot (ft): 1 ft = 0.3048 m (QUDT: `conversionMultiplier: 3.048E-1`)
- ✅ Pound-mass (lb): 1 lb = 0.45359237 kg (QUDT: `conversionMultiplier: 4.5359237E-1`)
- ✅ Gallon (US): 1 gal = 0.003785411784 m³ (QUDT: `conversionMultiplier: 3.785411784E-3`)

### Dimensional Exponent Verification

All derived units produce dimensional signatures matching QUDT:

**Example: Pressure (Pascal)**

QUDT definition:
```
unit:PA
  dimensionVector: L⁻¹ M¹ T⁻²
  conversionMultiplier: 1.0
  quantityKind: qudt:Pressure
```

Veggerby.Units:
```csharp
// Pa = N/m² = kg·m/s² / m² = kg/(m·s²)
var pascal = Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2));
// Dimension: M¹·L⁻¹·T⁻² ✓
```

### Temperature Affine Semantics

Veggerby.Units temperature handling aligns with QUDT specifications:

**Celsius Conversion (QUDT spec):**
```
unit:DEG_C
  conversionOffset: 273.15
  conversionMultiplier: 1.0
  hasBaseUnit: unit:K
```

**Veggerby.Units Implementation:**
```csharp
// °C to K: K = °C + 273.15
var celsius = new AffineUnit("°C", "celsius", Unit.SI.K, scale: 1.0, offset: 273.15);

// Example: 20°C → 293.15 K
var temp = new DoubleMeasurement(20, Unit.SI.C);
var kelvin = temp.ConvertTo(Unit.SI.K);  // 293.15 K ✓
```

**Fahrenheit Conversion (via Rankine):**
```csharp
// °F to °R: R = °F + 459.67
// °R to K: K = R × (5/9)
// Combined: K = (°F + 459.67) × (5/9)
```

## Intentional Deviations

The following QUDT features are **intentionally excluded** from Veggerby.Units:

| QUDT Feature | Reason for Exclusion |
|--------------|---------------------|
| **RDF/OWL ontology classes** | Adds semantic web dependencies, runtime complexity |
| **Linked data URIs** | No benefit for deterministic algebra; URIs are documentation-only |
| **Transitive inference graphs** | Violates explicit design principle; all conversions must be explicit |
| **Dynamic quantity derivation** | Non-deterministic; Veggerby.Units requires compile-time definition |
| **Provenance metadata** | Unnecessary for core algebra; bloats memory footprint |
| **Multi-language labels** | Internationalization is out of scope |

**Key Design Differences:**

1. **QUDT allows runtime quantity kind discovery via SPARQL queries**  
   → Veggerby.Units requires static `QuantityKind` registration at compile time

2. **QUDT supports transitive unit conversion chains** (e.g., A→B→C)  
   → Veggerby.Units requires direct scale/affine relationships only

3. **QUDT includes extensive metadata** (symbols in multiple languages, historical notes)  
   → Veggerby.Units includes only essential data (symbol, canonical unit, tags)

## Missing Units (Potential Additions)

Based on QUDT catalog review, the following units may be added in future releases:

**Pressure & Vacuum:**
- `torr` (1/760 atm) – QUDT: `unit:TORR`
- `bar` (100,000 Pa) – QUDT: `unit:BAR`
- `mmHg` (millimeter of mercury) – QUDT: `unit:MilliM_HG`

**Radiation:**
- `rem` (roentgen equivalent man) – QUDT: `unit:REM`
- `rad` (radiation absorbed dose) – QUDT: `unit:RAD`
- `curie` (Ci) – QUDT: `unit:Curie`
- `becquerel` (Bq) – QUDT: `unit:BQ`

**Energy (specialized):**
- `electronvolt` (eV) – QUDT: `unit:EV`
- `calorie (thermochemical)` – QUDT: `unit:CAL_TH`
- `BTU` variants – QUDT: `unit:BTU_IT`, `unit:BTU_TH`

**Length (specialized):**
- `angstrom` (Å) – QUDT: `unit:ANGSTROM`
- `mil` (1/1000 inch) – QUDT: `unit:MIL`
- `astronomical unit` – QUDT: `unit:AU`

**Engineering:**
- `slug` (Imperial mass unit) – QUDT: `unit:SLUG`
- `poundal` (Imperial force) – QUDT: `unit:PDL`

*Note: Units are added based on user demand, not merely QUDT presence. See issue tracker for requests.*

## Benefits of QUDT Alignment

**For Veggerby.Units Development:**
- ✅ External validation of correctness reduces definition errors
- ✅ Confidence in scientific accuracy against authoritative source
- ✅ Identification of missing but useful units from comprehensive catalog
- ✅ Alignment with international standards (SI Brochure, NIST, BIPM)
- ✅ Reduced risk of dimensional algebra bugs

**For Library Users:**
- ✅ Trust in library accuracy via QUDT cross-validation
- ✅ Interoperability with QUDT-aware systems (compatible definitions, not direct integration)
- ✅ Access to well-researched quantity classifications
- ✅ Clear scientific foundation for domain-specific applications

**What This Alignment Is NOT:**
- ❌ Adding QUDT as a runtime dependency
- ❌ Implementing ontology reasoning or SPARQL queries
- ❌ Changing the deterministic, explicit design philosophy
- ❌ Introducing external data sources or configuration files

## References

### QUDT Specifications
- **QUDT Homepage:** http://www.qudt.org/
- **QUDT Vocabulary Browser:** http://www.qudt.org/doc/DOC_VOCAB-UNITS.html
- **QUDT GitHub Repository:** https://github.com/qudt/qudt-public-repo
- **QUDT Unit Search Tool:** http://www.qudt.org/search.html

### SI/Metrology Standards
- **SI Brochure (BIPM):** https://www.bipm.org/en/publications/si-brochure/
- **NIST Guide to SI:** https://www.nist.gov/pml/owm/metric-si/si-units
- **ISO 80000 (Quantities and Units):** https://www.iso.org/standard/76921.html

### Related Veggerby.Units Documentation
- `docs/quantity-kinds.md` – Built-in QuantityKind catalog
- `docs/quantities.md` – Quantity system overview
- `docs/qudt-mapping-table.md` – Detailed QUDT ↔ Veggerby.Units mapping

## Conclusion

QUDT serves as a **conceptual anchor** and **validation reference** for Veggerby.Units, ensuring scientific rigor while preserving the library's core principles of **determinism, explicitness, and zero runtime dependencies**.

By aligning with QUDT semantics—without adopting its ontology infrastructure—Veggerby.Units achieves both **scientific correctness** and **engineering pragmatism**.
