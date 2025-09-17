namespace Veggerby.Units.Quantities;

/// <summary>
/// Core mechanical / kinematic quantity kinds extending the semantic disambiguation surface.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Base length kind (meters).</summary>
    public static readonly QuantityKind Length = new(
        "Length",
        Unit.SI.m,
        "L");

    /// <summary>Base time kind (seconds).</summary>
    public static readonly QuantityKind Time = new(
        "Time",
        Unit.SI.s,
        "t");

    /// <summary>Base mass kind (kilograms) included for completeness if future rules require.</summary>
    public static readonly QuantityKind Mass = new(
        "Mass",
        Unit.SI.kg,
        "m");
    /// <summary>Power (W) = J/s.</summary>
    public static readonly QuantityKind Power = new(
        "Power",
        Energy.CanonicalUnit / Unit.SI.s,
        "P");

    /// <summary>Force (N) = kg·m/s².</summary>
    public static readonly QuantityKind Force = new(
        "Force",
        Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2),
        "F");

    /// <summary>Pressure (Pa) = N/m².</summary>
    public static readonly QuantityKind Pressure = new(
        "Pressure",
        Force.CanonicalUnit / (Unit.SI.m ^ 2),
        "p");

    /// <summary>Volume (m³) = m^3 (geometric volume).</summary>
    public static readonly QuantityKind Volume = new(
        "Volume",
        Unit.SI.m ^ 3,
        "V");

    /// <summary>Area (m²) = m^2 (geometric area).</summary>
    public static readonly QuantityKind Area = new(
        "Area",
        Unit.SI.m ^ 2,
        "A_r");

    /// <summary>Velocity (m/s) = m·s^-1.</summary>
    public static readonly QuantityKind Velocity = new(
        "Velocity",
        Unit.SI.m / Unit.SI.s,
        "v");

    /// <summary>Acceleration (m/s²) = m·s^-2.</summary>
    public static readonly QuantityKind Acceleration = new(
        "Acceleration",
        Unit.SI.m / (Unit.SI.s ^ 2),
        "a");

    /// <summary>Momentum (kg·m/s).</summary>
    public static readonly QuantityKind Momentum = new(
        "Momentum",
        Unit.SI.kg * Unit.SI.m / Unit.SI.s,
        "p_m");

    /// <summary>Energy density (J/m³).</summary>
    public static readonly QuantityKind EnergyDensity = new(
        "EnergyDensity",
        Energy.CanonicalUnit / (Unit.SI.m ^ 3),
        "u");

    /// <summary>Specific heat capacity (J/(kg·K)).</summary>
    public static readonly QuantityKind SpecificHeatCapacity = new(
        "SpecificHeatCapacity",
        Energy.CanonicalUnit / (Unit.SI.kg * Unit.SI.K),
        "c_p");

    /// <summary>Specific entropy (J/(kg·K)).</summary>
    public static readonly QuantityKind SpecificEntropy = new(
        "SpecificEntropy",
        Energy.CanonicalUnit / (Unit.SI.kg * Unit.SI.K),
        "s");
}
