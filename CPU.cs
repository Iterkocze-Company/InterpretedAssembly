using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public static class CPU {
        private static Dictionary<string, int> _Registers = new() { 
            { "r0", 0 },
            { "r1", 0 },
            { "r2", 0 },
            { "r3", 0 },
            { "r4", 0 },
            { "r5", 0 },
            { "r6", 0 },
            { "r7", 0 },
            { "r8", 0 },
            { "r9", 0 },
        };
        private static Dictionary<string, bool> _Flags = new() {
            { "equal", false },
        };

        public static void ShowCPU() {
            Console.WriteLine("----- CPU -----");
            Console.WriteLine("----- REGISTERS -----");
            foreach (var reg in _Registers) {
                Console.WriteLine($"\t{reg.Key} = {reg.Value}");
            }
            Console.WriteLine("----- FLAGS -----");
            foreach (var flag in _Flags) {
                Console.WriteLine($"\t{flag.Key}? {flag.Value}");
            }
            Console.WriteLine("---------------");
        }

        public static void SetRegister(string regName, int value) {
            _Registers[regName] = value;
        }

        public static int GetRegister(string regName)  {
            return _Registers[regName];
        }

        public static void SetFlag(string flag, bool b) {
            _Flags[flag] = b;
        }

        public static bool GetFlag(string flag) {
            return _Flags[flag];
        }
    }
}
