using System;
using System.Runtime.InteropServices;
using NetCoreEx.Geometry;
using WinApi.Core;
using WinApi.UxTheme;

namespace WinApi.DwmApi
{
    public static class DwmApiMethods
    {
        public const string LibraryName = "dwmapi";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType dwAttribute,
            IntPtr pvAttribute,
            uint attrSize);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType dwAttribute,
            IntPtr pvAttribute,
            uint cbAttribute);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DwmIsCompositionEnabled(out bool pfEnabled);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DwmExtendFrameIntoClientArea(IntPtr hwnd, [In] ref Margins margins);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam,
            out IntPtr lResult);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DwmGetColorizationColor(out int pcrColorization, out bool pfOpaqueBlend);
    }
}