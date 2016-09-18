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
        private readonly IntPtr m_hwnd;

        public D3DResources(IntPtr hwnd)
        {
            m_hwnd = hwnd;
        }

        public Device Device { get; private set; }
        public DeviceContext D3DContext { get; private set; }
        public RenderTargetView D3DRenderTargetView { get; private set; }

        public Factory DxgiFactory { get; private set; }
        public Adapter Adapter { get; private set; }
        public SwapChain SwapChain { get; private set; }

        private void CreateDxgiFactory()
        {
            DxgiFactory = new Factory2(true);
        }

        private void EnsureDxgiFactory()
        {
            if (DxgiFactory == null)
                CreateDxgiFactory();
        }

        private void CreateAdapter()
        {
            EnsureDxgiFactory();
            Adapter = DxgiFactory.GetAdapter(0);
        }

        private void EnsureAdapter()
        {
            if (Adapter == null)
                CreateAdapter();
        }

        private void CreateDevice()
        {
            EnsureAdapter();
            Device = new Device(Adapter, DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded | DeviceCreationFlags.Debug);
        }

        private void EnsureDevice()
        {
            if (Device == null)
                CreateDevice();
        }

        private void CreateSwapChain()
        {
            EnsureDxgiFactory();
            EnsureDevice();
            var swapChainDesc = new SwapChainDescription
            {
                ModeDescription =
                    new SharpDX.DXGI.ModeDescription(0, 0, new Rational(60, 1),  
                        SharpDX.DXGI.Format.R8G8B8A8_UNorm),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.DXGI.Usage.RenderTargetOutput,
                BufferCount = 2,
                OutputHandle = m_hwnd,
                IsWindowed = true,
                SwapEffect = SharpDX.DXGI.SwapEffect.FlipDiscard,
            };
            SwapChain = new SwapChain(
                DxgiFactory,
                Device,
                swapChainDesc);

            DxgiFactory.MakeWindowAssociation(m_hwnd, WindowAssociationFlags.IgnoreAltEnter);
        }

        private void EnsureSwapChain()
        {
            if (SwapChain == null)
                CreateSwapChain();
        }

        private void CreateD3DContext()
        {
            EnsureDevice();
            D3DContext = Device.ImmediateContext;
        }

        private void EnsureD3DContext()
        {
            if (D3DContext == null)
                CreateD3DContext();
        }

        private void CreateD3DRenderTargetView()
        {
            EnsureDevice();
            EnsureSwapChain();
            using (var backBuffer = SwapChain.GetBackBuffer<Texture2D>(0))
            {
                D3DRenderTargetView = new RenderTargetView(Device, backBuffer);
            }
        }

        private void EnsureD3DRenderTargetView()
        {
            if (D3DRenderTargetView == null)
                CreateD3DRenderTargetView();
        }

        public void Initalize()
        {
            ConnectD3DRenderTargetView();
        }

        private void ConnectD3DRenderTargetView()
        {
            EnsureD3DContext();
            EnsureD3DRenderTargetView();
            D3DContext.OutputMerger.SetRenderTargets(D3DRenderTargetView);
        }

        private void DisconnectRenderTarget()
        {
            if (D3DContext == null) return;
            if (D3DRenderTargetView == null) return;
            D3DContext.ClearRenderTargetView(D3DRenderTargetView, new RawColor4(0, 0, 0, 1));
            D3DContext.OutputMerger.SetRenderTargets((RenderTargetView) null);
        }

        public void Resize(ref Size size)
        {
            DisconnectRenderTarget();
            DestroyD3DRenderTargetView();
            SwapChain.ResizeBuffers(2, size.Width, size.Height, Format.B8G8R8A8_UNorm, SwapChainFlags.None);
            CreateD3DRenderTargetView();
            ConnectD3DRenderTargetView();
        }

        public void Destroy()
        {
            DisconnectRenderTarget();
            DestroyD3DRenderTargetView();
            DestroySwapChain();
            DestroyD3DContext();
            DestroyDevice();
            DestroyAdapter();
            DestroyFactory();
        }

        private void DestroyAdapter()
        {
            if (Adapter != null)
            {
                Adapter.Dispose();
                Adapter = null;
            }
        }

        private void DestroyFactory()
        {
            if (DxgiFactory != null)
            {
                DxgiFactory.Dispose();
                DxgiFactory = null;
            }
        }

        private void DestroyDevice()
        {
            if (Device != null)
            {
                Device.Dispose();
                Device = null;
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