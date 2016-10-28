using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils.D3D11
{
    public abstract class D3D11ContainerCore : D3D11Dxgi1_2ContainerCore, IDxgi1ContainerWithSwapChain
    {
        public virtual DeviceContext Context { get; protected set; }
        public virtual RenderTargetView RenderTargetView { get; protected set; }
        public virtual SwapChain SwapChain { get; protected set; }

        public static Size GetValidatedSize(ref Size size)
        {
            var h = size.Height >= 0 ? size.Height : 0;
            var w = size.Width >= 0 ? size.Width : 0;
            return new Size(w, h);
        }

        protected void EnsureSwapChain()
        {
            if (SwapChain == null)
                CreateSwapChain();
        }

        protected abstract void CreateSwapChain();

        protected void EnsureContext()
        {
            if (Context == null)
                CreateContext();
        }

        protected abstract void CreateContext();

        protected void EnsureRenderTargetView()
        {
            if (RenderTargetView == null)
                CreateRenderTargetView();
        }

        protected abstract void CreateRenderTargetView();
    }

    // ReSharper disable once InconsistentNaming
    public abstract class D3D11_1ContainerCore : D3D11ContainerCore, IDxgi1_2ContainerWithSwapChain
    {
        public override Factory DxgiFactory => DxgiFactory2;
        public override SharpDX.DXGI.Device DxgiDevice => DxgiDevice2;

        public override DeviceContext Context => Context1;
        public override SwapChain SwapChain => SwapChain1;

        public virtual DeviceContext1 Context1 { get; protected set; }
        public virtual SwapChain1 SwapChain1 { get; protected set; }
    }

    // ReSharper disable once InconsistentNaming
    public abstract class D3D11_1Container : D3D11_1ContainerCore
    {
        protected override void CreateDxgiDevice()
        {
            EnsureDevice();
            DxgiDevice2 = Device1.QueryInterface<SharpDX.DXGI.Device2>();
        }

        protected override void CreateAdapter()
        {
            EnsureDxgiDevice();
            Adapter = DxgiDevice2.GetParent<Adapter>();
        }

        protected override void CreateDxgiFactory()
        {
            EnsureAdapter();
            DxgiFactory2 = Adapter.GetParent<Factory2>();
        }

        protected override void CreateContext()
        {
            EnsureDevice();
            Context1 = Device1.ImmediateContext1;
        }

        protected override void CreateRenderTargetView()
        {
            EnsureDevice();
            EnsureSwapChain();
            using (var backBuffer = SwapChain1.GetBackBuffer<Texture2D>(0))
            {
                RenderTargetView = new RenderTargetView(Device1, backBuffer);
            }
        }

        protected void ConnectRenderTargetView()
        {
            EnsureContext();
            EnsureRenderTargetView();
            Context1.OutputMerger.SetRenderTargets(RenderTargetView);
        }

        protected void DisconnectRenderTargetView()
        {
            if (Context == null) return;
            Context1.OutputMerger.SetRenderTargets((RenderTargetView) null);
        }
    }
}