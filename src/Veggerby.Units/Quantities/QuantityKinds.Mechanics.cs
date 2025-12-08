namespace Veggerby.Units.Quantities;

/// <summary>
/// Core mechanical / kinematic quantity kinds extending the semantic disambiguation surface.
/// </summary>
/// <remarks>
/// Quantity kind definitions align with QUDT ontology specifications for mechanical and kinematic quantities.
/// See <c>docs/qudt-mapping-table.md</c> for detailed QUDT URI mappings.
/// <para>
/// QUDT Reference: http://qudt.org/doc/DOC_VOCAB-QUANTITY-KINDS.html
/// </para>
/// </remarks>
public static partial class QuantityKinds
{
    /// <summary>Base length kind (meters).</summary>
    /// <remarks>QUDT: <c>quantitykind:Length</c></remarks>
    public static readonly QuantityKind Length = new(
        "Length",
        Unit.SI.m,
        "L");

    /// <summary>Base time kind (seconds).</summary>
    /// <remarks>QUDT: <c>quantitykind:Time</c></remarks>
    public static readonly QuantityKind Time = new(
        "Time",
        Unit.SI.s,
        "t");

    /// <summary>Base mass kind (kilograms) included for completeness if future rules require.</summary>
    /// <remarks>QUDT: <c>quantitykind:Mass</c></remarks>
    public static readonly QuantityKind Mass = new(
        "Mass",
        Unit.SI.kg,
        "m");
    /// <summary>Power (W) = J/s.</summary>
    /// <remarks>QUDT: <c>quantitykind:Power</c></remarks>
    public static readonly QuantityKind Power = new(
        "Power",
        Energy.CanonicalUnit / Unit.SI.s,
        "P");

    /// <summary>Force (N) = kg·m/s².</summary>
    /// <remarks>QUDT: <c>quantitykind:Force</c></remarks>
    public static readonly QuantityKind Force = new(
        "Force",
        Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2),
        "F");

    /// <summary>Pressure (Pa) = N/m².</summary>
    /// <remarks>QUDT: <c>quantitykind:Pressure</c></remarks>
    public static readonly QuantityKind Pressure = new(
        "Pressure",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "p");

    /// <summary>Young's modulus (Pa).</summary>
    /// <remarks>QUDT: <c>quantitykind:ModulusOfElasticity</c></remarks>
    public static readonly QuantityKind YoungsModulus = new(
        "YoungsModulus",
        Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)),
        "E",
        tags: [QuantityKindTags.DomainMaterial, QuantityKindTags.DomainMechanics]);

    /// <summary>Volume (m³) = m^3 (geometric volume).</summary>
    /// <remarks>QUDT: <c>quantitykind:Volume</c></remarks>
    public static readonly QuantityKind Volume = new(
        "Volume",
        Unit.SI.m ^ 3,
        "V");

    /// <summary>Area (m²) = m^2 (geometric area).</summary>
    /// <remarks>QUDT: <c>quantitykind:Area</c></remarks>
    public static readonly QuantityKind Area = new(
        "Area",
        Unit.SI.m ^ 2,
        "A_r");

    /// <summary>Velocity (m/s) = m·s^-1.</summary>
    /// <remarks>QUDT: <c>quantitykind:Velocity</c></remarks>
    public static readonly QuantityKind Velocity = new(
        "Velocity",
        Unit.SI.m / Unit.SI.s,
        "v");

    /// <summary>Acceleration (m/s²) = m·s^-2.</summary>
    /// <remarks>QUDT: <c>quantitykind:Acceleration</c></remarks>
    public static readonly QuantityKind Acceleration = new(
        "Acceleration",
        Unit.SI.m / (Unit.SI.s ^ 2),
        "a");

    /// <summary>Momentum (kg·m/s).</summary>
    /// <remarks>QUDT: <c>quantitykind:LinearMomentum</c></remarks>
    public static readonly QuantityKind Momentum = new(
        "Momentum",
        Unit.SI.kg * Unit.SI.m / Unit.SI.s,
        "p_m");

    /// <summary>Energy density (J/m³).</summary>
    /// <remarks>QUDT: <c>quantitykind:EnergyDensity</c></remarks>
    public static readonly QuantityKind EnergyDensity = new(
        "EnergyDensity",
        Energy.CanonicalUnit / (Unit.SI.m ^ 3),
        "u");

    /// <summary>Specific heat capacity (J/(kg·K)).</summary>
    /// <remarks>QUDT: <c>quantitykind:SpecificHeatCapacity</c></remarks>
    public static readonly QuantityKind SpecificHeatCapacity = new(
        "SpecificHeatCapacity",
        Energy.CanonicalUnit / (Unit.SI.kg * Unit.SI.K),
        "c_p");

    /// <summary>Specific entropy (J/(kg·K)).</summary>
    /// <remarks>QUDT: <c>quantitykind:SpecificEntropy</c></remarks>
    public static readonly QuantityKind SpecificEntropy = new(
        "SpecificEntropy",
        Energy.CanonicalUnit / (Unit.SI.kg * Unit.SI.K),
        "s");
}