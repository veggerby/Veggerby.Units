using System.Collections.Generic;

namespace Veggerby.Units.Reduction;

/// <summary>
/// Internal helper centralising factor accumulation logic for multiplication and division reductions.
/// Uses <see cref="ExponentMap{T}"/> to aggregate exponents while detecting duplicates / cancellations.
/// Zero exponent factors are omitted from enumeration results.
/// </summary>
internal static class Factorization
{
    /// <summary>
    /// Accumulates multiplicative operands (including nested products / powers) into the supplied map.
    /// </summary>
    /// <returns>True if any duplicate (reducible) factors were encountered.</returns>
    internal static bool AccumulateProduct<T>(ExponentMap<T> map, IEnumerable<T> operands) where T : IOperand
    {
        var duplicate = false;
        foreach (var operand in operands)
        {
            foreach (var expanded in operand.ExpandMultiplication())
            {
                if (expanded is IPowerOperation p)
                {
                    if (!duplicate && map.ContainsKey((T)p.Base))
                    {
                        duplicate = true;
                    }
                    map.Add((T)p.Base, p.Exponent);
                }
                else
                {
                    if (!duplicate && map.ContainsKey((T)expanded))
                    {
                        duplicate = true;
                    }
                    map.Add((T)expanded, 1);
                }
            }
        }

        return duplicate;
    }

    /// <summary>
    /// Accumulates dividend and divisor operands into a single map (+ for dividend, - for divisor).
    /// </summary>
    /// <returns>True if any cancellation potential was detected (shared factor).</returns>
    internal static bool AccumulateDivision<T>(ExponentMap<T> map, T dividend, T divisor) where T : IOperand
    {
        var cancellation = false;
        foreach (var term in dividend.ExpandMultiplication())
        {
            if (term is IPowerOperation p)
            {
                map.Add((T)p.Base, p.Exponent);
            }
            else
            {
                map.Add((T)term, 1);
            }
        }

        foreach (var term in divisor.ExpandMultiplication())
        {
            if (term is IPowerOperation p)
            {
                if (!cancellation && map.ContainsKey((T)p.Base))
                {
                    cancellation = true;
                }
                map.Add((T)p.Base, -p.Exponent);
            }
            else
            {
                if (!cancellation && map.ContainsKey((T)term))
                {
                    cancellation = true;
                }
                map.Add((T)term, -1);
            }
        }
        return cancellation;
    }

    /// <summary>
    /// Enumerates factors (base, exponent) excluding zero exponents.
    /// </summary>
    internal static IEnumerable<KeyValuePair<T, int>> EnumerateNonZero<T>(ExponentMap<T> map) where T : IOperand
    {
        foreach (var kv in map.Entries())
        {
            if (kv.Value != 0)
            {
                yield return kv;
            }
        }
    }
}