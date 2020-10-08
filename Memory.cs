using System.Collections.Generic;

namespace SPL
{
    static class Memory
    {
        static readonly List<Variable> DataMemory = new List<Variable>();

        static public List<string[]> CycleStateMemory = new List<string[]>();

        static public void AddVariable(Variable Data) { DataMemory.Add(Data); }

        static public double GetData(string Name)
        {
            foreach (Variable Var in DataMemory)
                if (Var.Name == Name) return Var.Value;
            throw new System.Exception();
        }

        static public void SetData(string Name, double Data)
        {
            foreach (Variable Var in DataMemory)
                if (Var.Name == Name) Var.SetValue(Data);
        }
        
        static public bool IsInMemory(string Name)
        {
            foreach (Variable Var in DataMemory)
                if (Var.Name == Name) return true;
            return false;
        }

        static public void Delete()
        {
            CycleStateMemory = new List<string[]>();
        }
    }
}
