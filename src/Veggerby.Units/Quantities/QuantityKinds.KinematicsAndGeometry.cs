namespace Veggerby.Units.Quantities;

/// <summary>
/// Pure kinematics (angles, angular rates, motion derivatives) and geometric curvature/shape descriptors.
/// </summary>
/// <remarks>
/// Quantity kind definitions align with QUDT ontology specifications for kinematics and geometry.
/// See <c>docs/qudt-mapping-table.md</c> for detailed QUDT URI mappings.
/// <para>
/// QUDT Reference: http://qudt.org/doc/DOC_VOCAB-QUANTITY-KINDS.html
/// </para>
/// </remarks>
public static partial class QuantityKinds
{
    /// <summary>Angle (dimensionless, radians).</summary>
    public static readonly QuantityKind Angle = new(
        "Angle",
        Unit.None,
        "θ",
        tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainMechanics]);

    /// <summary>Solid angle (steradian).</summary>
    public static readonly QuantityKind SolidAngle = new(
        "SolidAngle",
        Unit.SI.sr,
        "Ω_s",
        tags: [QuantityKindTags.DomainGeometry]);

    /// <summary>Angular velocity (rad/s).</summary>
    public static readonly QuantityKind AngularVelocity = new(
        "AngularVelocity",
        Unit.SI.rad / Unit.SI.s,
        "ω",
        tags: [QuantityKindTags.DomainKinematics]);

    /// <summary>Angular acceleration (rad/s^2).</summary>
    public static readonly QuantityKind AngularAcceleration = new(
        "AngularAcceleration",
        Unit.SI.rad / (Unit.SI.s ^ 2),
        "α_ang",
        tags: [QuantityKindTags.DomainKinematics]);

    /// <summary>Angular momentum (kg·m^2/s).</summary>
    public static readonly QuantityKind AngularMomentum = new(
        "AngularMomentum",
        Unit.SI.kg * (Unit.SI.m ^ 2) / Unit.SI.s,
        "L_ang",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Moment of inertia (kg·m^2).</summary>
    public static readonly QuantityKind MomentOfInertia = new(
        "MomentOfInertia",
        Unit.SI.kg * (Unit.SI.m ^ 2),
        "I_m",
        tags: [QuantityKindTags.DomainMechanics]);

    /// <summary>Jerk (m/s^3) third derivative of position.</summary>
    public static readonly QuantityKind Jerk = new(
        "Jerk",
        Unit.SI.m / (Unit.SI.s ^ 3),
        "j",
        tags: [QuantityKindTags.DomainKinematics]);

    /// <summary>Wavenumber (1/m).</summary>
    public static readonly QuantityKind Wavenumber = new(
        "Wavenumber",
        1 / Unit.SI.m,
        "k",
        tags: [QuantityKindTags.DomainGeometry]);

    /// <summary>Curvature (1/m).</summary>
    public static readonly QuantityKind Curvature = new(
        "Curvature",
        1 / Unit.SI.m,
        "κ",
        tags: [QuantityKindTags.DomainGeometry]);

    /// <summary>Torsion (1/m) geometric curve torsion.</summary>
    public static readonly QuantityKind Torsion = new(
        "Torsion",
        1 / Unit.SI.m,
        "τ_tor",
        tags: [QuantityKindTags.DomainGeometry]);

    /// <summary>Specific angular momentum (m^2/s).</summary>
    public static readonly QuantityKind SpecificAngularMomentum = new(
        "SpecificAngularMomentum",
        (Unit.SI.m ^ 2) / Unit.SI.s,
        "h_ang",
        tags: [QuantityKindTags.DomainMechanics, QuantityKindTags.DomainKinematics]);
}