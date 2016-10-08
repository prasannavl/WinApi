using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.DwmApi;
using WinApi.Extensions;
using WinApi.Gdi32;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Helpers;

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
                var factory = WindowFactory.Create(hBgBrush: IntPtr.Zero);
                using (var win = Window.Create(factory: factory, text: "Hello"))
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
    }
}