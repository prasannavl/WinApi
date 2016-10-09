using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Utils;
using Factory = SharpDX.Direct2D1.Factory;
using FactoryType = SharpDX.Direct2D1.FactoryType;

namespace Sample.DirectX.Contexts
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
            m_size = GetValidatedSize2(ref size);
            if (!deferInitUntilFirstDraw) CreateResources();
        }

        public void Resize(ref Size size)
        {
            m_size = GetValidatedSize2(ref size);
            ResizeRenderTarget();
        }

        public void Draw()
        {
            CreateResources();
            var rand = new Random();
            var w = m_size.Width;
            var h = m_size.Height;

            var b = new SolidColorBrush(m_renderTarget, new RawColor4(0, 0, 0, 0));

            m_renderTarget.BeginDraw();
            m_renderTarget.Clear(new RawColor4(0, 0, 0, 0f));
            //m_renderTarget.PushAxisAlignedClip(new RawRectangleF(0, 1, m_size.Width, m_size.Height), AntialiasMode.Aliased);
            m_renderTarget.Clear(new RawColor4(0f, 0f, 0f, 1f));
            b.Color = new RawColor4(rand.NextFloat(), rand.NextFloat(), rand.NextFloat(), 0.4f);

            m_renderTarget.FillRectangle(new RawRectangleF(200, 200, 500, 700), b);

            for (var i = 0; i < 10; i++)
            {
                b.Color = new RawColor4(rand.NextFloat(), rand.NextFloat(), rand.NextFloat(), 0.4f);
                m_renderTarget.FillEllipse(
                    new Ellipse(new RawVector2(rand.NextFloat(0, w), rand.NextFloat(0, h)), rand.NextFloat(0, w),
                        rand.Next(0, h)), b);
                m_renderTarget.FillRectangle(
                    new RawRectangleF(rand.NextFloat(0, w), rand.NextFloat(0, h), rand.NextFloat(0, w),
                        rand.NextFloat(0, h)), b);
            }
            //m_renderTarget.PopAxisAlignedClip();
            m_renderTarget.EndDraw();
            b.Dispose();
        }

        public void Dispose()
        {
            Destroy();
        }

        public static Size2 GetValidatedSize2(ref Size size)
        {
            var h = size.Height >= 0 ? size.Height : 0;
            var w = size.Width >= 0 ? size.Width : 0;
            return new Size2(w, h);
        }

        private void CreateResources()
        {
            if (m_2DFactory == null)
            {
                m_2DFactory = new Factory(FactoryType.SingleThreaded);
            }
            CreateRenderTargetIfRequired();
        }

        private void CreateRenderTargetIfRequired()
        {
            if (m_renderTarget != null) return;

            m_renderTarget = new WindowRenderTarget(m_2DFactory,
                new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                    AlphaMode.Premultiplied)),
                new HwndRenderTargetProperties
                {
                    Hwnd = m_hwnd,
                    PixelSize = m_size,
                    PresentOptions = PresentOptions.None
                });
        }

        private void ResizeRenderTarget()
        {
            m_renderTarget?.Resize(m_size);
        }

        private void Destroy()
        {
            m_renderTarget?.Dispose();
            m_renderTarget = null;
            m_2DFactory?.Dispose();
            m_2DFactory = null;
        }
    }
}