using System;
using System.Runtime.CompilerServices;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct2D1.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Factory1 = SharpDX.Direct2D1.Factory1;

namespace WinApi.DxUtils.D2D1
{
    public class D2DMetaResource : ID2D1MetaResourceImpl
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        private DeviceContext m_context;
        private CreationProperties m_creationProperties;
        private Device m_device;
        private IDxgi1Container m_dxgiContainer;
        private Factory1 m_factory;
        private bool m_isDisposed;
        private bool m_isLinked;

        public D2DMetaResource(CreationProperties props)
        {
            m_creationProperties = props;
            m_onDxgiDestroyedAction = () => DestroyInternal(true);
            m_onDxgiInitializedAction = () => InitializeInternal(true);
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

        public Factory1 Factory1
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

        public void Initialize(IDxgi1Container dxgiContainer, bool autoLink = true)
        {
            CheckDisposed();
            if (Device != null)
                Destroy();
            m_dxgiContainer = dxgiContainer;
            m_isLinked = autoLink;
            InitializeInternal(false);
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device == null)
                Initialize(m_dxgiContainer, m_isLinked);
        }

        public event Action Initialized;
        public event Action Destroyed;

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            ConnectContextToDxgiSurface();
            if (!dxgiSourcePreserved) ConnectToDxgiSource(m_isLinked);
            Initialized?.Invoke();
        }

        private void ConnectToDxgiSource(bool shouldLink)
        {
            m_dxgiContainer.Destroyed += m_onDxgiDestroyedAction;
            m_dxgiContainer.Initialized += m_onDxgiInitializedAction;
            if (shouldLink)
            {
                m_dxgiContainer.AddLinkedResource(this);
            }
        }

        protected void DisconnectFromDxgiSource()
        {
            if (m_dxgiContainer == null) return;
            m_dxgiContainer.Initialized -= m_onDxgiInitializedAction;
            m_dxgiContainer.Destroyed -= m_onDxgiDestroyedAction;
            if (m_isLinked) m_dxgiContainer.RemoveLinkedResource(this);
        }

        public void Destroy()
        {
            DestroyInternal(false);
        }

        private void DestroyInternal(bool preserveDxgiSource)
        {
            if (!preserveDxgiSource) DisconnectFromDxgiSource();
            DisconnectContextFromDxgiSurface();
            DisposableHelpers.DisposeAndSetNull(ref m_context);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            DisposableHelpers.DisposeAndSetNull(ref m_factory);
            Destroyed?.Invoke();
        }

        private void CreateFactory()
        {
            var props = m_creationProperties;
            Factory1 = new Factory1((FactoryType) props.ThreadingMode, props.DebugLevel);
        }

        private void EnsureFactory()
        {
            if (Factory1 == null)
                CreateFactory();
        }

        private void CreateDevice()
        {
            EnsureFactory();
            Device = new Device(m_dxgiContainer.DxgiDevice, m_creationProperties);
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
            using (var surface = m_dxgiContainer.SwapChain.GetBackBuffer<Surface>(0))
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