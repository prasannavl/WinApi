using System;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    // ReSharper disable once InconsistentNaming
    public class D3D11Dxgi1_2MetaResource : D3D11Dxgi1_2ContainerCore, IDxgi1MetaResource
    {
        private readonly D3D11DxgiOptions m_opts;
        private Adapter m_adapter;
        private Device1 m_device;
        private Device2 m_dxgiDevice;
        private Factory2 m_dxgiFactory;
        private bool m_isDisposed;

        public D3D11Dxgi1_2MetaResource(D3D11DxgiOptions opts)
        {
            this.m_opts = opts;
        }

        public override Device1 Device1 { get { return this.m_device; } protected set { this.m_device = value; } }

        public override Device2 DxgiDevice2
        {
            get { return this.m_dxgiDevice; }
            protected set { this.m_dxgiDevice = value; }
        }

        public override Factory2 DxgiFactory2
        {
            get { return this.m_dxgiFactory; }
            protected set { this.m_dxgiFactory = value; }
        }

        public override Adapter Adapter { get { return this.m_adapter; } protected set { this.m_adapter = value; } }

        public void Initialize()
        {
            this.CheckDisposed();
            if (this.Device1 != null) this.Destroy();
            this.InitializeInternal();
        }

        public void EnsureInitialized()
        {
            this.CheckDisposed();
            if (this.Device1 == null) this.InitializeInternal();
        }

        public void Destroy()
        {
            this.DisconnectLinkedResources();
            DisposableHelpers.DisposeAndSetNull(ref this.m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref this.m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref this.m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref this.m_device);
            this.InvokeDestroyedEvent();
        }

        protected override void CreateDxgiDevice()
        {
            this.EnsureDevice();
            this.DxgiDevice2 = this.Device1.QueryInterface<Device2>();
        }

        protected override void CreateAdapter()
        {
            this.EnsureDxgiDevice();
            this.Adapter = this.DxgiDevice2.GetParent<Adapter>();
        }

        protected override void CreateDxgiFactory()
        {
            this.EnsureAdapter();
            this.DxgiFactory2 = this.Adapter.GetParent<Factory2>();
        }

        protected override void CreateDevice()
        {
            this.Device1 = D3D11MetaFactory.CreateD3DDevice1(this.m_opts);
        }

        private void InitializeInternal()
        {
            this.EnsureDxgiDevice();
            this.InvokeInitializedEvent();
        }

        public void Dispose()
        {
            this.m_isDisposed = true;
            GC.SuppressFinalize(this);
            this.Destroy();
            this.LinkedResources.Clear();
        }

        ~D3D11Dxgi1_2MetaResource()
        {
            this.Dispose();
        }

        private void CheckDisposed()
        {
            if (this.m_isDisposed) throw new ObjectDisposedException(nameof(D3D11Dxgi1_2MetaResource));
        }
    }
}