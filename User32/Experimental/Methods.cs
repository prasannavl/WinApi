using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi.User32.Experimental
{
    [SuppressUnmanagedCodeSecurity]
    public static class User32ExperimentalMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}