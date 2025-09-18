namespace Veggerby.Units.Quantities;

/// <summary>
/// Transport phenomena and flow: fluxes, viscosities, conductivities (thermal), heat transfer coefficients, flow rates.
/// </summary>
public static partial class QuantityKinds
{
    /// <summary>Volumetric flow rate (m^3/s).</summary>
    public static readonly QuantityKind VolumetricFlowRate = new(
        "VolumetricFlowRate",
        (Unit.SI.m ^ 3) / Unit.SI.s,
        "Q_v",
        tags: [QuantityKindTags.DomainTransport]);

    /// <summary>Mass flow rate (kg/s).</summary>
    public static readonly QuantityKind MassFlowRate = new(
        "MassFlowRate",
        Unit.SI.kg / Unit.SI.s,
        "ṁ",
        tags: [QuantityKindTags.DomainTransport]);

    /// <summary>Molar flow rate (mol/s).</summary>
    public static readonly QuantityKind MolarFlowRate = new(
        "MolarFlowRate",
        Unit.SI.n / Unit.SI.s,
        "n_dot",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainChemistry]);

    /// <summary>Dynamic viscosity (Pa·s).</summary>
    public static readonly QuantityKind DynamicViscosity = new(
        "DynamicViscosity",
        (Unit.SI.kg / (Unit.SI.m * Unit.SI.s)),
        "η",
        tags: [QuantityKindTags.DomainTransport]);

    /// <summary>Kinematic viscosity (m^2/s).</summary>
    public static readonly QuantityKind KinematicViscosity = new(
        "KinematicViscosity",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "ν",
        tags: [QuantityKindTags.DomainTransport]);

    /// <summary>Specific weight (N/m^3).</summary>
    public static readonly QuantityKind SpecificWeight = new(
        "SpecificWeight",
        (Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 3),
        "γ_spec",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainMaterial]);

    /// <summary>Vorticity (1/s).</summary>
    public static readonly QuantityKind Vorticity = new(
        "Vorticity",
        1 / Unit.SI.s,
        "ω_v",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainKinematics]);

    /// <summary>Circulation (m^2/s).</summary>
    public static readonly QuantityKind Circulation = new(
        "Circulation",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "Γ",
        tags: [QuantityKindTags.DomainTransport, QuantityKindTags.DomainKinematics]);
}