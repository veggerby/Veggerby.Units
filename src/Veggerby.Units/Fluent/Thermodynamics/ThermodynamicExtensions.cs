using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Thermodynamics;

/// <summary>Thermodynamic quantity numeric extensions (entropy, heat capacity, thermal conductivity, etc.).</summary>
public static class ThermodynamicExtensions
{
    /// <summary>Creates a measurement in joules per kelvin (J/K) for entropy.</summary>
    public static DoubleMeasurement JoulesPerKelvin(this double value) => new(value, QuantityKinds.Entropy.CanonicalUnit);
    /// <summary>Creates a decimal measurement in joules per kelvin (J/K) for entropy.</summary>
    public static DecimalMeasurement JoulesPerKelvin(this decimal value) => new(value, QuantityKinds.Entropy.CanonicalUnit);

    /// <summary>Creates a measurement representing entropy (J/K).</summary>
    public static DoubleMeasurement Entropy(this double value) => new(value, QuantityKinds.Entropy.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing entropy (J/K).</summary>
    public static DecimalMeasurement Entropy(this decimal value) => new(value, QuantityKinds.Entropy.CanonicalUnit);

    /// <summary>Creates a measurement representing heat capacity (J/K).</summary>
    public static DoubleMeasurement HeatCapacity(this double value) => new(value, QuantityKinds.HeatCapacity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing heat capacity (J/K).</summary>
    public static DecimalMeasurement HeatCapacity(this decimal value) => new(value, QuantityKinds.HeatCapacity.CanonicalUnit);

    /// <summary>Creates a measurement representing volumetric heat capacity (J/(m³·K)).</summary>
    public static DoubleMeasurement VolumetricHeatCapacity(this double value) => new(value, QuantityKinds.VolumetricHeatCapacity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing volumetric heat capacity (J/(m³·K)).</summary>
    public static DecimalMeasurement VolumetricHeatCapacity(this decimal value) => new(value, QuantityKinds.VolumetricHeatCapacity.CanonicalUnit);

    /// <summary>Creates a measurement representing molar heat capacity (J/(mol·K)).</summary>
    public static DoubleMeasurement MolarHeatCapacity(this double value) => new(value, QuantityKinds.MolarHeatCapacity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing molar heat capacity (J/(mol·K)).</summary>
    public static DecimalMeasurement MolarHeatCapacity(this decimal value) => new(value, QuantityKinds.MolarHeatCapacity.CanonicalUnit);

    /// <summary>Creates a measurement representing specific enthalpy (J/kg).</summary>
    public static DoubleMeasurement SpecificEnthalpy(this double value) => new(value, QuantityKinds.SpecificEnthalpy.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing specific enthalpy (J/kg).</summary>
    public static DecimalMeasurement SpecificEnthalpy(this decimal value) => new(value, QuantityKinds.SpecificEnthalpy.CanonicalUnit);

    /// <summary>Creates a measurement representing specific latent heat (J/kg).</summary>
    public static DoubleMeasurement SpecificLatentHeat(this double value) => new(value, QuantityKinds.SpecificLatentHeat.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing specific latent heat (J/kg).</summary>
    public static DecimalMeasurement SpecificLatentHeat(this decimal value) => new(value, QuantityKinds.SpecificLatentHeat.CanonicalUnit);

    /// <summary>Creates a measurement representing molar latent heat (J/mol).</summary>
    public static DoubleMeasurement MolarLatentHeat(this double value) => new(value, QuantityKinds.MolarLatentHeat.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing molar latent heat (J/mol).</summary>
    public static DecimalMeasurement MolarLatentHeat(this decimal value) => new(value, QuantityKinds.MolarLatentHeat.CanonicalUnit);

    /// <summary>Creates a measurement representing heat transfer coefficient (W/(m²·K)).</summary>
    public static DoubleMeasurement HeatTransferCoefficient(this double value) => new(value, QuantityKinds.HeatTransferCoefficient.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing heat transfer coefficient (W/(m²·K)).</summary>
    public static DecimalMeasurement HeatTransferCoefficient(this decimal value) => new(value, QuantityKinds.HeatTransferCoefficient.CanonicalUnit);

    /// <summary>Creates a measurement representing thermal conductivity (W/(m·K)).</summary>
    public static DoubleMeasurement ThermalConductivity(this double value) => new(value, QuantityKinds.ThermalConductivity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing thermal conductivity (W/(m·K)).</summary>
    public static DecimalMeasurement ThermalConductivity(this decimal value) => new(value, QuantityKinds.ThermalConductivity.CanonicalUnit);

    /// <summary>Creates a measurement representing thermal diffusivity (m²/s).</summary>
    public static DoubleMeasurement ThermalDiffusivity(this double value) => new(value, QuantityKinds.ThermalDiffusivity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing thermal diffusivity (m²/s).</summary>
    public static DecimalMeasurement ThermalDiffusivity(this decimal value) => new(value, QuantityKinds.ThermalDiffusivity.CanonicalUnit);

    /// <summary>Creates a measurement representing heat flux (W/m²).</summary>
    public static DoubleMeasurement HeatFlux(this double value) => new(value, QuantityKinds.HeatFlux.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing heat flux (W/m²).</summary>
    public static DecimalMeasurement HeatFlux(this decimal value) => new(value, QuantityKinds.HeatFlux.CanonicalUnit);

    /// <summary>Creates a measurement representing thermal resistance (K/W).</summary>
    public static DoubleMeasurement ThermalResistance(this double value) => new(value, QuantityKinds.ThermalResistance.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing thermal resistance (K/W).</summary>
    public static DecimalMeasurement ThermalResistance(this decimal value) => new(value, QuantityKinds.ThermalResistance.CanonicalUnit);

    /// <summary>Creates a measurement representing thermal conductance (W/K).</summary>
    public static DoubleMeasurement ThermalConductance(this double value) => new(value, QuantityKinds.ThermalConductance.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing thermal conductance (W/K).</summary>
    public static DecimalMeasurement ThermalConductance(this decimal value) => new(value, QuantityKinds.ThermalConductance.CanonicalUnit);

    /// <summary>Creates a measurement representing coefficient of thermal expansion (1/K).</summary>
    public static DoubleMeasurement CoefficientOfThermalExpansion(this double value) => new(value, QuantityKinds.CoefficientOfThermalExpansion.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing coefficient of thermal expansion (1/K).</summary>
    public static DecimalMeasurement CoefficientOfThermalExpansion(this decimal value) => new(value, QuantityKinds.CoefficientOfThermalExpansion.CanonicalUnit);
}
