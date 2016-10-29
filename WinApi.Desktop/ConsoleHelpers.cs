using System;
using System.IO;
using WinApi.Kernel32;

namespace WinApi.Desktop
{
    public static class ConsoleHelpers
    {
        public static bool EnsureConsole()
        {
            if (Kernel32Methods.GetConsoleWindow() != IntPtr.Zero) return true;
            if (Kernel32Methods.AllocConsole()) return false;
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true});
            Console.SetError(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true});
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            return true;
        }
    }
}