using System.Linq;
using Veggerby.Units.Dimensions;
using Veggerby.Units.Reduction;

namespace Veggerby.Units;

/// <summary>
/// Composite unit representing a division between a dividend and a divisor unit.
/// Reduction logic cancels shared factors during construction via operator helpers.
/// </summary>
public class DivisionUnit : Unit, IDivisionOperation, ICanonicalFactorsProvider
{
    private readonly Unit _dividend;
    private readonly Unit _divisor;

    internal DivisionUnit(Unit dividend, Unit divisor)
    {
        _dividend = dividend;
        _divisor = divisor;
    }

    /// <inheritdoc />
    public override string Symbol => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Symbol)}/{_divisor.Symbol}";
    /// <inheritdoc />
    public override string Name => $"{(_dividend.Symbol == string.Empty ? "1" : _dividend.Name)} / {_divisor.Name}";
    /// <inheritdoc />
    public override UnitSystem System => _dividend != Unit.None ? _dividend.System : _divisor.System;
    /// <inheritdoc />
    public override Dimension Dimension => _dividend.Dimension / _divisor.Dimension;
    internal override T Accept<T>(Visitors.Visitor<T> visitor) => visitor.Visit(this);
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
        // Represent dividend factors with positive exponents, divisor with negative.
        var map = ExponentMap<IOperand>.Rent();
        try
        {
            Factorization.AccumulateProduct(map, new[] { (IOperand)_dividend });
            var dividendEntries = map.Entries().ToDictionary(kv => kv.Key, kv => kv.Value);
            map.Return();
            var map2 = ExponentMap<IOperand>.Rent();
            try
            {
                Factorization.AccumulateProduct(map2, new[] { (IOperand)_divisor });
                foreach (var kv in map2.Entries())
                {
                    dividendEntries[kv.Key] = dividendEntries.TryGetValue(kv.Key, out var v) ? v - kv.Value : -kv.Value;
                }
                var arr = dividendEntries
                    .Where(x => x.Value != 0)
                    .OrderBy(t => t.Key.GetType().FullName)
                    .ThenBy(t => (t.Key as Unit)?.Symbol ?? string.Empty)
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
            // In case of unexpected error, ensure map returned.
            throw;
        }
    }

    /// <inheritdoc />
    public override bool Equals(object obj) => OperationUtility.Equals(this, obj as IDivisionOperation);
    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            // Order matters for division; incorporate a prime and tag
            int hash = 23;
            hash = hash * 31 + _dividend.GetHashCode();
            hash = hash * 31 + (_divisor.GetHashCode() ^ unchecked((int)0xAAAAAAAA));
            return hash ^ 0x33333333;
        }
    }

    internal override double GetScaleFactor() => _dividend.GetScaleFactor() / _divisor.GetScaleFactor();
}