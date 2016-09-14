using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi.ShCore
{
    [SuppressUnmanagedCodeSecurity]
    public static class ShCoreMethods
    {
        public const string LibraryName = "shcore";

        [DllImport(LibraryName)]
        public static extern int GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);
    }
}