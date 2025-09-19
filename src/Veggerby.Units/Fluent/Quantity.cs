using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent;

/// <summary>
/// Static facade providing per-kind factory helpers for ergonomic construction of measurements bound to a
/// specific <see cref="QuantityKind"/>. Factories do not infer or attach semantic kind metadata to the
/// returned measurement beyond unit selection; semantic rules remain unchanged.
/// </summary>
public static class Quantity
{
    // Pattern: public static DoubleMeasurement Energy(double value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    // Provide overload accepting a unit (must be dimensionally compatible; validation left to conversion paths).

    /// <summary>Create length measurement in metres.</summary>
    public static DoubleMeasurement Length(double value) => new(value, QuantityKinds.Length.CanonicalUnit);
    /// <summary>Create length measurement with explicit unit.</summary>
    public static DoubleMeasurement Length(double value, Unit unit) => new(value, unit);
    /// <summary>Create length measurement (decimal) in metres.</summary>
    public static DecimalMeasurement Length(decimal value) => new(value, QuantityKinds.Length.CanonicalUnit);
    /// <summary>Create length measurement (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Length(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create length measurement (int) in metres.</summary>
    public static Int32Measurement Length(int value) => new(value, QuantityKinds.Length.CanonicalUnit);
    /// <summary>Create length measurement (int) with explicit unit.</summary>
    public static Int32Measurement Length(int value, Unit unit) => new(value, unit);

    /// <summary>Create mass measurement in kilograms.</summary>
    public static DoubleMeasurement Mass(double value) => new(value, QuantityKinds.Mass.CanonicalUnit);
    /// <summary>Create mass measurement with explicit unit.</summary>
    public static DoubleMeasurement Mass(double value, Unit unit) => new(value, unit);
    /// <summary>Create mass (decimal) in kilograms.</summary>
    public static DecimalMeasurement Mass(decimal value) => new(value, QuantityKinds.Mass.CanonicalUnit);
    /// <summary>Create mass (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Mass(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create mass (int) in kilograms.</summary>
    public static Int32Measurement Mass(int value) => new(value, QuantityKinds.Mass.CanonicalUnit);
    /// <summary>Create mass (int) with explicit unit.</summary>
    public static Int32Measurement Mass(int value, Unit unit) => new(value, unit);

    /// <summary>Create time measurement in seconds.</summary>
    public static DoubleMeasurement Time(double value) => new(value, QuantityKinds.Time.CanonicalUnit);
    /// <summary>Create time measurement with explicit unit.</summary>
    public static DoubleMeasurement Time(double value, Unit unit) => new(value, unit);
    /// <summary>Create time (decimal) in seconds.</summary>
    public static DecimalMeasurement Time(decimal value) => new(value, QuantityKinds.Time.CanonicalUnit);
    /// <summary>Create time (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Time(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create time (int) in seconds.</summary>
    public static Int32Measurement Time(int value) => new(value, QuantityKinds.Time.CanonicalUnit);
    /// <summary>Create time (int) with explicit unit.</summary>
    public static Int32Measurement Time(int value, Unit unit) => new(value, unit);

