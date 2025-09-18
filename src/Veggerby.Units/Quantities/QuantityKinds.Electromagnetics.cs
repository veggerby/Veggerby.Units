namespace Veggerby.Units.Quantities;

/// <summary>
/// Electromagnetic field, circuit, and charge transport quantities (fields, potentials, impedances, mobilities, susceptibilities).
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Electric current (A).</summary>
    public static readonly QuantityKind ElectricCurrent = new("ElectricCurrent", Unit.SI.A, "I", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Frequency (1/s).</summary>
    public static readonly QuantityKind Frequency = new("Frequency", 1 / Unit.SI.s, "Hz", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric charge (C = A·s).</summary>
    public static readonly QuantityKind ElectricCharge = new("ElectricCharge", Unit.SI.A * Unit.SI.s, "q", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Voltage (V = J/C).</summary>
    public static readonly QuantityKind Voltage = new("Voltage", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.s), "V", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric resistance (Ω).</summary>
    public static readonly QuantityKind ElectricResistance = new("ElectricResistance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A * Unit.SI.s), "Ω", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric conductance (S).</summary>
    public static readonly QuantityKind ElectricConductance = new("ElectricConductance", (Unit.SI.A * Unit.SI.A * Unit.SI.s) / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)), "S", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Capacitance (F).</summary>
    public static readonly QuantityKind Capacitance = new("Capacitance", ((Unit.SI.A * Unit.SI.s) ^ 2) / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)), "F", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Inductance (H).</summary>
    public static readonly QuantityKind Inductance = new("Inductance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A), "H", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetic flux (Wb).</summary>
    public static readonly QuantityKind MagneticFlux = new("MagneticFlux", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / Unit.SI.A, "Wb", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetic flux density (T).</summary>
    public static readonly QuantityKind MagneticFluxDensity = new("MagneticFluxDensity", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * (Unit.SI.m ^ 2)), "T", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric field strength (V/m).</summary>
    public static readonly QuantityKind ElectricFieldStrength = new("ElectricFieldStrength", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.s * Unit.SI.m), "E_f", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric charge density (C/m^3).</summary>
    public static readonly QuantityKind ElectricChargeDensity = new("ElectricChargeDensity", (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 3), "ρ_q", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric current density (A/m^2).</summary>
    public static readonly QuantityKind ElectricCurrentDensity = new("ElectricCurrentDensity", Unit.SI.A / (Unit.SI.m ^ 2), "J_d", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetic field strength (A/m).</summary>
    public static readonly QuantityKind MagneticFieldStrength = new("MagneticFieldStrength", Unit.SI.A / Unit.SI.m, "H", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetization (A/m).</summary>
    public static readonly QuantityKind Magnetization = new("Magnetization", Unit.SI.A / Unit.SI.m, "M_mag", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric displacement (C/m^2).</summary>
    public static readonly QuantityKind ElectricDisplacement = new("ElectricDisplacement", (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2), "D", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetic vector potential (Wb/m).</summary>
    public static readonly QuantityKind MagneticVectorPotential = new("MagneticVectorPotential", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.m), "A_vec", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Permittivity (F/m).</summary>
    public static readonly QuantityKind Permittivity = new("Permittivity", ((Unit.SI.A * Unit.SI.s) ^ 2) / ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.m), "ε", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Permeability (H/m).</summary>
    public static readonly QuantityKind Permeability = new("Permeability", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A * Unit.SI.m), "μ", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Impedance (Ω).</summary>
    public static readonly QuantityKind Impedance = new("Impedance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.A * Unit.SI.s), "Z", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Admittance (S).</summary>
    public static readonly QuantityKind Admittance = new("Admittance", (Unit.SI.A * Unit.SI.A * Unit.SI.s) / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)), "Y", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electrical conductivity (S/m).</summary>
    public static readonly QuantityKind ElectricalConductivity = new("ElectricalConductivity", (Unit.SI.s ^ 3) * (Unit.SI.A ^ 2) / (Unit.SI.kg * (Unit.SI.m ^ 3)), "σ_cond", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electrical resistivity (Ω·m).</summary>
    public static readonly QuantityKind ElectricalResistivity = new("ElectricalResistivity", (Unit.SI.kg * (Unit.SI.m ^ 3)) / ((Unit.SI.s ^ 3) * (Unit.SI.A ^ 2)), "ρ_res", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Surface charge density (C/m^2).</summary>
    public static readonly QuantityKind SurfaceChargeDensity = new("SurfaceChargeDensity", (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2), "σ_q", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Line charge density (C/m).</summary>
    public static readonly QuantityKind LineChargeDensity = new("LineChargeDensity", (Unit.SI.A * Unit.SI.s) / Unit.SI.m, "λ_q", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Surface current density (A/m).</summary>
    public static readonly QuantityKind SurfaceCurrentDensity = new("SurfaceCurrentDensity", Unit.SI.A / Unit.SI.m, "K_s", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric dipole moment (C·m).</summary>
    public static readonly QuantityKind ElectricDipoleMoment = new("ElectricDipoleMoment", (Unit.SI.A * Unit.SI.s) * Unit.SI.m, "p_e", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Magnetic dipole moment (A·m^2).</summary>
    public static readonly QuantityKind MagneticDipoleMoment = new("MagneticDipoleMoment", Unit.SI.A * (Unit.SI.m ^ 2), "m_dip", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Polarization (C/m^2).</summary>
    public static readonly QuantityKind Polarization = new("Polarization", (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2), "P_pol", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Bound current density (A/m^2).</summary>
    public static readonly QuantityKind BoundCurrentDensity = new("BoundCurrentDensity", Unit.SI.A / (Unit.SI.m ^ 2), "J_b", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Hall coefficient (m^3/C).</summary>
    public static readonly QuantityKind HallCoefficient = new("HallCoefficient", (Unit.SI.m ^ 3) / (Unit.SI.A * Unit.SI.s), "R_H", tags: [QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Seebeck coefficient (V/K).</summary>
    public static readonly QuantityKind SeebeckCoefficient = new("SeebeckCoefficient", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.A * Unit.SI.s * Unit.SI.K), "S_seeb", tags: [QuantityKindTags.DomainElectromagnetic, QuantityKindTags.DomainThermodynamic]);
    /// <summary>Magnetic susceptibility (dimensionless).</summary>
    public static readonly QuantityKind MagneticSusceptibility = new("MagneticSusceptibility", Unit.None, "χ_m", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Electric susceptibility (dimensionless).</summary>
    public static readonly QuantityKind ElectricSusceptibility = new("ElectricSusceptibility", Unit.None, "χ_e", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Relative permittivity (dimensionless).</summary>
    public static readonly QuantityKind RelativePermittivity = new("RelativePermittivity", Unit.None, "ε_r", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Relative permeability (dimensionless).</summary>
    public static readonly QuantityKind RelativePermeability = new("RelativePermeability", Unit.None, "μ_r", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainElectromagnetic]);
    /// <summary>Charge carrier mobility (m^2/(V·s)).</summary>
    public static readonly QuantityKind ChargeMobility = new("ChargeMobility", (Unit.SI.m ^ 2) * (Unit.SI.A * Unit.SI.s) / ((Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) * Unit.SI.s), "μ_e", tags: [QuantityKindTags.DomainElectromagnetic]);
}