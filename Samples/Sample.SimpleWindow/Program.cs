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
        protected override void OnPaint(ref PaintPacket packet)
        {
            PaintStruct ps;
            var hdc = this.BeginPaint(out ps);
            User32Methods.FillRect(hdc, ref ps.PaintRect,
                Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
            this.EndPaint(ref ps);
            base.OnPaint(ref packet);
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
                    // basis, when I need it since all the Packets decoding,
                    // and encoding are cleanly abstracted away into the Packet
                    // structs itself.
                    //
                    // fixed (var msgPtr = &msg)
                    // {
                    //    var packet = new EraseBkgndPacket(msg);
                    //    // Do anything you want with the packet.
                    // }
                    // return;

                    msg.Result = new IntPtr(1);
                    return;
                }
            }
            base.OnMessage(ref msg);
        }
    }
}