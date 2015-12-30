namespace Veggerby.Units.Dimensions
{
    public class BasicDimension : Dimension
    {
        private readonly string _symbol;
        private readonly string _same;

        public BasicDimension(string symbol, string name)
        {
            _symbol = symbol;
            _same = name;
        }

        public override string Symbol
        {
            get { return _symbol; }
        }

        public override string Name
        {
            get { return _same; }
        }
    }
}