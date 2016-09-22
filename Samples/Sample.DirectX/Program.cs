using System;
using WinApi.Desktop;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.XWin;
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
                var cache = WindowFactory.FactoryCache.Instance;
                var factory = new WindowFactory("MainWindow",
                    WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                    cache.ProcessHandle, IntPtr.Zero, cache.ArrowCursorHandle, IntPtr.Zero, null);
                using (
                    var win = factory.CreateFrameWindow<MainWindow>(text: null,
                        exStyles:
                        WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE |
                        WindowExStyles.WS_EX_DLGMODALFRAME))
                {
                    win.Show();
                    return new EventLoop(win).Run();
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