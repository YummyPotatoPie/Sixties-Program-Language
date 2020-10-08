namespace SPL
{
    class Variable
    {
        public string Name  { get; private set; }
        public double Value { get; private set; }

        public void SetValue(double Value) => this.Value = Value;

        public Variable(string Name, double Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
