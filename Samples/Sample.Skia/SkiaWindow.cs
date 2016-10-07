using System;
using System.Runtime.InteropServices;
using SkiaSharp;
using WinApi.Desktop;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.Windows;
using Size = WinApi.Core.Size;

namespace Sample.Skia
{
    public abstract class WindowWithPixelBuffers : EventedWindowCore
    {
        protected IntPtr PixelBufferPtr { get; private set; }
        protected Size PixelBufferImageSize { get; private set; }
        protected int PixelBufferSize { get; private set; }
        protected int PixelBufferStride { get; private set; }

        protected void ResizePixelBuffersIfRequired(ref Size targetSize)
        {
            var size = targetSize;
            if (targetSize == PixelBufferImageSize) return;

            var stride = 4*((size.Width*32 + 31)/32);
            var pixelBufferRequiredSize = size.Height*stride;
            if (pixelBufferRequiredSize != PixelBufferSize)
            {
                if (PixelBufferPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(PixelBufferPtr);
                PixelBufferPtr = Marshal.AllocHGlobal(pixelBufferRequiredSize);
                PixelBufferSize = pixelBufferRequiredSize;
            }

            PixelBufferStride = stride;
            PixelBufferImageSize = size;
        }

        protected override void Dispose(bool disposing)
        {
            if (PixelBufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(PixelBufferPtr);
                PixelBufferPtr = IntPtr.Zero;
                PixelBufferSize = 0;
                PixelBufferImageSize = new Size();
            }
            base.Dispose(disposing);
        }
    }

    public class SkiaWindowBase : WindowWithPixelBuffers
    {
        protected virtual void OnSkiaPaint(SKSurface surface) {}

        protected override void OnPaint(ref WindowMessage msg, IntPtr cHdc)
        {
            PaintStruct ps;
            var hdc = BeginPaint(out ps);
            try
            {
                var size = GetClientSize();

                ResizePixelBuffersIfRequired(ref size);
                using (var surface = SKSurface.Create(
                    size.Width,
                    size.Height,
                    SKColorType.Bgra8888,
                    SKAlphaType.Premul,
                    PixelBufferPtr,
                    PixelBufferStride))
                {
                    if (surface != null)
                    {
                        OnSkiaPaint(surface);
                        Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, PixelBufferPtr);
                    }
                }
                base.OnPaint(ref msg, hdc);
            }
            finally
            {
                EndPaint(ref ps);
            }
        }
    }
}