using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Reduction;

internal static class OperandExtensions
{
    internal static IEnumerable<IOperand> ExpandMultiplication(this IOperand o)
    {
        if (o is IProductOperation)
        {
            return (o as IProductOperation).Operands;
        }

        return [o];
    }

    public static T Multiply<T>(this IEnumerable<T> operands, Func<T, T, T> multiply, T @default) where T : IOperand
    {
        if (operands.Any())
        {
            T result = operands.First();
            foreach (var o in operands.Skip(1))
            {
                result = multiply(result, o);
            }

            return result;
        }

        return @default;
    }
}