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

    /// <summary>When true, registering a rule that overwrites an existing mapping throws instead of replacing it.</summary>
    public static bool StrictConflictDetection { get; set; } = true;

    static QuantityKindInferenceRegistry()
    {
        // Seed rules (thermodynamics): Entropy (J/K) * Absolute Temperature (K) => Energy (J)
        Register(new QuantityKindInference(QuantityKinds.Entropy, QuantityKindBinaryOperator.Multiply, QuantityKinds.TemperatureAbsolute, QuantityKinds.Energy, Commutative: true));
        // Derived inverse rules for division
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.TemperatureAbsolute, QuantityKinds.Entropy));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Entropy, QuantityKinds.TemperatureAbsolute));

        // Rotational work: Torque * Angle => Energy (dimensionally N·m * 1 = N·m)
        Register(new QuantityKindInference(QuantityKinds.Torque, QuantityKindBinaryOperator.Multiply, QuantityKinds.Angle, QuantityKinds.Energy, Commutative: true));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Angle, QuantityKinds.Torque));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Torque, QuantityKinds.Angle));
    }

    /// <summary>Registers an inference rule. For commutative multiplication the symmetric rule is generated. Throws on conflict when <see cref="StrictConflictDetection"/> is true.</summary>
    public static void Register(QuantityKindInference inf)
    {
        if (inf == null)
        {
            throw new ArgumentNullException(nameof(inf));
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
}