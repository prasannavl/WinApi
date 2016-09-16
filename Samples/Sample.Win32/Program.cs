using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using WinApi.XWin;

namespace Sample.Win32
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create("MainWindow");
            using (var win = factory.CreateFrameWindow<MainWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }
    }
}