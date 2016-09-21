using System;
using System.Runtime.InteropServices;
using SkiaSharp;
using WinApi.Core;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.XWin;

namespace Sample.Skia
{
    public class SkiaWindowBase : WindowBase
    {
        private Size m_currentClientSize;
        private IntPtr m_pixelBufferPtr;
        private int m_pixelBufferSize;
        private int m_pixelBufferStride;

        private void ResizePixelBuffersIfRequired()
        {
            var size = GetClientSize();
            var sizeCurrent = m_currentClientSize;
            if (sizeCurrent.Width + sizeCurrent.Height == size.Width + size.Height)
                return;

            var stride = 4*((size.Width*32 + 31)/32);
            var pixelBufferRequiredSize = size.Height*stride;
            if (pixelBufferRequiredSize != m_pixelBufferSize)
            {
                if (m_pixelBufferPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(m_pixelBufferPtr);
                m_pixelBufferPtr = Marshal.AllocHGlobal(pixelBufferRequiredSize);
                m_pixelBufferSize = pixelBufferRequiredSize;
                m_currentClientSize = size;
                m_pixelBufferStride = stride;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_pixelBufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_pixelBufferPtr);
                m_pixelBufferPtr = IntPtr.Zero;
                m_pixelBufferSize = 0;
                m_currentClientSize = new Size();
            }
            base.Dispose(disposing);
        }

        protected virtual void OnSkiaPaint(SKSurface surface) {}

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            PaintStruct ps;
            hdc = User32Methods.BeginPaint(Handle, out ps);
            ResizePixelBuffersIfRequired();
            var size = m_currentClientSize;
            using (var surface = SKSurface.Create(
                size.Width,
                size.Height,
                SKColorType.Bgra8888,
                SKAlphaType.Premul,
                m_pixelBufferPtr,
                m_pixelBufferStride))
            {
                OnSkiaPaint(surface);
            }
            Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, m_pixelBufferPtr);
            User32Methods.EndPaint(Handle, ref ps);
            base.OnPaint(ref msg, hdc);
        }
    }

    public sealed class SkiaWindow : SkiaWindowBase {}

    public class SkiaMainWindowBase : SkiaWindowBase
    {
        protected override void OnDestroy(ref WindowMessage msg)
        {
            base.OnDestroy(ref msg);
            MessageHelpers.PostQuitMessage();
        }
    }

    public sealed class SkiaMainWindow : SkiaMainWindowBase {}
}