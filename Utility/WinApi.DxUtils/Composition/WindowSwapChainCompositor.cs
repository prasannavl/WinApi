using System;
using SharpDX.DirectComposition;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.Composition
{
    public class WindowSwapChainCompositor : IDisposable, INotifyOnInitDestroy
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        private IDisposable m_device;
        private bool m_disposed;
        private IDxgi1ContainerWithSwapChain m_dxgiContainer;

        private bool m_isTopMost;
        private Target m_target;
        private Visual m_visual;

        public WindowSwapChainCompositor(int variant = -1)
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

        public Target Target
        {
            get { return m_target; }
            private set { m_target = value; }
        }

        public Visual Visual
        {
            get { return m_visual; }
            private set { m_visual = value; }
        }

        public IntPtr Hwnd { get; private set; }

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
            if (m_disposed) throw new ObjectDisposedException(nameof(WindowSwapChainCompositor));
        }

        ~WindowSwapChainCompositor()
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
            DisposableHelpers.DisposeAndSetNull(ref m_visual);
            DisposableHelpers.DisposeAndSetNull(ref m_target);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
            Destroyed?.Invoke();
        }

        private void EnsureDevice()
        {
            if (Device == null)
                Device = (IDisposable)CompositionHelper.CreateDevice(m_dxgiContainer.DxgiDevice, DeviceVariant);
        }

        private void CreateResources()
        {
            EnsureDevice();
            if (DeviceVariant > 1)
            {
                var device = (DesktopDevice) Device;
                Target = Target.FromHwnd(device, Hwnd, m_isTopMost);
                Visual = new Visual2(device);
            }

//             Wait for SharpDX PR to be released before uncommenting the
//             Windows 8 version of the codepath below.

//            if (DeviceVariant == 1)
//            {
//                var device = (Device)Device;
//                Target = Target.FromHwnd(device, Hwnd, m_isTopMost);
//                Visual = new Visual(device);
//                return;
//            }
        }

        private void EnsureResources()
        {
            if (Target == null)
                CreateResources();
        }

        public void Initialize(IntPtr hwnd, IDxgi1ContainerWithSwapChain dxgiContainer, bool isTopMost = false)
        {
            if (Device != null)
                Destroy();
            Hwnd = hwnd;
            m_isTopMost = isTopMost;
            m_dxgiContainer = dxgiContainer;
            InitializeInternal(false);
        }

        private void InitializeInternal(bool dxgiSourcePreserved)
        {
            Compose();
            if (!dxgiSourcePreserved) ConnectToDxgiSource();
            Initialized?.Invoke();
        }

        private void ConnectToDxgiSource()
        {
            m_dxgiContainer.Destroyed += m_onDxgiDestroyedAction;
            m_dxgiContainer.Initialized += m_onDxgiInitializedAction;
        }

        protected void DisconnectFromDxgiSource()
        {
            if (m_dxgiContainer == null) return;
            m_dxgiContainer.Initialized -= m_onDxgiInitializedAction;
            m_dxgiContainer.Destroyed -= m_onDxgiDestroyedAction;
        }

        public void EnsureInitialized()
        {
            CheckDisposed();
            if (Device == null)
                Initialize(Hwnd, m_dxgiContainer, m_isTopMost);
        }

        private void Compose()
        {
            EnsureResources();
            // Target and Visual are always created together. So
            // if is not null, assume the other is not as well.
            if (Target != null)
            {
                Visual.Content = m_dxgiContainer.SwapChain;
                Target.Root = Visual;
                if (DeviceVariant > 1) ((DesktopDevice) Device).Commit();
                else if (DeviceVariant == 1) ((Device) Device).Commit();
            }
        }
    }
}