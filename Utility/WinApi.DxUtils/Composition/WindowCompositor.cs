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

        public Target Target
        {
            get { return m_target; }
            private set { m_target = value; }
        }

        public Visual Visual
        {
            get { return m_visual; }
            private set { m_visual = value; }
        }

        protected override void InitializeResources()
        {
            EnsureDevice();
            var opts = Options;
            if (DeviceVariant > 1)
            {
                var device = (DesktopDevice) Device;
                Target = Target.FromHwnd(device, opts.Hwnd, opts.IsTopMost);
                Visual = new Visual2(device);
            }

            //             TODO: Wait for SharpDX PR to be released before uncommenting the
            //             Windows 8 version of the codepath below.

            //            if (DeviceVariant == 1)
            //            {
            //                var device = (Device) Device;
            //                Target = Target.FromHwnd(device, opts.Hwnd, opts.IsTopMost);
            //                Visual = new Visual(device);
            //            }
        }

        public void SetContent(ComObject rootContent)
        {
            if (Target != null)
            {
                Visual.Content = rootContent;
                Target.Root = Visual;
            }
        }

        protected override void DestroyResources()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_visual);
            DisposableHelpers.DisposeAndSetNull(ref m_target);
        }
    }

    public class WindowSwapChainCompositor : WindowCompositor<IDxgi1ContainerWithSwapChain, WindowCompositorOptions>
    {
        public WindowSwapChainCompositor(int variant = -1) : base(variant) {}

        protected override void InitializeResources()
        {
            base.InitializeResources();
            SetContent(DxgiContainer.SwapChain);
            Commit();
        }
    }

    public class WindowCompositorOptions
    {
        public IntPtr Hwnd;
        public bool IsTopMost;

        public WindowCompositorOptions(IntPtr hwnd, bool isTopMost = false)
        {
            Hwnd = hwnd;
            IsTopMost = isTopMost;
        }
    }
}