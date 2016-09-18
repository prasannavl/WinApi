using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApi.User32;
using WinApi.XWin;

namespace Sample.Simple
{
    class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create(
                className: "MainWindow", 
                // Use window color brush to emulate Win Forms like behaviour. 
                hBgBrush: new IntPtr((int)SystemColor.COLOR_WINDOW));
            using (var win = factory.CreateFrameWindow<MainWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }
    }
}
