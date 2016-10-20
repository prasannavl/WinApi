using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi
{
    internal static class Properties
    {
#if UNICODE
        public const CharSet BuildCharSet = CharSet.Unicode;
#else
        public const CharSet BuildCharSet = CharSet.Ansi;
#endif
        public const string Version = "1.0.1.0";
    }
}
