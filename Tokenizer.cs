using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public static class Tokenizer {
        private static readonly char[] DELIMETERS = { ' ', ',', '\n', '\t' };
        private static readonly string[] OPERANDS = { "mov", "add", "cmp", "jmp" };
        //private static readonly string[] FUNCTIONS = { "write" };
        private static string CurrentLabel = "_start:";
        public static Dictionary<string, string> codeStrings = new();
        public static Dictionary<string, List<Token>> TokenizeFile(string path) {
            Log.Info("Tokenization started...");
            RuntimeTimer.StartTimer();

            Dictionary<string, List<Token>> lables = new();
            string source = File.ReadAllText(path);
            string[] strings = source.Split(DELIMETERS);
            Dictionary<string, List<Token>> labels = new();
            labels[CurrentLabel] = new List<Token>();
            

            for (int i = 0; i < strings.Length; i++) {
                string s = strings[i].Trim();
                if (string.IsNullOrEmpty(s)) 
                    continue;

                if (OPERANDS.Contains(s)) {
                    labels[CurrentLabel].Add(new(TokenType.OPERAND, name: s.Trim()));
                } else if (Char.IsDigit(s[0])) {
                    labels[CurrentLabel].Add(new(TokenType.NUMBER, value: Convert.ToInt32(s)));
                } else if (s[0] == 'r') {
                    labels[CurrentLabel].Add(new(TokenType.REGISTER, name: s.Trim()));
                } else if (s[0] == '_' && s.Trim() != "_start:" && s.Trim().EndsWith(":")) {
                    CurrentLabel = s.Trim();
                    labels[CurrentLabel] = new List<Token>();
                } else if (s[0] == '_' && !s.Trim().EndsWith(":")) {
                    labels[CurrentLabel].Add(new(TokenType.LABEL_ITERAL, name: s.Trim() + ":"));
                } else if (s.Trim() == "ds") {
                    if (!strings[i+1].StartsWith("\"") && !String.IsNullOrEmpty(strings[i+1])) {
                        codeStrings[strings[i + 1]] = strings[i+2].Replace("\"", "");
                    } else {
                        Console.WriteLine("Invalid string definition");
                        Environment.Exit(1);
                    }
                } else if (Interpreter.Functions.Keys.Contains(s)) {
                    labels[CurrentLabel].Add(new(TokenType.FUNCTION, name: s));
                } else if (codeStrings.ContainsKey(s)) {
                    labels[CurrentLabel].Add(new(TokenType.STRING, name: s));
                }
            }

            Log.Info($"Tokenization took {RuntimeTimer.GetTimer()} seconds");
            return labels;
        }
    }
}
