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
        public static void ProcessPaint(ref WindowMessage msg, PixelBufferManager pixelBufferManager,
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
                pixelBufferManager.ResizePixelBuffersIfRequired(ref size);
                using (var surface = SKSurface.Create(
                    size.Width,
                    size.Height,
                    SKColorType.Bgra8888,
                    SKAlphaType.Premul,
                    pixelBufferManager.PixelBufferPtr,
                    pixelBufferManager.PixelBufferStride))
                {
                    if (surface != null)
                    {
                        handler(surface);
                        Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, pixelBufferManager.PixelBufferPtr);
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
        private readonly PixelBufferManager m_pixelBufferManager = new PixelBufferManager();

        protected virtual void OnSkiaPaint(SKSurface surface) {}

        protected override void OnPaint(ref WindowMessage msg, IntPtr cHdc)
        {
            SkiaPainter.ProcessPaint(ref msg, m_pixelBufferManager, OnSkiaPaint);
        }

        protected override void Dispose(bool disposing)
        {
            m_pixelBufferManager.Dispose();
            base.Dispose(disposing);
        }
    }



   
}