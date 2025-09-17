using Veggerby.Units.Calculations;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Factory helpers for additional quantity kinds (geometry, kinematics, flow, material, thermal, EM field, radiation, chemistry, ratios).
/// </summary>
public static partial class Quantity
{
    // Geometry & Kinematics
    /// <summary>Create solid angle quantity (sr).</summary>
    public static Quantity<double> SolidAngle(double steradians) => new(Create(steradians, QuantityKinds.SolidAngle.CanonicalUnit), QuantityKinds.SolidAngle);
    /// <summary>Create angular velocity quantity (rad/s).</summary>
    public static Quantity<double> AngularVelocity(double radiansPerSecond) => new(Create(radiansPerSecond, QuantityKinds.AngularVelocity.CanonicalUnit), QuantityKinds.AngularVelocity);
    /// <summary>Create angular acceleration quantity (rad/s^2).</summary>
    public static Quantity<double> AngularAcceleration(double radiansPerSecondSquared) => new(Create(radiansPerSecondSquared, QuantityKinds.AngularAcceleration.CanonicalUnit), QuantityKinds.AngularAcceleration);
    /// <summary>Create angular momentum quantity (kg·m^2/s).</summary>
    public static Quantity<double> AngularMomentum(double kilogramMetreSquaredPerSecond) => new(Create(kilogramMetreSquaredPerSecond, QuantityKinds.AngularMomentum.CanonicalUnit), QuantityKinds.AngularMomentum);
    /// <summary>Create moment of inertia quantity (kg·m^2).</summary>
    public static Quantity<double> MomentOfInertia(double kilogramMetreSquared) => new(Create(kilogramMetreSquared, QuantityKinds.MomentOfInertia.CanonicalUnit), QuantityKinds.MomentOfInertia);
    /// <summary>Create jerk quantity (m/s^3).</summary>
    public static Quantity<double> Jerk(double metrePerSecondCubed) => new(Create(metrePerSecondCubed, QuantityKinds.Jerk.CanonicalUnit), QuantityKinds.Jerk);
    /// <summary>Create wavenumber quantity (1/m).</summary>
    public static Quantity<double> Wavenumber(double reciprocalMetres) => new(Create(reciprocalMetres, QuantityKinds.Wavenumber.CanonicalUnit), QuantityKinds.Wavenumber);
    /// <summary>Create curvature quantity (1/m).</summary>
    public static Quantity<double> Curvature(double reciprocalMetres) => new(Create(reciprocalMetres, QuantityKinds.Curvature.CanonicalUnit), QuantityKinds.Curvature);
    /// <summary>Create strain quantity (dimensionless).</summary>
    public static Quantity<double> Strain(double strain) => new(Create(strain, QuantityKinds.Strain.CanonicalUnit), QuantityKinds.Strain);
    /// <summary>Create strain rate quantity (1/s).</summary>
    public static Quantity<double> StrainRate(double reciprocalSeconds) => new(Create(reciprocalSeconds, QuantityKinds.StrainRate.CanonicalUnit), QuantityKinds.StrainRate);

    // Flow & Rates
    /// <summary>Create volumetric flow rate quantity (m^3/s).</summary>
    public static Quantity<double> VolumetricFlowRate(double cubicMetresPerSecond) => new(Create(cubicMetresPerSecond, QuantityKinds.VolumetricFlowRate.CanonicalUnit), QuantityKinds.VolumetricFlowRate);
    /// <summary>Create mass flow rate quantity (kg/s).</summary>
    public static Quantity<double> MassFlowRate(double kilogramsPerSecond) => new(Create(kilogramsPerSecond, QuantityKinds.MassFlowRate.CanonicalUnit), QuantityKinds.MassFlowRate);
    /// <summary>Create molar flow rate quantity (mol/s).</summary>
    public static Quantity<double> MolarFlowRate(double molesPerSecond) => new(Create(molesPerSecond, QuantityKinds.MolarFlowRate.CanonicalUnit), QuantityKinds.MolarFlowRate);

