using System;
using SharpDX;
using SharpDX.Direct3D11;
using WinApi.Core;
using WinApi.DxUtils.Composition;
using WinApi.DxUtils.Core;
using WinApi.DxUtils.D3D11;
using NetCoreEx.Geometry;

namespace WinApi.DxUtils.Component
{
    public class ComposedD3DComponent : IDisposable
    {
        public WindowSwapChainCompositor Compositor;
        public IntPtr Hwnd;
        private int m_compVariant;
        private ID3D11_1MetaResource m_d3D;
        public Size Size;

        public ID3D11MetaResourceCore D3D => this.m_d3D;

        public bool IsInitialized => this.m_d3D?.Device != null;

        public void Dispose()
        {
            DisposableHelpers.DisposeAndSetNull(ref this.Compositor);
            DisposableHelpers.DisposeAndSetNull(ref this.m_d3D);
        }

        public void Initialize(IntPtr hwnd, Size size, int directCompositionVariant = -1)
        {
            if (this.IsInitialized) this.Destroy();
            this.Hwnd = hwnd;
            this.Size = size;
            this.m_compVariant = directCompositionVariant != -1
                ? directCompositionVariant
                : CompositionHelper.GetVariantForPlatform();
            this.Create();
            this.InitializeInternal();
        }

        public void EnsureInitialized()
        {
            if (!this.IsInitialized) this.Initialize(this.Hwnd, this.Size, this.m_compVariant);
        }

        public void Resize(Size size)
        {
            if ((this.m_compVariant > 0) && ((size.Width <= 0) || (size.Height <= 0))) return;
            this.Size = size;
            this.m_d3D?.Resize(size);
        }

        private void InitializeInternal()
        {
            this.m_d3D.Initialize(this.Hwnd, this.Size);
            if (this.m_compVariant > 0)
                this.Compositor.Initialize(this.m_d3D,
                    new WindowCompositorOptions(this.Hwnd));
        }

        private void Create()
        {
            var d3dCreationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
            this.m_d3D = this.m_compVariant > 0
                ? D3D11MetaFactory.CreateForComposition(creationFlags: d3dCreationFlags)
                : D3D11MetaFactory.CreateForWindowTarget(creationFlags: d3dCreationFlags);
            this.Compositor = new WindowSwapChainCompositor(this.m_compVariant);
        }

        public bool PerformResetOnException(SharpDXException ex)
        {
            if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor))
            {
                this.m_d3D?.Destroy();
                return true;
            }
            return false;
        }

        public void Destroy()
        {
            this.Compositor?.Destroy();
            this.m_d3D?.Destroy();
        }
    }
}