using System;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;
using Factory = SharpDX.Direct2D1.Factory;

namespace WinApi.DxUtils.D2D1
{
    public class D2DMetaResource : ID2D1MetaResourceImpl
    {
        private RenderTarget m_renderTarget;
        private readonly CreationProperties m_creationProperties;
        private readonly RenderTargetProperties m_renderTargetProps;
        private Factory m_factory;
        private bool m_isDisposed;
        private IDxgi1Container m_dxgiContainer;

        public D2DMetaResource(CreationProperties props, RenderTargetProperties renderTargetProps)
        {
            m_creationProperties = props;
            m_renderTargetProps = renderTargetProps;
        }

        public RenderTarget RenderTarget
        {
            get { return m_renderTarget; }
            private set { m_renderTarget = value; }
        }

        public Factory Factory
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

        public void Initialize(IDxgi1Container dxgiContainer, bool autoLink = false)
        {
            CheckDisposed();
            m_dxgiContainer = dxgiContainer;
            if (autoLink) dxgiContainer.AddLinkedResource(this);
        }

        public void Destroy()
        {
            m_dxgiContainer?.RemoveLinkedResource(this);
            DisconnectContextFromDxgiSurface();
            DisposableHelpers.DisposeAndSetNull(ref m_renderTarget);
            DisposableHelpers.DisposeAndSetNull(ref m_factory);
        }

        private void CreateFactory()
        {
            var props = m_creationProperties;
            Factory = new Factory((FactoryType) props.ThreadingMode, props.DebugLevel);
        }

        private void EnsureFactory()
        {
            if (Factory == null)
                CreateFactory();
        }


        private void ConnectContextToDxgiSurface()
        {
            EnsureFactory();
            using (var surface = m_dxgiContainer.SwapChain.GetBackBuffer<Surface>(0))
            {
                RenderTarget = new RenderTarget(this.Factory, surface, m_renderTargetProps);
            }
        }

        private void DisconnectContextFromDxgiSurface()
        {
            var context = RenderTarget;
            if (context != null)
            {
                context.Dispose();
                RenderTarget = null;
            }
        }

        private void CheckDisposed()
        {
            if (m_isDisposed) throw new ObjectDisposedException(nameof(D2DMetaResource));
        }

        ~D2DMetaResource()
        {
            Dispose();
        }
    }
}