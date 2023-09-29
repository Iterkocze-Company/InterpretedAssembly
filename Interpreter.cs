using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public class Interpreter {
        private static Dictionary<string, List<Token>> _Labels;
        private static Dictionary<string, string> _CodeStrings;

        public static Dictionary<string, Func<object?, object?>> Functions = new() { 
            {"write", new Func<object?, object?>(RuntimeFunctions.Write) } 
        };
        public Interpreter(Dictionary<string, List<Token>> labels, Dictionary<string, string> codeStrings) {
            _Labels = labels;
            _CodeStrings = codeStrings;
        }

        public void InterpretProgram() {
            foreach (var label in _Labels) {
                InterpretLabel(label.Key);
            }
        }

        public void InterpretLabel(string label) {
            var tokens = _Labels[label];

            for (int i = 0; i < tokens.Count; i++) {
                var token = tokens[i];
                if (token.Type == TokenType.OPERAND) {
                    if (token.Name == "mov") {
                        VerifyInstruction(out Token left, out Token right, i, ref tokens, 2);
                        CPU.SetRegister(left.Name, (int)right.Value);
                        i = i + 2;
                    } else if (token.Name == "add") {
                        VerifyInstruction(out Token left, out Token right, i, ref tokens, 2);
                        CPU.SetRegister(left.Name, CPU.GetRegister(left.Name) + CPU.GetRegister(right.Name));
                        i = i + 2;
                    } else if (token.Name == "cmp") {
                        Token left = new(TokenType.OPERAND);
                        Token right = new(TokenType.OPERAND);
                        try  {
                            left = tokens[i + 1];
                            right = tokens[i + 2];
                        }
                        catch {
                            Console.WriteLine("Left or right side invalid");
                            Environment.Exit(1);
                        }

                        int leftValue = CPU.GetRegister(left.Name);
                        int? rightValue = right.Value;
                        if (right.Type == TokenType.REGISTER) {
                            rightValue = CPU.GetRegister(right.Name);
                        }

                        if (leftValue == rightValue)
                            CPU.SetFlag("equal", true);

                        i = i + 2;
                    } else if (token.Name == "jmp") {
                        Token left = new(TokenType.OPERAND);
                        try {
                            left = tokens[i + 1];
                        }
                        catch {
                            Console.WriteLine("Left side invalid");
                            Environment.Exit(1);
                        }

                        InterpretLabel(left.Name);

                        i = i + 1;
                    }
                } else if (token.Type == TokenType.FUNCTION) {
                    VerifyInstruction(out Token left, out Token right, i, ref tokens, 1);
                    Functions[token.Name].Invoke(_CodeStrings[left.Name]);
                }
            }
            
        }
        public void VerifyInstruction(out Token ol, out Token or, int i, ref List<Token> tokens, int req = 2) {
            ol = new(TokenType.FUNCTION);
            or = new(TokenType.FUNCTION);
            if (req == 1) {
                try {
                    ol = tokens[i + 1];
                }
                catch {
                    Console.WriteLine("Left side invalid");
                    Environment.Exit(1);
                }
            } else {
                try {
                    ol = tokens[i + 1];
                    or = tokens[i + 2];
                }
                catch {
                    Console.WriteLine("Left or right side invalid");
                    Environment.Exit(1);
                }
            }
        }
    }
}
