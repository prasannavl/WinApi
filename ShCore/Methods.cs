using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
