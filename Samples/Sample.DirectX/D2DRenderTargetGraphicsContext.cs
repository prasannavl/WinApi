using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;

namespace Sample.DirectX
{
    public class D2DRenderTargetGraphicsContext : IGraphicsContext
    {
        private Factory m_2DFactory;
        private IntPtr m_hwnd;
        private WindowRenderTarget m_renderTarget;
        private Size2 m_size;

        public void Init(IntPtr hwnd, ref Size size, bool deferInitUntilFirstDraw = true)
        {
            m_hwnd = hwnd;
            m_size = new Size2(size.Width, size.Height);
            if (!deferInitUntilFirstDraw) CreateResources();
        }

        private void CreateResources()
        {
            if (m_2DFactory == null)
            {
                m_2DFactory = new Factory2(FactoryType.SingleThreaded);
            }
            CreateRenderTargetIfRequired();
        }

        private void CreateRenderTargetIfRequired()
        {
            if (m_renderTarget != null) return;

            m_renderTarget = new WindowRenderTarget(m_2DFactory,
                new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    AlphaMode.Premultiplied)),
                new HwndRenderTargetProperties
                {
                    Hwnd = m_hwnd,
                    PixelSize = m_size,
                    PresentOptions = PresentOptions.None
                });
        }

        public void Resize(ref Size size)
        {
            m_size = new Size2(size.Width, size.Height);
            ResizeRenderTarget();
        }

        private void ResizeRenderTarget()
        {
            if (m_renderTarget != null)
            {
                m_renderTarget.Resize(m_size);
            }
        }

        public void Draw()
        {
            CreateResources();
            var rand = new Random();
            var w = m_size.Width;
            var h = m_size.Height;

            var b = new SolidColorBrush(m_renderTarget, new RawColor4(0, 0, 0, 0));

            m_renderTarget.BeginDraw();
            m_renderTarget.Clear(new RawColor4(0.3f, 0.4f, 0.5f, 0.5f));

            for (var i = 0; i < 10; i++)
            {
                b.Color = new RawColor4(rand.NextFloat(), rand.NextFloat(), rand.NextFloat(), 1);
                m_renderTarget.FillEllipse(
                    new Ellipse(new RawVector2(rand.NextFloat(0, w), rand.NextFloat(0, h)), rand.NextFloat(0, w),
                        rand.Next(0, h)), b);
                m_renderTarget.FillRectangle(
                    new RawRectangleF(rand.NextFloat(0, w), rand.NextFloat(0, h), rand.NextFloat(0, w),
                        rand.NextFloat(0, h)), b);
            }
            m_renderTarget.EndDraw();
            b.Dispose();
        }

        private void Destroy()
        {
            m_renderTarget?.Dispose();
            m_renderTarget = null;
            m_2DFactory?.Dispose();
            m_2DFactory = null;
        }

        public void Dispose()
        {
            this.Destroy();
        }
    }
}