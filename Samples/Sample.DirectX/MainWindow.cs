using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectComposition;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils;
using WinApi.DxUtils.Component;
using WinApi.User32;
using WinApi.Utils;
using WinApi.Windows;

namespace Sample.DirectX
{
    public sealed class MainWindow : EventedWindowCore
    {
        private readonly Dx11Component m_dx = new Dx11Component();

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            m_dx.Initialize(Handle, GetClientSize());
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            var rand = new Random();

            var size = GetClientSize();
            var w = size.Width;
            var h = size.Height;

            m_dx.EnsureInitialized();
            try
            {
                var context = m_dx.D2D.Context;

                context.BeginDraw();
                context.Clear(new RawColor4(0, 0, 0, 0f));
                var b = new SolidColorBrush(context,
                    new RawColor4(rand.NextFloat(), rand.NextFloat(), rand.NextFloat(), 0.4f));

                context.FillRectangle(new RawRectangleF(200, 200, 500, 700), b);

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
                context.EndDraw();
                m_dx.D3D.SwapChain.Present(1, 0);
                this.Validate();
            }
            catch (SharpDXException ex)
            {
                if (!m_dx.PerformResetOnException(ex))
                    throw;
            }
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            m_dx.Resize(size);
        }

        protected override void Dispose(bool disposing)
        {
            m_dx.Dispose();
            base.Dispose(disposing);
        }
    }
}