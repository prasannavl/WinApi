using System;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.Utils;

namespace WinApi.DxUtils.Contexts
{
    public class D2DGraphicsContext : IGraphicsContext
    {
        private D2DResourceManager m_dxResourceManager;
        private IntPtr m_hwnd;
        private Size m_size;

        public void Init(IntPtr hwnd, ref Size size, bool deferInitUntilFirstDraw = true)
        {
            this.m_hwnd = hwnd;
            m_size = size;
            if (!deferInitUntilFirstDraw) EnsureDxResources();
        }

        public void Draw()
        {
            EnsureDxResources();
            Draw2D();
            m_dxResourceManager.SwapChain.Present(1, PresentFlags.None);
        }

        private void Draw2D()
        {
            var context = m_dxResourceManager.D2DContext;
            var rand = new Random();
            var w = m_size.Width;
            var h = m_size.Height;

            var b = new SolidColorBrush(context, new RawColor4(0.5f, 0.6f, 0.4f, 0.6f));

            context.BeginDraw();
            context.Clear(new RawColor4(0, 0, 0, 0f));
            context.PushAxisAlignedClip(new RawRectangleF(0, 1, m_size.Width, m_size.Height), AntialiasMode.Aliased);
            context.Clear(new RawColor4(0.3f, 0.4f, 0.5f, 0.3f));

            context.DrawText("Hello there!", new TextFormat(m_dxResourceManager.DWriteFactory, "Segoe UI", 24), new RawRectangleF(0, 0, 200, 200), b);

            for (var i = 0; i < 10; i++)
            {
                b.Color = new RawColor4(rand.NextFloat(), rand.NextFloat(), rand.NextFloat(), 0.4f);
                context.FillEllipse(
                    new Ellipse(new RawVector2(rand.NextFloat(0, w), rand.NextFloat(0, h)), rand.NextFloat(0, w),
                        rand.Next(0, h)), b);
                context.FillRectangle(
                    new RawRectangleF(rand.NextFloat(0, w), rand.NextFloat(0, h), rand.NextFloat(0, w),
                        rand.NextFloat(0, h)), b);
            }
            context.PopAxisAlignedClip();
            context.EndDraw();
            b.Dispose();
        }

        public void Resize(ref Size size)
        {
            m_size = size;
            m_dxResourceManager?.Resize(ref m_size);
        }

        private void EnsureDxResources()
        {
            if (m_dxResourceManager == null)
            {
                PaintDefault();
                m_dxResourceManager = new D2DResourceManager();
                m_dxResourceManager.Initalize(m_hwnd, m_size);
            }
        }

        private void PaintDefault()
        {
            PaintStruct ps;
            var hdc = User32Methods.BeginPaint(m_hwnd, out ps);
            var b = Gdi32Methods.CreateSolidBrush(0);
            User32Methods.FillRect(hdc, ref ps.PaintRect, b);
            Gdi32Methods.DeleteObject(b);
            User32Methods.EndPaint(m_hwnd, ref ps);
        }

        public void Dispose()
        {
            m_dxResourceManager?.Destroy();
            m_dxResourceManager = null;
        }
    }
}
