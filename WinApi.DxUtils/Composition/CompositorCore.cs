using System;
using SharpDX;
using SharpDX.DirectComposition;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.Composition
{
    public abstract class CompositorCore<TDxgiContainer, TOptions> : IDisposable, INotifyOnInitDestroy
        where TDxgiContainer : IDxgi1Container
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        protected TDxgiContainer DxgiContainer;
        private ComObject m_device;
        private bool m_disposed;
        protected TOptions Options;

        protected CompositorCore(int variant)
        {
            if (variant == -1) variant = CompositionHelper.GetVariantForPlatform();
            this.DeviceVariant = variant;
            this.m_onDxgiDestroyedAction = () => this.DestroyInternal(true);
            this.m_onDxgiInitializedAction = () => this.InitializeInternal(true);
        }

        public int DeviceVariant { get; }

        public ComObject Device { get { return this.m_device; } private set { this.m_device = value; } }

        public void Dispose()
        {
            this.m_disposed = true;
            GC.SuppressFinalize(this);
            this.Destroy();
        }

        public event Action Initialized;
        public event Action Destroyed;

        private void CheckDisposed()
        {
            if (this.m_disposed) throw new ObjectDisposedException(this.GetType().Name);
        }

        ~CompositorCore()
        {
            this.Dispose();
        }

        public void Destroy()
        {
            this.DestroyInternal(false);
        }

        public void Commit()
        {
            CompositionHelper.Commit(this.Device, this.DeviceVariant);
        }

        private void DestroyInternal(bool preserveDxgiSource)
        {
            if (!preserveDxgiSource) this.DisconnectFromDxgiSource();
            this.DestroyResources();
            DisposableHelpers.DisposeAndSetNull(ref this.m_device);
            this.Destroyed?.Invoke();
        }

        protected void EnsureDevice()
        {
            if (this.Device == null) this.Device = CompositionHelper.CreateDevice(this.DxgiContainer.DxgiDevice, this.DeviceVariant);
        }

        public void Initialize(TDxgiContainer dxgiContainer, TOptions opts = default(TOptions))
        {
            this.CheckDisposed();
            if (this.Device != null) this.Destroy();
            this.Options = opts;
            this.DxgiContainer = dxgiContainer;
            this.InitializeInternal(false);
        }

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            this.InitializeResources();
            if (!dxgiSourcePreserved) this.ConnectToDxgiSource();
            this.Initialized?.Invoke();
        }

        protected abstract void InitializeResources();

        protected abstract void DestroyResources();

        private void ConnectToDxgiSource()
        {
            this.DxgiContainer.Destroyed += this.m_onDxgiDestroyedAction;
            this.DxgiContainer.Initialized += this.m_onDxgiInitializedAction;
        }

        private void DisconnectFromDxgiSource()
        {
            if (this.DxgiContainer == null) return;
            this.DxgiContainer.Initialized -= this.m_onDxgiInitializedAction;
            this.DxgiContainer.Destroyed -= this.m_onDxgiDestroyedAction;
        }

        public void EnsureInitialized()
        {
            this.CheckDisposed();
            if (this.Device == null) this.InitializeInternal(true);
        }
    }
}