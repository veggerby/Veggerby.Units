using System.Collections.Generic;

namespace Veggerby.Units.Reduction;

/// <summary>
/// Internal abstraction representing a commutative product of one or more operands. Order is not semantically
/// significant; reduction utilities may reorder operands to achieve canonical form.
/// </summary>
internal interface IProductOperation : IOperand
{
    /// <summary>Logical operand sequence (may not reflect original source order).</summary>
    IEnumerable<IOperand> Operands { get; }
}