using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils.D3D11
{
    public abstract class D3D11ContainerCore : DxgiContainerBase, IDxgi1Container
    {
        public virtual Device Device { get; protected set; }
        public virtual Adapter Adapter { get; protected set; }
        public virtual DeviceContext Context { get; protected set; }
        public virtual RenderTargetView RenderTargetView { get; protected set; }
        public virtual SharpDX.DXGI.Device DxgiDevice { get; protected set; }
        public virtual Factory DxgiFactory { get; protected set; }
        public virtual SwapChain SwapChain { get; protected set; }

        public static Size GetValidatedSize(ref Size size)
        {
            var h = size.Height >= 0 ? size.Height : 0;
            var w = size.Width >= 0 ? size.Width : 0;
            return new Size(w, h);
        }

        protected void EnsureDevice()
        {
            if (Device == null)
                CreateDevice();
        }

        protected abstract void CreateDevice();

        protected void EnsureDxgiDevice()
        {
            if (DxgiDevice == null)
                CreateDxgiDevice();
        }

        protected abstract void CreateDxgiDevice();

        protected void EnsureAdapter()
        {
            if (Adapter == null)
                CreateAdapter();
        }

        protected abstract void CreateAdapter();

        protected void EnsureDxgiFactory()
        {
            if (DxgiFactory == null)
                CreateDxgiFactory();
        }

        protected abstract void CreateDxgiFactory();

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

    public abstract class D3D11Container : D3D11ContainerCore
    {
        protected override void CreateDxgiDevice()
        {
            EnsureDevice();
            DxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device>();
        }

        protected override void CreateAdapter()
        {
            EnsureDxgiDevice();
            Adapter = DxgiDevice.GetParent<Adapter>();
        }

        protected override void CreateDxgiFactory()
        {
            EnsureAdapter();
            DxgiFactory = Adapter.GetParent<Factory>();
        }

        protected override void CreateContext()
        {
            EnsureDevice();
            Context = Device.ImmediateContext;
        }

        protected override void CreateRenderTargetView()
        {
            EnsureDevice();
            EnsureSwapChain();
            using (var backBuffer = SwapChain.GetBackBuffer<Texture2D>(0))
            {
                RenderTargetView = new RenderTargetView(Device, backBuffer);
            }
        }

        protected void ConnectRenderTargetView()
        {
            EnsureContext();
            EnsureRenderTargetView();
            Context.OutputMerger.SetRenderTargets(RenderTargetView);
        }

        protected void DisconnectRenderTargetView()
        {
            if (Context == null) return;
            if (RenderTargetView == null) return;
            Context.ClearRenderTargetView(RenderTargetView, new RawColor4(0, 0, 0, 1));
            Context.OutputMerger.SetRenderTargets((RenderTargetView) null);
        }
    }
}