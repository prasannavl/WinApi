using System;
using SkiaSharp;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.DwmApi;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.Utils;
using WinApi.UxTheme;
using WinApi.Windows;
using NetCoreEx.Geometry;

namespace Sample.Skia
{
    public class SkiaPainter
    {
        public static void ProcessPaint(ref PaintPacket packet, NativePixelBuffer pixelBuffer,
            Action<SKSurface> handler)
        {
            var hwnd = packet.Hwnd;
            Rectangle clientRect;
            User32Methods.GetClientRect(hwnd, out clientRect);
            var size = clientRect.Size;
            pixelBuffer.EnsureSize(size.Width, size.Height);
            PaintStruct ps;
            var hdc = User32Methods.BeginPaint(hwnd, out ps);
            var skPainted = false;
            try
            {
                using (var surface = SKSurface.Create(
                    size.Width,
                    size.Height,
                    SKColorType.Bgra8888,
                    SKAlphaType.Premul,
                    pixelBuffer.Handle,
                    pixelBuffer.Stride))
                {
                    if (surface != null)
                    {
                        handler(surface);
                        skPainted = true;
                    }
                }
            }
            finally
            {
                if (skPainted) Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, pixelBuffer.Handle);
                User32Methods.EndPaint(hwnd, ref ps);
            }
        }
    }

    public class SkiaWindowBase : EventedWindowCore
    {
        private readonly NativePixelBuffer m_pixelBuffer = new NativePixelBuffer();

        protected virtual void OnSkiaPaint(SKSurface surface) {}

        protected override void OnPaint(ref PaintPacket packet)
        {
            SkiaPainter.ProcessPaint(ref packet, this.m_pixelBuffer, this.OnSkiaPaint);
        }

        protected override void Dispose(bool disposing)
        {
            this.m_pixelBuffer.Dispose();
            base.Dispose(disposing);
        }
    }
}