    // Materials & Mechanics
    /// <summary>Create Young's modulus quantity (Pa).</summary>
    public static Quantity<double> YoungsModulus(double pascals) => new(Create(pascals, QuantityKinds.YoungsModulus.CanonicalUnit), QuantityKinds.YoungsModulus);
    /// <summary>Create shear modulus quantity (Pa).</summary>
    public static Quantity<double> ShearModulus(double pascals) => new(Create(pascals, QuantityKinds.ShearModulus.CanonicalUnit), QuantityKinds.ShearModulus);
    /// <summary>Create bulk modulus quantity (Pa).</summary>
    public static Quantity<double> BulkModulus(double pascals) => new(Create(pascals, QuantityKinds.BulkModulus.CanonicalUnit), QuantityKinds.BulkModulus);
    /// <summary>Create compressibility quantity (1/Pa).</summary>
    public static Quantity<double> Compressibility(double reciprocalPascals) => new(Create(reciprocalPascals, QuantityKinds.Compressibility.CanonicalUnit), QuantityKinds.Compressibility);
    /// <summary>Create linear mass density quantity (kg/m).</summary>
    public static Quantity<double> LinearMassDensity(double kilogramsPerMetre) => new(Create(kilogramsPerMetre, QuantityKinds.LinearMassDensity.CanonicalUnit), QuantityKinds.LinearMassDensity);
    /// <summary>Create areal mass density quantity (kg/m^2).</summary>
    public static Quantity<double> ArealMassDensity(double kilogramsPerSquareMetre) => new(Create(kilogramsPerSquareMetre, QuantityKinds.ArealMassDensity.CanonicalUnit), QuantityKinds.ArealMassDensity);
    /// <summary>Create number density quantity (1/m^3).</summary>
    public static Quantity<double> NumberDensity(double perCubicMetre) => new(Create(perCubicMetre, QuantityKinds.NumberDensity.CanonicalUnit), QuantityKinds.NumberDensity);
    /// <summary>Create areal number density quantity (1/m^2).</summary>
    public static Quantity<double> ArealNumberDensity(double perSquareMetre) => new(Create(perSquareMetre, QuantityKinds.ArealNumberDensity.CanonicalUnit), QuantityKinds.ArealNumberDensity);
    /// <summary>Create porosity quantity (dimensionless).</summary>
    public static Quantity<double> Porosity(double ratio) => new(Create(ratio, QuantityKinds.Porosity.CanonicalUnit), QuantityKinds.Porosity);

    // Thermal
    /// <summary>Create thermal resistance quantity (K/W).</summary>
    public static Quantity<double> ThermalResistance(double kelvinPerWatt) => new(Create(kelvinPerWatt, QuantityKinds.ThermalResistance.CanonicalUnit), QuantityKinds.ThermalResistance);
    /// <summary>Create thermal conductance quantity (W/K).</summary>
    public static Quantity<double> ThermalConductance(double wattPerKelvin) => new(Create(wattPerKelvin, QuantityKinds.ThermalConductance.CanonicalUnit), QuantityKinds.ThermalConductance);
    /// <summary>Create heat transfer coefficient quantity (W/(m^2·K)).</summary>
    public static Quantity<double> HeatTransferCoefficient(double wattPerSquareMetreKelvin) => new(Create(wattPerSquareMetreKelvin, QuantityKinds.HeatTransferCoefficient.CanonicalUnit), QuantityKinds.HeatTransferCoefficient);
    /// <summary>Create molar heat capacity quantity (J/(mol·K)).</summary>
    public static Quantity<double> MolarHeatCapacity(double joulePerMoleKelvin) => new(Create(joulePerMoleKelvin, QuantityKinds.MolarHeatCapacity.CanonicalUnit), QuantityKinds.MolarHeatCapacity);
    /// <summary>Create specific enthalpy quantity (J/kg).</summary>
    public static Quantity<double> SpecificEnthalpy(double joulePerKilogram) => new(Create(joulePerKilogram, QuantityKinds.SpecificEnthalpy.CanonicalUnit), QuantityKinds.SpecificEnthalpy);

    // Electromagnetic field additions
    /// <summary>Create magnetic field strength quantity (A/m).</summary>
    public static Quantity<double> MagneticFieldStrength(double amperePerMetre) => new(Create(amperePerMetre, QuantityKinds.MagneticFieldStrength.CanonicalUnit), QuantityKinds.MagneticFieldStrength);
    /// <summary>Create magnetization quantity (A/m).</summary>
    public static Quantity<double> Magnetization(double amperePerMetre) => new(Create(amperePerMetre, QuantityKinds.Magnetization.CanonicalUnit), QuantityKinds.Magnetization);
    /// <summary>Create electric displacement quantity (C/m^2).</summary>
    public static Quantity<double> ElectricDisplacement(double coulombPerSquareMetre) => new(Create(coulombPerSquareMetre, QuantityKinds.ElectricDisplacement.CanonicalUnit), QuantityKinds.ElectricDisplacement);
    /// <summary>Create magnetic vector potential quantity (Wb/m).</summary>
    public static Quantity<double> MagneticVectorPotential(double weberPerMetre) => new(Create(weberPerMetre, QuantityKinds.MagneticVectorPotential.CanonicalUnit), QuantityKinds.MagneticVectorPotential);
    /// <summary>Create permittivity quantity (F/m).</summary>
    public static Quantity<double> Permittivity(double faradPerMetre) => new(Create(faradPerMetre, QuantityKinds.Permittivity.CanonicalUnit), QuantityKinds.Permittivity);
    /// <summary>Create permeability quantity (H/m).</summary>
    public static Quantity<double> Permeability(double henryPerMetre) => new(Create(henryPerMetre, QuantityKinds.Permeability.CanonicalUnit), QuantityKinds.Permeability);
    /// <summary>Create impedance quantity (Ω).</summary>
    public static Quantity<double> Impedance(double ohms) => new(Create(ohms, QuantityKinds.Impedance.CanonicalUnit), QuantityKinds.Impedance);
    /// <summary>Create admittance quantity (S).</summary>
    public static Quantity<double> Admittance(double siemens) => new(Create(siemens, QuantityKinds.Admittance.CanonicalUnit), QuantityKinds.Admittance);
    /// <summary>Create charge mobility quantity (m^2/(V·s)).</summary>
    public static Quantity<double> ChargeMobility(double squareMetresPerVoltSecond) => new(Create(squareMetresPerVoltSecond, QuantityKinds.ChargeMobility.CanonicalUnit), QuantityKinds.ChargeMobility);

