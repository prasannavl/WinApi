using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

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
            using (var pbmi = BitmapInfo.NativeAlloc(ref bitmapInfo))
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
            using (var pbmi = BitmapInfo.NativeAlloc(ref bitmapInfo))
            {
                return Gdi32Methods.CreateDIBitmap(hdc, ref lpbmih, fdwInit, lpbInit, pbmi.GetDangerousHandle(),
                    fuUsage);
            }
        }

        public static int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            byte[] lpvBits, ref BitmapInfo bitmapInfo, DibBmiColorUsageFlag fuColorUse)
        {
            using (var pbmi = BitmapInfo.NativeAlloc(ref bitmapInfo))
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

        public static unsafe int SetRgbBitsToDevice(IntPtr hdc, int width, int height, byte[] bits, int xSrc = 0,
            int ySrc = 0, int xDest = 0, int yDest = 0, bool isRgba = true, bool isImageTopDown = true)
        {
            fixed (byte* ptr = &bits[0]) {
                return SetRgbBitsToDevice(hdc, width, height, (IntPtr) ptr, xSrc, ySrc, xDest, yDest, isRgba);
            }
        }

        public static unsafe int SetRgbBitsToDevice(IntPtr hdc, int width, int height, IntPtr pixelBufferPtr,
            int xSrc = 0,
            int ySrc = 0, int xDest = 0, int yDest = 0, bool isRgba = true, bool isImageTopDown = true)
        {
            var bi = new BitmapInfoHeader
            {
                Size = (uint) Marshal.SizeOf<BitmapInfoHeader>(),
                Width = width,
                Height = isImageTopDown ? -height : height,
                CompressionMode = BitmapCompressionMode.BI_RGB,
                BitCount = isRgba ? (ushort) 32 : (ushort) 24,
                Planes = 1
            };
            return Gdi32Methods.SetDIBitsToDevice(hdc, xDest, yDest, (uint) width, (uint) height, xSrc, ySrc, 0,
                (uint) height, pixelBufferPtr, new IntPtr(&bi),
                DibBmiColorUsageFlag.DIB_RGB_COLORS);
        }

        public static IntPtr CreateSolidBrush(uint r, uint g, uint b)
        {
            return Gdi32Methods.CreateSolidBrush(Bgr32(r, g, b));
        }

        public static uint Bgr32(uint r, uint g, uint b)
        {
            return r | (g << 8) | (b << 16);
        }
    }
}