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