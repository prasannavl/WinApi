using System;
using Newtonsoft.Json;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;

namespace Sample.Win32
{
    public class GfxContext
    {
        private Factory m_2dfactory;
        private WindowRenderTarget m_renderTarget;
        private IntPtr m_hwnd;

        public void Init(IntPtr hwnd, ref Size size)
        {
            this.m_hwnd = hwnd;
            m_2dfactory = new Factory2(FactoryType.SingleThreaded);
            CreateRenderTarget(ref size);
        }

        private void CreateRenderTarget(ref Size size)
        {
            m_renderTarget = new WindowRenderTarget(m_2dfactory,
                new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied)),
                new HwndRenderTargetProperties()
                {
                    Hwnd = m_hwnd,
                    PixelSize = new Size2(size.Width, size.Height),
                    PresentOptions = PresentOptions.None
                });
        }

        public void Resize(ref Size size)
        {
            if (m_renderTarget != null)
            {
                m_renderTarget.Dispose();
            }
            CreateRenderTarget(ref size);
        }

        public void BeginDraw()
        {
            m_renderTarget.BeginDraw();
        }

        public void Clear()
        {
            m_renderTarget.Clear(new RawColor4(0.3f, 0.4f, 0.4f, 1));
        }

        public void DrawCircle()
        {
            var ellipse = new Ellipse();
            ellipse.Point = new RawVector2();
            //m_renderTarget.DrawEllipse(new Ellipse(), );
        }

        public void EndDraw()
        {
            m_renderTarget.EndDraw();
        }
    }
}