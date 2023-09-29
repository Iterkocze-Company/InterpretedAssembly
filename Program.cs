using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretedAssembly_
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var labels = Tokenizer.TokenizeFile(args[0]);
            Interpreter interpreter = new(labels, Tokenizer.codeStrings);
            Log.Info("Assembly VM start");
            RuntimeTimer.StartTimer();
            //interpreter.InterpretLabel("_start:");
            interpreter.InterpretProgram();
            Log.Info($"Runtime took {RuntimeTimer.GetTimer()} seconds");
            CPU.ShowCPU();
        }
    }
}
