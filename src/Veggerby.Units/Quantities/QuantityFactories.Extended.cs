using Veggerby.Units.Calculations;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Factory helpers for constructing quantities of extended semantic kinds using double precision.
/// Keeps creation concise while enforcing canonical unit usage.
/// </summary>
public static partial class Quantity
{
    private static DoubleMeasurement Create(double value, Unit unit) => new(value, unit);

    /// <summary>Create electric current quantity (A).</summary>
    public static Quantity<double> ElectricCurrent(double amperes) =>
        new(Create(amperes, Unit.SI.A), QuantityKinds.ElectricCurrent);

    /// <summary>Create frequency quantity (Hz).</summary>
    public static Quantity<double> Frequency(double hertz) =>
        new(Create(hertz, QuantityKinds.Frequency.CanonicalUnit), QuantityKinds.Frequency);

    /// <summary>Create electric charge quantity (C).</summary>
    public static Quantity<double> ElectricCharge(double coulombs) =>
        new(Create(coulombs, QuantityKinds.ElectricCharge.CanonicalUnit), QuantityKinds.ElectricCharge);

    /// <summary>Create voltage quantity (V).</summary>
    public static Quantity<double> Voltage(double volts) =>
        new(Create(volts, QuantityKinds.Voltage.CanonicalUnit), QuantityKinds.Voltage);

    /// <summary>Create electric resistance quantity (Ω).</summary>
    public static Quantity<double> ElectricResistance(double ohms) =>
        new(Create(ohms, QuantityKinds.ElectricResistance.CanonicalUnit), QuantityKinds.ElectricResistance);

    /// <summary>Create electric conductance quantity (S).</summary>
    public static Quantity<double> ElectricConductance(double siemens) =>
        new(Create(siemens, QuantityKinds.ElectricConductance.CanonicalUnit), QuantityKinds.ElectricConductance);

    /// <summary>Create capacitance quantity (F).</summary>
    public static Quantity<double> Capacitance(double farads) =>
        new(Create(farads, QuantityKinds.Capacitance.CanonicalUnit), QuantityKinds.Capacitance);

    /// <summary>Create inductance quantity (H).</summary>
    public static Quantity<double> Inductance(double henries) =>
        new(Create(henries, QuantityKinds.Inductance.CanonicalUnit), QuantityKinds.Inductance);

