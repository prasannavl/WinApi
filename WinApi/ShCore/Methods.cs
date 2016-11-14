using System;
using System.Runtime.InteropServices;
using WinApi.Core;

namespace WinApi.ShCore
{
    public static class ShCoreMethods
    {
        public const string LibraryName = "shcore";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, out uint dpiX,
            out uint dpiY);
    }
}