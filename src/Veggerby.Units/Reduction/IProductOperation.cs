using System.Collections.Generic;

namespace Veggerby.Units.Reduction
{
    internal interface IProductOperation : IOperand
    {   
        IEnumerable<IOperand> Operands { get; }
    }
}