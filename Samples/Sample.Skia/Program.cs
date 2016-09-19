using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using WinApi.Core;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.XWin;

namespace Sample.Skia
{
    class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create("MainWindow");
            using (var win = factory.CreateFrameWindow<AppWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }
    }

    public class AppWindow : MainWindowBase
    {
        private IntPtr m_pixelBufferPtr;
        private int m_pixelBufferSize;
        private Size m_currentClientSize;
        private int m_pixelBufferStride;

        public AppWindow()
        {
            ResizePixelBuffersIfRequired();
        }

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

        protected override void OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            ResizePixelBuffersIfRequired();
            base.OnSize(ref msg, flag, ref size);
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

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            PaintStruct ps;
            hdc = User32Methods.BeginPaint(Handle, out ps);
            var size = m_currentClientSize;
            using (var surface = SKSurface.Create(
                width: size.Width,
                height: size.Height,
                colorType: SKColorType.Bgra8888,
                alphaType: SKAlphaType.Premul,
                pixels: m_pixelBufferPtr,
                rowBytes: m_pixelBufferStride))
            {
                var canvas = surface.Canvas;
                canvas.Clear(new SKColor(23, 56, 42, 80));
            }
            Gdi32Helpers.SetRgbBitsToDevice(hdc, size.Width, size.Height, m_pixelBufferPtr);
            User32Methods.EndPaint(Handle, ref ps);
            base.OnPaint(ref msg, hdc);
        }
    }
}
