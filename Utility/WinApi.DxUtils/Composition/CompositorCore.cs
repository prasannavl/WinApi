using System;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.Composition
{
    public abstract class CompositorCore<TDxgiContainer, TOptions> : IDisposable, INotifyOnInitDestroy
        where TDxgiContainer : IDxgi1Container
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        private IDisposable m_device;
        private bool m_disposed;
        protected TDxgiContainer DxgiContainer;
        protected TOptions Options;

        protected CompositorCore(int variant)
        {
            if (variant == -1) variant = CompositionHelper.GetVariantForPlatform();
            DeviceVariant = variant;
            m_onDxgiDestroyedAction = () => DestroyInternal(true);
            m_onDxgiInitializedAction = () => InitializeInternal(true);
        }

        public int DeviceVariant { get; }

        public IDisposable Device
        {
            get { return m_device; }
            private set { m_device = value; }
        }

        public void Dispose()
        {
            m_disposed = true;
            GC.SuppressFinalize(this);
            Destroy();
        }

        public event Action Initialized;
        public event Action Destroyed;

        private void CheckDisposed()
        {
            if (m_disposed) throw new ObjectDisposedException(this.GetType().Name);
        }

        ~CompositorCore()
        {
            Dispose();
        }

        public void Destroy()
        {
            DestroyInternal(false);
        }

        private void DestroyInternal(bool preserveDxgiSource)
        {
            if (!preserveDxgiSource) DisconnectFromDxgiSource();
            DestroyResources();
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            Destroyed?.Invoke();
        }

        protected void EnsureDevice()
        {
            if (Device == null)
                Device = (IDisposable) CompositionHelper.CreateDevice(DxgiContainer.DxgiDevice, DeviceVariant);
        }

        public void Initialize(TDxgiContainer dxgiContainer, TOptions opts = default(TOptions))
        {
            CheckDisposed();
            if (Device != null)
                Destroy();
            Options = opts;
            DxgiContainer = dxgiContainer;
            InitializeInternal(false);
        }

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            InitializeResources();
            if (!dxgiSourcePreserved) ConnectToDxgiSource();
            Initialized?.Invoke();
        }

        protected abstract void InitializeResources();

        protected abstract void DestroyResources();

        private void ConnectToDxgiSource()
        {
            DxgiContainer.Destroyed += m_onDxgiDestroyedAction;
            DxgiContainer.Initialized += m_onDxgiInitializedAction;
        }

        private void DisconnectFromDxgiSource()
        {
            if (DxgiContainer == null) return;
            DxgiContainer.Initialized -= m_onDxgiInitializedAction;
            DxgiContainer.Destroyed -= m_onDxgiDestroyedAction;
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device == null)
                Initialize(DxgiContainer);
        }
    }
}