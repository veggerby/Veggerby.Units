using System.Collections.Generic;

namespace Veggerby.Units.Reduction;

/// <summary>
/// Prototype internal helper for accumulating integer exponents for operands without repeated dictionary
/// lookups in tight reduction loops. Not yet integrated; reserved for future optimization work.
/// </summary>
/// <remarks>
/// Potential future usage: Replace LINQ GroupBy in ReduceMultiplication / ReduceDivision with pooled ExponentMap
/// to reduce allocations. Requires profiling validation before integration.
/// </remarks>
internal sealed class ExponentMap<T>
    where T : IOperand
{
    private readonly Dictionary<T, int> _map = new();
    private static readonly Stack<ExponentMap<T>> _pool = new();

    public static ExponentMap<T> Rent()
    {
        lock (_pool)
        {
            return _pool.Count > 0 ? _pool.Pop().Reset() : new ExponentMap<T>();
        }
    }

    public void Add(T operand, int exponent)
    {
        if (_map.TryGetValue(operand, out var existing))
        {
            _map[operand] = existing + exponent;
        }
        else
        {
            _map.Add(operand, exponent);
        }
    }

    public IEnumerable<KeyValuePair<T, int>> Entries() => _map;

    public bool TryGetValue(T key, out int value) => _map.TryGetValue(key, out value);

    public bool ContainsKey(T key) => _map.ContainsKey(key);

    private ExponentMap<T> Reset()
    {
        _map.Clear();
        return this;
    }

    public void Return()
    {
        lock (_pool)
        {
            _pool.Push(this);
        }
    }
}