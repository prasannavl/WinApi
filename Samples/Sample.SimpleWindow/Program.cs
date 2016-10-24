using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;

namespace Sample.SimpleWindow
{
    internal class Program
    {
        static int Main(string[] args)
        {
            using (var win = Window.Create<AppWindow>("Hello"))
            {
                win.Show();
                return new EventLoop().Run(win);
            }
        }
    }

    public class AppWindow : Window
    {
        protected override void OnPaint(ref WindowMessage msg, IntPtr cHdc)
        {
            PaintStruct ps;
            var hdc = BeginPaint(out ps);
            User32Methods.FillRect(hdc, ref ps.PaintRect,
                Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
            EndPaint(ref ps);
            base.OnPaint(ref msg, cHdc);
        }

        protected override void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                // Note: OnEraseBkgnd method is already available in 
                // EventedWindowCore, but directly intercepted here
                // just for the sake of overriding the
                // message loop.
                // Also, note that the message loop is 
                // short-cicuited here.

                case WM.ERASEBKGND:
                {
                    // I can even build the loop only on pay-per-use
                    // basis, when I need it since all the default methods
                    // are publicly, exposed with the MessageDecoder class.
                    //
                    // MessageDecoder.ProcessEraseBkgnd(ref msg, this.OnEraseBkgnd);
                    // return;

                    msg.Result = new IntPtr(1);
                    return;
                }
            }
            base.OnMessage(ref msg);
        }
    }
}