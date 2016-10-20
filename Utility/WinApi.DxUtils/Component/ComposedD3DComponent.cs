using System;
using SharpDX;
using SharpDX.Direct3D11;
using WinApi.Core;
using WinApi.DxUtils.Composition;
using WinApi.DxUtils.Core;
using WinApi.DxUtils.D3D11;

namespace WinApi.DxUtils.Component
{
    public class ComposedD3DComponent : IDisposable
    {
        public WindowSwapChainCompositor Compositor;
        public IntPtr Hwnd;
        private int m_compVariant;
        private ID3D11_1MetaResource m_d3D;
        public Size Size;

        public ID3D11MetaResourceCore D3D => m_d3D;

        public bool IsInitialized => m_d3D?.Device != null;

        public void Dispose()
        {
            DisposableHelpers.DisposeAndSetNull(ref Compositor);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
        }

        public void Initialize(IntPtr hwnd, Size size, int directCompositionVariant = -1)
        {
            if (IsInitialized)
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
            if (!IsInitialized)
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