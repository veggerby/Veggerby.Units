using System;
using System.Collections.Generic;
using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Reduction;

using Xunit;

namespace Veggerby.Units.Tests;

public class ReductionEqualityFlagTests
{
    private readonly Unit[] _baseUnits = new[] { Unit.SI.m, Unit.SI.s, Unit.SI.kg, Unit.SI.A, Unit.SI.K };

    [Fact]
    public void GivenRandomisedComposites_WhenComparingWithAndWithoutMapEquality_ThenParityIsMaintained()
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

        var original = ReductionSettings.EqualityUsesMap;
        try
        {
            ReductionSettings.EqualityUsesMap = false;
            var expected = pairs.Select(p => p.left == p.right).ToArray();

            ReductionSettings.EqualityUsesMap = true;
            var actual = pairs.Select(p => p.left == p.right).ToArray();

            // Assert
            actual.Should().BeEquivalentTo(expected);
            actual.Should().BeEquivalentTo(Enumerable.Repeat(true, actual.Length)); // all true
        }
        finally
        {
            ReductionSettings.EqualityUsesMap = original;
        }
    }

    [Fact]
    public void GivenAdversarialOrderingWithPowers_WhenComparingEquality_ThenParityIsMaintained()
    {
        // Arrange
        var left = ((Unit.SI.m * Unit.SI.s) ^ 2) * Unit.SI.kg; // (m*s)^2 * kg
        var right = (Unit.SI.m ^ 2) * (Unit.SI.s ^ 2) * Unit.SI.kg; // m^2 * s^2 * kg

        var original = ReductionSettings.EqualityUsesMap;
        try
        {
            ReductionSettings.EqualityUsesMap = false;
            var expected = left == right;

            ReductionSettings.EqualityUsesMap = true;
            var actual = left == right;

            // Assert
            expected.Should().BeTrue();
            actual.Should().BeTrue();
        }
        finally
        {
            ReductionSettings.EqualityUsesMap = original;
        }
    }

    [Fact]
    public void GivenEarlyMismatch_WhenComparingEquality_ThenBothPathsReturnFalse()
    {
        // Arrange
        var left = Unit.SI.m * Unit.SI.s * Unit.SI.kg;
        var right = Unit.SI.m * (Unit.SI.s ^ 2) * Unit.SI.kg; // structural difference (s vs s^2)

        var original = ReductionSettings.EqualityUsesMap;
        try
        {
            ReductionSettings.EqualityUsesMap = false;
            var expected = left == right;

            ReductionSettings.EqualityUsesMap = true;
            var actual = left == right;

            // Assert
            expected.Should().BeFalse();
            actual.Should().BeFalse();
        }
        finally
        {
            ReductionSettings.EqualityUsesMap = original;
        }
    }
}