using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils.Component;
using WinApi.User32;
using WinApi.Utils;
using WinApi.Windows;

namespace Sample.DirectX
{
    public sealed class MainWindow : EventedWindowCore
    {
        private readonly Dx11Component m_dx = new Dx11Component();

        protected override void OnCreate(ref CreateWindowPacket packet)
        {
            this.m_dx.Initialize(this.Handle, this.GetClientSize());
            base.OnCreate(ref packet);
        }

        protected override void OnPaint(ref PaintPacket packet)
        {
            var rand = new Random();

            var size = this.GetClientSize();
            var w = size.Width;
            var h = size.Height;

            this.m_dx.EnsureInitialized();
            try
            {
                var context = this.m_dx.D2D.Context;

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
                this.m_dx.D3D.SwapChain.Present(1, 0);
                this.Validate();
            }
            catch (SharpDXException ex)
            {
                if (!this.m_dx.PerformResetOnException(ex)) throw;
            }
        }

        protected override void OnSize(ref SizePacket packet)
        {
            this.m_dx.Resize(packet.Size);
            base.OnSize(ref packet);
        }

        protected override void Dispose(bool disposing)
        {
            this.m_dx.Dispose();
            base.Dispose(disposing);
        }
    }
}