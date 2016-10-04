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
        // STA not strictly required for simple applications
        // that doesn't use COM, but just keeping up with convention here.
        [STAThread]
        static int Main(string[] args)
        {
            using (var win = Window.Create<AppWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop().Run(win);
            }
        }
    }

    public class AppWindow : Window
    {
        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            PaintStruct ps;
            hdc = User32Methods.BeginPaint(Handle, out ps);
            User32Methods.FillRect(hdc, ref ps.PaintRect,
                Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
            User32Methods.EndPaint(Handle, ref ps);

            // Prevent default processing. Not actually
            // required here. This is one of the reasons msg ref is 
            // always passed along to all message loop events.
            msg.SetHandled();
        }

        protected override void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                // Note: OnEraseBkgnd method is already available in 
                // WindowBase, but directly interception here
                // just for the sake of overriding the
                // message loop.
                // Also, note that the message loop is 
                // short-cicuited here.

                case WM.ERASEBKGND:
                {
                    // I can even build the loop only on pay-per-use
                    // basis, when I need it since all the default methods
                    // are publicly, exposed with the MessageHandlers class.
                    //
                    // MessageHandlers.OnEraseBkgnd(this, ref msg);
                    // return;

                    msg.Result = new IntPtr(1);
                    msg.SetHandled();
                    return;
                }
            }
            base.OnMessage(ref msg);
        }
    }
}