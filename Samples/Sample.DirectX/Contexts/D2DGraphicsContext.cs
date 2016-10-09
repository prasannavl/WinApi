using System;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.Utils;
using FactoryType = SharpDX.DirectWrite.FactoryType;

namespace WinApi.DxUtils.Contexts
{
    public class D2DGraphicsContext : IGraphicsContext
    {
        private D2DMetaResource m_d2DMetaResource;
        private D3DMetaResource m_d3DMetaResource;
        private SharpDX.DirectWrite.Factory m_dWriteFactory;

        private IntPtr m_hwnd;
        private Size m_size;

        public void Init(IntPtr hwnd, ref Size size, bool deferInitUntilFirstDraw = true)
        {
            m_hwnd = hwnd;
            m_size = size;
            if (!deferInitUntilFirstDraw) EnsureDxResources();
        }

        public void Draw()
        {
            EnsureDxResources();
            Draw2D();
            m_d3DMetaResource.SwapChain.Present(1, PresentFlags.None);
        }

        public void Resize(ref Size size)
        {
            m_size = size;
            m_d3DMetaResource?.Resize(ref m_size);
        }

        public void Dispose()
        {
            m_dWriteFactory?.Dispose();
            m_dWriteFactory = null;
            m_d2DMetaResource?.Destroy();
            m_d2DMetaResource = null;
            m_d3DMetaResource?.Destroy();
            m_d3DMetaResource = null;
        }

        private void Draw2D()
        {
            var context = m_d2DMetaResource.Context;
            var rand = new Random();
            var w = m_size.Width;
            var h = m_size.Height;

            context.BeginDraw();
            context.Clear(new RawColor4(0, 0, 0, 0f));
            context.PushAxisAlignedClip(new RawRectangleF(0, 1, m_size.Width, m_size.Height), AntialiasMode.Aliased);
            context.Clear(new RawColor4(0.3f, 0.4f, 0.5f, 0.3f));

            var b = new SolidColorBrush(context, new RawColor4(0.5f, 0.6f, 0.4f, 0.6f));
            var textFormat = new TextFormat(m_dWriteFactory, "Segoe UI", 24);
            context.DrawText("Hello there!", textFormat, new RawRectangleF(0, 0, 200, 200), b);
            textFormat.Dispose();
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
            b.Dispose();
            context.PopAxisAlignedClip();
            context.EndDraw();
        }

        private void EnsureDxResources()
        {
            if (m_d2DMetaResource != null) return;
            PaintDefault();
            m_d2DMetaResource = DxMetaFactory.Create2D();
            m_d3DMetaResource =
                DxMetaFactory.Create3D(creationFlags:
                    DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded);
            m_d2DMetaResource.Initialize(m_d3DMetaResource, true);
            m_d3DMetaResource.Initalize(m_hwnd, m_size);
            m_dWriteFactory = new SharpDX.DirectWrite.Factory(FactoryType.Shared);
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
    }
}