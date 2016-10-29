using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectComposition;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using AlphaMode = SharpDX.DXGI.AlphaMode;
using Device = SharpDX.DirectComposition.Device;

namespace WinApi.DxUtils.Composition
{
    public class WindowCompositor<TDxgiContainer, TOptions> :
            CompositorCore<TDxgiContainer, TOptions>
        where TDxgiContainer : IDxgi1Container
        where TOptions : WindowCompositorOptions
    {
        private Target m_target;
        private Visual m_visual;
        public WindowCompositor(int variant = -1) : base(variant) {}

        public Target Target { get { return this.m_target; } private set { this.m_target = value; } }

        public Visual Visual { get { return this.m_visual; } private set { this.m_visual = value; } }

        protected override void InitializeResources()
        {
            this.EnsureDevice();
            var opts = this.Options;
            if (this.DeviceVariant > 1)
            {
                var device = (DesktopDevice) this.Device;
                this.Target = Target.FromHwnd(device, opts.Hwnd, opts.IsTopMost);
                this.Visual = new Visual2(device);
            }
            if (this.DeviceVariant == 1)
            {
                var device = (Device) this.Device;
                this.Target = Target.FromHwnd(device, opts.Hwnd, opts.IsTopMost);
                this.Visual = new Visual(device);
            }
        }

        public void SetContent(ComObject rootContent)
        {
            if (this.Target != null)
            {
                this.Visual.Content = rootContent;
                this.Target.Root = this.Visual;
            }
        }

        protected override void DestroyResources()
        {
            DisposableHelpers.DisposeAndSetNull(ref this.m_visual);
            DisposableHelpers.DisposeAndSetNull(ref this.m_target);
        }
    }

    public class WindowSwapChainCompositor : WindowCompositor<IDxgi1ContainerWithSwapChain, WindowCompositorOptions>
    {
        public WindowSwapChainCompositor(int variant = -1) : base(variant) {}

        protected override void InitializeResources()
        {
            base.InitializeResources();
            this.SetContent(this.DxgiContainer.SwapChain);
            this.Commit();
        }
    }

    public class WindowCompositorOptions
    {
        public IntPtr Hwnd;
        public bool IsTopMost;

        public WindowCompositorOptions(IntPtr hwnd, bool isTopMost = false)
        {
            this.Hwnd = hwnd;
            this.IsTopMost = isTopMost;
        }
    }
}