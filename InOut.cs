namespace SPL
{
    static class InOut
    {
        static public void Input(string Name)
        {
            if (Memory.IsInMemory(Name))
            {
                System.Console.Write("INPUT: ");
                string Data = System.Console.ReadLine();
                if (TokenBuilder.IsNumber(ref Data))
                        Memory.SetData(Name, System.Convert.ToDouble(Data));
                else
                    Interpreter.ErrorFlag = "ERROR: INVALID INPUT";
            }
            else
            {
                Interpreter.ErrorFlag = "ERROR: UNKNOWN IDENTIFICATOR";
            }
        }

        static public void Output(string Name)
        {
            if (Memory.IsInMemory(Name)) System.Console.WriteLine("OUTPUT: " + Memory.GetData(Name));
            else Interpreter.ErrorFlag = "ERROR: UNKNOWN IDENTIFICATOR";
        }
    }
}
