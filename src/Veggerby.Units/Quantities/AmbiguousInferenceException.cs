using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Exception thrown when multiple conflicting transitive inference paths are discovered
/// for the same operation in strict mode.
/// </summary>
public class AmbiguousInferenceException : InvalidOperationException
{
    /// <summary>Gets the left operand quantity kind.</summary>
    public QuantityKind Left { get; }

    /// <summary>Gets the binary operator.</summary>
    public QuantityKindBinaryOperator Operator { get; }

    /// <summary>Gets the right operand quantity kind.</summary>
    public QuantityKind Right { get; }

    /// <summary>Gets the conflicting inference paths discovered.</summary>
    public IReadOnlyList<InferencePath> ConflictingPaths { get; }

    /// <summary>
    /// Creates a new ambiguous inference exception.
    /// </summary>
    /// <param name="left">Left operand quantity kind.</param>
    /// <param name="op">Binary operator.</param>
    /// <param name="right">Right operand quantity kind.</param>
    /// <param name="conflictingPaths">The conflicting paths that were discovered.</param>
    public AmbiguousInferenceException(
        QuantityKind left,
        QuantityKindBinaryOperator op,
        QuantityKind right,
        IEnumerable<InferencePath> conflictingPaths)
        : base(BuildMessage(left, op, right, conflictingPaths))
    {
        Left = left;
        Operator = op;
        Right = right;
        ConflictingPaths = conflictingPaths?.ToList() ?? [];
    }

    private static string BuildMessage(
        QuantityKind left,
        QuantityKindBinaryOperator op,
        QuantityKind right,
        IEnumerable<InferencePath> paths)
    {
        var pathList = paths?.ToList() ?? [];
        var results = pathList.Select(p => p.Steps.LastOrDefault()?.Result?.Name ?? "unknown").Distinct().ToList();

        return $"Ambiguous inference for {left.Name} {op} {right.Name}: multiple paths lead to different results ({string.Join(", ", results)}). " +
               $"Found {pathList.Count} conflicting path(s).";
    }
}
