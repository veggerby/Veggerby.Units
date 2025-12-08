namespace Veggerby.Units.Quantities;

/// <summary>
/// Radiometry, photometry, radiation dosimetry, optical attenuation and exposure quantities.
/// </summary>
/// <remarks>
/// Quantity kind definitions align with QUDT ontology specifications for radiation and optics.
/// See <c>docs/qudt-mapping-table.md</c> for detailed QUDT URI mappings.
/// <para>
/// QUDT Reference: http://qudt.org/doc/DOC_VOCAB-QUANTITY-KINDS.html
/// </para>
/// </remarks>
public static partial class QuantityKinds
{
    /// <summary>Radiant flux (W).</summary>
    public static readonly QuantityKind RadiantFlux = new("RadiantFlux", Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3), "Φ_e", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Radiant intensity (W/sr).</summary>
    public static readonly QuantityKind RadiantIntensity = new("RadiantIntensity", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / Unit.SI.sr, "I_e", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Irradiance (W/m^2).</summary>
    public static readonly QuantityKind Irradiance = new("Irradiance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 2), "E_e", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Radiance (W/(m^2·sr)).</summary>
    public static readonly QuantityKind Radiance = new("Radiance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / ((Unit.SI.m ^ 2) * Unit.SI.sr), "L_e", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Radiant exposure (J/m^2).</summary>
    public static readonly QuantityKind RadiantExposure = new("RadiantExposure", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)) / (Unit.SI.m ^ 2), "H_e", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Radiation exposure (C/kg).</summary>
    public static readonly QuantityKind RadiationExposure = new("RadiationExposure", (Unit.SI.A * Unit.SI.s) / Unit.SI.kg, "X", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Radioactivity (1/s).</summary>
    public static readonly QuantityKind Radioactivity = new("Radioactivity", 1 / Unit.SI.s, "Bq", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Absorbed dose (Gy).</summary>
    public static readonly QuantityKind AbsorbedDose = new("AbsorbedDose", (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), "Gy", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Dose equivalent (Sv).</summary>
    public static readonly QuantityKind DoseEquivalent = new("DoseEquivalent", (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), "Sv", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Absorbed dose rate (Gy/s).</summary>
    public static readonly QuantityKind AbsorbedDoseRate = new("AbsorbedDoseRate", (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3), "Ḋ_abs", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Equivalent dose rate (Sv/s).</summary>
    public static readonly QuantityKind EquivalentDoseRate = new("EquivalentDoseRate", (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3), "Ḋ_eq", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Fluence (1/m^2).</summary>
    public static readonly QuantityKind Fluence = new("Fluence", 1 / (Unit.SI.m ^ 2), "Φ_flu", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Fluence rate (1/(m^2·s)).</summary>
    public static readonly QuantityKind FluenceRate = new("FluenceRate", (1 / (Unit.SI.m ^ 2)) / Unit.SI.s, "Φ̇_flu", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Luminous flux (lm).</summary>
    public static readonly QuantityKind LuminousFlux = new("LuminousFlux", Unit.SI.cd, "lm", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Illuminance (lx).</summary>
    public static readonly QuantityKind Illuminance = new("Illuminance", Unit.SI.cd / (Unit.SI.m ^ 2), "lx", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Luminance (cd/m^2).</summary>
    public static readonly QuantityKind Luminance = new("Luminance", Unit.SI.cd / (Unit.SI.m ^ 2), "L_v", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Luminous intensity (cd).</summary>
    public static readonly QuantityKind LuminousIntensity = new("LuminousIntensity", Unit.SI.cd, "I_v", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Luminous efficacy (lm/W).</summary>
    public static readonly QuantityKind LuminousEfficacy = new("LuminousEfficacy", Unit.SI.cd / (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)), "K_cd", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Luminous exposure (lx·s).</summary>
    public static readonly QuantityKind LuminousExposure = new("LuminousExposure", (Unit.SI.cd / (Unit.SI.m ^ 2)) * Unit.SI.s, "H_v", tags: [QuantityKindTags.DomainOptics]);
    /// <summary>Linear attenuation coefficient (1/m).</summary>
    public static readonly QuantityKind LinearAttenuationCoefficient = new("LinearAttenuationCoefficient", 1 / Unit.SI.m, "μ_lin", tags: [QuantityKindTags.DomainRadiation, QuantityKindTags.DomainMaterial]);
    /// <summary>Optical depth (dimensionless).</summary>
    public static readonly QuantityKind OpticalDepth = new("OpticalDepth", Unit.None, "τ_opt", tags: [QuantityKindTags.FormDimensionless, QuantityKindTags.DomainOptics]);
    /// <summary>Spectral radiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralRadiance = new("SpectralRadiance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3), "L_eλ", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Spectral irradiance (W/(m^3)).</summary>
    public static readonly QuantityKind SpectralIrradiance = new("SpectralIrradiance", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / (Unit.SI.m ^ 3), "E_eλ", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Spectral radiant flux (W/m).</summary>
    public static readonly QuantityKind SpectralFlux = new("SpectralFlux", (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) / Unit.SI.m, "Φ_eλ", tags: [QuantityKindTags.DomainRadiation]);
    /// <summary>Refractive index (dimensionless).</summary>
    public static readonly QuantityKind RefractiveIndex = new("RefractiveIndex", Unit.None, "n_refr", tags: [QuantityKindTags.DomainOptics, QuantityKindTags.FormDimensionless]);
    /// <summary>Optical path length (m).</summary>
    public static readonly QuantityKind OpticalPathLength = new("OpticalPathLength", Unit.SI.m, "L_opt", tags: [QuantityKindTags.DomainOptics]);
}