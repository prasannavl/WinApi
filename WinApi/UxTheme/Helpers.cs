using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinApi.Core;

namespace WinApi.UxTheme
{
    public static class UxThemeHelpers
    {
        public static unsafe HResult SetWindowThemeNonClientAttributes(IntPtr hwnd,
            WindowThemeNcAttributeFlags mask,
            WindowThemeNcAttributeFlags attributes)
        {
            var opts = new WindowThemeAttributeOptions
            {
                Mask = (uint) mask,
                Flags = (uint) attributes
            };
            return UxThemeMethods.SetWindowThemeAttribute(hwnd, WindowThemeAttributeType.WTA_NONCLIENT,
                new IntPtr(&opts), (uint) Marshal.SizeOf<WindowThemeAttributeOptions>());
        }
    }
}