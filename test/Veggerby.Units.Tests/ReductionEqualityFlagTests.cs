using System;
using System.Collections.Generic;
using System.Linq;

using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class ReductionEqualityFlagTests
{
    private readonly Unit[] _baseUnits = new[] { Unit.SI.m, Unit.SI.s, Unit.SI.kg, Unit.SI.A, Unit.SI.K };

    [Fact]
    public void GivenRandomisedComposites_WhenComparingEquality_ThenCanonicalParityMaintained()
    {
        // Arrange
        var sizes = new[] { 2, 3, 5 }; // modest sizes; larger sizes covered by benchmarks
        var pairs = new List<(Unit left, Unit right)>();
        foreach (var size in sizes)
        {
            var factors = _baseUnits.Take(size).ToArray();
            var left = factors.Aggregate((a, b) => a * b);
            var right = factors.Reverse().Aggregate((a, b) => a * b);
            pairs.Add((left, right));
        }

        var actual = pairs.Select(p => p.left == p.right).ToArray();
        actual.Should().BeEquivalentTo(Enumerable.Repeat(true, actual.Length));
    }

    [Fact]
    public void GivenAdversarialOrderingWithPowers_WhenComparingEquality_ThenCanonicalPathSucceeds()
    {
        // Arrange
        var left = ((Unit.SI.m * Unit.SI.s) ^ 2) * Unit.SI.kg; // (m*s)^2 * kg
        var right = (Unit.SI.m ^ 2) * (Unit.SI.s ^ 2) * Unit.SI.kg; // m^2 * s^2 * kg
        var eq = left == right;
        if (!eq)
        {
            // Diagnostic factors (best effort, will not throw)
            try
            {
                var lf = Reduction.OperationUtility.TryGetCanonicalFactorsForDiagnostics(left);
                var rf = Reduction.OperationUtility.TryGetCanonicalFactorsForDiagnostics(right);
                System.Diagnostics.Debug.WriteLine($"[TEST DIAG] Adversarial equality mismatch. L=({string.Join(' ', lf.Select(f => f.Symbol + "^" + f.Exponent))}) R=({string.Join(' ', rf.Select(f => f.Symbol + "^" + f.Exponent))})");
            }
            catch { }
        }
        eq.Should().BeTrue();
    }

    [Fact]
    public void GivenEarlyMismatch_WhenComparingEquality_ThenCanonicalReturnsFalse()
    {
        // Arrange
        var left = Unit.SI.m * Unit.SI.s * Unit.SI.kg;
        var right = Unit.SI.m * (Unit.SI.s ^ 2) * Unit.SI.kg; // structural difference (s vs s^2)
        (left == right).Should().BeFalse();
    }
}