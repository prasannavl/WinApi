using NetCoreEx.Geometry;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinApi.Kernel32
{
    public static class Kernel32Methods
    {
        public const string LibraryName = "kernel32";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetLastError();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void SetLastError(uint dwErrCode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DisableThreadLibraryCalls(IntPtr hModule);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void Sleep(uint dwMilliseconds);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetTickCount();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern ulong GetTickCount64();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool QueryPerformanceCounter(out long value);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool QueryPerformanceFrequency(out long value);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void QueryUnbiasedInterruptTime(out ulong unbiasedTime);

        [DllImport(LibraryName)]
        public static extern void GetLocalTime(out SystemTime lpSystemTime);

        [DllImport(LibraryName)]
        public static extern bool SetLocalTime(ref SystemTime lpSystemTime);

        [DllImport(LibraryName)]
        public static extern void GetSystemTime(out SystemTime lpSystemTime);

        [DllImport(LibraryName)]
        public static extern bool SetSystemTime(ref SystemTime lpSystemTime);

        #region Console Functions

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AllocConsole();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FreeConsole();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AttachConsole(uint dwProcessId);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetStdHandle(uint nStdHandle);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetStdHandle(uint nStdHandle, IntPtr hHandle);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SetConsoleTitle(string lpConsoleTitle);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint GetConsoleTitle(StringBuilder lpConsoleTitle, uint nSize);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, int bAbsolute,
            [In] ref RectangleS lpConsoleWindow);

        #endregion

        #region Memory Methods

        [DllImport(LibraryName, ExactSpelling = true, EntryPoint = "RtlZeroMemory")]
        public static extern void ZeroMemory(IntPtr dest, IntPtr size);

        [DllImport(LibraryName, ExactSpelling = true, EntryPoint = "RtlSecureZeroMemory")]
        public static extern void SecureZeroMemory(IntPtr dest, IntPtr size);

        #endregion

        #region Handle and Object Functions 

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DuplicateHandle(
            IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle,
            out IntPtr lpTargetHandle,
            uint dwDesiredAccess,
            int bInheritHandle,
            DuplicateHandleFlags dwOptions);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetHandleInformation(IntPtr hObject, out HandleInfoFlags lpdwFlags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetHandleInformation(IntPtr hObject, HandleInfoFlags dwMask, HandleInfoFlags dwFlags);

        #endregion

        #region DLL Methods

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr GetModuleHandle(IntPtr modulePtr);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool GetModuleHandleEx(GetModuleHandleFlags dwFlags, string lpModuleName,
            out IntPtr phModule);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadLibraryEx(string fileName, IntPtr hFileReservedAlwaysZero,
            LoadLibraryFlags dwFlags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport(LibraryName, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SetDllDirectory(string fileName);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetDefaultDllDirectories(LibrarySearchFlags directoryFlags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr AddDllDirectory(string newDirectory);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool RemoveDllDirectory(IntPtr cookieFromAddDllDirectory);

        #endregion

        #region System Information Functions

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint GetSystemDirectory(StringBuilder lpBuffer, uint uSize);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint GetWindowsDirectory(StringBuilder lpBuffer, uint uSize);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetVersion();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsWow64Process(IntPtr hProcess, out int isWow64Process);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void GetNativeSystemInfo(out SystemInfo lpSystemInfo);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void GetSystemInfo(out SystemInfo lpSystemInfo);

        #endregion

        #region Process and Thread Functions

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetCurrentProcessId();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetCurrentThreadId();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetCurrentThread();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetCurrentProcessorNumber();

        #endregion

        #region File Management Functions

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern FileAttributes GetFileAttributes(string lpFileName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SetFileAttributes(string lpFileName, FileAttributes dwFileAttributes);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool GetFileAttributesEx(string lpFileName, FileAttributeInfoLevel fInfoLevelId,
            out FileAttributeData lpFileInformation);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr CreateFile(string lpFileName,
            uint dwDesiredAccess,
            FileShareMode dwShareMode,
            IntPtr lpSecurityAttributes,
            FileCreationDisposition dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr CreateFile(string lpFileName,
            uint dwDesiredAccess,
            FileShareMode dwShareMode,
            ref SecurityAttributes lpSecurityAttributes,
            FileCreationDisposition dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        #endregion
    }
}