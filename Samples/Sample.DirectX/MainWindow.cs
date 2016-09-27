using System;
using System.Threading.Tasks;
using WinApi.Core;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.XWin;

namespace Sample.DirectX
{
    public sealed class MainWindow : EventedWindowCore
    {
        private readonly IGraphicsContext m_graphicsContext = Kernel32Helpers.IsWin8OrGreater()
            ? (IGraphicsContext)new D2DGraphicsContext()
            : new D2DRenderTargetGraphicsContext();

        protected override bool OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            var size = GetClientSize();

//            if (Environment.OSVersion.Version.Major > 6)
//                User32ExperimentalHelpers.EnableBlurBehind(Handle);

            m_graphicsContext.Init(Handle, ref size);
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            m_graphicsContext.Draw();
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            m_graphicsContext.Resize(ref size);
        }

        protected override void Dispose(bool disposing)
        {
            m_graphicsContext.Dispose();
            base.Dispose(disposing);
        }
    }
}