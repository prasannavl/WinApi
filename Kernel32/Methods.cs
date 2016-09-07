using System;
using System.Runtime.InteropServices;
using System.Security;

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

        [DllImport(LibraryName)]
        public static extern IntPtr GetCurrentProcess();
    }
}