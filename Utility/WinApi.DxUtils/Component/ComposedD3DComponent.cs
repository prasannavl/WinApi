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
    public class ComposedD3DComponent : IDisposable
    {
        public IntPtr Hwnd;
        private ID3D11_1MetaResourceImpl m_d3D;
        private int m_compVariant;
        public WindowSwapChainCompositor Compositor;
        public Size Size;

        public ID3D11MetaResource D3D => m_d3D;

        public void Dispose()
        {
            DisposableHelpers.DisposeAndSetNull(ref Compositor);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
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
            Compositor = new WindowSwapChainCompositor(m_compVariant);
        }

        public bool PerformResetOnException(SharpDXException ex)
        {
            if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor))
            {
                m_d3D?.Destroy();
                return true;
            }
            return false;
        }

        public void Destroy()
        {
            Compositor?.Destroy();
            m_d3D?.Destroy();
        }
    }
}