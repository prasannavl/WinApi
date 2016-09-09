using System;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.Gdi32
{
    public static class Gdi32Helpers
    {
        public static IntPtr GetStockObject(StockObject fnObject)
        {
            return Gdi32Methods.GetStockObject((int) fnObject);
        }

        public static IntPtr CreateDIBSection(IntPtr hdc, ref BitmapInfo bitmapInfo,
            uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.CreateDIBSection(hdc, pbmi.GetDangerousHandle(), pila, out ppvBits, hSection,
                    dwOffset);
            }
        }

        public static IntPtr CreateDIBitmap(IntPtr hdc, ref BitmapInfoHeader
                lpbmih, uint fdwInit, byte[] lpbInit, ref BitmapInfo bitmapInfo,
            uint fuUsage)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.CreateDIBitmap(hdc, ref lpbmih, fdwInit, lpbInit, pbmi.GetDangerousHandle(),
                    fuUsage);
            }
        }

        public static int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            byte[] lpvBits, ref BitmapInfo bitmapInfo, uint fuColorUse)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.SetDIBitsToDevice(hdc, xDest, yDest,
                    dwWidth, dwHeight, xSrc, ySrc, uStartScan, cScanLines, lpvBits, pbmi.GetDangerousHandle(),
                    fuColorUse);
            }
        }
    }
}