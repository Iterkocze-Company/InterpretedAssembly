using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public static class RuntimeFunctions {
        public static object? Write(object? msg) {
            Console.WriteLine(msg);
            return null;
        }
    }
}
