using System;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.Utils;
using Device = SharpDX.Direct2D1.Device;
using Factory1 = SharpDX.Direct2D1.Factory1;

namespace WinApi.DxUtils
{
    public class D2DMetaResource : IDisposable, IDxgiConnectable
    {
        private DeviceContext m_context;
        private CreationProperties m_creationProperties;
        private D3DMetaResource m_d3DMetaResource;
        private Device m_device;
        private Factory1 m_factory;
        private bool m_isDisposed;

        public D2DMetaResource(CreationProperties props)
        {
            m_creationProperties = props;
        }

        public Device Device
        {
            get { return m_device; }
            private set { m_device = value; }
        }

        public DeviceContext Context
        {
            get { return m_context; }
            private set { m_context = value; }
        }

        public Factory1 Factory
        {
            get { return m_factory; }
            private set { m_factory = value; }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
        }

        public void ConnectToDxgi()
        {
            CheckDisposed();
            ConnectContextToDxgiSurface();
        }

        public void DisconnectFromDxgi()
        {
            DisconnectContextFromDxgiSurface();
        }

        public void Initialize(D3DMetaResource d3DMetaResource, bool linkToD3DManager = false)
        {
            CheckDisposed();
            m_d3DMetaResource = d3DMetaResource;
            if (linkToD3DManager) d3DMetaResource.AddLinkedResource(this);
        }

        public void Destroy()
        {
            m_d3DMetaResource?.RemoveLinkedResource(this);
            DisconnectContextFromDxgiSurface();
            DisposableHelpers.DisposeAndSetNull(ref m_context);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            DisposableHelpers.DisposeAndSetNull(ref m_factory);
        }

        private void CreateFactory()
        {
            var props = m_creationProperties;
            Factory = new Factory1((FactoryType) props.ThreadingMode, props.DebugLevel);
        }

        private void EnsureFactory()
        {
            if (Factory == null)
                CreateFactory();
        }

        private void CreateDevice()
        {
            EnsureFactory();
            Device = new Device(m_d3DMetaResource.DxgiDevice, m_creationProperties);
        }

        private void EnsureDevice()
        {
            if (Device == null)
                CreateDevice();
        }

        private void CreateContext()
        {
            EnsureDevice();
            Context = new DeviceContext(Device, m_creationProperties.Options);
        }

        private void EnsureContext()
        {
            if (Context == null)
                CreateContext();
        }

        private void ConnectContextToDxgiSurface()
        {
            EnsureContext();
            using (var surface = m_d3DMetaResource.SwapChain.GetBackBuffer<Surface>(0))
            {
                using (var bitmap = new Bitmap1(Context, surface))
                    Context.Target = bitmap;
            }
        }

        private void DisconnectContextFromDxgiSurface()
        {
            var currentTarget = Context?.Target;
            if (currentTarget == null) return;
            currentTarget.Dispose();
            Context.Target = null;
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D2DMetaResource));
        }

        ~D2DMetaResource()
        {
            Dispose();
        }
    }
}