    /// <summary>Create magnetic flux quantity (Wb).</summary>
    public static Quantity<double> MagneticFlux(double webers) =>
        new(Create(webers, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);

    /// <summary>Create magnetic flux density quantity (T).</summary>
    public static Quantity<double> MagneticFluxDensity(double tesla) =>
        new(Create(tesla, QuantityKinds.MagneticFluxDensity.CanonicalUnit), QuantityKinds.MagneticFluxDensity);

    /// <summary>Create electric field strength quantity (V/m).</summary>
    public static Quantity<double> ElectricFieldStrength(double voltPerMetre) =>
        new(Create(voltPerMetre, QuantityKinds.ElectricFieldStrength.CanonicalUnit), QuantityKinds.ElectricFieldStrength);

    /// <summary>Create electric charge density quantity (C/m^3).</summary>
    public static Quantity<double> ElectricChargeDensity(double coulombPerCubicMetre) =>
        new(Create(coulombPerCubicMetre, QuantityKinds.ElectricChargeDensity.CanonicalUnit), QuantityKinds.ElectricChargeDensity);

    /// <summary>Create electric current density quantity (A/m^2).</summary>
    public static Quantity<double> ElectricCurrentDensity(double amperePerSquareMetre) =>
        new(Create(amperePerSquareMetre, QuantityKinds.ElectricCurrentDensity.CanonicalUnit), QuantityKinds.ElectricCurrentDensity);

    /// <summary>Create luminous flux quantity (lm).</summary>
    public static Quantity<double> LuminousFlux(double lumens) =>
        new(Create(lumens, QuantityKinds.LuminousFlux.CanonicalUnit), QuantityKinds.LuminousFlux);

    /// <summary>Create illuminance quantity (lx).</summary>
    public static Quantity<double> Illuminance(double lux) =>
        new(Create(lux, QuantityKinds.Illuminance.CanonicalUnit), QuantityKinds.Illuminance);

    /// <summary>Create radioactivity quantity (Bq).</summary>
    public static Quantity<double> Radioactivity(double becquerel) =>
        new(Create(becquerel, QuantityKinds.Radioactivity.CanonicalUnit), QuantityKinds.Radioactivity);

    /// <summary>Create absorbed dose quantity (Gy).</summary>
    public static Quantity<double> AbsorbedDose(double gray) =>
        new(Create(gray, QuantityKinds.AbsorbedDose.CanonicalUnit), QuantityKinds.AbsorbedDose);

    /// <summary>Create dose equivalent quantity (Sv).</summary>
    public static Quantity<double> DoseEquivalent(double sievert) =>
        new(Create(sievert, QuantityKinds.DoseEquivalent.CanonicalUnit), QuantityKinds.DoseEquivalent);

    /// <summary>Create catalytic activity quantity (kat).</summary>
    public static Quantity<double> CatalyticActivity(double katal) =>
        new(Create(katal, QuantityKinds.CatalyticActivity.CanonicalUnit), QuantityKinds.CatalyticActivity);

    /// <summary>Create mass density quantity (kg/m^3).</summary>
    public static Quantity<double> MassDensity(double kilogramsPerCubicMetre) =>
        new(Create(kilogramsPerCubicMetre, QuantityKinds.MassDensity.CanonicalUnit), QuantityKinds.MassDensity);

    /// <summary>Create mass concentration quantity (kg/m^3).</summary>
    public static Quantity<double> MassConcentration(double kilogramsPerCubicMetre) =>
        new(Create(kilogramsPerCubicMetre, QuantityKinds.MassConcentration.CanonicalUnit), QuantityKinds.MassConcentration);

    /// <summary>Create specific energy quantity (J/kg).</summary>
    public static Quantity<double> SpecificEnergy(double joulesPerKilogram) =>
        new(Create(joulesPerKilogram, QuantityKinds.SpecificEnergy.CanonicalUnit), QuantityKinds.SpecificEnergy);

    /// <summary>Create specific power quantity (W/kg).</summary>
    public static Quantity<double> SpecificPower(double wattsPerKilogram) =>
        new(Create(wattsPerKilogram, QuantityKinds.SpecificPower.CanonicalUnit), QuantityKinds.SpecificPower);

    /// <summary>Create specific volume quantity (m^3/kg).</summary>
    public static Quantity<double> SpecificVolume(double cubicMetresPerKilogram) =>
        new(Create(cubicMetresPerKilogram, QuantityKinds.SpecificVolume.CanonicalUnit), QuantityKinds.SpecificVolume);

    /// <summary>Create dynamic viscosity quantity (Pa·s).</summary>
    public static Quantity<double> DynamicViscosity(double pascalSeconds) =>
        new(Create(pascalSeconds, QuantityKinds.DynamicViscosity.CanonicalUnit), QuantityKinds.DynamicViscosity);

    /// <summary>Create kinematic viscosity quantity (m^2/s).</summary>
    public static Quantity<double> KinematicViscosity(double squareMetresPerSecond) =>
        new(Create(squareMetresPerSecond, QuantityKinds.KinematicViscosity.CanonicalUnit), QuantityKinds.KinematicViscosity);

    /// <summary>Create thermal conductivity quantity (W/(m·K)).</summary>
    public static Quantity<double> ThermalConductivity(double wattsPerMetreKelvin) =>
        new(Create(wattsPerMetreKelvin, QuantityKinds.ThermalConductivity.CanonicalUnit), QuantityKinds.ThermalConductivity);

    /// <summary>Create thermal diffusivity quantity (m^2/s).</summary>
    public static Quantity<double> ThermalDiffusivity(double squareMetresPerSecond) =>
        new(Create(squareMetresPerSecond, QuantityKinds.ThermalDiffusivity.CanonicalUnit), QuantityKinds.ThermalDiffusivity);

    /// <summary>Create heat flux quantity (W/m^2).</summary>
    public static Quantity<double> HeatFlux(double wattsPerSquareMetre) =>
        new(Create(wattsPerSquareMetre, QuantityKinds.HeatFlux.CanonicalUnit), QuantityKinds.HeatFlux);

    /// <summary>Create surface tension quantity (N/m).</summary>
    public static Quantity<double> SurfaceTension(double newtonsPerMetre) =>
        new(Create(newtonsPerMetre, QuantityKinds.SurfaceTension.CanonicalUnit), QuantityKinds.SurfaceTension);

    /// <summary>Create molar mass quantity (kg/mol).</summary>
    public static Quantity<double> MolarMass(double kilogramsPerMole) =>
        new(Create(kilogramsPerMole, QuantityKinds.MolarMass.CanonicalUnit), QuantityKinds.MolarMass);

    /// <summary>Create molar volume quantity (m^3/mol).</summary>
    public static Quantity<double> MolarVolume(double cubicMetresPerMole) =>
        new(Create(cubicMetresPerMole, QuantityKinds.MolarVolume.CanonicalUnit), QuantityKinds.MolarVolume);

    /// <summary>Create molar concentration quantity (mol/m^3).</summary>
    public static Quantity<double> MolarConcentration(double molePerCubicMetre) =>
        new(Create(molePerCubicMetre, QuantityKinds.MolarConcentration.CanonicalUnit), QuantityKinds.MolarConcentration);
}