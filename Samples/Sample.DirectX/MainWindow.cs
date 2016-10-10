using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.Text;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.DwmApi;
using WinApi.DxUtils;
using WinApi.Gdi32;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;
using WinApi.Windows.Helpers;

namespace Sample.DirectX
{
    public sealed class MainWindow : EventedWindowCore
    {
        private readonly Dx11CompatibleManager dxManager = new Dx11CompatibleManager();

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            var size = GetClientSize();
            //m_graphicsContext.Init(Handle, ref size);
            dxManager.Initialize(Handle, ref size, false);
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            //m_graphicsContext.Draw();

            var rand = new Random();

            var size = GetClientSize();
            var w = size.Width;
            var h = size.Height;

            var context = dxManager.D2D.RenderTarget;

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
            dxManager.D3D.SwapChain.Present(1, 0);
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            dxManager.Resize(ref size);
            //m_graphicsContext.Resize(ref size);
        }

        protected override void Dispose(bool disposing)
        {
            dxManager.Dispose();
            //m_graphicsContext.Dispose();
            base.Dispose(disposing);
        }

    }
}