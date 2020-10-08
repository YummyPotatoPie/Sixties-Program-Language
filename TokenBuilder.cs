using System.Collections.Generic;

namespace SPL
{
    static class TokenBuilder
    {
        static readonly List<char> Digits = new List<char>()
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        static public string[] ToTokens(string CodeLine)
        {
            if (CodeLine.Length == 0) return new string[] { "NULL" };
            else if (CodeLine[0] == '#') return new string[] { "COMMENT" };
            List<string> Tokens = new List<string>();

            string Temp = "";
            foreach(char Symbol in CodeLine)
            {
                if (Symbol == ' ' && Temp != "")
                {
                    Tokens.Add(Temp);
                    Temp = "";
                }
                else if (Symbol == ' ' && Temp == "")
                    continue;
                else if (Symbol == '\t')
                    continue;
                else
                    Temp += Symbol;
            }
            Tokens.Add(Temp);
            foreach(string Token in Tokens)
            {
                if(Token == "" || Token == " ")
                {
                    Tokens.Remove(Token);
                }
            }
            return Tokens.ToArray();
        }

        static public bool IsNumber(ref string Token)
        {
            int i;
            string Temp = "";
            if (Token[0] == '-')
            {
                i = 1; Temp += '-';
            }
            else i = 0;
            for (; i < Token.Length; i++)
            {
                if (Digits.IndexOf(Token[i]) == -1 && Token[i] == '.' && i != 0)
                    Temp += ',';
                else if (Digits.IndexOf(Token[i]) != -1)
                    Temp += Token[i];
                else
                    return false;
            }
            Token = Temp;
            return true;
        }
    }
}
