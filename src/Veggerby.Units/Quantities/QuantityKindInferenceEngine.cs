using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Internal engine for performing transitive quantity kind inference using breadth-first search.
/// Supports multi-step inference chains with cycle detection and ambiguity handling.
/// </summary>
internal static class QuantityKindInferenceEngine
{
    /// <summary>
    /// Attempts to find an inference path from left ⊗ right to a result kind within the specified depth limit.
    /// </summary>
    /// <param name="left">Left operand quantity kind.</param>
    /// <param name="op">Binary operator.</param>
    /// <param name="right">Right operand quantity kind.</param>
    /// <param name="maxDepth">Maximum inference chain depth (must be ≥ 1).</param>
    /// <param name="strictAmbiguity">When true, throws <see cref="AmbiguousInferenceException"/> if multiple paths to different results exist.</param>
    /// <param name="cache">Optional cache for transitive results.</param>
    /// <returns>The inference path if found; otherwise null.</returns>
    /// <exception cref="AmbiguousInferenceException">Thrown when strictAmbiguity is true and multiple conflicting paths exist.</exception>
    public static InferencePath FindPath(
        QuantityKind left,
        QuantityKindBinaryOperator op,
        QuantityKind right,
        int maxDepth,
        bool strictAmbiguity,
        Dictionary<(QuantityKind, QuantityKindBinaryOperator, QuantityKind, int), QuantityKind> cache = null)
    {
        if (maxDepth < 1)
        {
            return null;
        }

        // Check direct mapping first (depth 1)
        var direct = QuantityKindInferenceRegistry.ResolveOrNull(left, op, right);
        if (direct is not null)
        {
            var step = new InferenceStep(left, op, right, direct);
            return new InferencePath([step], 1);
        }

        if (maxDepth == 1)
        {
            return null; // single-hop only, no result
        }

        // BFS for multi-step paths
        var allPaths = new List<InferencePath>();
        var queue = new Queue<SearchState>();
        var visited = new HashSet<(QuantityKind, QuantityKindBinaryOperator, QuantityKind, int)>();

        // Initial state: search for left ⊗ right at depth maxDepth
        queue.Enqueue(new SearchState(left, op, right, [], maxDepth, new HashSet<QuantityKind> { left, right }));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            // Mark as visited
            var visitKey = (state.Left, state.Op, state.Right, state.RemainingDepth);
            if (!visited.Add(visitKey))
            {
                continue;
            }

            if (state.RemainingDepth < 1)
            {
                continue;
            }

            // Check cache first if provided
            if (cache is not null && cache.TryGetValue((state.Left, state.Op, state.Right, state.RemainingDepth), out var cachedResult))
            {
                if (cachedResult is not null)
                {
                    // Reconstruct path from cache (we cache only the result, not the full path)
                    // For simplicity, we'll treat cached results as single-step
                    var cachedStep = new InferenceStep(state.Left, state.Op, state.Right, cachedResult);
                    var cachedPath = new InferencePath([.. state.Steps, cachedStep], state.Steps.Count + 1);
                    allPaths.Add(cachedPath);
                    continue;
                }
            }

            // Direct resolution at current level
            var result = QuantityKindInferenceRegistry.ResolveOrNull(state.Left, state.Op, state.Right);
            if (result is not null)
            {
                var step = new InferenceStep(state.Left, state.Op, state.Right, result);
                var path = new InferencePath([.. state.Steps, step], state.Steps.Count + 1);
                allPaths.Add(path);

                // Cache this result
                cache?.TryAdd((state.Left, state.Op, state.Right, state.RemainingDepth), result);

                continue; // found a path, don't expand further from this state
            }

            // No direct mapping; try expanding via intermediate steps
            if (state.RemainingDepth > 1)
            {
                ExpandSearchState(state, queue);
            }
        }

        // Handle results
        if (allPaths.Count == 0)
        {
            return null;
        }

        // Check for ambiguity (multiple paths to different results)
        var distinctResults = allPaths.Select(p => p.Steps.Last().Result).Distinct().ToList();

        if (strictAmbiguity && distinctResults.Count > 1)
        {
            throw new AmbiguousInferenceException(left, op, right, allPaths);
        }

