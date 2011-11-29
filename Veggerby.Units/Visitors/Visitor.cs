using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Units.Visitors
{
    public abstract class Visitor<T>
    {
        public abstract T Visit(BasicUnit v);
        public abstract T Visit(ProductUnit v);
        public abstract T Visit(DivisionUnit v);
        public abstract T Visit(PowerUnit v);
        public abstract T Visit(DerivedUnit v);
        public abstract T Visit(PrefixedUnit v);
        public abstract T Visit(NullUnit v);
        public abstract T Visit(ScaleUnit v);
    }
}
