namespace Veggerby.Units.Quantities;

/// <summary>
/// Extended SI derived and domain semantic quantity kinds (electromagnetic, radiation, transport, material).
/// These add semantic disambiguation atop existing dimensional compositions without altering core algebra.
/// </summary>
public static partial class QuantityKinds
{
    // Electromagnetic
    /// <summary>
    /// Electric current (ampere) – flow of electric charge (base dimension I).
    /// </summary>
    /// <summary>Electric current (ampere) – flow of electric charge (base dimension I).</summary>
    public static readonly QuantityKind ElectricCurrent = new(
        "ElectricCurrent",
        Unit.SI.A,
        "I",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Frequency (Hz) – occurrences per unit time (s^-1).
    /// </summary>
    /// <summary>Frequency (Hz) – occurrences per unit time (s^-1).</summary>
    public static readonly QuantityKind Frequency = new(
        "Frequency",
        1 / Unit.SI.s,
        "Hz",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric charge (coulomb) – time integral of electric current (A·s).
    /// </summary>
    /// <summary>Electric charge (coulomb) – time integral of electric current (A·s).</summary>
    public static readonly QuantityKind ElectricCharge = new(
        "ElectricCharge",
        Unit.SI.A * Unit.SI.s,
        "q",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric potential (volt) – energy per unit charge (J·C^-1).
    /// </summary>
    /// <summary>Electric potential (volt) – energy per unit charge (J·C^-1).</summary>
    public static readonly QuantityKind Voltage = new(
        "Voltage",
        QuantityKinds.Energy.CanonicalUnit / (Unit.SI.A * Unit.SI.s), // (J)/(A·s) = kg·m^2/(s^3·A)
        "V",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric resistance (ohm) – voltage per unit current (V·A^-1).
    /// </summary>
    /// <summary>Electric resistance (ohm) – voltage per unit current (V·A^-1).</summary>
    public static readonly QuantityKind ElectricResistance = new(
        "ElectricResistance",
        // R = V / I = (Energy / (A·s)) / A = Energy / (A^2 · s)
        QuantityKinds.Energy.CanonicalUnit / (Unit.SI.A * Unit.SI.A * Unit.SI.s), // kg·m^2/(s^3·A^2)
        "Ω",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric conductance (siemens) – inverse of resistance (A·V^-1).
    /// </summary>
    /// <summary>Electric conductance (siemens) – inverse of resistance (A·V^-1).</summary>
    public static readonly QuantityKind ElectricConductance = new(
        "ElectricConductance",
        // G = 1/R = A^2 · s / Energy
        (Unit.SI.A * Unit.SI.A * Unit.SI.s) / QuantityKinds.Energy.CanonicalUnit,
        "S",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Capacitance (farad) – charge per unit voltage (C·V^-1).
    /// </summary>
    /// <summary>Capacitance (farad) – charge per unit voltage (C·V^-1).</summary>
    public static readonly QuantityKind Capacitance = new(
        "Capacitance",
        // C = Q / V = Charge^2 / Energy; Charge = A·s => F = (A·s)^2 / Energy = A^2·s^4/(kg·m^2)
        ((Unit.SI.A * Unit.SI.s) ^ 2) / QuantityKinds.Energy.CanonicalUnit,
        "F",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Inductance (henry) – magnetic flux per unit current (Wb·A^-1).
    /// </summary>
    /// <summary>Inductance (henry) – magnetic flux per unit current (Wb·A^-1).</summary>
    public static readonly QuantityKind Inductance = new(
        "Inductance",
        // L = Φ / I and Φ = Energy / A => L = Energy / A^2
        QuantityKinds.Energy.CanonicalUnit / (Unit.SI.A * Unit.SI.A), // H = kg·m^2/(A^2·s^2)
        "H",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Magnetic flux (weber) – time integral of voltage (V·s).
    /// </summary>
    /// <summary>Magnetic flux (weber) – time integral of voltage (V·s).</summary>
    public static readonly QuantityKind MagneticFlux = new(
        "MagneticFlux",
        QuantityKinds.Energy.CanonicalUnit / Unit.SI.A, // Wb = V·s = J/A
        "Wb",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Magnetic flux density (tesla) – flux per unit area (Wb·m^-2).
    /// </summary>
    /// <summary>Magnetic flux density (tesla) – flux per unit area (Wb·m^-2).</summary>
    public static readonly QuantityKind MagneticFluxDensity = new(
        "MagneticFluxDensity",
        QuantityKinds.Energy.CanonicalUnit / (Unit.SI.A * (Unit.SI.m ^ 2)), // T = Wb/m^2
        "T",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric field strength – voltage gradient (V·m^-1).
    /// </summary>
    /// <summary>Electric field strength – voltage gradient (V·m^-1).</summary>
    public static readonly QuantityKind ElectricFieldStrength = new(
        "ElectricFieldStrength",
        QuantityKinds.Energy.CanonicalUnit / (Unit.SI.A * Unit.SI.s * Unit.SI.m), // V/m
        "E_f",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric charge density – charge per unit volume (C·m^-3).
    /// </summary>
    /// <summary>Electric charge density – charge per unit volume (C·m^-3).</summary>
    public static readonly QuantityKind ElectricChargeDensity = new(
        "ElectricChargeDensity",
        (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 3),
        "ρ_q",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    /// <summary>
    /// Electric current density – current per unit area (A·m^-2).
    /// </summary>
    /// <summary>Electric current density – current per unit area (A·m^-2).</summary>
    public static readonly QuantityKind ElectricCurrentDensity = new(
        "ElectricCurrentDensity",
        Unit.SI.A / (Unit.SI.m ^ 2),
        "J_d",
    tags: new[] { QuantityKindTags.DomainElectromagnetic });

    // Luminous / Optical
    /// <summary>
    /// Luminous flux (lumen) – photometric radiant power (cd·sr; here base cd due to dimensionless sr).
    /// </summary>
    /// <summary>Luminous flux (lumen) – photometric radiant power (cd·sr).</summary>
    public static readonly QuantityKind LuminousFlux = new(
        "LuminousFlux",
        Unit.SI.cd, // lumen shares candela base with steradian dimensionless
        "lm",
    tags: new[] { QuantityKindTags.DomainOptics });

    /// <summary>
    /// Illuminance (lux) – luminous flux per unit area (lm·m^-2).
    /// </summary>
    /// <summary>Illuminance (lux) – luminous flux per unit area (lm·m^-2).</summary>
    public static readonly QuantityKind Illuminance = new(
        "Illuminance",
        Unit.SI.cd / (Unit.SI.m ^ 2), // lux
        "lx",
    tags: new[] { QuantityKindTags.DomainOptics });

    // Radiation
    /// <summary>
    /// Radioactivity (becquerel) – disintegrations per second (s^-1).
    /// </summary>
    /// <summary>Radioactivity (becquerel) – disintegrations per second (s^-1).</summary>
    public static readonly QuantityKind Radioactivity = new(
        "Radioactivity",
        1 / Unit.SI.s, // Bq
        "Bq",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>
    /// Absorbed dose (gray) – energy deposited per unit mass (J·kg^-1).
    /// </summary>
    /// <summary>Absorbed dose (gray) – energy deposited per unit mass (J·kg^-1).</summary>
    public static readonly QuantityKind AbsorbedDose = new(
        "AbsorbedDose",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), // Gy = J/kg = m^2/s^2
        "Gy",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>
    /// Dose equivalent (sievert) – biological effect dose (same dimension as gray).
    /// </summary>
    /// <summary>Dose equivalent (sievert) – biological effect dose (same dimension as gray).</summary>
    public static readonly QuantityKind DoseEquivalent = new(
        "DoseEquivalent",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), // Sv same dimension as Gy
        "Sv",
    tags: new[] { QuantityKindTags.DomainRadiation });

    /// <summary>
    /// Catalytic activity (katal) – amount of substance transformed per second (mol·s^-1).
    /// </summary>
    /// <summary>Catalytic activity (katal) – amount of substance transformed per second (mol·s^-1).</summary>
    public static readonly QuantityKind CatalyticActivity = new(
        "CatalyticActivity",
        Unit.SI.n / Unit.SI.s, // katal
        "kat",
    tags: new[] { QuantityKindTags.DomainChemistry });

    // Transport / Material
    /// <summary>
    /// Mass density – mass per unit volume (kg·m^-3).
    /// </summary>
    /// <summary>Mass density – mass per unit volume (kg·m^-3).</summary>
    public static readonly QuantityKind MassDensity = new(
        "MassDensity",
        Unit.SI.kg / (Unit.SI.m ^ 3),
        "ρ",
    tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>
    /// Specific energy – energy per unit mass (J·kg^-1 = m^2·s^-2).
    /// </summary>
    /// <summary>Specific energy – energy per unit mass (J·kg^-1).</summary>
    public static readonly QuantityKind SpecificEnergy = new(
        "SpecificEnergy",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), // J/kg
        "e_spec",
    tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>
    /// Specific power – power per unit mass (W·kg^-1 = m^2·s^-3).
    /// </summary>
    /// <summary>Specific power – power per unit mass (W·kg^-1).</summary>
    public static readonly QuantityKind SpecificPower = new(
        "SpecificPower",
        (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3), // W/kg
        "p_spec",
    tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>
    /// Specific volume – volume per unit mass (m^3·kg^-1).
    /// </summary>
    /// <summary>Specific volume – volume per unit mass (m^3·kg^-1).</summary>
    public static readonly QuantityKind SpecificVolume = new(
        "SpecificVolume",
        (Unit.SI.m ^ 3) / Unit.SI.kg,
        "v_spec",
    tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>
    /// Dynamic viscosity (Pa·s) – shear stress per velocity gradient (kg·m^-1·s^-1).
    /// </summary>
    /// <summary>Dynamic viscosity (Pa·s) – shear stress per velocity gradient (kg·m^-1·s^-1).</summary>
    public static readonly QuantityKind DynamicViscosity = new(
        "DynamicViscosity",
        (Unit.SI.kg / (Unit.SI.m * Unit.SI.s)), // Pa·s
        "η",
    tags: new[] { QuantityKindTags.DomainTransport });

    /// <summary>
    /// Kinematic viscosity – dynamic viscosity divided by mass density (m^2·s^-1).
    /// </summary>
    /// <summary>Kinematic viscosity – dynamic viscosity divided by mass density (m^2·s^-1).</summary>
    public static readonly QuantityKind KinematicViscosity = new(
        "KinematicViscosity",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "ν",
    tags: new[] { QuantityKindTags.DomainTransport });

    /// <summary>
    /// Thermal conductivity – heat flow rate per length and temperature gradient (W·m^-1·K^-1).
    /// </summary>
    /// <summary>Thermal conductivity – heat flow rate per length and temperature gradient (W·m^-1·K^-1).</summary>
    public static readonly QuantityKind ThermalConductivity = new(
        "ThermalConductivity",
        (QuantityKinds.Energy.CanonicalUnit / Unit.SI.s) / (Unit.SI.m * Unit.SI.K), // W/(m·K)
        "k_th",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainThermodynamic });

    /// <summary>
    /// Thermal diffusivity – thermal conductivity divided by volumetric heat capacity (m^2·s^-1).
    /// </summary>
    /// <summary>Thermal diffusivity – thermal conductivity divided by volumetric heat capacity (m^2·s^-1).</summary>
    public static readonly QuantityKind ThermalDiffusivity = new(
        "ThermalDiffusivity",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "α",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.DomainThermodynamic });

    /// <summary>
    /// Heat flux – heat transfer rate per unit area (W·m^-2).
    /// </summary>
    /// <summary>Heat flux – heat transfer rate per unit area (W·m^-2).</summary>
    public static readonly QuantityKind HeatFlux = new(
        "HeatFlux",
        (QuantityKinds.Energy.CanonicalUnit / Unit.SI.s) / (Unit.SI.m ^ 2), // W/m^2
        "q_dot",
    tags: new[] { QuantityKindTags.DomainTransport, QuantityKindTags.EnergyPathFunction });

    /// <summary>
    /// Surface tension – force per unit length (N·m^-1 = kg·s^-2).
    /// </summary>
    /// <summary>Surface tension – force per unit length (N·m^-1 = kg·s^-2).</summary>
    public static readonly QuantityKind SurfaceTension = new(
        "SurfaceTension",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / Unit.SI.m, // N/m = kg/s^2
        "γ",
    tags: new[] { QuantityKindTags.DomainMaterial });

    /// <summary>
    /// Molar mass – mass per amount of substance (kg·mol^-1).
    /// </summary>
    /// <summary>Molar mass – mass per amount of substance (kg·mol^-1).</summary>
    public static readonly QuantityKind MolarMass = new(
        "MolarMass",
        Unit.SI.kg / Unit.SI.n,
        "M",
    tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>
    /// Molar volume – volume per amount of substance (m^3·mol^-1).
    /// </summary>
    /// <summary>Molar volume – volume per amount of substance (m^3·mol^-1).</summary>
    public static readonly QuantityKind MolarVolume = new(
        "MolarVolume",
        (Unit.SI.m ^ 3) / Unit.SI.n,
        "V_m",
    tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>
    /// Molar concentration (amount concentration) – amount per unit volume (mol·m^-3).
    /// </summary>
    /// <summary>Molar concentration – amount per unit volume (mol·m^-3).</summary>
    public static readonly QuantityKind MolarConcentration = new(
        "MolarConcentration",
        Unit.SI.n / (Unit.SI.m ^ 3),
        "c",
    tags: new[] { QuantityKindTags.DomainChemistry });

    /// <summary>
    /// Mass concentration – mass per unit volume (kg·m^-3) (duplicate dimension of mass density but distinct semantic role).
    /// </summary>
    /// <summary>Mass concentration – mass per unit volume (kg·m^-3) distinct from MassDensity semantically.</summary>
    public static readonly QuantityKind MassConcentration = new(
        "MassConcentration",
        Unit.SI.kg / (Unit.SI.m ^ 3),
        "ρ_m",
    tags: new[] { QuantityKindTags.DomainChemistry, QuantityKindTags.DomainMaterial });
}