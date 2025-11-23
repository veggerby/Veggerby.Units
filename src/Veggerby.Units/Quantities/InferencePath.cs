using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Represents a complete inference path from source kinds to a result kind,
/// including all intermediate steps.
/// </summary>
/// <param name="Steps">The sequence of inference steps that lead to the result.</param>
/// <param name="Depth">The number of steps in the inference chain.</param>
public sealed record InferencePath(
    IReadOnlyList<InferenceStep> Steps,
    int Depth)
{
    /// <summary>
    /// Returns a human-readable representation of the inference path.
    /// </summary>
    public override string ToString()
    {
        if (Steps is null || Steps.Count == 0)
        {
            return "Empty path";
        }

        var parts = Steps.Select(s => $"{s.Left.Name} {s.Operator} {s.Right.Name} → {s.Result.Name}");
        return string.Join(" ; ", parts);
    }
}
