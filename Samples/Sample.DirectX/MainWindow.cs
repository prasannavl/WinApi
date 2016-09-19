using System;
using WinApi.Core;
using WinApi.User32;
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