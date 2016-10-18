using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    public abstract class D3D11Dxgi1ContainerCore : DxgiContainerBase, IDxgi1Container
    {
        public virtual Device Device { get; protected set; }
        public virtual SharpDX.DXGI.Device DxgiDevice { get; protected set; }
        public virtual Factory DxgiFactory { get; protected set; }
        public virtual Adapter Adapter { get; protected set; }

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

        protected void EnsureDevice()
        {
            if (Device == null)
                CreateDevice();
        }

        protected abstract void CreateDevice();
    }

    // ReSharper disable once InconsistentNaming
    public abstract class D3D11Dxgi1_2ContainerCore : D3D11Dxgi1ContainerCore, IDxgi1_2Container
    {
        public override Device Device => Device1;
        public virtual Device1 Device1 { get; protected set; }
        public override Factory DxgiFactory => DxgiFactory2;
        public override SharpDX.DXGI.Device DxgiDevice => DxgiDevice2;

        public virtual Factory2 DxgiFactory2 { get; protected set; }
        public virtual SharpDX.DXGI.Device2 DxgiDevice2 { get; protected set; }
    }

    // ReSharper disable once InconsistentNaming
    public class D3D11Dxgi1_2MetaResource : D3D11Dxgi1_2ContainerCore
    {
        private readonly D3D11DxgiOptions m_opts;
        private Adapter m_adapter;
        private Device1 m_device;
        private SharpDX.DXGI.Device2 m_dxgiDevice;
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