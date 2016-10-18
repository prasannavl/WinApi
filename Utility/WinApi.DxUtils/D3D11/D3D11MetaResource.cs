using System;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.DXGI.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    public class D3D11MetaResource : D3D11_1Container, ID3D11_1MetaResourceImpl
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
            m_creationOpts = creationOpts;
        }

        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public override Device1 Device1
        {
            get { return m_device; }
            protected set { m_device = value; }
        }

        public override DeviceContext1 Context1
        {
            get { return m_context; }
            protected set { m_context = value; }
        }

        public override SharpDX.DXGI.Device2 DxgiDevice2
        {
            get { return m_dxgiDevice; }
            protected set { m_dxgiDevice = value; }
        }

        public override Factory2 DxgiFactory2
        {
            get { return m_dxgiFactory; }
            protected set { m_dxgiFactory = value; }
        }

        public override SwapChain1 SwapChain1
        {
            get { return m_swapChain; }
            protected set { m_swapChain = value; }
        }

        public override RenderTargetView RenderTargetView
        {
            get { return m_renderTargetView; }
            protected set { m_renderTargetView = value; }
        }

        public override Adapter Adapter
        {
            get { return m_adapter; }
            protected set { m_adapter = value; }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
            LinkedResources.Clear();
        }

        public void Initialize(IntPtr hwnd, Size size)
        {
            CheckDisposed();
            if (Device1 != null)
                Destroy();
            Hwnd = hwnd;
            Size = GetValidatedSize(ref size);
            ConnectRenderTargetView();
            InvokeInitializedEvent();
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device1 == null)
                Initialize(Hwnd, Size);
        }

        public void Resize(Size size)
        {
            CheckDisposed();
            Size = GetValidatedSize(ref size);
            try
            {
                DisconnectLinkedResources();
                DisconnectRenderTargetView();
                DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
                // Resize retaining other properties.
                SwapChain1?.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
                ConnectRenderTargetView();
                ConnectLinkedResources();
            }
            catch (SharpDXException ex)
            {
                if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor))
                    Destroy();
                else throw;
            }
        }

        ~D3D11MetaResource()
        {
            Dispose();
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D3D11MetaResource));
        }

        public void Destroy()
        {
            DisconnectLinkedResources();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
            DisposableHelpers.DisposeAndSetNull(ref m_swapChain);
            DisposableHelpers.DisposeAndSetNull(ref m_context);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            InvokeDestroyedEvent();
        }

        protected override void CreateDevice()
        {
            Device1 = D3D11MetaFactory.CreateD3DDevice1(m_creationOpts);
        }

        protected override void CreateSwapChain()
        {
            EnsureDevice();
            EnsureDxgiFactory();
            SwapChain1 = D3D11MetaFactory.CreateSwapChain1(m_creationOpts, this);
        }
    }
}