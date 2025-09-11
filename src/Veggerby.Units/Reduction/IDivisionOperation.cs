namespace Veggerby.Units.Reduction;

internal interface IDivisionOperation : IOperand
{
    IOperand Dividend { get; }
    IOperand Divisor { get; }
}