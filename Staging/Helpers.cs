using System;

namespace WinApi
{
    public static partial class Helpers
    {
        public static bool SetWindowPos(IntPtr hwnd, HwndZOrderFlag order, int x, int y, int cx, int cy,
            SetWindowPosFlag uFlags)
        {
            return NativeMethods.SetWindowPos(hwnd, new IntPtr((int) order), x, y, cx, cy, uFlags);
        }
    }
}