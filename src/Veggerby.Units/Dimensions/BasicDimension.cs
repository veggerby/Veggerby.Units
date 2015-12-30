namespace Veggerby.Units.Dimensions
{
    public class BasicDimension : Dimension
    {
        private readonly string _Symbol;
        private readonly string _Name;

        public BasicDimension(string symbol, string name)
        {
            this._Symbol = symbol;
            this._Name = name;
        }

        public override string Symbol
        {
            get { return this._Symbol; }
        }

        public override string Name
        {
            get { return this._Name; }
        }
    }
}