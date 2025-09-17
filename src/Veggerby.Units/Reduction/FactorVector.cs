namespace Veggerby.Units.Reduction;

/// <summary>
/// Immutable canonical factor representation. Factors are sorted deterministically by (Type, Symbol)
/// and contain only non-zero exponents. Used for fast equality when enabled.
/// </summary>
internal readonly struct FactorVector<T>((T Base, int Exponent)[] factors) where T : IOperand
{
    public readonly (T Base, int Exponent)[] Factors = factors;
}

internal interface ICanonicalFactorsProvider
{
    FactorVector<IOperand>? GetCanonicalFactors();
}