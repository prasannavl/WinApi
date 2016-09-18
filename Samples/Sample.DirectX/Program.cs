using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using WinApi.Gdi32;
using WinApi.XWin;

namespace Sample.Win32
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create(className: "MainWindow");
            using (var win = factory.CreateFrameWindow<WinApi.XWin.MainWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }
    }
}