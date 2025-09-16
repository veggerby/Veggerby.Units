namespace Veggerby.Units.Reduction;

/// <summary>
/// Marker interface for operands in algebraic unit / dimension expressions used by reduction algorithms.
/// Implemented by concrete unit and dimension composite leaf / composite types to allow uniform traversal and
/// transformation without exposing their concrete API surface to the reduction layer.
/// </summary>
internal interface IOperand { }