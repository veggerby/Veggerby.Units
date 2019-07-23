namespace Veggerby.Units.Dimensions
{
    public class BasicDimension : Dimension
    {
        public BasicDimension(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public override string Symbol { get; }
        public override string Name { get; }
    }
}