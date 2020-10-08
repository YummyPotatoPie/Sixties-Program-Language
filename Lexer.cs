using System.Collections.Generic;

namespace SPL
{
    static class Lexer
    {
        static public void LexAnalysys(string[] Tokens, bool IsCycle)
        {
            switch(Tokens[0])
            {
                case "NULL":    return;
                case "COMMENT": return;
                case "WHILE":
                    Interpreter.CycleFlag = true;
                    Memory.CycleStateMemory.Add(Tokens);
                    break;
                case "ENDWHILE": 
                    Interpreter.CycleFlag = false; 
                    WhileStateAnalysys(Memory.CycleStateMemory);
                    Memory.Delete();
                    break;
                case "VAR":
                    if (IsCycle)
                        Memory.CycleStateMemory.Add(Tokens);
                    else
                        VariableBuilder(Tokens);    
                    break;
                case "INPUT":
                    if (IsCycle)
                        Memory.CycleStateMemory.Add(Tokens);
                    else
                        InputAnalysys(Tokens);
                    break;
                case "OUTPUT":
                    if (IsCycle)
                        Memory.CycleStateMemory.Add(Tokens);
                    else
                        OutputAnalysys(Tokens);
                    break;
                case "IF":
                    if (IsCycle)
                        Memory.CycleStateMemory.Add(Tokens);
                    else
                        IfStateAnalysys(Tokens);
                    break;
                default: 
                    if (Memory.IsInMemory(Tokens[0]) && Tokens.Length == 3)
                        if (IsCycle)
                            Memory.CycleStateMemory.Add(Tokens);
                        else
                            IdNewValue(Tokens);
                    else
                        Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX"; break;
            }
        }

        static private void IdNewValue(string[] Tokens)
        {
            if (TokenBuilder.IsNumber(ref Tokens[2]))
                Operation(System.Convert.ToDouble(Tokens[2]), Tokens[1], Tokens[0]);
            else if (Memory.IsInMemory(Tokens[2]))
                Operation(Memory.GetData(Tokens[2]), Tokens[1], Tokens[0]);
            else
                Interpreter.ErrorFlag = "ERROR: UNKNOWN IDENTIFICATOR";
        }

        static private void Operation(double Value, string Operation, string IdName)
        {
            switch (Operation)
            {
                case "=": Memory.SetData(IdName, Value); break;
                case "+=": Memory.SetData(IdName, Memory.GetData(IdName) + Value); break;
                case "-=": Memory.SetData(IdName, Memory.GetData(IdName) - Value); break;
                case "*=": Memory.SetData(IdName, Memory.GetData(IdName) * Value); break;
                case "%=": Memory.SetData(IdName, Memory.GetData(IdName) % Value); break;
                case "/=":
                    if (Value != 0)
                        Memory.SetData(IdName, Memory.GetData(IdName) / Value);
                    else
                        Interpreter.ErrorFlag = "ERROR: DIVISION BY ZERO";
                    break;
                default:
                    Interpreter.ErrorFlag = "ERROR: UNKNOWN OPERATOR";
                    break;
            }
        }

        static private void WhileStateAnalysys(List<string[]> CycleState)
        {
            if (CycleState.Count == 1)
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
            else if (Memory.IsInMemory(CycleState[0][1]))
            {
                while (Memory.GetData(CycleState[0][1]) > 0)
                {
                    for (int i = 1; i < CycleState.Count; i++)
                    {
                        LexAnalysys(CycleState[i], false);
                    }
                }
            }
            else
                Interpreter.ErrorFlag = "ERROR: UNKNOWN IDENTIFICATOR";
        }

        static private void IfStateAnalysys(string[] Tokens)
        {
            if (Tokens.Length < 4)
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
            else if (Memory.IsInMemory(Tokens[1]) && Tokens[2] == "THEN")
            {
                if (Memory.GetData(Tokens[1]) > 0)
                {
                    if (Tokens[3] == "BREAK")
                    {
                        System.Console.WriteLine("PROGRAM HAD BEEN INTERRAPTED");
                        Interpreter.ExecuteFlag = "BREAK";
                    }
                    else if (Tokens[3] == "CONTINUE")
                        return;
                    else
                        Interpreter.ErrorFlag = "ERROR: INVALID COMMAND";
                }
                if (Memory.GetData(Tokens[1]) <= 0)
                {
                    if (Tokens[3] == "BREAK")
                        return;
                    else if (Tokens[3] == "CONTINUE")
                    {
                        System.Console.WriteLine("PROGRAM HAD BEEN INTERRAPTED");
                        Interpreter.ExecuteFlag = "BREAK";
                    }
                    else
                        Interpreter.ErrorFlag = "ERROR: INVALID COMMAND";
                }
                else return;
            }
            else
            {
                if (Memory.IsInMemory(Tokens[1]))
                    Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
                else
                    Interpreter.ErrorFlag = "ERROR: UNKNOWN IDENTIFICATOR";
            }
        }

        static private void InputAnalysys(string[] Tokens)
        {
            if (Tokens.Length == 2)
                InOut.Input(Tokens[1]);
            else
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
        }

        static private void OutputAnalysys(string[] Tokens)
        {
            if (Tokens.Length == 2)
                InOut.Output(Tokens[1]);
            else
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
        }

        static private void VariableBuilder(string[] Tokens)
        {
            if (Tokens.Length < 4)
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
            else if (Tokens.Length == 4 && Tokens[0] == "VAR" && Tokens[2] == "=" && TokenBuilder.IsNumber(ref Tokens[3]))
                Memory.AddVariable(new Variable(Tokens[1], System.Convert.ToDouble(Tokens[3])));
            else if (Tokens[2] == "=") 
                Interpreter.ErrorFlag = "ERROR: INVALID SYNTAX";
            else 
                Interpreter.ErrorFlag = "ERROR: INVALID VALUE";
        }
    }
}
