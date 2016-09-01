using System;
using System.Runtime.InteropServices;
using System.Security;

// ReSharper disable InconsistentNaming

namespace WinApi.Experimental
{
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

    [SuppressUnmanagedCodeSecurity]
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}