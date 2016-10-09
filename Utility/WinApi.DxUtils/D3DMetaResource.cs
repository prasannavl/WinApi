using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.Utils;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils
{
    public interface IDxgiConnectable
    {
        void ConnectToDxgi();
        void DisconnectFromDxgi();
    }

    public class D3DMetaResource : IDisposable
    {
        private readonly Func<Device> m_deviceCreator;
        private readonly Func<D3DMetaResource, SwapChain> m_swapChainCreator;

        private Adapter m_adapter;

        private List<IDxgiConnectable> m_connectedResources;
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

        public IntPtr Hwnd { get; private set; }
        public Size Size { get; private set; }

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

        public RenderTargetView RenderTargetView
        {
            get { return m_renderTargetView; }
            private set { m_renderTargetView = value; }
        }

        public SharpDX.DXGI.Device DxgiDevice
        {
            get { return m_dxgiDevice; }
            private set { m_dxgiDevice = value; }
        }

        public Factory DxgiFactory
        {
            get { return m_dxgiFactory; }
            private set { m_dxgiFactory = value; }
        }

        public Adapter Adapter
        {
            get { return m_adapter; }
            private set { m_adapter = value; }
        }

        public SwapChain SwapChain
        {
            get { return m_swapChain; }
            private set { m_swapChain = value; }
        }

        private List<IDxgiConnectable> LinkedResources
        {
            get { return m_connectedResources ?? (m_connectedResources = new List<IDxgiConnectable>()); }
            set { m_connectedResources = value; }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            Destroy();
            LinkedResources.Clear();
        }

        public void AddLinkedResource(IDxgiConnectable resource)
        {
            if (LinkedResources.Contains(resource)) return;
            LinkedResources.Add(resource);
        }

        public void AddLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            LinkedResources.AddRange(resources.Except(LinkedResources));
        }

        public void RemoveLinkedResource(IDxgiConnectable resource)
        {
            LinkedResources.Remove(resource);
        }

        public void RemoveLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            foreach (var res in resources)
            {
                LinkedResources.Remove(res);
            }
        }

        ~D3DMetaResource()
        {
            Dispose();
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

        public void Resize(ref Size size)
        {
            CheckDisposed();
            Size = GetValidatedSize(ref size);
            DisconnectLinkedResources();
            DisconnectRenderTargetView();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTargetView);
            // Resize retaining other properties.
            SwapChain.ResizeBuffers(0, Size.Width, Size.Height, Format.Unknown, SwapChainFlags.None);
            ConnectRenderTargetView();
            ConnectLinkedResources();
        }

        private void DisconnectLinkedResources()
        {
            foreach (var res in LinkedResources)
            {
                res.DisconnectFromDxgi();
            }
        }

        private void ConnectLinkedResources()
        {
            foreach (var res in LinkedResources)
            {
                res.ConnectToDxgi();
            }
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D3DMetaResource));
        }

        public static Size GetValidatedSize(ref Size size)
        {
            var h = size.Height >= 0 ? size.Height : 0;
            var w = size.Width >= 0 ? size.Width : 0;
            return new Size(w, h);
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

        private void CreateDevice()
        {
            Device = m_deviceCreator();
        }

        private void EnsureDevice()
        {
            if (Device == null)
                CreateDevice();
        }

        private void CreateDxgiDevice()
        {
            EnsureDevice();
            DxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device>();
        }

        private void EnsureDxgiDevice()
        {
            if (DxgiDevice == null)
                CreateDxgiDevice();
        }

        private void CreateAdapter()
        {
            EnsureDxgiDevice();
            Adapter = DxgiDevice.GetParent<Adapter>();
        }

        private void EnsureAdapter()
        {
            if (Adapter == null)
                CreateAdapter();
        }

        private void CreateDxgiFactory()
        {
            EnsureAdapter();
            DxgiFactory = Adapter.GetParent<Factory>();
        }

        private void EnsureDxgiFactory()
        {
            if (DxgiFactory == null)
                CreateDxgiFactory();
        }

        private void CreateSwapChain()
        {
            EnsureDevice();
            EnsureDxgiFactory();
            SwapChain = m_swapChainCreator(this);
        }

        private void EnsureSwapChain()
        {
            if (SwapChain == null)
                CreateSwapChain();
        }

        private void CreateContext()
        {
            EnsureDevice();
            Context = Device.ImmediateContext;
        }

        private void EnsureContext()
        {
            if (Context == null)
                CreateContext();
        }

        private void CreateRenderTargetView()
        {
            EnsureDevice();
            EnsureSwapChain();
            using (var backBuffer = SwapChain.GetBackBuffer<Texture2D>(0))
            {
                RenderTargetView = new RenderTargetView(Device, backBuffer);
            }
        }

        private void EnsureRenderTargetView()
        {
            if (RenderTargetView == null)
                CreateRenderTargetView();
        }

        private void ConnectRenderTargetView()
        {
            EnsureContext();
            EnsureRenderTargetView();
            Context.OutputMerger.SetRenderTargets(RenderTargetView);
        }

        private void DisconnectRenderTargetView()
        {
            if (Context == null) return;
            if (RenderTargetView == null) return;
            Context.ClearRenderTargetView(RenderTargetView, new RawColor4(0, 0, 0, 1));
            Context.OutputMerger.SetRenderTargets((RenderTargetView) null);
        }
    }
}