using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NetCoreEx.Geometry;
using WinApi.Core;

// ReSharper disable InconsistentNaming

namespace WinApi.DwmApi
{
    public static class DwmApiHelpers
    {
        public static unsafe HResult DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType dwAttribute,
            [In] ref int attrValue)
        {
            fixed (int* ptr = &attrValue) return DwmApiMethods.DwmSetWindowAttribute(hwnd, dwAttribute, new IntPtr(ptr), sizeof(int));
        }

        public static unsafe HResult DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType dwAttribute,
            out Rectangle rect)
        {
            fixed (Rectangle* ptr = &rect) return DwmApiMethods.DwmGetWindowAttribute(hwnd, dwAttribute, new IntPtr(ptr), (uint) sizeof(Rectangle));
        }
    }
}