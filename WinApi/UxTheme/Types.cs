using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.UxTheme
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Margin
    {
        public Margin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Margin(int x = 0, int y = 0)
        {
            Left = Right = x;
            Top = Bottom = y;
        }

        public Margin(int all = 0)
        {
            Left = Right = Top = Bottom = all;
        }

        public int Left, Right, Top, Bottom;
    }

    public enum WindowThemeAttributeType
    {
        /// <summary>Specifies non-client related attributes. PvAttribute must be a pointer of type WTA_OPTIONS</summary>
        WTA_NONCLIENT = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowThemeAttributeOptions
    {
        public WindowThemeNCAttribute Flags;
        public WindowThemeNCAttribute Mask;
    }

    [Flags]
    public enum WindowThemeNCAttribute
    {
        /// <summary>
        ///     Prevents the window caption from being drawn.
        /// </summary>
        WTNCA_NODRAWCAPTION = 0x00000001,

        /// <summary>
        ///     Prevents the system icon from being drawn.
        /// </summary>
        WTNCA_NODRAWICON = 0x00000002,

        /// <summary>
        ///     Prevents the system icon menu from appearing.
        /// </summary>
        WTNCA_NOSYSMENU = 0x00000004,

        /// <summary>
        ///     Prevents mirroring of the question mark, even in right-to-left (RTL) layout.
        /// </summary>
        WTNCA_NOMIRRORHELP = 0x00000008
    }
}