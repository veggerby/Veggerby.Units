using System;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Convenience creation helpers prioritising semantic intent over raw unit composition. All factories use double
/// precision. For other numeric types construct the underlying measurement and wrap manually.
/// </summary>
public static class Quantity
{
    /// <summary>Generic semantic quantity factory.</summary>
    public static Quantity<double> Of(double value, Unit unit, QuantityKind kind, bool strict = true)
        => new(new DoubleMeasurement(value, unit), kind, strict);

    /// <summary>Energy (Joules) quantity.</summary>
    public static Quantity<double> Energy(double joules)
        => Of(joules, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Energy);

    /// <summary>Gibbs free energy (Joules) quantity.</summary>
    public static Quantity<double> Gibbs(double joules)
        => Of(joules, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.GibbsFreeEnergy);

    /// <summary>Enthalpy (Joules) quantity.</summary>
    public static Quantity<double> Enthalpy(double joules)
        => Of(joules, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Enthalpy);

    /// <summary>Helmholtz free energy (Joules) quantity.</summary>
    public static Quantity<double> Helmholtz(double joules)
        => Of(joules, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.HelmholtzFreeEnergy);

    /// <summary>Internal energy (Joules) quantity.</summary>
    public static Quantity<double> InternalEnergy(double joules)
        => Of(joules, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.InternalEnergy);

    /// <summary>Entropy (J/K) quantity.</summary>
    public static Quantity<double> Entropy(double value, Unit unit = null)
    {
        var resolved = unit ?? (QuantityKinds.Energy.CanonicalUnit / Unit.SI.K);
        return Of(value, resolved, QuantityKinds.Entropy);
    }

    /// <summary>Heat capacity (J/K) quantity.</summary>
    public static Quantity<double> HeatCapacity(double value, Unit unit = null)
    {
        var resolved = unit ?? (QuantityKinds.Energy.CanonicalUnit / Unit.SI.K);
        return Of(value, resolved, QuantityKinds.HeatCapacity);
    }

    /// <summary>Chemical potential (J/mol) quantity.</summary>
    public static Quantity<double> ChemicalPotential(double value, Unit unit = null)
    {
        var resolved = unit ?? (QuantityKinds.Energy.CanonicalUnit / Unit.SI.n);
        return Of(value, resolved, QuantityKinds.ChemicalPotential);
    }

    /// <summary>Torque (N·m) quantity.</summary>
    public static Quantity<double> Torque(double value)
        => Of(value, QuantityKinds.Energy.CanonicalUnit, QuantityKinds.Torque);

    /// <summary>Angle (dimensionless semantic) quantity.</summary>
    public static Quantity<double> Angle(double value)
        => Of(value, Unit.None, QuantityKinds.Angle);

    /// <summary>Power (W) quantity.</summary>
    public static Quantity<double> Power(double value)
        => Of(value, QuantityKinds.Power.CanonicalUnit, QuantityKinds.Power);

    /// <summary>Force (N) quantity.</summary>
    public static Quantity<double> Force(double value)
        => Of(value, QuantityKinds.Force.CanonicalUnit, QuantityKinds.Force);

    /// <summary>Pressure (Pa) quantity.</summary>
    public static Quantity<double> Pressure(double value)
        => Of(value, QuantityKinds.Pressure.CanonicalUnit, QuantityKinds.Pressure);

    /// <summary>Volume (m^3) quantity.</summary>
    public static Quantity<double> Volume(double value)
        => Of(value, QuantityKinds.Volume.CanonicalUnit, QuantityKinds.Volume);

    /// <summary>Area (m^2) quantity.</summary>
    public static Quantity<double> Area(double value)
        => Of(value, QuantityKinds.Area.CanonicalUnit, QuantityKinds.Area);

    /// <summary>Velocity (m/s) quantity.</summary>
    public static Quantity<double> Velocity(double value)
        => Of(value, QuantityKinds.Velocity.CanonicalUnit, QuantityKinds.Velocity);

    /// <summary>Acceleration (m/s^2) quantity.</summary>
    public static Quantity<double> Acceleration(double value)
        => Of(value, QuantityKinds.Acceleration.CanonicalUnit, QuantityKinds.Acceleration);

    /// <summary>Momentum (kg·m/s) quantity.</summary>
    public static Quantity<double> Momentum(double value)
        => Of(value, QuantityKinds.Momentum.CanonicalUnit, QuantityKinds.Momentum);

    /// <summary>Energy density (J/m^3) quantity.</summary>
    public static Quantity<double> EnergyDensity(double value)
        => Of(value, QuantityKinds.EnergyDensity.CanonicalUnit, QuantityKinds.EnergyDensity);

    /// <summary>Specific heat capacity (J/(kg·K)) quantity.</summary>
    public static Quantity<double> SpecificHeatCapacity(double value)
        => Of(value, QuantityKinds.SpecificHeatCapacity.CanonicalUnit, QuantityKinds.SpecificHeatCapacity);

    /// <summary>Specific entropy (J/(kg·K)) quantity.</summary>
    public static Quantity<double> SpecificEntropy(double value)
        => Of(value, QuantityKinds.SpecificEntropy.CanonicalUnit, QuantityKinds.SpecificEntropy);
}