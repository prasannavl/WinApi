using System;
using System.Runtime.InteropServices;
using System.Security;

// ReSharper disable InconsistentNaming
namespace WinApi.Experimental
{
    [SuppressUnmanagedCodeSecurity]
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}
