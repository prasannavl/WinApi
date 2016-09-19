using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace WinApi.Kernel32
{
    public static class Kernel32Methods
    {
        public const string LibraryName = "kernel32";

        [DllImport(LibraryName)]
        public static extern uint GetLastError();

        #region Memory Methods

        [DllImport(LibraryName, EntryPoint = "RtlZeroMemory")]
        public static extern void ZeroMemory(IntPtr dest, IntPtr size);

        #endregion

        [DllImport(LibraryName)]
        public static extern IntPtr GetCurrentProcess();

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

        #region Handle and Object Functions 

        [DllImport(LibraryName)]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport(LibraryName)]
        public static extern int CompareObjectHandles(IntPtr hFirstObjectHandle, IntPtr hSecondObjectHandle);

        [DllImport(LibraryName)]
        public static extern int DuplicateHandle(
            IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle,
            out IntPtr lpTargetHandle,
            uint dwDesiredAccess,
            int bInheritHandle,
            DuplicateHandleFlags dwOptions);

        [DllImport(LibraryName)]
        public static extern int GetHandleInformation(IntPtr hObject, out HandleInfoFlags lpdwFlags);

        [DllImport(LibraryName)]
        public static extern int SetHandleInformation(IntPtr hObject, HandleInfoFlags dwMask, HandleInfoFlags dwFlags);

        #endregion

        #region DLL Methods

        [DllImport(LibraryName)]
        public static extern IntPtr GetModuleHandle(IntPtr modulePtr);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetModuleHandleEx(GetModuleHandleFlags dwFlags, string lpModuleName, out IntPtr phModule);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadLibraryEx(string fileName, IntPtr hFileReservedAlwaysZero,
            LoadLibraryFlags dwFlags);

        [DllImport(LibraryName)]
        public static extern int FreeLibrary(IntPtr hModule);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int SetDllDirectory(string fileName);

        [DllImport(LibraryName)]
        public static extern int SetDefaultDllDirectories(LibrarySearchFlags directoryFlags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr AddDllDirectory(string newDirectory);

        [DllImport(LibraryName)]
        public static extern int RemoveDllDirectory(IntPtr cookieFromAddDllDirectory);

        #endregion

        #region System Information Functions

        [DllImport(LibraryName)]
        public static extern uint GetSystemDirectory(StringBuilder lpBuffer, uint uSize);

        [DllImport(LibraryName)]
        public static extern uint GetWindowsDirectory(StringBuilder lpBuffer, uint uSize);

        #endregion
    }

    /// <summary>
    ///     These methods are either for internal purposes only, or is exposed only with the helpers
    ///     since it may be very dangerous to use them directly.
    /// </summary>
    internal static class Kernel32InternalMethods
    {
        [DllImport(Kernel32Methods.LibraryName)]
        public static extern int SetProcessUserModeExceptionPolicy(uint dwFlags);

        [DllImport(Kernel32Methods.LibraryName)]
        public static extern int GetProcessUserModeExceptionPolicy(out uint dwFlags);
    }
}