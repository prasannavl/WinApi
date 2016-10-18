using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct3D11;
using WinApi.Core;
using WinApi.DxUtils.Composition;
using WinApi.DxUtils.Core;
using WinApi.DxUtils.D2D1;
using WinApi.DxUtils.D3D11;
using Factory = SharpDX.DirectWrite.Factory;
using FactoryType = SharpDX.DirectWrite.FactoryType;

namespace WinApi.DxUtils.Component
{
    public class Dx11Component : IDisposable
    {
        public IntPtr Hwnd;
        private ID2D1_1MetaResourceImpl<IDxgi1_2ContainerWithSwapChain> m_d2D;
        private ID3D11_1MetaResourceImpl m_d3D;
        private int m_compVariant;
        private Factory m_dWriteFactory;
        public WindowSwapChainCompositor Compositor;
        public Size Size;

        public ID2D1_1MetaResource D2D => m_d2D;
        public ID3D11MetaResource D3D => m_d3D;
        public Factory TextFactory => m_dWriteFactory;

        public void Dispose()
        {
            Destroy();
        }

        public void Initialize(IntPtr hwnd, Size size, int directCompositionVariant = -1)
        {
            if (m_d3D != null)
                Destroy();
            Hwnd = hwnd;
            Size = size;
            m_compVariant = directCompositionVariant != -1
                ? directCompositionVariant
                : CompositionHelper.GetVariantForPlatform();
            Create();
            InitializeInternal();
        }

        public void EnsureInitialized()
        {
            if (m_d3D?.Device == null)
                Initialize(Hwnd, Size, m_compVariant);
        }

        public void Resize(Size size)
        {
            if ((m_compVariant > 0) && ((size.Width <= 0) || (size.Height <= 0))) return;
            Size = size;
            m_d3D?.Resize(size);
        }

        private void InitializeInternal()
        {
            m_d3D.Initialize(Hwnd, Size);
            m_d2D.Initialize(m_d3D);
            if (m_compVariant > 0)
                Compositor.Initialize(m_d3D,
                    new WindowCompositorOptions(Hwnd));
        }

        private void Create()
        {
            var d3dCreationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
            m_d3D = m_compVariant > 0
                ? D3D11MetaFactory.CreateForComposition(creationFlags: d3dCreationFlags)
                : D3D11MetaFactory.CreateForWindowTarget(creationFlags: d3dCreationFlags);
            m_d2D = D2D1MetaFactory.CreateForSwapChain();
            m_dWriteFactory = new Factory(FactoryType.Shared);
            Compositor = new WindowSwapChainCompositor(m_compVariant);
        }

        public bool PerformResetOnException(SharpDXException ex)
        {
            if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor)
                || ErrorHelpers.ShouldResetD2DForError(ex.Descriptor))
            {
                Destroy();
                return true;
            }
            return false;
        }

        public void Destroy()
        {
            DisposableHelpers.DisposeAndSetNull(ref Compositor);
            DisposableHelpers.DisposeAndSetNull(ref m_dWriteFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_d2D);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
        }
    }
}