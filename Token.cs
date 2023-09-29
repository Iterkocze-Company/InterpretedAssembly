using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public enum TokenType {
        OPERAND,
        STRING,
        NUMBER,
        REGISTER,
        LABEL_ITERAL,
        FUNCTION
    }

    public class Token {
        public TokenType Type { get; set; }
        public string? Name { get; set; } = null;
        public int? Value { get; set; } = null;

        public Token(TokenType type, string? name = null, int? value = null) {
            Type = type; Name = name; Value = value;
        }
    }
}
