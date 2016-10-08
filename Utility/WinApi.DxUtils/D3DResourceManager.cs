using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Utils;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils
{
    public class D3DResourceManager : IDisposable
    {
        public static SwapEffect GetDefaultSwapEffectForPlatform(Version version = null)
        {
            if (version == null)
                version = Environment.OSVersion.Version;
            if (version.Major > 6) return SwapEffect.FlipDiscard; // Win 10+
            if (version.Major > 5 && version.Minor > 1) return SwapEffect.FlipSequential; // 6.2+ - Win 8+
            return SwapEffect.Discard;
        }

        private Device m_d3DDevice;
        private DeviceContext m_d3DContext;
        private RenderTargetView m_d3DRenderTargetView;
        private SharpDX.DXGI.Device m_dxgiDevice;
        private Factory m_dxgiFactory;
        private Adapter m_adapter;
        private SwapChain m_swapChain;

        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public Device D3DDevice
        {
            get { return m_d3DDevice; }
            private set { m_d3DDevice = value; }
        }

        public DeviceContext D3DContext
        {
            get { return m_d3DContext; }
            private set { m_d3DContext = value; }
        }

        public RenderTargetView D3DRenderTargetView
        {
            get { return m_d3DRenderTargetView; }
            private set { m_d3DRenderTargetView = value; }
        }

        public SharpDX.DXGI.Device DxgiDevice
        {
            get { return m_dxgiDevice; }
            private set { m_dxgiDevice = value; }
        }

        public Factory DxgiFactory
        {
            get { return m_dxgiFactory; }
            private set { m_dxgiFactory = value; }
        }

        public Adapter Adapter
        {
            get { return m_adapter; }
            private set { m_adapter = value; }
        }

        public SwapChain SwapChain
        {
            get { return m_swapChain; }
            private set { m_swapChain = value; }
        }

        public virtual void Initalize(IntPtr hwnd, Size size)
        {
            Hwnd = hwnd;
            Size = GetValidatedSize(ref size);
            ConnectD3DRenderTargetView();
        }

        public virtual void Resize(ref Size size)
        {
            Size = GetValidatedSize(ref size);
            Disconnect3DRenderTargetView();
            DisposableHelpers.DisposeAndSetNull(ref m_d3DRenderTargetView);
            // Resize retaining other properties.
            SwapChain.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
            ConnectD3DRenderTargetView();
        }

        public static Size GetValidatedSize(ref Size size)
        {
            var h = size.Height >= 0 ? size.Height : 0;
            var w = size.Width >= 0 ? size.Width : 0;
            return new Size(w, h);
        }

        public virtual void Destroy()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_d3DRenderTargetView);
            DisposableHelpers.DisposeAndSetNull(ref m_swapChain);
            DisposableHelpers.DisposeAndSetNull(ref m_d3DContext);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_d3DDevice);
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

        protected void EnsureD3DDevice()
        {
            if (D3DDevice == null)
                CreateD3DDevice();
        }

        private void CreateDxgiDevice()
        {
            EnsureD3DDevice();
            DxgiDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device>();
        }

        protected void EnsureDxgiDevice()
        {
            if (DxgiDevice == null)
                CreateDxgiDevice();
        }

        private void CreateAdapter()
        {
            EnsureDxgiDevice();
            Adapter = DxgiDevice.GetParent<Adapter>();
        }

        protected void EnsureAdapter()
        {
            if (Adapter == null)
                CreateAdapter();
        }

        private void CreateDxgiFactory()
        {
            EnsureAdapter();
            DxgiFactory = Adapter.GetParent<Factory>();
        }

        protected void EnsureDxgiFactory()
        {
            if (DxgiFactory == null)
                CreateDxgiFactory();
        }

        private void CreateSwapChain()
        {
            EnsureD3DDevice();
            EnsureDxgiFactory();
            var m = new ModeDescription(Size.Width, Size.Height, new Rational(60, 1),
                Format.B8G8R8A8_UNorm) { Scaling = DisplayModeScaling.Unspecified };

            var swapChainDesc = new SwapChainDescription
            {
                ModeDescription = m,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                OutputHandle = Hwnd,
                IsWindowed = true,
                SwapEffect = GetDefaultSwapEffectForPlatform(),
            };

            SwapChain = new SwapChain(
                DxgiFactory,
                D3DDevice,
                swapChainDesc);

            DxgiFactory.MakeWindowAssociation(Hwnd, WindowAssociationFlags.IgnoreAltEnter);
        }

        protected void EnsureSwapChain()
        {
            if (SwapChain == null)
                CreateSwapChain();
        }

        private void CreateD3DContext()
        {
            EnsureD3DDevice();
            D3DContext = D3DDevice.ImmediateContext;
        }

        protected void EnsureD3DContext()
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

        protected void EnsureD3DRenderTargetView()
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

        public virtual void Dispose()
        {
            this.Destroy();
        }
    }
}