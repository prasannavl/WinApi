using System;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.XWin;
using WinApi.Desktop;
using WinApi.Gdi32;
using WinApi.XWin.Helpers;

namespace Sample.DirectX
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ApplicationHelpers.SetupDefaultExceptionHandlers();
                var cache = WindowFactory.Cache.Instance;
                var factory = new WindowFactory("MainWindow",
                    WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                    cache.ProcessHandle, IntPtr.Zero, cache.ArrowCursorHandle, IntPtr.Zero, null);

                // Create the window without a dependency on WinApi.XWin.Controls
                using (
                    var win = factory.CreateWindow(() => new MainWindow(), "Hello",
                        constructionParams: new FrameWindowConstructionParams()))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelpers.ShowError(ex);
                return 1;
            }
        }
    }
}