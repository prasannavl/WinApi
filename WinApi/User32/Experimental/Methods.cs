using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi.User32.Experimental
{
    public static class User32ExperimentalMethods
    { 
        [DllImport(User32Methods.LibraryName)]
        internal static extern bool SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}