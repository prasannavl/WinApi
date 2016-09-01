using System;

namespace WinApi
{
    public static partial class Helpers
    {
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex)
        {
            return NativeMethods.GetWindowLongPtr(hwnd, (int) nIndex);
        }

        public static IntPtr SetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            return NativeMethods.SetWindowLongPtr(hwnd, (int) nIndex, dwNewLong);
        }

        public static IntPtr LoadIcon(IntPtr hInstance, SystemIcon icon)
        {
            return NativeMethods.LoadIcon(hInstance, new IntPtr((int) icon));
        }

        public static IntPtr LoadCursor(IntPtr hInstance, SystemCursor cursor)
        {
            return NativeMethods.LoadCursor(hInstance, new IntPtr((int) cursor));
        }

        public static IntPtr GetStockObject(StockObject fnObject)
        {
            return NativeMethods.GetStockObject((int) fnObject);
        }

        public static int DrawText(IntPtr hdc, string lpString, int nCount, ref Rectangle lpRect,
            DrawTextFormatFlags uFormat)
        {
            return NativeMethods.DrawText(hdc, lpString, nCount, ref lpRect, (uint) uFormat);
        }

        public static bool SetWindowPos(IntPtr hwnd, HwndZOrder order, int x, int y, int cx, int cy,
            SetWindowPosFlags flags)
        {
            return NativeMethods.SetWindowPos(hwnd, new IntPtr((int) order), x, y, cx, cy, flags);
        }
    }
}