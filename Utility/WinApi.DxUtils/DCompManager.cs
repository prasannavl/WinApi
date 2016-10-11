using System;
using SharpDX.DirectComposition;
using SharpDX.Win32;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils
{
    public class DCompManager : IDisposable, INotifyOnInitDestroy
    {
        private readonly Action m_onDxgiDestroyedAction;
        private readonly Action m_onDxgiInitializedAction;
        private IDisposable m_device;
        private bool m_disposed;
        private IDxgi1Container m_dxgiContainer;

        private bool m_isTopMost;
        private Target m_target;
        private Visual m_visual;

        public DCompManager(int variant)
        {
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
            if (m_disposed) throw new ObjectDisposedException(nameof(DCompManager));
        }

        ~DCompManager()
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
                CreateDevice();
        }

        private void CreateDevice()
        {
            if (DeviceVariant > 1)
            {
                Device = new DesktopDevice(m_dxgiContainer.DxgiDevice);
            }
            else if (DeviceVariant == 1)
            {
                Device = new Device(m_dxgiContainer.DxgiDevice);
            }
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

        public void Initialize(IntPtr hwnd, IDxgi1Container dxgiContainer, bool isTopMost = false)
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

        public static int GetVariantForPlatform(Version platformVersion = null)
        {
            if (platformVersion == null)
                platformVersion = Environment.OSVersion.Version;
            if (platformVersion.Major > 6) return 2;
            if (platformVersion.Major == 6)
            {
                if (platformVersion.Minor > 2) return 2;
                if (platformVersion.Minor > 1) return 1;
            }
            return 0;
        }
    }
}