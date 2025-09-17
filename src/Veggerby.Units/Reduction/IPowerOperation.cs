namespace Veggerby.Units.Reduction;

/// <summary>
/// Internal abstraction representing exponentiation (Base ^ Exponent). Negative exponent handling is performed
/// outside this interface (caller transforms to division before reduction).
/// </summary>
internal interface IPowerOperation : IOperand
{
    /// <summary>Base operand being exponentiated.</summary>
    IOperand Base { get; }
    /// <summary>Integer exponent (always positive within this abstraction).</summary>
    int Exponent { get; }
}