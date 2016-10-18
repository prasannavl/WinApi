using System;
using System.Runtime.CompilerServices;
using SharpDX.Direct2D1;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct2D1.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Factory1 = SharpDX.Direct2D1.Factory1;

namespace WinApi.DxUtils.D2D1
{
    public class D2D1MetaResource<TDxgiContainer> : ID2D1_1MetaResourceImpl<TDxgiContainer>
        where TDxgiContainer : IDxgi1Container
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        private readonly Action<D2D1MetaResource<TDxgiContainer>> m_dxgiConnector;
        private readonly Action<D2D1MetaResource<TDxgiContainer>> m_dxgiDisconnector;

        public TDxgiContainer DxgiContainer;
        private bool m_isDisposed;
        private bool m_isLinked;
        private DeviceContext m_context;
        private CreationProperties m_creationProperties;
        private Device m_device;
        private Factory1 m_factory;

        public D2D1MetaResource(CreationProperties props, Action<D2D1MetaResource<TDxgiContainer>> dxgiConnector, Action<D2D1MetaResource<TDxgiContainer>> dxgiDisconnector)
        {
            m_creationProperties = props;
            m_onDxgiDestroyedAction = () => DestroyInternal(true);
            m_onDxgiInitializedAction = () => InitializeInternal(true);
            m_dxgiConnector = dxgiConnector;
            m_dxgiDisconnector = dxgiDisconnector;
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

        public event Action Initialized;
        public event Action Destroyed;

        public void Initialize(TDxgiContainer dxgiContainer, bool autoLink = true)
        {
            CheckDisposed();
            if (Device != null)
                Destroy();
            DxgiContainer = dxgiContainer;
            m_isLinked = autoLink;
            InitializeInternal(false);
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device == null)
                Initialize(DxgiContainer, m_isLinked);
        }

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            ConnectContextToDxgiSurface();
            if (!dxgiSourcePreserved) ConnectToDxgiSource(m_isLinked);
            Initialized?.Invoke();
        }

        private void ConnectToDxgiSource(bool shouldLink)
        {
            DxgiContainer.Destroyed += m_onDxgiDestroyedAction;
            DxgiContainer.Initialized += m_onDxgiInitializedAction;
            if (shouldLink)
            {
                DxgiContainer.AddLinkedResource(this);
            }
        }

        private void DisconnectFromDxgiSource()
        {
            if (DxgiContainer == null) return;
            DxgiContainer.Initialized -= m_onDxgiInitializedAction;
            DxgiContainer.Destroyed -= m_onDxgiDestroyedAction;
            if (m_isLinked) DxgiContainer.RemoveLinkedResource(this);
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
            Device = new Device(DxgiContainer.DxgiDevice, m_creationProperties);
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

        private void CheckDisposed()
        {
            if (m_isDisposed)
                throw new ObjectDisposedException(nameof(D2D1MetaResource<TDxgiContainer>));
        }

        ~D2D1MetaResource()
        {
            Dispose();
        }

        private void ConnectContextToDxgiSurface()
        {
            EnsureContext();
            m_dxgiConnector?.Invoke(this);
        }

        private void DisconnectContextFromDxgiSurface()
        {
            m_dxgiDisconnector?.Invoke(this);
        }
    }
}