using System;
using System.Runtime.ExceptionServices;
using WinApi.Desktop;
using WinApi.User32;
using WinApi.XWin;
using WinApi.XWin.Controls;

namespace WinApi.TestGround
{
    class Program
    {
        [HandleProcessCorruptedStateExceptions]
        static int Main(string[] args)
        {
            ApplicationHelpers.SetupDefaultExceptionHandlers();

            try
            {
                // Use window color brush to emulate Win Forms like behaviour
                var factory = WindowFactory.Create(hBgBrush: new IntPtr((int) SystemColor.COLOR_WINDOW));
                using (var win = Window.Create<TestWindow>(factory: factory, text: "Hello"))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                ApplicationHelpers.ShowCriticalError(ex);
            }
            return 0;
        }

        public sealed class TestWindow : Window
        {

        }
    }
}