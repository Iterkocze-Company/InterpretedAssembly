using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public static class Log {
        public static void Info(string msg) {
            Console.WriteLine($"[LOG] - {msg}");
        }
    }
}
