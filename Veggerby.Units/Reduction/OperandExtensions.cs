using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Reduction
{
    internal static class OperandExtensions
    {
        internal static IEnumerable<IOperand> ExpandMultiplication(this IOperand o)
        {
            if (o is IProductOperation)
            {
                return (o as IProductOperation).Operands;
            }

            return new[] { o };
        }

        public static T Multiply<T>(this IEnumerable<T> operands, Func<T, T, T> mult, T @default) where T : IOperand
        {
            if (operands.Any())
            {
                T result = operands.First();
                foreach (var o in operands.Skip(1))
                {
                    result = mult(result, o);
                }

                return result;
            }

            return @default;
        }
    }
}