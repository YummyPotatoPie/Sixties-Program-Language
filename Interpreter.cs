using System;
using System.IO;

namespace SPL
{
    class Interpreter
    {
        static public string ErrorFlag = "OK: 0";
        static public string ExecuteFlag = "CONTINUE";
        static public bool CycleFlag = false; 

        static public void Main(string[] Args)
        {
            if (Args.Length < 1)
            {
                Console.WriteLine("SPL [FILE_PATH] or SPL -CI (Console Interpreter)");
            }
            else if (Args[0] == "-CI")
            {
                string Line;
                while (ExecuteFlag == "CONTINUE" && ErrorFlag == "OK: 0" && (Line = Console.ReadLine()) != null)
                {
                    if (Line == "EXIT")
                    {
                        Console.WriteLine("PROGRAM INTERRAPTED");
                        break;
                    }
                    Lexer.LexAnalysys(TokenBuilder.ToTokens(Line), CycleFlag);
                }
                Console.WriteLine("EXECUTE_CODE_" + ErrorFlag);
            }
            else
            {
                try
                {
                    using StreamReader sr = new StreamReader(Args[0]);
                    string Line;
                    while (ExecuteFlag == "CONTINUE" && ErrorFlag == "OK: 0" && (Line = sr.ReadLine()) != null)
                    {
                        Lexer.LexAnalysys(TokenBuilder.ToTokens(Line), CycleFlag);
                    }
                    Console.WriteLine("EXECUTE_CODE_" + ErrorFlag);
                }
                catch
                {
                    Console.WriteLine("INVALID FILE_PATH");
                }
            }
        }
    }
}
