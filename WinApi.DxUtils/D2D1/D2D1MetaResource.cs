using System;
using System.Runtime.CompilerServices;
using SharpDX.Direct2D1;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct2D1.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Factory1 = SharpDX.Direct2D1.Factory1;

namespace WinApi.DxUtils.D2D1
{
    public class D2D1MetaResource<TDxgiContainer> : ID2D1_1MetaResource<TDxgiContainer>
        where TDxgiContainer : IDxgi1Container
    {
        private readonly Action<D2D1MetaResource<TDxgiContainer>> m_dxgiConnector;
        private readonly Action<D2D1MetaResource<TDxgiContainer>> m_dxgiDisconnector;
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;

        public TDxgiContainer DxgiContainer;
        private DeviceContext m_context;
        private CreationProperties m_creationProperties;
        private Device m_device;
        private Factory1 m_factory;
        private bool m_isDisposed;
        private bool m_isLinked;

        public D2D1MetaResource(CreationProperties props, Action<D2D1MetaResource<TDxgiContainer>> dxgiConnector,
            Action<D2D1MetaResource<TDxgiContainer>> dxgiDisconnector)
        {
            this.m_creationProperties = props;
            this.m_onDxgiDestroyedAction = () => this.DestroyInternal(true);
            this.m_onDxgiInitializedAction = () => this.InitializeInternal(true);
            this.m_dxgiConnector = dxgiConnector;
            this.m_dxgiDisconnector = dxgiDisconnector;
        }

        public Device Device { get { return this.m_device; } private set { this.m_device = value; } }

        public DeviceContext Context { get { return this.m_context; } private set { this.m_context = value; } }

        public Factory1 Factory1 { get { return this.m_factory; } private set { this.m_factory = value; } }

        public void Dispose()
        {
            this.m_isDisposed = true;
            GC.SuppressFinalize(this);
            this.Destroy();
        }

        public void ConnectToDxgi()
        {
            this.CheckDisposed();
            this.ConnectContextToDxgiSurface();
        }

        public void DisconnectFromDxgi()
        {
            this.DisconnectContextFromDxgiSurface();
        }

        public event Action Initialized;
        public event Action Destroyed;

        public void Initialize(TDxgiContainer dxgiContainer, bool autoLink = true)
        {
            this.CheckDisposed();
            if (this.Device != null) this.Destroy();
            this.DxgiContainer = dxgiContainer;
            this.m_isLinked = autoLink;
            this.InitializeInternal(false);
        }

        public void EnsureInitialized()
        {
            this.CheckDisposed();
            if (this.Device == null) this.Initialize(this.DxgiContainer, this.m_isLinked);
        }

        public void Destroy()
        {
            this.DestroyInternal(false);
        }

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            this.ConnectContextToDxgiSurface();
            if (!dxgiSourcePreserved) this.ConnectToDxgiSource(this.m_isLinked);
            this.Initialized?.Invoke();
        }

        private void ConnectToDxgiSource(bool shouldLink)
        {
            this.DxgiContainer.Destroyed += this.m_onDxgiDestroyedAction;
            this.DxgiContainer.Initialized += this.m_onDxgiInitializedAction;
            if (shouldLink) { this.DxgiContainer.AddLinkedResource(this); }
        }

        private void DisconnectFromDxgiSource()
        {
            if (this.DxgiContainer == null) return;
            this.DxgiContainer.Initialized -= this.m_onDxgiInitializedAction;
            this.DxgiContainer.Destroyed -= this.m_onDxgiDestroyedAction;
            if (this.m_isLinked) this.DxgiContainer.RemoveLinkedResource(this);
        }

        private void DestroyInternal(bool preserveDxgiSource)
        {
            if (!preserveDxgiSource) this.DisconnectFromDxgiSource();
            this.DisconnectContextFromDxgiSurface();
            DisposableHelpers.DisposeAndSetNull(ref this.m_context);
            DisposableHelpers.DisposeAndSetNull(ref this.m_device);
            DisposableHelpers.DisposeAndSetNull(ref this.m_factory);
            this.Destroyed?.Invoke();
        }

        private void CreateFactory()
        {
            var props = this.m_creationProperties;
            this.Factory1 = new Factory1((FactoryType) props.ThreadingMode, props.DebugLevel);
        }

        private void EnsureFactory()
        {
            if (this.Factory1 == null) this.CreateFactory();
        }

        private void CreateDevice()
        {
            this.EnsureFactory();
            this.Device = new Device(this.DxgiContainer.DxgiDevice, this.m_creationProperties);
        }

        private void EnsureDevice()
        {
            if (this.Device == null) this.CreateDevice();
        }

        private void CreateContext()
        {
            this.EnsureDevice();
            this.Context = new DeviceContext(this.Device, this.m_creationProperties.Options);
        }

        private void EnsureContext()
        {
            if (this.Context == null) this.CreateContext();
        }

        private void CheckDisposed()
        {
            if (this.m_isDisposed) throw new ObjectDisposedException(nameof(D2D1MetaResource<TDxgiContainer>));
        }

        ~D2D1MetaResource()
        {
            this.Dispose();
        }

        private void ConnectContextToDxgiSurface()
        {
            this.EnsureContext();
            this.m_dxgiConnector?.Invoke(this);
        }

        private void DisconnectContextFromDxgiSurface()
        {
            this.m_dxgiDisconnector?.Invoke(this);
        }
    }
}