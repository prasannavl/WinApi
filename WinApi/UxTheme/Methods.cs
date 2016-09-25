using System;
using System.Runtime.InteropServices;
using System.Security;
using WinApi.Core;

namespace WinApi.UxTheme
{
    public static class UxThemeMethods
    {
        public const string LibraryName = "uxtheme";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult SetWindowThemeAttribute(
            IntPtr hwnd,
            WindowThemeAttributeType eAttributeType,
            [In] ref WindowThemeNCAttribute pvAttribute,
            int size);
    }
}