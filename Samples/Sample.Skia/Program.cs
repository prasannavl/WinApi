using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using WinApi;
using WinApi.Desktop;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.XWin;
using WinApi.XWin.Helpers;

namespace Sample.Skia
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ApplicationHelpers.SetupDefaultExceptionHandlers();

                var factory = WindowFactory.Create("MainWindow");
                using (var win = factory.CreateFrameWindow<SkiaAppWindow>(text: "Hello"))
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

    public class SkiaAppWindow : SkiaMainWindowBase
    {
        protected override void OnSkiaPaint(SKSurface surface)
        {
            var canvas = surface.Canvas;
            canvas.Clear(new SKColor(70, 120, 110, 200));
            base.OnSkiaPaint(surface);
        }
    }
}