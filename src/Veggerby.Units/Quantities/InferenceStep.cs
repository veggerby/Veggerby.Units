namespace Veggerby.Units.Quantities;

/// <summary>
/// Represents a single step in a quantity kind inference chain.
/// </summary>
/// <param name="Left">Left operand quantity kind.</param>
/// <param name="Operator">Binary operator applied.</param>
/// <param name="Right">Right operand quantity kind.</param>
/// <param name="Result">Resulting quantity kind from this step.</param>
public sealed record InferenceStep(
    QuantityKind Left,
    QuantityKindBinaryOperator Operator,
    QuantityKind Right,
    QuantityKind Result);