        // Return shortest path (or first if multiple paths to same result)
        return allPaths.OrderBy(p => p.Depth).First();
    }

    /// <summary>
    /// Enumerates all possible inference paths within the depth limit.
    /// </summary>
    public static IEnumerable<InferencePath> EnumerateAllPaths(
        QuantityKind left,
        QuantityKindBinaryOperator op,
        QuantityKind right,
        int maxDepth)
    {
        if (maxDepth < 1)
        {
            yield break;
        }

        // Check direct mapping
        var direct = QuantityKindInferenceRegistry.ResolveOrNull(left, op, right);
        if (direct is not null)
        {
            yield return new InferencePath([new InferenceStep(left, op, right, direct)], 1);
        }

        if (maxDepth == 1)
        {
            yield break;
        }

        // BFS for all paths
        var queue = new Queue<SearchState>();
        var seenPaths = new HashSet<string>(); // use string representation to detect duplicate paths

        queue.Enqueue(new SearchState(left, op, right, [], maxDepth, new HashSet<QuantityKind> { left, right }));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.RemainingDepth < 1)
            {
                continue;
            }

            var result = QuantityKindInferenceRegistry.ResolveOrNull(state.Left, state.Op, state.Right);
            if (result is not null)
            {
                var step = new InferenceStep(state.Left, state.Op, state.Right, result);
                var path = new InferencePath([.. state.Steps, step], state.Steps.Count + 1);
                var pathKey = path.ToString();

                if (seenPaths.Add(pathKey))
                {
                    yield return path;
                }

                continue; // found result, don't expand further
            }

            if (state.RemainingDepth > 1)
            {
                ExpandSearchState(state, queue);
            }
        }
    }

    private static void ExpandSearchState(SearchState state, Queue<SearchState> queue)
    {
        // Strategy: For L ⊗ R, try all known rules I1 ⊗ I2 → Intermediate:
        // - If I1 == L, try resolve(Intermediate ⊗ R) at depth-1
        // - If I2 == R (and op is Multiply due to commutativity), try resolve(L ⊗ Intermediate) at depth-1
        // - If I2 == L (multiply commutative), try resolve(Intermediate ⊗ R)
        // - If I1 == R (multiply commutative), try resolve(L ⊗ Intermediate)

        foreach (var rule in QuantityKindInferenceRegistry.EnumerateRules())
        {
            // Case 1: rule.Left == state.Left
            if (ReferenceEquals(rule.Left, state.Left) && rule.Operator == state.Op)
            {
                // We have L ⊗ I2 → Intermediate, now try Intermediate ⊗ R
                var intermediate = rule.Result;

                // Cycle detection: ensure intermediate not in visited kinds
                if (state.VisitedKinds.Contains(intermediate))
                {
                    continue;
                }

                var newVisited = new HashSet<QuantityKind>(state.VisitedKinds) { intermediate };
                var step = new InferenceStep(state.Left, state.Op, rule.Right, intermediate);

                // Queue: resolve Intermediate ⊗ R
                queue.Enqueue(new SearchState(
                    intermediate,
                    state.Op,
                    state.Right,
                    [.. state.Steps, step],
                    state.RemainingDepth - 1,
                    newVisited));
            }

            // Case 2: rule.Right == state.Right (non-commutative division or explicit multiply)
            if (ReferenceEquals(rule.Right, state.Right) && rule.Operator == state.Op)
            {
                var intermediate = rule.Result;

                if (state.VisitedKinds.Contains(intermediate))
                {
                    continue;
                }

                var newVisited = new HashSet<QuantityKind>(state.VisitedKinds) { intermediate };
                var step = new InferenceStep(rule.Left, state.Op, state.Right, intermediate);

                // Queue: resolve L ⊗ Intermediate
                queue.Enqueue(new SearchState(
                    state.Left,
                    state.Op,
                    intermediate,
                    [.. state.Steps, step],
                    state.RemainingDepth - 1,
                    newVisited));
            }

            // For multiply: additional commutative cases
            if (state.Op == QuantityKindBinaryOperator.Multiply && rule.Operator == QuantityKindBinaryOperator.Multiply)
            {
                // Case 3: rule.Right == state.Left (commutative)
                if (ReferenceEquals(rule.Right, state.Left))
                {
                    var intermediate = rule.Result;

                    if (state.VisitedKinds.Contains(intermediate))
                    {
                        continue;
                    }

                    var newVisited = new HashSet<QuantityKind>(state.VisitedKinds) { intermediate };
                    var step = new InferenceStep(rule.Left, state.Op, state.Left, intermediate);

                    queue.Enqueue(new SearchState(
                        intermediate,
                        state.Op,
                        state.Right,
                        [.. state.Steps, step],
                        state.RemainingDepth - 1,
                        newVisited));
                }

                // Case 4: rule.Left == state.Right (commutative)
                if (ReferenceEquals(rule.Left, state.Right))
                {
                    var intermediate = rule.Result;

                    if (state.VisitedKinds.Contains(intermediate))
                    {
                        continue;
                    }

                    var newVisited = new HashSet<QuantityKind>(state.VisitedKinds) { intermediate };
                    var step = new InferenceStep(state.Right, state.Op, rule.Right, intermediate);

                    queue.Enqueue(new SearchState(
                        state.Left,
                        state.Op,
                        intermediate,
                        [.. state.Steps, step],
                        state.RemainingDepth - 1,
                        newVisited));
                }
            }
        }
    }

    private sealed class SearchState
    {
        public QuantityKind Left { get; }
        public QuantityKindBinaryOperator Op { get; }
        public QuantityKind Right { get; }
        public List<InferenceStep> Steps { get; }
        public int RemainingDepth { get; }
        public HashSet<QuantityKind> VisitedKinds { get; }

        public SearchState(
            QuantityKind left,
            QuantityKindBinaryOperator op,
            QuantityKind right,
            List<InferenceStep> steps,
            int remainingDepth,
            HashSet<QuantityKind> visitedKinds)
        {
            Left = left;
            Op = op;
            Right = right;
            Steps = steps;
            RemainingDepth = remainingDepth;
            VisitedKinds = visitedKinds;
        }
    }
}
