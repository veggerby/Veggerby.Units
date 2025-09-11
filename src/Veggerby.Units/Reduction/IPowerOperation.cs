namespace Veggerby.Units.Reduction;

internal interface IPowerOperation : IOperand
{
    IOperand Base { get; }
    int Exponent { get; }
}