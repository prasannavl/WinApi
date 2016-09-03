using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.User32.Experimental
{

    #region Constants

    public enum WindowCompositionAttributeType
    {
        WCA_ACCENT_POLICY = 19
    }

    public enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [Flags]
    public enum AccentFlags
    {
        AF_LEFTBORDER = 0x20,
        AF_TOPBORDER = 0x40,
        AF_RIGHTBORDER = 0x80,
        AF_BOTTOMBORDER = 0x100,
        AF_ALLBORDERS = AF_LEFTBORDER | AF_TOPBORDER | AF_RIGHTBORDER | AF_BOTTOMBORDER
    }

    #endregion

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowCompositionAttributeData
    {
        public WindowCompositionAttributeType Attribute;
        public IntPtr Data;
        public int DataSize;
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