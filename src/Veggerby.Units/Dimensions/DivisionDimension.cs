using System.Linq;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Dimensions;

/// <summary>Composite dimension representing dividend/divisor.</summary>
public class DivisionDimension : Dimension, IDivisionOperation, ICanonicalFactorsProvider
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
    private FactorVector<IOperand>? _cachedFactors;
    FactorVector<IOperand>? ICanonicalFactorsProvider.GetCanonicalFactors()
    {
        if (!ReductionSettings.UseFactorVector)
        {
            return null;
        }
        if (_cachedFactors.HasValue)
        {
            return _cachedFactors;
        }
        var map = ExponentMap<IOperand>.Rent();
        try
        {
            Factorization.AccumulateProduct(map, new[] { (IOperand)_dividend });
            var dict = map.Entries().ToDictionary(kv => kv.Key, kv => kv.Value);
            map.Return();
            var map2 = ExponentMap<IOperand>.Rent();
            try
            {
                Factorization.AccumulateProduct(map2, new[] { (IOperand)_divisor });
                foreach (var kv in map2.Entries())
                {
                    dict[kv.Key] = dict.TryGetValue(kv.Key, out var v) ? v - kv.Value : -kv.Value;
                }
                var arr = dict
                    .Where(x => x.Value != 0)
                    .OrderBy(t => t.Key.GetType().FullName)
                    .ThenBy(t => (t.Key as Dimension)?.Symbol ?? string.Empty)
                    .Select(t => (t.Key, t.Value))
                    .ToArray();
                _cachedFactors = new FactorVector<IOperand>(arr);
                return _cachedFactors;
            }
            finally
            {
                map2.Return();
            }
        }
        catch
        {
            throw;
        }
    }
}