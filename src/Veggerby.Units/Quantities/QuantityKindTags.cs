using System;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Canonical strongly-named <see cref="QuantityKindTag"/> instances for common domains/forms.
/// Prefer these over repeated <c>QuantityKindTag.Get(name)</c> calls when defining built-in kinds
/// to improve discoverability and minimize string duplication. The underlying <see cref="QuantityKindTag"/>
/// already interns by name; this surface is a convenience and governance aid.
/// </summary>
public static class QuantityKindTags
{
    // Core domain roots
    /// <summary>Geometry related domain tag.</summary>
    public static readonly QuantityKindTag DomainGeometry = QuantityKindTag.Get("Domain.Geometry");
    /// <summary>Kinematics related domain tag.</summary>
    public static readonly QuantityKindTag DomainKinematics = QuantityKindTag.Get("Domain.Kinematics");
    /// <summary>Mechanics related domain tag.</summary>
    public static readonly QuantityKindTag DomainMechanics = QuantityKindTag.Get("Domain.Mechanics");
    /// <summary>Material science related domain tag.</summary>
    public static readonly QuantityKindTag DomainMaterial = QuantityKindTag.Get("Domain.Material");
    /// <summary>Transport phenomena related domain tag.</summary>
    public static readonly QuantityKindTag DomainTransport = QuantityKindTag.Get("Domain.Transport");
    /// <summary>Thermodynamics related domain tag.</summary>
    public static readonly QuantityKindTag DomainThermodynamic = QuantityKindTag.Get("Domain.Thermodynamic");
    /// <summary>Optics related domain tag.</summary>
    public static readonly QuantityKindTag DomainOptics = QuantityKindTag.Get("Domain.Optics");
    /// <summary>Radiation / radiometry related domain tag.</summary>
    public static readonly QuantityKindTag DomainRadiation = QuantityKindTag.Get("Domain.Radiation");
    /// <summary>Electromagnetics related domain tag.</summary>
    public static readonly QuantityKindTag DomainElectromagnetic = QuantityKindTag.Get("Domain.Electromagnetic");
    /// <summary>Chemistry related domain tag.</summary>
    public static readonly QuantityKindTag DomainChemistry = QuantityKindTag.Get("Domain.Chemistry");

    // Form / structural descriptors
    /// <summary>Dimensionless form tag (unitless scalar semantics).</summary>
    public static readonly QuantityKindTag FormDimensionless = QuantityKindTag.Get("Form.Dimensionless");

    // Energy semantic namespace
    /// <summary>Generic energy semantic namespace tag.</summary>
    public static readonly QuantityKindTag Energy = QuantityKindTag.Get("Energy");
    /// <summary>Energy state function tag (path independent).</summary>
    public static readonly QuantityKindTag EnergyStateFunction = QuantityKindTag.Get("Energy.StateFunction");
    /// <summary>Energy path function tag (path dependent / transfer).</summary>
    public static readonly QuantityKindTag EnergyPathFunction = QuantityKindTag.Get("Energy.PathFunction");
}