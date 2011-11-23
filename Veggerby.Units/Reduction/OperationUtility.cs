using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Reduction
{
    internal static class OperationUtility
    {
        internal static bool Equals(IOperand o1, IOperand o2)
        {
            if (ReferenceEquals(o1, o2))
            {
                return true;
            }

            if (o1 == null || o2 == null)
            {
                return false;
            }

            return
                Equals(o1 as IProductOperation, o2 as IProductOperation) ||
                Equals(o1 as IDivisionOperation, o2 as IDivisionOperation) ||
                Equals(o1 as IPowerOperation, o2 as IPowerOperation);
        }

        private static bool Equals(IProductOperation o1, IProductOperation o2)
        {
            if (o1 == null || o2 == null)
            {
                return false;
            }

            return o1.Operands
                .OrderBy(x => x.GetHashCode())
                .Zip(o2.Operands.OrderBy(x => x.GetHashCode()), Equals)
                .All(x => x);
        }

        private static bool Equals(IDivisionOperation o1, IDivisionOperation o2)
        {
            if (o1 == null || o2 == null)
            {
                return false;
            }

            return Equals(o1.Dividend, o2.Dividend) && Equals(o1.Divisor, o2.Divisor);
        }

        private static bool Equals(IPowerOperation o1, IPowerOperation o2)
        {
            if (o1 == null || o2 == null)
            {
                return false;
            }

            return Equals(o1.Base, o2.Base) && Equals(o1.Exponent, o2.Exponent);
        }

        /// <summary>
        /// Rearrange operands to ensure that any division is <u>always</u> outermost, i.e. A*(B/C) => (A*B)/C
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="mult">Multiplication function <u>with</u> reduction/rearrange</param>
        /// <param name="div">Division function <u>with</u> reduction/rearrange</param>
        /// <param name="operands">The operands to rearrange</param>
        /// <returns>A rearrange operand <b>if</b> any rearranging has occurred, otherwise default(T)</returns>
        internal static T RearrangeMultiplication<T>(Func<IEnumerable<T>, T> mult, Func<T, T, T> div, params T[] operands)
            where T : IOperand
        {
            // if A*(B/C) ensure division is outermost => A*B/C

            var divisions = operands.OfType<IDivisionOperation>();
            var rest = operands.Where(x => !(x is IDivisionOperation));

            if (divisions.Any())
            {
                var dividends = divisions.Select(x => (T)x.Dividend).Concat(rest);
                var divisors = divisions.Select(x => (T)x.Divisor);
                return div(mult(dividends), mult(divisors));
            }

            return default(T);
        }

        /// <summary>
        /// Rearrange operands to ensure we only have a single division operation (and it is <u>always</u> outermost, i.e. (A/B)/(C/D) => (A*D)/(B*C)
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="mult">Multiplication function <u>with</u> reduction/rearrange</param>
        /// <param name="div">Division function <u>with</u> reduction/rearrange</param>
        /// <param name="dividend">The dividend</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>A rearrange operand <b>if</b> any rearranging has occurred, otherwise default(T)</returns>
        internal static T RearrangeDivision<T>(Func<T, T, T> mult, Func<T, T, T> div, T dividend, T divisor)
            where T : IOperand
        {
            var d1 = dividend as IDivisionOperation;
            var d2 = divisor as IDivisionOperation;
            if (d1 != null && d2 != null) // (A/B) / (C/D) => AD/BC
            {
                return div(mult((T)d1.Dividend, (T)d2.Divisor), mult((T)d1.Divisor, (T)d2.Dividend));
            }

            if (d1 != null) // (A/B) / C => A/BC
            {
                return div((T)d1.Dividend, mult((T)d1.Divisor, divisor));
            }

            if (d2 != null)  // A / (B/C) => AC/B
            {
                return div(mult(dividend, (T)d2.Divisor), (T)d2.Dividend);
            }

            return default(T);
        }

        /// <summary>
        /// Reduce a division operation, i.e. (A*C)/(A*B) => C/B
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="mult">Multiplication function <u>with</u> reduction/rearrange</param>
        /// <param name="div">Division function <u>with</u> reduction/rearrange</param>
        /// <param name="dividend">The dividend</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>A reduced operand <b>if</b> any reduction has occurred, otherwise default(T)</returns>
        internal static T ReduceDivision<T>(Func<T, T, T> mult, Func<T, T, T> div, T dividend, T divisor)
            where T : IOperand
        {
            return default(T);
        }

        /// <summary>
        /// Reduce a division operation, i.e. (A*C)/(A*B) => C/B
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="mult">Multiplication function <u>with</u> reduction/rearrange</param>
        /// <param name="pow">Power function <u>with</u> reduction/rearrange</param>
        /// <param name="operands">The operands to reduce</param>
        /// <returns>A reduced operand <b>if</b> any reduction has occurred, otherwise default(T)</returns>
        internal static T ReduceMultiplication<T>(Func<IEnumerable<T>, T> mult, Func<T, int, T> pow, params T[] operands)
            where T : IOperand
        {
            var powers = operands
                .SelectMany(x => x.ExpandMultiplication())
                .GroupBy(x => x is IPowerOperation ? (x as IPowerOperation).Base : x)
                .Select(g => new { IsReduced = g.Count() > 1, Operand = pow((T)g.Key, g.Sum(x => x is IPowerOperation ? (x as IPowerOperation).Exponent : 1)) })
                .ToList();

            return powers.Any(x => x.IsReduced) ? mult(powers.Select(x => x.Operand)) : default(T);
        }

        /// <summary>
        /// Ensure that multiplication operations are linear (e.g. (A*B)*(C*D*E)*F => A*B*C*D*E*F
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="operands">The operands to linearize</param>
        /// <returns>A linear set of operands, i.e. if multiplications occur, the operands will all be included in list</returns>
        internal static IEnumerable<T> LinearizeMultiplication<T>(params T[] operands) where T : IOperand
        {
            return operands.SelectMany(x => x.ExpandMultiplication().OfType<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="mult">Multiplication function <u>with</u> reduction/rearrange</param>
        /// <param name="div">Division function <u>with</u> reduction/rearrange</param>
        /// <param name="pow">Power function <u>with</u> reduction/rearrange</param>
        /// <param name="base">The power base operand</param>
        /// <param name="exponent">The power exponent</param>
        /// <returns>An operand representing expanded power operation</returns>
        internal static T ExpandPower<T>(Func<IEnumerable<T>, T> mult, Func<T, T, T> div, Func<T, int, T> pow, T @base, int exponent) where T : IOperand
        {
            if (@base is IPowerOperation)
            {
                return pow((T)(@base as IPowerOperation).Base, (@base as IPowerOperation).Exponent * exponent);
            }

            if (@base is IDivisionOperation)
            {
                return div(pow((T)(@base as IDivisionOperation).Dividend, exponent), pow((T)(@base as IDivisionOperation).Divisor, exponent));
            }

            if (@base is IProductOperation)
            {
                return mult((@base as IProductOperation).Operands.Select(x => pow((T)x, exponent)));
            }

            return default(T);
        }
    }
}