using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace WinApi.Experimental
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int DataSize;
    }

    public enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    public enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4,
    }

    [Flags]
    public enum AccentFlags
    {
        DrawLeftBorder = 0x20,
        DrawTopBorder = 0x40,
        DrawRightBorder = 0x80,
        DrawBottomBorder = 0x100,
        DrawAllBorders = (DrawLeftBorder | DrawTopBorder | DrawRightBorder | DrawBottomBorder)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AccentPolicy
    {
        public AccentState AccentState;
        public AccentFlags AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }
}
