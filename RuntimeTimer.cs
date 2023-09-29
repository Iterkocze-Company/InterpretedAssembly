using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_ {
    public static class RuntimeTimer {
        private static Stopwatch Timer = null;
        public static void StartTimer() {
            Timer = new();
            Timer.Start();
        }
         
        public static double GetTimer() {
            double el = Timer.Elapsed.TotalSeconds;
            Timer.Reset();
            Timer.Stop();
            return el;
        }
    }
}
