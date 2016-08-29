using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Experimental
{
    public static class Helpers
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
                    Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                    DataSize = accentStructSize,
                    Data = accentPtr
                };
                NativeMethods.SetWindowCompositionAttribute(hwnd, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }
    }
}
