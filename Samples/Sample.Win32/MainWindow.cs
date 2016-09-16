using System;
using WinApi.Core;
using WinApi.User32;
using WinApi.XWin;

namespace Sample.Win32
{
    public class MainWindow : MainWindowBase
    {
        private GfxContext gfxContext;

        protected override void OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            base.OnCreate(ref msg, ref createStruct);
            gfxContext = new GfxContext();
            Rectangle rect;
            this.GetClientRectangle(out rect);
            var s = new Size() { Height = rect.Height, Width = rect.Width };
            gfxContext.Init(Handle, ref s);
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            gfxContext.BeginDraw();
            gfxContext.Clear();
            gfxContext.EndDraw();
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            gfxContext.Resize(ref size);
        }

        protected override int OnEraseBkgnd(ref WindowMessage msg, IntPtr hdc)
        {
            msg.SetHandled();
            return 1;
        }

        private int x = 0;

        protected override void OnMessage(ref WindowMessage msg)
        {
            Console.WriteLine(x++ + ": " + msg.Id.ToString());
            base.OnMessage(ref msg);
        }
    }
}