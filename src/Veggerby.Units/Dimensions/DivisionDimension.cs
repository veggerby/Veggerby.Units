using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

/// <summary>Composite dimension representing dividend/divisor.</summary>
public class DivisionDimension : Dimension, IDivisionOperation
{
    private readonly Dimension _dividend;
    private readonly Dimension _divisor;

    internal DivisionDimension(Dimension dividend, Dimension divisor)
    {
        _dividend = dividend;
        _divisor = divisor;
    }

    /// <inheritdoc />
    public override string Symbol => string.Format("{0}/{1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Symbol, _divisor.Symbol);
    /// <inheritdoc />
    public override string Name => string.Format("{0} / {1}", _dividend.Symbol == string.Empty ? "1" : _dividend.Name, _divisor.Name);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 23;
            hash = hash * 31 + _dividend.GetHashCode();
            hash = hash * 31 + (_divisor.GetHashCode() ^ unchecked((int)0xAAAAAAAA));
            return hash ^ 0x3333AAAA;
        }
    }
    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IDivisionOperation);

    IOperand IDivisionOperation.Dividend => _dividend;
    IOperand IDivisionOperation.Divisor => _divisor;
}