using System;
using System.Runtime.InteropServices;
using System.Security;
using WinApi.UxTheme;

namespace WinApi.DwmApi
{
    [SuppressUnmanagedCodeSecurity]
    public static class DwmApiMethods
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType attr, [In] ref int attrValue,
            int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern void DwmIsCompositionEnabled(out bool pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, [In] ref Margin margin);

        [DllImport("dwmapi.dll")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam,
            out IntPtr lResult);
    }
}