using System;
using SharpDX.DirectComposition;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using Device = SharpDX.DirectComposition.Device;
using Surface = SharpDX.DirectComposition.Surface;

namespace WinApi.DxUtils.Composition
{
    public class WindowSurfaceCompositor : WindowCompositor<IDxgi1Container, WindowSurfaceCompositorOptions>
    {
        private Surface m_surface;
        public WindowSurfaceCompositor(int variant = -1) : base(variant) {}

        public Surface Surface
        {
            get { return m_surface; }
            set { m_surface = value; }
        }

        public Size Size
        {
            get { return Options.Size; }
            set { Options.Size = value; }
        }

        public void EnsureInitialized(Size size)
        {
            EnsureInitialized();
            if (Size != size)
                Resize(size);
        }

        protected override void InitializeResources()
        {
            base.InitializeResources();
            CreateSurface();
        }

        public void Resize(Size size)
        {
            if (Options.Size != size)
            {
                Options.Size = size;
                DestroySurface();
                CreateSurface();
            }
        }

        private void CreateSurface()
        {
            var size = Options.Size;
            Surface = CompositionHelper.CreateSurface(Device, DeviceVariant, size.Width, size.Height,
                Options.PixelFormat, Options.AlphaMode);
            SetContent(Surface);
        }

        private void DestroySurface()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_surface);
        }

        protected override void DestroyResources()
        {
            DestroySurface();
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
            Size = size;
            PixelFormat = format;
            AlphaMode = mode;
        }
    }
}