using System;
using System.Runtime.InteropServices;

namespace WinApi.User32.Experimental
{
    public static class User32ExperimentalMethods
    {
        [DllImport(User32Methods.LibraryName, CharSet = Properties.BuildCharSet)]
        internal static extern bool SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}