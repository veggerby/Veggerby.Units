using System.Collections.Generic;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Binary operator kinds supported by the quantity kind inference registry.
/// Only multiplication is considered commutative for lookup purposes.
/// </summary>
public enum QuantityKindBinaryOperator
{
    /// <summary>Binary multiplication (commutative): attempts both (L,R) and (R,L) when resolving.</summary>
    Multiply,
    /// <summary>Binary division (non-commutative): only (L / R) queried.</summary>
    Divide
}

/// <summary>
/// Represents a semantic inference mapping (LeftKind op RightKind => ResultKind).
/// For commutative operators (Multiply) the registry automatically installs the symmetric mapping.
/// </summary>
public sealed record QuantityKindInference(QuantityKind Left, QuantityKindBinaryOperator Operator, QuantityKind Right, QuantityKind Result, bool Commutative = false);

/// <summary>
/// Central registry for quantity kind inference rules. Keeps mappings immutable after static initialization.
/// </summary>
public static class QuantityKindInferenceRegistry
{
    private static readonly Dictionary<(QuantityKind, QuantityKindBinaryOperator, QuantityKind), QuantityKind> _map = new();

    static QuantityKindInferenceRegistry()
    {
        // Seed rules (thermodynamics): Entropy (J/K) * Absolute Temperature (K) => Energy (J)
        Register(new QuantityKindInference(QuantityKinds.Entropy, QuantityKindBinaryOperator.Multiply, QuantityKinds.TemperatureAbsolute, QuantityKinds.Energy, Commutative: true));
        // Derived inverse rules for division
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.TemperatureAbsolute, QuantityKinds.Entropy));
        Register(new QuantityKindInference(QuantityKinds.Energy, QuantityKindBinaryOperator.Divide, QuantityKinds.Entropy, QuantityKinds.TemperatureAbsolute));
    }

    private static void Register(QuantityKindInference inf)
    {
        _map[(inf.Left, inf.Operator, inf.Right)] = inf.Result;
        if (inf.Commutative && inf.Operator == QuantityKindBinaryOperator.Multiply)
        {
            _map[(inf.Right, inf.Operator, inf.Left)] = inf.Result;
        }
    }

    /// <summary>Attempts to resolve an inference. Returns null when no mapping exists.</summary>
    public static QuantityKind ResolveOrNull(QuantityKind left, QuantityKindBinaryOperator op, QuantityKind right)
    {
        _map.TryGetValue((left, op, right), out var res);
        return res;
    }
}