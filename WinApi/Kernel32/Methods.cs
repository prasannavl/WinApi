using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi.Kernel32
{
    public static class Kernel32Methods
    {
        public const string LibraryName = "kernel32";

        [DllImport(LibraryName)]
        public static extern uint GetLastError();

        [DllImport(LibraryName)]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport(LibraryName)]
        public static extern IntPtr GetModuleHandle(IntPtr modulePtr);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(LibraryName)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport(LibraryName, EntryPoint = "RtlZeroMemory")]
        public static extern void ZeroMemory(IntPtr dest, IntPtr size);

        [DllImport(LibraryName)]
        public static extern int GetTickCount();

        [DllImport(LibraryName)]
        public static extern ulong GetTickCount64();

        [DllImport(LibraryName)]
        public static extern int QueryPerformanceCounter(out long value);

        [DllImport(LibraryName)]
        public static extern int QueryPerformanceFrequency(out long value);

        [DllImport(LibraryName)]
        public static extern int QueryUnbiasedInterruptTime(out ulong unbiasedTime);
    }
}