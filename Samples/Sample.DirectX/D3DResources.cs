using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using Device = SharpDX.Direct3D11.Device;

namespace Sample.Win32
{
    class D3DResources
    {
        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public Device D3DDevice { get; private set; }
        public DeviceContext D3DContext { get; private set; }
        public RenderTargetView D3DRenderTargetView { get; private set; }

        public SharpDX.DXGI.Device DxgiDevice { get; private set; }
        public Factory DxgiFactory { get; private set; }
        public Adapter Adapter { get; private set; }
        public SwapChain SwapChain { get; private set; }

        public void Initalize(IntPtr hwnd, Size size)
        {
            Hwnd = hwnd;
            Size = size;
            ConnectD3DRenderTargetView();
        }

        private void CreateD3DDevice()
        {
            var creationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif
            try
            {
                D3DDevice = new Device(DriverType.Hardware, creationFlags);
            }
            catch
            {
                D3DDevice = new Device(DriverType.Warp, creationFlags);
            }
        }

        private void EnsureD3DDevice()
        {
            if (D3DDevice == null)
                CreateD3DDevice();
        }

        private void CreateDxgiDevice()
        {
            EnsureD3DDevice();
            DxgiDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device>();
        }

        private void EnsureDxgiDevice()
        {
            if (DxgiDevice == null)
                CreateDxgiDevice();
        }

        private void CreateAdapter()
        {
            EnsureDxgiDevice();
            Adapter = DxgiDevice.GetParent<Adapter>();
        }

        private void EnsureAdapter()
        {
            if (Adapter == null)
                CreateAdapter();
        }

        private void CreateDxgiFactory()
        {
            EnsureAdapter();
            DxgiFactory = Adapter.GetParent<SharpDX.DXGI.Factory>();
        }

        private void EnsureDxgiFactory()
        {
            if (DxgiFactory == null)
                CreateDxgiFactory();
        }

        private void CreateSwapChain()
        {
            EnsureD3DDevice();
            EnsureDxgiFactory();
            var swapChainDesc = new SwapChainDescription
            {
                ModeDescription =
                    new ModeDescription(Size.Width, Size.Height, new Rational(60, 1),
                        Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                OutputHandle = Hwnd,
                IsWindowed = true,
                SwapEffect = SwapEffect.FlipDiscard
            };
            SwapChain = new SwapChain(
                DxgiFactory,
                D3DDevice,
                swapChainDesc);

            DxgiFactory.MakeWindowAssociation(Hwnd, WindowAssociationFlags.IgnoreAltEnter);
        }

        private void EnsureSwapChain()
        {
            if (SwapChain == null)
                CreateSwapChain();
        }

        private void CreateD3DContext()
        {
            EnsureD3DDevice();
            D3DContext = D3DDevice.ImmediateContext;
        }

        private void EnsureD3DContext()
        {
            if (D3DContext == null)
                CreateD3DContext();
        }

        private void CreateD3DRenderTargetView()
        {
            EnsureD3DDevice();
            EnsureSwapChain();
            using (var backBuffer = SwapChain.GetBackBuffer<Texture2D>(0))
            {
                D3DRenderTargetView = new RenderTargetView(D3DDevice, backBuffer);
            }
        }

        private void EnsureD3DRenderTargetView()
        {
            if (D3DRenderTargetView == null)
                CreateD3DRenderTargetView();
        }

        private void ConnectD3DRenderTargetView()
        {
            EnsureD3DContext();
            EnsureD3DRenderTargetView();
            D3DContext.OutputMerger.SetRenderTargets(D3DRenderTargetView);
        }

        private void Disconnect3DRenderTargetView()
        {
            if (D3DContext == null) return;
            if (D3DRenderTargetView == null) return;
            D3DContext.ClearRenderTargetView(D3DRenderTargetView, new RawColor4(0, 0, 0, 1));
            D3DContext.OutputMerger.SetRenderTargets((RenderTargetView) null);
        }

        public void Resize(ref Size size)
        {
            Size = size;
            Disconnect3DRenderTargetView();
            DestroyD3DRenderTargetView();
            SwapChain.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
            CreateD3DRenderTargetView();
            ConnectD3DRenderTargetView();
        }

        public void Destroy()
        {
            Disconnect3DRenderTargetView();
            DestroyD3DRenderTargetView();
            DestroySwapChain();
            DestroyD3DContext();
            DestroyAdapter();
            DestroyDxgiFactory();
            DestroyDxgiDevice();
            DestroyD3DDevice();
        }

        private void DestroyAdapter()
        {
            if (Adapter != null)
            {
                Adapter.Dispose();
                Adapter = null;
            }
        }

        private void DestroyDxgiFactory()
        {
            if (DxgiFactory != null)
            {
                DxgiFactory.Dispose();
                DxgiFactory = null;
            }
        }

        private void DestroyDxgiDevice()
        {
            if (DxgiDevice != null)
            {
                DxgiDevice.Dispose();
                DxgiDevice = null;
            }
        }

        private void DestroyD3DDevice()
        {
            if (D3DDevice != null)
            {
                D3DDevice.Dispose();
                D3DDevice = null;
            }
        }

        private void DestroySwapChain()
        {
            if (SwapChain != null)
            {
                SwapChain.Dispose();
                SwapChain = null;
            }
        }

        private void DestroyD3DContext()
        {
            if (D3DContext != null)
            {
                D3DContext.Dispose();
                D3DContext = null;
            }
        }

        private void DestroyD3DRenderTargetView()
        {
            if (D3DRenderTargetView != null)
            {
                D3DRenderTargetView.Dispose();
                D3DRenderTargetView = null;
            }
        }
    }
}