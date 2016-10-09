using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.Utils;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils.D3D11
{
    public class D3DMetaResource : D3D11Container, IDisposable
    {
        private readonly Func<Device> m_deviceCreator;
        private readonly Func<D3DMetaResource, SwapChain> m_swapChainCreator;

        private Adapter m_adapter;
        private DeviceContext m_context;
        private Device m_device;
        private SharpDX.DXGI.Device m_dxgiDevice;
        private Factory m_dxgiFactory;
        private bool m_isDisposed;
        private RenderTargetView m_renderTargetView;
        private SwapChain m_swapChain;

        public D3DMetaResource(Func<Device> deviceCreator, Func<D3DMetaResource, SwapChain> swapChainCreator)
        {
            m_deviceCreator = deviceCreator;
            m_swapChainCreator = swapChainCreator;
        }

        public void Initalize(IntPtr hwnd, Size size)
        {
            CheckDisposed();
            if (Device != null)
                Destroy();
            Hwnd = hwnd;
            Size = GetValidatedSize(ref size);
            ConnectRenderTargetView();
        }

        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

        public override Device Device
        {
            get { return m_device; }
            protected set { m_device = value; }
        }

        public override DeviceContext Context
        {
            get { return m_context; }
            protected set { m_context = value; }
        }

        public override RenderTargetView RenderTargetView
        {
            get { return m_renderTargetView; }
            protected set { m_renderTargetView = value; }
        }

        public override SharpDX.DXGI.Device DxgiDevice
        {
            get { return m_dxgiDevice; }
            protected set { m_dxgiDevice = value; }
        }

        public override Factory DxgiFactory
        {
            get { return m_dxgiFactory; }
            protected set { m_dxgiFactory = value; }
        }

        public override Adapter Adapter
        {
            get { return m_adapter; }
            protected set { m_adapter = value; }
        }

        public override SwapChain SwapChain
        {
            get { return m_swapChain; }
            protected set { m_swapChain = value; }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
            LinkedResources.Clear();
        }

        ~D3DMetaResource()
        {
            Dispose();
        }

        public void Resize(ref Size size)
        {
            CheckDisposed();
            Size = D3D11Container.GetValidatedSize(ref size);
            DisconnectLinkedResources();
            DisconnectRenderTargetView();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
            // Resize retaining other properties.
            SwapChain.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
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

        protected override void CreateSwapChain()
        {
            EnsureDevice();
            EnsureDxgiFactory();
            SwapChain = m_swapChainCreator(this);
        }
    }
}