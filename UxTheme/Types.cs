using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace WinApi.UxTheme
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Margin
    {
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
