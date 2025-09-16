namespace Veggerby.Units.Reduction;

/// <summary>
/// Internal abstraction representing a division expression (Dividend / Divisor). Enables reduction utilities to
/// inspect structural operands without depending on concrete types.
/// </summary>
internal interface IDivisionOperation : IOperand
{
    /// <summary>Left side of the division (numerator).</summary>
    IOperand Dividend { get; }
    /// <summary>Right side of the division (denominator).</summary>
    IOperand Divisor { get; }
}