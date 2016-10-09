using System;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.DxUtils.D2D1_1;
using WinApi.DxUtils.D3D11;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Helpers;
using FactoryType = SharpDX.DirectWrite.FactoryType;

namespace DxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ApplicationHelpers.SetupDefaultExceptionHandlers();
                var factory = WindowFactory.Create(hBgBrush: IntPtr.Zero);
                using (var win = Window.Create<MainWindow>("Hello", factory: factory))
                {
                    win.Show();
                    new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelpers.ShowError(ex);
            }
        }
    }

    public sealed class MainWindow : Window
    {
        private readonly D2DMetaResource m_d2DMetaResource = D2DMetaFactory.Create();

        private readonly D3DMetaResource m_d3DMetaResource =
            D3DMetaFactory.Create(creationFlags:
                DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded);

        private SharpDX.DirectWrite.Factory m_dWriteFactory;

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            m_d3DMetaResource.Initalize(Handle, GetClientSize());
            m_d2DMetaResource.Initialize(m_d3DMetaResource, true);

            m_d3DMetaResource.DxgiFactory.MakeWindowAssociation(Handle, WindowAssociationFlags.IgnoreAltEnter);
            m_dWriteFactory = new SharpDX.DirectWrite.Factory(FactoryType.Shared);

            return base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            var target = m_d3DMetaResource.RenderTargetView;
            var swapChain = m_d3DMetaResource.SwapChain;

            var size = GetClientSize();

            var context = m_d2DMetaResource.Context;
            var w = size.Width;
            var h = size.Height;
            context.BeginDraw();
            context.Clear(new RawColor4(0, 0, 0, 0f));
            context.PushAxisAlignedClip(new RawRectangleF(0, 1, w, h), AntialiasMode.Aliased);
            context.Clear(new RawColor4(0.3f, 0.4f, 0.5f, 0.3f));
            var b = new SolidColorBrush(context, new RawColor4(0.5f, 0.6f, 0.4f, 0.6f));
            var textFormat = new TextFormat(m_dWriteFactory, "Segoe UI", 24);
            context.DrawText("Hello there!", textFormat, new RawRectangleF(0, 0, 200, 200), b);
            textFormat.Dispose();
            b.Dispose();
            context.PopAxisAlignedClip();
            context.EndDraw();
            //context.ClearRenderTargetView(target, new RawColor4(0.5f, 0.6f, 0.7f, 0.7f));
            swapChain.Present(1, 0);
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            m_d3DMetaResource.Resize(ref size);
        }

        protected override void Dispose(bool disposing)
        {
            m_d2DMetaResource.Dispose();
            m_d3DMetaResource.Dispose();
            base.Dispose(disposing);
        }
    }
}