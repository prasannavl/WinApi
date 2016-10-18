using System;
using SharpDX.DXGI;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    // ReSharper disable once InconsistentNaming
    public class D3D11Dxgi1_2MetaResource : D3D11Dxgi1_2ContainerCore
    {
        private readonly D3D11DxgiOptions m_opts;
        private Adapter m_adapter;
        private Device1 m_device;
        private Device2 m_dxgiDevice;
        private Factory2 m_dxgiFactory;
        private bool m_isDisposed;

        public D3D11Dxgi1_2MetaResource(D3D11DxgiOptions opts)
        {
            m_opts = opts;
        }

        public override Device1 Device1
        {
            get { return m_device; }
            protected set { m_device = value; }
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

        public override Adapter Adapter
        {
            get { return m_adapter; }
            protected set { m_adapter = value; }
        }

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

        protected override void CreateDevice()
        {
            Device1 = D3D11MetaFactory.CreateD3DDevice1(m_opts);
        }

        public void Initialize()
        {
            CheckDisposed();
            if (Device1 != null)
                Destroy();
            EnsureDxgiDevice();
            InvokeInitializedEvent();
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device1 == null)
                Initialize();
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
            LinkedResources.Clear();
        }

        ~D3D11Dxgi1_2MetaResource()
        {
            Dispose();
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D3D11Dxgi1_2MetaResource));
        }

        public void Destroy()
        {
            DisconnectLinkedResources();
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            InvokeDestroyedEvent();
        }
    }
}