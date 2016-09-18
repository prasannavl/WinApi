using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Gdi32;
using WinApi.User32;

namespace Sample.Win32
{
    class D3DGraphicsContext : IGraphicsContext
    {
        private DxResources m_dxResources;
        private IntPtr m_hwnd;
        private Size m_size;

        public void Init(IntPtr hwnd, ref Size size, bool deferInitUntilFirstDraw = true)
        {
            this.m_hwnd = hwnd;
            this.m_size = size;
            if (!deferInitUntilFirstDraw) EnsureDxResources();
        }

        public void Draw()
        {
            EnsureDxResources();
            var target = m_dxResources.D3DRenderTargetView;
            var context = m_dxResources.D3DContext;
            var swapChain = m_dxResources.SwapChain;

            context.ClearRenderTargetView(target, new RawColor4(0.5f, 0.6f, 0.7f, 0.7f));
            swapChain.Present(1, 0);
        }

        public void Resize(ref Size size)
        {
            m_dxResources?.Resize(ref size);
        }

        private void EnsureDxResources()
        {
            if (m_dxResources == null)
            {
                PaintDefault();
                m_dxResources = new DxResources();
                m_dxResources.Initalize(m_hwnd, m_size);
            }
        }

        private void PaintDefault()
        {
            PaintStruct ps;
            var hdc = User32Methods.BeginPaint(m_hwnd, out ps);
            var b = Gdi32Methods.CreateSolidBrush(0);
            User32Methods.FillRect(hdc, ref ps.PaintRectangle, b);
            Gdi32Methods.DeleteObject(b);
            User32Methods.EndPaint(m_hwnd, ref ps);
        }
    }
}
