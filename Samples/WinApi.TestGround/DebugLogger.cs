using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WinApi.Desktop;

namespace WinApi.TestGround
{
    public sealed class DebugLogger
    {
        [Conditional("DEBUG")]
        private static void EnsureConsole()
        {
            ConsoleHelpers.EnsureConsole();
        }

        [Conditional("DEBUG")]
        public static void Print(string format, params object[] o)
        {
            EnsureConsole();
            var sb = new StringBuilder();
            var serializedObjs = o.Select(x => JsonConvert.SerializeObject(x, Formatting.Indented)).ToArray();
            sb.AppendFormat(format, serializedObjs);
            Console.WriteLine(sb.ToString());
        }

        [Conditional("DEBUG")]
        public static void Print(string s)
        {
            EnsureConsole();
            Console.WriteLine(s);
        }

        [Conditional("DEBUG")]
        public static void Print(object o)
        {
            EnsureConsole();
            Console.WriteLine(JsonConvert.SerializeObject(o, Formatting.Indented));
        }
    }
}