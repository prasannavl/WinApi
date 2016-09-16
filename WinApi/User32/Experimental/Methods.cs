using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi.User32.Experimental
{
    public static class User32ExperimentalMethods
    {
        public const string LibraryName = "user32";

        [DllImport(LibraryName)]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}