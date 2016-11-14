using System;
using SharpDX.DirectComposition;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.DirectComposition.Device;
using Surface = SharpDX.DirectComposition.Surface;
using NetCoreEx.Geometry;

namespace WinApi.DxUtils.Composition
{
    public class WindowSurfaceCompositor : WindowCompositor<IDxgi1Container, WindowSurfaceCompositorOptions>
    {
        private Surface m_surface;
        public WindowSurfaceCompositor(int variant = -1) : base(variant) {}

        public Surface Surface { get { return this.m_surface; } set { this.m_surface = value; } }

        public Size Size { get { return this.Options.Size; } set { this.Options.Size = value; } }

        public void EnsureInitialized(Size size)
        {
            this.EnsureInitialized();
            if (this.Size != size) this.Resize(size);
        }

        protected override void InitializeResources()
        {
            base.InitializeResources();
            this.CreateSurface();
        }

        public void Resize(Size size)
        {
            if (this.Options.Size != size)
            {
                this.Options.Size = size;
                this.DestroySurface();
                this.CreateSurface();
            }
        }

        private void CreateSurface()
        {
            var size = this.Options.Size;
            this.Surface = CompositionHelper.CreateSurface(this.Device, this.DeviceVariant, size.Width, size.Height,
                this.Options.PixelFormat, this.Options.AlphaMode);
            this.SetContent(this.Surface);
        }

        private void DestroySurface()
        {
            DisposableHelpers.DisposeAndSetNull(ref this.m_surface);
        }

        protected override void DestroyResources()
        {
            this.DestroySurface();
            base.DestroyResources();
        }
    }

    public class WindowSurfaceCompositorOptions : WindowCompositorOptions
    {
        public AlphaMode AlphaMode;
        public Format PixelFormat;

        public Size Size;

        public WindowSurfaceCompositorOptions(IntPtr hwnd, Size size, Format format, AlphaMode mode,
            bool isTopMost = false) : base(hwnd, isTopMost)
        {
            this.Size = size;
            this.PixelFormat = format;
            this.AlphaMode = mode;
        }
    }
}