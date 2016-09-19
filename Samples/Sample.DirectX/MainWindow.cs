using System;
using System.Threading.Tasks;
using WinApi.Core;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.XWin;

namespace Sample.DirectX
{
    public class MainWindow : MainWindowBase
    {
        private readonly IGraphicsContext m_graphicsContext = new D2DGraphicsContext();

        protected override void OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            base.OnCreate(ref msg, ref createStruct);
            var size = GetClientSize();

            if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && Environment.OSVersion.Version.Major > 6)
                User32ExperimentalHelpers.EnableBlurBehind(Handle);
            m_graphicsContext.Init(Handle, ref size);
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