using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Central registry for quantity kind inference rules. Keeps mappings immutable after static initialization.
/// </summary>
public static class QuantityKindInferenceRegistry
{
    private static readonly Dictionary<(QuantityKind, QuantityKindBinaryOperator, QuantityKind), QuantityKind> _map = new();
    private static readonly List<QuantityKindInference> _rules = new();
    private static bool _sealed;

    /// <summary>When true, registering a rule that overwrites an existing mapping throws instead of replacing it.</summary>
    public static bool StrictConflictDetection { get; set; } = true;

    /// <summary>Returns true when further registrations are disallowed.</summary>
    public static bool IsSealed => _sealed;

    static QuantityKindInferenceRegistry()
    {
        Seed();
    }

    private static void Seed()
    {
        _map.Clear();
        _rules.Clear();
        _sealed = false;

        // Seed rules (thermodynamics): Entropy (J/K) * Absolute Temperature (K) => Energy (J)
        Register(new QuantityKindInference(QuantityKinds.Entropy, QuantityKindBinaryOperator.Multiply, QuantityKinds.TemperatureAbsolute, QuantityKinds.Energy, Commutative: true));
        // Derived inverse rules for division
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.TemperatureAbsolute, QuantityKinds.Entropy));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Entropy, QuantityKinds.TemperatureAbsolute));

        // Rotational work: Torque * Angle => Energy (dimensionally N·m * 1 = N·m)
        Register(new QuantityKindInference(QuantityKinds.Torque, QuantityKindBinaryOperator.Multiply, QuantityKinds.Angle, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Angle, QuantityKinds.Torque));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Torque, QuantityKinds.Angle));

        // --- Step 2 seeded mechanical & thermodynamic single-step rules ---
        // Power × Time = Energy (J/s * s)
        Register(new QuantityKindInference(QuantityKinds.Power, QuantityKindBinaryOperator.Multiply, QuantityKinds.Time, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Time, QuantityKinds.Power));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Power, QuantityKinds.Time));

        // Force × Length = Energy (N * m)
        Register(new QuantityKindInference(QuantityKinds.Force, QuantityKindBinaryOperator.Multiply, QuantityKinds.Length, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Length, QuantityKinds.Force));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Force, QuantityKinds.Length));

        // Pressure × Volume = Energy (Pa * m^3)
        Register(new QuantityKindInference(QuantityKinds.Pressure, QuantityKindBinaryOperator.Multiply, QuantityKinds.Volume, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Volume, QuantityKinds.Pressure));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Pressure, QuantityKinds.Volume));

        // Pressure × Area = Force (Pa * m^2)
        Register(new QuantityKindInference(QuantityKinds.Pressure, QuantityKindBinaryOperator.Multiply, QuantityKinds.Area, QuantityKinds.Force, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Force, QuantityKindBinaryOperator.Divide, QuantityKinds.Area, QuantityKinds.Pressure));
        Register(new QuantityKindInference(QuantityKinds.Force, QuantityKindBinaryOperator.Divide, QuantityKinds.Pressure, QuantityKinds.Area));

        // --- Electromagnetic canonical single-step rules ---
        // Current (A) × Time (s) = Charge (C)
        Register(new QuantityKindInference(QuantityKinds.ElectricCurrent, QuantityKindBinaryOperator.Multiply, QuantityKinds.Time, QuantityKinds.ElectricCharge, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.ElectricCharge, QuantityKindBinaryOperator.Divide, QuantityKinds.Time, QuantityKinds.ElectricCurrent));
        Register(new QuantityKindInference(QuantityKinds.ElectricCharge, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricCurrent, QuantityKinds.Time));

        // Voltage (V) × Charge (C) = Energy (J)
        Register(new QuantityKindInference(QuantityKinds.Voltage, QuantityKindBinaryOperator.Multiply, QuantityKinds.ElectricCharge, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricCharge, QuantityKinds.Voltage));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Voltage, QuantityKinds.ElectricCharge));

        // Current (A) × Voltage (V) = Power (W)
        Register(new QuantityKindInference(QuantityKinds.ElectricCurrent, QuantityKindBinaryOperator.Multiply, QuantityKinds.Voltage, QuantityKinds.Power, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Power, QuantityKindBinaryOperator.Divide, QuantityKinds.Voltage, QuantityKinds.ElectricCurrent));
        Register(new QuantityKindInference(QuantityKinds.Power, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricCurrent, QuantityKinds.Voltage));

        // Current^2 (A^2) × Resistance (Ω) = Power (W)  (single-step only recognizes multiplicative grouping; no exponent semantics here)
        // We model Current * Resistance = Voltage so prefer explicit: Current × Resistance = Voltage; Voltage × Current = Power already covers but we add direct mapping for clarity.
        Register(new QuantityKindInference(QuantityKinds.ElectricCurrent, QuantityKindBinaryOperator.Multiply, QuantityKinds.ElectricResistance, QuantityKinds.Voltage, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Voltage, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricResistance, QuantityKinds.ElectricCurrent));
        Register(new QuantityKindInference(QuantityKinds.Voltage, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricCurrent, QuantityKinds.ElectricResistance));

        // Voltage^2 / Resistance = Power (Ohm's law variant) not added; would require power exponent pattern – omitted by design (single-step, no chaining expansion).

        // Flux (Wb) / Time (s) = Voltage (V)
        Register(new QuantityKindInference(QuantityKinds.MagneticFlux, QuantityKindBinaryOperator.Divide, QuantityKinds.Time, QuantityKinds.Voltage));
        // Voltage × Time = Flux
        Register(new QuantityKindInference(QuantityKinds.Voltage, QuantityKindBinaryOperator.Multiply, QuantityKinds.Time, QuantityKinds.MagneticFlux, Commutative: true));

        // Flux (Wb) / Area (m^2) = Flux Density (T)
        Register(new QuantityKindInference(QuantityKinds.MagneticFlux, QuantityKindBinaryOperator.Divide, QuantityKinds.Area, QuantityKinds.MagneticFluxDensity));
        Register(new QuantityKindInference(QuantityKinds.MagneticFluxDensity, QuantityKindBinaryOperator.Multiply, QuantityKinds.Area, QuantityKinds.MagneticFlux, Commutative: true));

        // Charge (C) / Time (s) = Current already implied by first rule symmetrical division but explicitly added for clarity (non-commutative mapping already present) – skipped to avoid duplication.

        // Capacitance (F) × Voltage (V) = Charge (C)
        Register(new QuantityKindInference(QuantityKinds.Capacitance, QuantityKindBinaryOperator.Multiply, QuantityKinds.Voltage, QuantityKinds.ElectricCharge, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.ElectricCharge, QuantityKindBinaryOperator.Divide, QuantityKinds.Voltage, QuantityKinds.Capacitance));
        Register(new QuantityKindInference(QuantityKinds.ElectricCharge, QuantityKindBinaryOperator.Divide, QuantityKinds.Capacitance, QuantityKinds.Voltage));

        // Inductance (H) × Current (A) = Magnetic Flux (Wb)
        Register(new QuantityKindInference(QuantityKinds.Inductance, QuantityKindBinaryOperator.Multiply, QuantityKinds.ElectricCurrent, QuantityKinds.MagneticFlux, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.MagneticFlux, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricCurrent, QuantityKinds.Inductance));
        Register(new QuantityKindInference(QuantityKinds.MagneticFlux, QuantityKindBinaryOperator.Divide, QuantityKinds.Inductance, QuantityKinds.ElectricCurrent));

        // Conductance (S) × Voltage (V) = Current (A)
        Register(new QuantityKindInference(QuantityKinds.ElectricConductance, QuantityKindBinaryOperator.Multiply, QuantityKinds.Voltage, QuantityKinds.ElectricCurrent, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.ElectricCurrent, QuantityKindBinaryOperator.Divide, QuantityKinds.Voltage, QuantityKinds.ElectricConductance));
        Register(new QuantityKindInference(QuantityKinds.ElectricCurrent, QuantityKindBinaryOperator.Divide, QuantityKinds.ElectricConductance, QuantityKinds.Voltage));
    }

    /// <summary>Registers an inference rule. For commutative multiplication the symmetric rule is generated. Throws on conflict when <see cref="StrictConflictDetection"/> is true.</summary>
    public static void Register(QuantityKindInference inf)
    {
        if (inf is null)
        {
            throw new ArgumentNullException(nameof(inf));
        }

        if (_sealed)
        {
            throw new InvalidOperationException("QuantityKindInferenceRegistry is sealed; no further rules can be registered.");
        }

        AddOrConflict(inf.Left, inf.Operator, inf.Right, inf.Result);
        _rules.Add(inf);

        if (inf.Commutative && inf.Operator == QuantityKindBinaryOperator.Multiply)
        {
            AddOrConflict(inf.Right, inf.Operator, inf.Left, inf.Result);
        }
    }

    private static void AddOrConflict(QuantityKind left, QuantityKindBinaryOperator op, QuantityKind right, QuantityKind result)
    {
        var key = (left, op, right);

        if (_map.TryGetValue(key, out var existing))
        {
            if (StrictConflictDetection && !ReferenceEquals(existing, result))
            {
                throw new InvalidOperationException($"Inference rule conflict: {left.Name} {op} {right.Name} already maps to {existing.Name}, attempted {result.Name}.");
            }
        }

        _map[key] = result;
    }

    /// <summary>Attempts to resolve an inference. Returns null when no mapping exists.</summary>
    public static QuantityKind ResolveOrNull(QuantityKind left, QuantityKindBinaryOperator op, QuantityKind right)
    {
        _map.TryGetValue((left, op, right), out var res);
        return res;
    }

    /// <summary>Enumerates currently registered canonical (non-symmetric duplicate) rules.</summary>
    public static IEnumerable<QuantityKindInference> EnumerateRules() => _rules.ToList();

    /// <summary>Prevents further modification of the registry. Idempotent.</summary>
    public static void Seal() => _sealed = true;

    /// <summary>Test-only helper to reset registry state for isolation (clears, reseeds, and unseals). Not for production use.</summary>
    internal static void ResetForTests()
    {
        Seed();
    }
}