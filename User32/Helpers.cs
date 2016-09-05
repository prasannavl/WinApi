using System;

namespace WinApi.User32
{
    public static class User32Helpers
    {
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex)
        {
            return User32Methods.GetWindowLongPtr(hwnd, (int) nIndex);
        }

        public static IntPtr SetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            return User32Methods.SetWindowLongPtr(hwnd, (int) nIndex, dwNewLong);
        }

        public static IntPtr GetClassLongPtr(IntPtr hwnd, ClassLongFlags nIndex)
        {
            return User32Methods.GetClassLongPtr(hwnd, (int) nIndex);
        }

        public static IntPtr SetClassLongPtr(IntPtr hwnd, ClassLongFlags nIndex, IntPtr dwNewLong)
        {
            return User32Methods.SetClassLongPtr(hwnd, (int) nIndex, dwNewLong);
        }

        public static IntPtr LoadIcon(IntPtr hInstance, SystemIcon icon)
        {
            return User32Methods.LoadIcon(hInstance, new IntPtr((int) icon));
        }

        public static IntPtr LoadCursor(IntPtr hInstance, SystemCursor cursor)
        {
            return User32Methods.LoadCursor(hInstance, new IntPtr((int) cursor));
        }

        public static int DrawText(IntPtr hdc, string lpString, int nCount, ref Rectangle lpRect,
            DrawTextFormatFlags uFormat)
        {
            return User32Methods.DrawText(hdc, lpString, nCount, ref lpRect, (uint) uFormat);
        }

        public static int SetWindowPos(IntPtr hwnd, HwndZOrder order, int x, int y, int cx, int cy,
            SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(hwnd, new IntPtr((int) order), x, y, cx, cy, flags);
        }

        public static int PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin,
            uint wMsgFilterMax, PeekMessageFlags wRemoveMsg)
        {
            return User32Methods.PeekMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, (uint) wRemoveMsg);
        }
    }
}