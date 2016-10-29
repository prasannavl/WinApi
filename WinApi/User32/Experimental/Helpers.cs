using System;
using System.Runtime.InteropServices;

namespace WinApi.User32.Experimental
{
    public static class User32ExperimentalHelpers
    {
        public static void EnableBlurBehind(IntPtr hwnd)
        {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf<AccentPolicy>();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            try
            {
                Marshal.StructureToPtr(accent, accentPtr, false);
                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttributeType.WCA_ACCENT_POLICY,
                    DataSize = accentStructSize,
                    Data = accentPtr
                };
                User32ExperimentalMethods.SetWindowCompositionAttribute(hwnd, ref data);
            }
            finally { Marshal.FreeHGlobal(accentPtr); }
        }
    }
}