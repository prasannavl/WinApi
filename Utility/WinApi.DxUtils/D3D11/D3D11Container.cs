using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;

namespace WinApi.DxUtils.D3D11
{
    public abstract class D3D11Container: D3D11ContainerCore
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
            // Bail if it was explicitly created without SwapChain
            if (SwapChain == null) return;
            using (var backBuffer = SwapChain.GetBackBuffer<Texture2D>(0))
            {
                RenderTargetView = new RenderTargetView(Device, backBuffer);
            }
        }

        protected void ConnectRenderTargetView()
        {
            EnsureContext();
            EnsureRenderTargetView();
            // Bail if it was explicitly created without RTV
            if (RenderTargetView == null) return;
            Context.OutputMerger.SetRenderTargets(RenderTargetView);
        }

        protected void DisconnectRenderTargetView()
        {
            if (Context == null) return;
            if (RenderTargetView == null) return;
            Context.ClearRenderTargetView(RenderTargetView, new RawColor4(0, 0, 0, 1));
            Context.OutputMerger.SetRenderTargets((RenderTargetView)null);
        }
    }
}