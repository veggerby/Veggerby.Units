using Veggerby.Units.Reduction;
namespace Veggerby.Units.Dimensions;

/// <summary>Composite dimension representing a base raised to an integer exponent.</summary>
public class PowerDimension : Dimension, IPowerOperation, ICanonicalFactorsProvider
{
    private readonly Dimension _base;
    private readonly int _exponent;

    internal PowerDimension(Dimension @base, int exponent)
    {
        _base = @base;
        _exponent = exponent;
    }

    /// <inheritdoc />
    public override string Symbol => $"{_base.Symbol}^{_exponent}";
    /// <inheritdoc />
    public override string Name => $"{_base.Name} ^ {_exponent}";

    IOperand IPowerOperation.Base => _base;
    int IPowerOperation.Exponent => _exponent;

    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IPowerOperation);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 29;
            hash = hash * 37 + _base.GetHashCode();
            hash = hash * 37 + _exponent.GetHashCode();
            return hash ^ 0x7777AAAA;
        }
    }
    FactorVector<IOperand>? ICanonicalFactorsProvider.GetCanonicalFactors()
    {
        if (!ReductionSettings.UseFactorVector)
        {
            return null;
        }
        return new FactorVector<IOperand>(new[] { ((IOperand)_base, _exponent) });
    }
}