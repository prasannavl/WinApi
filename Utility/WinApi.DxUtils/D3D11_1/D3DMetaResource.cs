using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils.Core;
using WinApi.DxUtils.D3D11;
using WinApi.Utils;
using Device = SharpDX.DXGI.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11_1
{
    public class D3DMetaResource : D3D11Container, IDisposable, IDxgi1_2
    {
        private readonly Func<Device1> m_deviceCreator;
        private readonly Func<D3DMetaResource, SwapChain1> m_swapChainCreator;

        private Adapter m_adapter;
        private DeviceContext1 m_context;
        private Device1 m_device;
        private SharpDX.DXGI.Device2 m_dxgiDevice;
        private Factory2 m_dxgiFactory;
        private bool m_isDisposed;
        private RenderTargetView m_renderTargetView;
        private SwapChain1 m_swapChain;

        public D3DMetaResource(Func<Device1> deviceCreator, Func<D3DMetaResource, SwapChain1> swapChainCreator,
            ref Size size, bool autoInit = true)
        {
            m_deviceCreator = deviceCreator;
            m_swapChainCreator = swapChainCreator;
            if (autoInit) InitializeInternal(ref size);
        }

        public Size Size { get; private set; }

        public Device1 Device1
        {
            get { return m_device; }
            private set { m_device = value; }
        }

        public DeviceContext1 Context1
        {
            get { return m_context; }
            private set { m_context = value; }
        }

        public override RenderTargetView RenderTargetView
        {
            get { return m_renderTargetView; }
            protected set { m_renderTargetView = value; }
        }

        public SharpDX.DXGI.Device2 DxgiDevice2
        {
            get { return m_dxgiDevice; }
            private set { m_dxgiDevice = value; }
        }

        public Factory2 DxgiFactory2
        {
            get { return m_dxgiFactory; }
            private set { m_dxgiFactory = value; }
        }

        public override Adapter Adapter
        {
            get { return m_adapter; }
            protected set { m_adapter = value; }
        }

        public SwapChain1 SwapChain1
        {
            get { return m_swapChain; }
            private set { m_swapChain = value; }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
            LinkedResources.Clear();
        }

        public override Device DxgiDevice => DxgiDevice2;
        public override Factory DxgiFactory => DxgiFactory2;
        public override SwapChain SwapChain => SwapChain1;

        ~D3DMetaResource()
        {
            Dispose();
        }

        public void Initalize(ref Size size)
        {
            CheckDisposed();
            if (Device != null)
                Destroy();
            InitializeInternal(ref size);
        }

        private void InitializeInternal(ref Size size)
        {
            Size = GetValidatedSize(ref size);
            ConnectRenderTargetView();
        }

        public void Resize(ref Size size)
        {
            CheckDisposed();
            Size = GetValidatedSize(ref size);
            DisconnectLinkedResources();
            DisconnectRenderTargetView();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
            // Resize retaining other properties.
            SwapChain1.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
            ConnectRenderTargetView();
            ConnectLinkedResources();
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D3DMetaResource));
        }

        public void Destroy()
        {
            DisconnectLinkedResources();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
            DisposableHelpers.DisposeAndSetNull(ref m_swapChain);
            DisposableHelpers.DisposeAndSetNull(ref m_context);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_adapter);
            DisposableHelpers.DisposeAndSetNull(ref m_dxgiDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_device);
        }

        protected override void CreateDevice()
        {
            Device = m_deviceCreator();
        }

        protected override void CreateDxgiDevice()
        {
            EnsureDevice();
            DxgiDevice2 = Device.QueryInterface<SharpDX.DXGI.Device2>();
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

        protected override void CreateSwapChain()
        {
            EnsureDevice();
            EnsureDxgiFactory();
            SwapChain1 = m_swapChainCreator(this);
        }

        protected override void CreateContext()
        {
            EnsureDevice();
            Context1 = Device1.ImmediateContext1;
        }
    }
}