using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.UxTheme
{
    [SuppressUnmanagedCodeSecurity]
    public static class UxThemeMethods
    {
        public const string LibraryName = "uxtheme";

        [DllImport(LibraryName)]
        public static extern int SetWindowThemeAttribute(
            IntPtr hwnd,
            WindowThemeAttributeType eAttributeType,
            [In] ref WindowThemeNCAttribute pvAttribute,
            int size);
    }
}
