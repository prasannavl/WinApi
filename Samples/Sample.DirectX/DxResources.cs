using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using Device = SharpDX.Direct3D11.Device;

namespace Sample.Win32
{
    class DxResources
    {
        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public Device Device { get; private set; }
        public DeviceContext D3DContext { get; private set; }
        public RenderTargetView D3DRenderTargetView { get; private set; }

        public Factory DxgiFactory { get; private set; }
        public Adapter Adapter { get; private set; }
        public SwapChain SwapChain { get; private set; }

        public void Initalize(IntPtr hwnd, Size size)
        {
            Hwnd = hwnd;
            Size = size;
            ConnectD3DRenderTargetView();
        }

        private void CreateDxgiFactory()
        {
            var flag = false;
#if DEBUG
            flag = true;
#endif
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            DxgiFactory = new Factory2(flag);
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
            var creationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif
            Device = new Device(Adapter, creationFlags);
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
                Device,
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