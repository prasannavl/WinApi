using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.Core;
using Device = SharpDX.Direct2D1.Device;
using Factory = SharpDX.DirectWrite.Factory;
using Factory1 = SharpDX.Direct2D1.Factory1;

namespace Sample.DirectX
{
    public class D2DResources : D3DResources
    {
        private Device m_d2DDevice;
        private DeviceContext m_d2DContext;
        private Factory1 m_d2DFactory;
        private Factory m_dWriteFactory;

        public Device D2DDevice
        {
            get { return m_d2DDevice; }
            private set { m_d2DDevice = value; }
        }

        public DeviceContext D2DContext
        {
            get { return m_d2DContext; }
            private set { m_d2DContext = value; }
        }

        public Factory1 D2DFactory
        {
            get { return m_d2DFactory; }
            private set { m_d2DFactory = value; }
        }

        public Factory DWriteFactory
        {
            get { return m_dWriteFactory; }
            private set { m_dWriteFactory = value; }
        }

        public override void Initalize(IntPtr hwnd, Size size)
        {
            base.Initalize(hwnd, size);
            ConnectD2DContextToDxgiSurface();
        }

        public override void Resize(ref Size size)
        {
            DisconnectD2DContextFromDxgiSurface();
            base.Resize(ref size);
            ConnectD2DContextToDxgiSurface();
        }

        public override void Destroy()
        {
            DisconnectD2DContextFromDxgiSurface();
            DisposableHelpers.DisposeAndSetNull(ref m_d2DContext);
            DisposableHelpers.DisposeAndSetNull(ref m_d2DDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_d2DFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_dWriteFactory);
            base.Destroy();
        }

        private void CreateD2DFactory()
        {
            DebugLevel level = DebugLevel.None;
#if DEBUG
            level = DebugLevel.Information;
#endif
            D2DFactory = new Factory1(FactoryType.SingleThreaded, level);
        }

        private void EnsureD2DFactory()
        {
            if (D2DFactory == null)
                CreateD2DFactory();
        }

        private void CreateDWriteFactory()
        {
            DWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
        }

        private void EnsureDWriteFactory()
        {
            if (DWriteFactory == null)
                CreateDWriteFactory();
        }

        private void CreateD2DDevice()
        {
            EnsureD2DFactory();
            EnsureDxgiDevice();   
            var props = new CreationProperties()
            {
#if DEBUG
                DebugLevel = DebugLevel.Information,                
#else
                DebugLevel = DebugLevel.None,
#endif           
                ThreadingMode = ThreadingMode.SingleThreaded,
                Options = DeviceContextOptions.EnableMultithreadedOptimizations
            };
            D2DDevice = new Device(DxgiDevice, props);
        }

        private void EnsureD2DDevice()
        {
            if (D2DDevice == null)
                CreateD2DDevice();
        }

        private void CreateD2DContext()
        {
            EnsureD2DDevice();
            D2DContext = new DeviceContext(D2DDevice, DeviceContextOptions.EnableMultithreadedOptimizations);
        }

        private void EnsureD2DContext()
        {
            if (D2DContext == null)
                CreateD2DContext();
        }

        private void ConnectD2DContextToDxgiSurface()
        {
            EnsureSwapChain();
            EnsureD2DContext();
            using (var surface = SwapChain.GetBackBuffer<Surface>(0))
            {
                using (var bitmap = new Bitmap1(D2DContext, surface))
                    D2DContext.Target = bitmap;
            }
        }

        private void DisconnectD2DContextFromDxgiSurface()
        {
            var currentTarget = D2DContext?.Target;
            if (currentTarget == null) return;
            D2DContext.Target = null;
            currentTarget.Dispose();
        }
    }
}
