namespace Veggerby.Units.Quantities;

/// <summary>Advanced electromagnetic and related derived semantic kinds (conductivity, resistivity, densities, dipole moments).</summary>
public static partial class QuantityKinds
{
    /// <summary>Electrical conductivity (S/m).</summary>
    public static readonly QuantityKind ElectricalConductivity = new(
        "ElectricalConductivity",
        // S/m = (s^3·A^2)/(kg·m^3)
        (Unit.SI.s ^ 3) * (Unit.SI.A ^ 2) / (Unit.SI.kg * (Unit.SI.m ^ 3)),
        "σ_cond",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Electrical resistivity (Ω·m).</summary>
    public static readonly QuantityKind ElectricalResistivity = new(
        "ElectricalResistivity",
        // Ω·m = (kg·m^3)/(s^3·A^2)
        (Unit.SI.kg * (Unit.SI.m ^ 3)) / ((Unit.SI.s ^ 3) * (Unit.SI.A ^ 2)),
        "ρ_res",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Surface charge density (C/m^2).</summary>
    public static readonly QuantityKind SurfaceChargeDensity = new(
        "SurfaceChargeDensity",
        (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2),
        "σ_q",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Line charge density (C/m).</summary>
    public static readonly QuantityKind LineChargeDensity = new(
        "LineChargeDensity",
        (Unit.SI.A * Unit.SI.s) / Unit.SI.m,
        "λ_q",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Surface current density (A/m).</summary>
    public static readonly QuantityKind SurfaceCurrentDensity = new(
        "SurfaceCurrentDensity",
        Unit.SI.A / Unit.SI.m,
        "K_s",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Electric dipole moment (C·m).</summary>
    public static readonly QuantityKind ElectricDipoleMoment = new(
        "ElectricDipoleMoment",
        (Unit.SI.A * Unit.SI.s) * Unit.SI.m,
        "p_e",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Magnetic dipole moment (A·m^2).</summary>
    public static readonly QuantityKind MagneticDipoleMoment = new(
        "MagneticDipoleMoment",
        Unit.SI.A * (Unit.SI.m ^ 2),
        "m_dip",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Polarization (C/m^2) distinct from surface charge density semantic anchor.</summary>
    public static readonly QuantityKind Polarization = new(
        "Polarization",
        (Unit.SI.A * Unit.SI.s) / (Unit.SI.m ^ 2),
        "P_pol",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });

    /// <summary>Bound (magnetization) current density (A/m^2).</summary>
    public static readonly QuantityKind BoundCurrentDensity = new(
        "BoundCurrentDensity",
        Unit.SI.A / (Unit.SI.m ^ 2),
        "J_b",
        tags: new[] { QuantityKindTag.Get("Domain.Electromagnetic") });
}