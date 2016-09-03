using System;
using System.Runtime.InteropServices;
using System.Security;

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