    // Radiation / Photometry
    /// <summary>Create radiant flux quantity (W).</summary>
    public static Quantity<double> RadiantFlux(double watts) => new(Create(watts, QuantityKinds.RadiantFlux.CanonicalUnit), QuantityKinds.RadiantFlux);
    /// <summary>Create radiant intensity quantity (W/sr).</summary>
    public static Quantity<double> RadiantIntensity(double wattPerSteradian) => new(Create(wattPerSteradian, QuantityKinds.RadiantIntensity.CanonicalUnit), QuantityKinds.RadiantIntensity);
    /// <summary>Create irradiance quantity (W/m^2).</summary>
    public static Quantity<double> Irradiance(double wattPerSquareMetre) => new(Create(wattPerSquareMetre, QuantityKinds.Irradiance.CanonicalUnit), QuantityKinds.Irradiance);
    /// <summary>Create radiance quantity (W/(m^2·sr)).</summary>
    public static Quantity<double> Radiance(double wattPerSquareMetreSteradian) => new(Create(wattPerSquareMetreSteradian, QuantityKinds.Radiance.CanonicalUnit), QuantityKinds.Radiance);
    /// <summary>Create radiation exposure quantity (C/kg).</summary>
    public static Quantity<double> RadiationExposure(double coulombPerKilogram) => new(Create(coulombPerKilogram, QuantityKinds.RadiationExposure.CanonicalUnit), QuantityKinds.RadiationExposure);
    /// <summary>Create luminance quantity (cd/m^2).</summary>
    public static Quantity<double> Luminance(double candelaPerSquareMetre) => new(Create(candelaPerSquareMetre, QuantityKinds.Luminance.CanonicalUnit), QuantityKinds.Luminance);
    /// <summary>Create luminous intensity quantity (cd).</summary>
    public static Quantity<double> LuminousIntensity(double candelas) => new(Create(candelas, QuantityKinds.LuminousIntensity.CanonicalUnit), QuantityKinds.LuminousIntensity);
    /// <summary>Create luminous efficacy quantity (lm/W).</summary>
    public static Quantity<double> LuminousEfficacy(double lumenPerWatt) => new(Create(lumenPerWatt, QuantityKinds.LuminousEfficacy.CanonicalUnit), QuantityKinds.LuminousEfficacy);

    // Chemistry
    /// <summary>Create diffusion coefficient quantity (m^2/s).</summary>
    public static Quantity<double> DiffusionCoefficient(double squareMetresPerSecond) => new(Create(squareMetresPerSecond, QuantityKinds.DiffusionCoefficient.CanonicalUnit), QuantityKinds.DiffusionCoefficient);
    /// <summary>Create reaction rate quantity (mol/(m^3·s)).</summary>
    public static Quantity<double> ReactionRate(double molePerCubicMetreSecond) => new(Create(molePerCubicMetreSecond, QuantityKinds.ReactionRate.CanonicalUnit), QuantityKinds.ReactionRate);
    /// <summary>Create mole fraction quantity (dimensionless).</summary>
    public static Quantity<double> MoleFraction(double fraction) => new(Create(fraction, QuantityKinds.MoleFraction.CanonicalUnit), QuantityKinds.MoleFraction);
    /// <summary>Create mass fraction quantity (dimensionless).</summary>
    public static Quantity<double> MassFraction(double fraction) => new(Create(fraction, QuantityKinds.MassFraction.CanonicalUnit), QuantityKinds.MassFraction);
    /// <summary>Create osmotic pressure quantity (Pa).</summary>
    public static Quantity<double> OsmoticPressure(double pascals) => new(Create(pascals, QuantityKinds.OsmoticPressure.CanonicalUnit), QuantityKinds.OsmoticPressure);

    // Odds & Ends
    /// <summary>Create generic dimensionless ratio quantity.</summary>
    public static Quantity<double> DimensionlessRatio(double ratio) => new(Create(ratio, QuantityKinds.DimensionlessRatio.CanonicalUnit), QuantityKinds.DimensionlessRatio);
}