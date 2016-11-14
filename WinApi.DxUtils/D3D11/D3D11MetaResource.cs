using System;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.DXGI.Device;
using Device1 = SharpDX.Direct3D11.Device1;
using NetCoreEx.Geometry;

namespace WinApi.DxUtils.D3D11
{
    public class D3D11MetaResource : D3D11_1Container, ID3D11_1MetaResource
    {
        private readonly D3D11MetaResourceOptions m_creationOpts;
        private Adapter m_adapter;
        private DeviceContext1 m_context;
        private Device1 m_device;
        private SharpDX.DXGI.Device2 m_dxgiDevice;
        private Factory2 m_dxgiFactory;
        private bool m_isDisposed;
        private RenderTargetView m_renderTargetView;
        private SwapChain1 m_swapChain;

        public D3D11MetaResource(D3D11MetaResourceOptions creationOpts)
        {
            this.m_creationOpts = creationOpts;
        }

        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public override Device1 Device1 { get { return this.m_device; } protected set { this.m_device = value; } }

        public override DeviceContext1 Context1
        {
            get { return this.m_context; }
            protected set { this.m_context = value; }
        }

        public override SharpDX.DXGI.Device2 DxgiDevice2
        {
            get { return this.m_dxgiDevice; }
            protected set { this.m_dxgiDevice = value; }
        }

        public override Factory2 DxgiFactory2
        {
            get { return this.m_dxgiFactory; }
            protected set { this.m_dxgiFactory = value; }
        }

        public override SwapChain1 SwapChain1
        {
            get { return this.m_swapChain; }
            protected set { this.m_swapChain = value; }
        }

        public override RenderTargetView RenderTargetView
        {
            get { return this.m_renderTargetView; }
            protected set { this.m_renderTargetView = value; }
        }

        public override Adapter Adapter { get { return this.m_adapter; } protected set { this.m_adapter = value; } }

        public void Dispose()
        {
            this.m_isDisposed = true;
            GC.SuppressFinalize(this);
            this.Destroy();
            this.LinkedResources.Clear();
        }

        public void Initialize(IntPtr hwnd, Size size)
        {
            this.CheckDisposed();
            if (this.Device1 != null) this.Destroy();
            this.Hwnd = hwnd;
            this.Size = GetValidatedSize(ref size);
            this.ConnectRenderTargetView();
            this.InvokeInitializedEvent();
        }

        public void EnsureInitialized()
        {
            this.CheckDisposed();
            if (this.Device1 == null) this.Initialize(this.Hwnd, this.Size);
        }

        public void Resize(Size size)
        {
            this.CheckDisposed();
            this.Size = GetValidatedSize(ref size);
            try
            {
                this.DisconnectLinkedResources();
                this.DisconnectRenderTargetView();
                DisposableHelpers.DisposeAndSetNull(ref this.m_renderTargetView);
                // Resize retaining other properties.
                this.SwapChain1?.ResizeBuffers(0, this.Size.Width, this.Size.Height, Format.Unknown, SwapChainFlags.None);
                this.ConnectRenderTargetView();
                this.ConnectLinkedResources();
            }
            catch (SharpDXException ex)
            {
                if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor)) this.Destroy();
                else throw;
            }
        }

        public void Destroy()
        {
            this.DisconnectLinkedResources();
            DisposableHelpers.DisposeAndSetNull(ref this.m_renderTargetView);
            DisposableHelpers.DisposeAndSetNull(ref this.m_swapChain);
            DisposableHelpers.DisposeAndSetNull(ref this.m_context);
            DisposableHelpers.DisposeAndSetNull(ref this.m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref this.m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref this.m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref this.m_device);
            this.InvokeDestroyedEvent();
        }

        ~D3D11MetaResource()
        {
            this.Dispose();
        }

        private void CheckDisposed()
        {
            if (this.m_isDisposed) throw new ObjectDisposedException(nameof(D3D11MetaResource));
        }

        protected override void CreateDevice()
        {
            this.Device1 = D3D11MetaFactory.CreateD3DDevice1(this.m_creationOpts);
        }

        protected override void CreateSwapChain()
        {
            this.EnsureDevice();
            this.EnsureDxgiFactory();
            this.SwapChain1 = D3D11MetaFactory.CreateSwapChain1(this.m_creationOpts, this);
        }
    }
}