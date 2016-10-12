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

namespace Sample.Skia
{
    public class SkiaPainter
    {
        public static void ProcessPaint(ref WindowMessage msg, NativePixelBuffer pixelBuffer,
            Action<SKSurface> handler)
        {
            var hwnd = msg.Hwnd;
            PaintStruct ps;
            var hdc = User32Methods.BeginPaint(hwnd, out ps);
            try
            {
                Rectangle clientRect;
                User32Methods.GetClientRect(hwnd, out clientRect);
                var size = clientRect.GetSize();
                pixelBuffer.EnsureSize(ref size);
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
                        Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, pixelBuffer.Handle);
                    }
                }
            }
            finally
            {
                User32Methods.EndPaint(hwnd, ref ps);
            }
        }
    }

    public class SkiaWindowBase : EventedWindowCore
    {
        private readonly NativePixelBuffer m_pixelBuffer = new NativePixelBuffer();

        protected virtual void OnSkiaPaint(SKSurface surface) {}

        protected override void OnPaint(ref WindowMessage msg, IntPtr cHdc)
        {
            SkiaPainter.ProcessPaint(ref msg, m_pixelBuffer, OnSkiaPaint);
        }

        protected override void Dispose(bool disposing)
        {
            m_pixelBuffer.Dispose();
            base.Dispose(disposing);
        }
    }
}