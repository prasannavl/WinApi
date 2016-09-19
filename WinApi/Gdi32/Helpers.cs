using System;
using System.Runtime.InteropServices.ComTypes;

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
            DibBmiColorUsageFlag iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.CreateDIBSection(hdc, pbmi.GetDangerousHandle(), iUsage, out ppvBits, hSection,
                    dwOffset);
            }
        }

        public static unsafe IntPtr CreateDIBSection(IntPtr hdc, ref BitmapInfoHeader bitmapInfoHeader,
            DibBmiColorUsageFlag iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset)
        {
            fixed (BitmapInfoHeader* bitmapInfoHeaderPtr = &bitmapInfoHeader)
            {
                return Gdi32Methods.CreateDIBSection(hdc, new IntPtr(bitmapInfoHeaderPtr), iUsage, out ppvBits, hSection,
                    dwOffset);
            }
        }

        public static IntPtr CreateDIBitmap(IntPtr hdc, ref BitmapInfoHeader
                lpbmih, uint fdwInit, byte[] lpbInit, ref BitmapInfo bitmapInfo,
            DibBmiColorUsageFlag fuUsage)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.CreateDIBitmap(hdc, ref lpbmih, fdwInit, lpbInit, pbmi.GetDangerousHandle(),
                    fuUsage);
            }
        }

        public static int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            byte[] lpvBits, ref BitmapInfo bitmapInfo, DibBmiColorUsageFlag fuColorUse)
        {
            using (var pbmi = BitmapInfo.CreateNativeHandle(ref bitmapInfo))
            {
                return Gdi32Methods.SetDIBitsToDevice(hdc, xDest, yDest,
                    dwWidth, dwHeight, xSrc, ySrc, uStartScan, cScanLines, lpvBits, pbmi.GetDangerousHandle(),
                    fuColorUse);
            }
        }

        public static unsafe int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            byte[] lpvBits, ref BitmapInfoHeader bitmapInfoHeader, DibBmiColorUsageFlag fuColorUse)
        {
            fixed (BitmapInfoHeader* bitmapInfoHeaderPtr = &bitmapInfoHeader)
            {
                return Gdi32Methods.SetDIBitsToDevice(hdc, xDest, yDest,
                    dwWidth, dwHeight, xSrc, ySrc, uStartScan, cScanLines, lpvBits, new IntPtr(bitmapInfoHeaderPtr),
                    fuColorUse);
            }
        }
    }
}