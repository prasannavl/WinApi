using System;
using SharpDX.DirectComposition;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.Composition
{

    public class WindowCompositor :
        CompositorCore<IDxgi1ContainerWithSwapChain, WindowCompositorOptions>
    {
        public WindowCompositor(int variant = -1) : base(variant) { }

        private Target m_target;
        private Visual m_visual;

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
                var device = (DesktopDevice)Device;
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

        protected override void DestroyResources()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_visual);
            DisposableHelpers.DisposeAndSetNull(ref m_target);
        }
    }

    public class WindowSwapChainCompositor : WindowCompositor
    {
        public WindowSwapChainCompositor(int variant = -1) : base(variant) { }

        protected override void InitializeResources()
        {
            base.InitializeResources();
            Compose();
        }

        private void Compose()
        {
            if (Target != null)
            {
                Visual.Content = DxgiContainer.SwapChain;
                Target.Root = Visual;
                if (DeviceVariant > 1) ((DesktopDevice)Device).Commit();
                else if (DeviceVariant == 1) ((Device)Device).Commit();
            }
        }
    }

    public struct WindowCompositorOptions
    {
        public WindowCompositorOptions(IntPtr hwnd, bool isTopMost = false)
        {
            Hwnd = hwnd;
            IsTopMost = isTopMost;
        }

        public IntPtr Hwnd;
        public bool IsTopMost;
    }
}