    /// <summary>Create energy measurement in joules.</summary>
    public static DoubleMeasurement Energy(double value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    /// <summary>Create energy measurement with explicit unit.</summary>
    public static DoubleMeasurement Energy(double value, Unit unit) => new(value, unit);
    /// <summary>Create energy (decimal) in joules.</summary>
    public static DecimalMeasurement Energy(decimal value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    /// <summary>Create energy (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Energy(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create energy (int) in joules.</summary>
    public static Int32Measurement Energy(int value) => new(value, QuantityKinds.Energy.CanonicalUnit);
    /// <summary>Create energy (int) with explicit unit.</summary>
    public static Int32Measurement Energy(int value, Unit unit) => new(value, unit);

    /// <summary>Create force measurement in newtons.</summary>
    public static DoubleMeasurement Force(double value) => new(value, QuantityKinds.Force.CanonicalUnit);
    /// <summary>Create force measurement with explicit unit.</summary>
    public static DoubleMeasurement Force(double value, Unit unit) => new(value, unit);
    /// <summary>Create force (decimal) in newtons.</summary>
    public static DecimalMeasurement Force(decimal value) => new(value, QuantityKinds.Force.CanonicalUnit);
    /// <summary>Create force (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Force(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create force (int) in newtons.</summary>
    public static Int32Measurement Force(int value) => new(value, QuantityKinds.Force.CanonicalUnit);
    /// <summary>Create force (int) with explicit unit.</summary>
    public static Int32Measurement Force(int value, Unit unit) => new(value, unit);

    /// <summary>Create velocity measurement in metres per second.</summary>
    public static DoubleMeasurement Velocity(double value) => new(value, QuantityKinds.Velocity.CanonicalUnit);
    /// <summary>Create velocity measurement with explicit unit.</summary>
    public static DoubleMeasurement Velocity(double value, Unit unit) => new(value, unit);
    /// <summary>Create velocity (decimal) in metres per second.</summary>
    public static DecimalMeasurement Velocity(decimal value) => new(value, QuantityKinds.Velocity.CanonicalUnit);
    /// <summary>Create velocity (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Velocity(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create velocity (int) in metres per second.</summary>
    public static Int32Measurement Velocity(int value) => new(value, QuantityKinds.Velocity.CanonicalUnit);
    /// <summary>Create velocity (int) with explicit unit.</summary>
    public static Int32Measurement Velocity(int value, Unit unit) => new(value, unit);

    /// <summary>Create acceleration measurement in metres per second squared.</summary>
    public static DoubleMeasurement Acceleration(double value) => new(value, QuantityKinds.Acceleration.CanonicalUnit);
    /// <summary>Create acceleration measurement with explicit unit.</summary>
    public static DoubleMeasurement Acceleration(double value, Unit unit) => new(value, unit);
    /// <summary>Create acceleration (decimal) in metres per second squared.</summary>
    public static DecimalMeasurement Acceleration(decimal value) => new(value, QuantityKinds.Acceleration.CanonicalUnit);
    /// <summary>Create acceleration (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Acceleration(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create acceleration (int) in metres per second squared.</summary>
    public static Int32Measurement Acceleration(int value) => new(value, QuantityKinds.Acceleration.CanonicalUnit);
    /// <summary>Create acceleration (int) with explicit unit.</summary>
    public static Int32Measurement Acceleration(int value, Unit unit) => new(value, unit);

    /// <summary>Create power measurement in watts.</summary>
    public static DoubleMeasurement Power(double value) => new(value, QuantityKinds.Power.CanonicalUnit);
    /// <summary>Create power measurement with explicit unit.</summary>
    public static DoubleMeasurement Power(double value, Unit unit) => new(value, unit);
    /// <summary>Create power (decimal) in watts.</summary>
    public static DecimalMeasurement Power(decimal value) => new(value, QuantityKinds.Power.CanonicalUnit);
    /// <summary>Create power (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Power(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create power (int) in watts.</summary>
    public static Int32Measurement Power(int value) => new(value, QuantityKinds.Power.CanonicalUnit);
    /// <summary>Create power (int) with explicit unit.</summary>
    public static Int32Measurement Power(int value, Unit unit) => new(value, unit);

    /// <summary>Create pressure measurement in pascals.</summary>
    public static DoubleMeasurement Pressure(double value) => new(value, QuantityKinds.Pressure.CanonicalUnit);
    /// <summary>Create pressure measurement with explicit unit.</summary>
    public static DoubleMeasurement Pressure(double value, Unit unit) => new(value, unit);
    /// <summary>Create pressure (decimal) in pascals.</summary>
    public static DecimalMeasurement Pressure(decimal value) => new(value, QuantityKinds.Pressure.CanonicalUnit);
    /// <summary>Create pressure (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Pressure(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create pressure (int) in pascals.</summary>
    public static Int32Measurement Pressure(int value) => new(value, QuantityKinds.Pressure.CanonicalUnit);
    /// <summary>Create pressure (int) with explicit unit.</summary>
    public static Int32Measurement Pressure(int value, Unit unit) => new(value, unit);

    /// <summary>Create voltage measurement in volts.</summary>
    public static DoubleMeasurement Voltage(double value) => new(value, QuantityKinds.Voltage.CanonicalUnit);
    /// <summary>Create voltage measurement with explicit unit.</summary>
    public static DoubleMeasurement Voltage(double value, Unit unit) => new(value, unit);
    /// <summary>Create voltage (decimal) in volts.</summary>
    public static DecimalMeasurement Voltage(decimal value) => new(value, QuantityKinds.Voltage.CanonicalUnit);
    /// <summary>Create voltage (decimal) with explicit unit.</summary>
    public static DecimalMeasurement Voltage(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create voltage (int) in volts.</summary>
    public static Int32Measurement Voltage(int value) => new(value, QuantityKinds.Voltage.CanonicalUnit);
    /// <summary>Create voltage (int) with explicit unit.</summary>
    public static Int32Measurement Voltage(int value, Unit unit) => new(value, unit);

    /// <summary>Create electric current measurement in amperes.</summary>
    public static DoubleMeasurement ElectricCurrent(double value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Create electric current measurement with explicit unit.</summary>
    public static DoubleMeasurement ElectricCurrent(double value, Unit unit) => new(value, unit);
    /// <summary>Create electric current (decimal) in amperes.</summary>
    public static DecimalMeasurement ElectricCurrent(decimal value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Create electric current (decimal) with explicit unit.</summary>
    public static DecimalMeasurement ElectricCurrent(decimal value, Unit unit) => new(value, unit);
    /// <summary>Create electric current (int) in amperes.</summary>
    public static Int32Measurement ElectricCurrent(int value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Create electric current (int) with explicit unit.</summary>
    public static Int32Measurement ElectricCurrent(int value, Unit unit) => new(value, unit);

    // Add additional commonly used kinds as needed (kept intentionally focused for initial Tier 1 surface).
}