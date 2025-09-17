using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests.Property;

public class ProductPowerEquality_Property
{
    private static readonly Unit[] Bases = new[] { Unit.SI.m, Unit.SI.s, Unit.SI.kg, Unit.SI.A };

    [Fact]
    public void ProductPowerEquality_FuzzCases()
    {
        // Arrange
        var origLazy = ReductionSettings.LazyPowerExpansion;
        var origNorm = ReductionSettings.EqualityNormalizationEnabled;
        var rnd = new Random(12345);
        ReductionSettings.EqualityNormalizationEnabled = true;
        ReductionSettings.LazyPowerExpansion = true;
        try
        {
            for (int seedIter = 0; seedIter < 2; seedIter++)
            {
                for (int caseIdx = 0; caseIdx < 250; caseIdx++)
                {
                    int len = rnd.Next(1, 6); // up to 5 factors
                    var list = new List<Unit>();
                    for (int i = 0; i < len; i++)
                    {
                        list.Add(Bases[rnd.Next(Bases.Length)]);
                    }
                    int exponent = 0;
                    while (exponent == 0) { exponent = rnd.Next(-5, 6); } // exclude 0

                    // Build lazy: (product)^n (if positive); for negative: reciprocal handled by ^ operator already
                    var product = list[0];
                    for (int i = 1; i < list.Count; i++) { product *= list[i]; }
                    var lazy = product ^ exponent;

                    // Build eager distributed equivalent by aggregating base counts * exponent magnitude
                    var counts = new Dictionary<Unit, int>();
                    foreach (var u in list)
                    {
                        counts[u] = counts.TryGetValue(u, out var c) ? c + 1 : 1;
                    }
                    Unit eager = Unit.None;
                    foreach (var kv in counts)
                    {
                        var exp = kv.Value * Math.Abs(exponent);
                        var pow = kv.Key ^ exp;
                        eager = eager == Unit.None ? pow : eager * pow;
                    }
                    if (exponent < 0) { eager = 1 / eager; }

                    // Act & Assert
                    (lazy == eager).Should().BeTrue();
                }
            }
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = origLazy;
            ReductionSettings.EqualityNormalizationEnabled = origNorm;
        }
    }
}
