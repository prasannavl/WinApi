using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Kernel32
{
    [SuppressUnmanagedCodeSecurity]
    public static class Kernel32Methods
    {
        public const string LibraryName = "kernel32";

        [DllImport(LibraryName)]
        public static extern uint GetLastError();

        [DllImport(LibraryName)]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport(LibraryName)]
        public static extern IntPtr GetModuleHandle(IntPtr modulePtr);

        [DllImport(LibraryName, CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
