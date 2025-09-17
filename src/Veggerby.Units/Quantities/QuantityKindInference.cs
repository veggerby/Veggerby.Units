namespace Veggerby.Units.Quantities;

/// <summary>
/// Represents a semantic inference mapping (LeftKind op RightKind => ResultKind).
/// For commutative operators (Multiply) the registry automatically installs the symmetric mapping.
/// </summary>
public sealed record QuantityKindInference(QuantityKind Left, QuantityKindBinaryOperator Operator, QuantityKind Right, QuantityKind Result, bool Commutative = false);
