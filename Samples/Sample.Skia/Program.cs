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
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.XWin;
using WinApi.Desktop;
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
                // Using it without a dependency on WinApi.XWin.Controls
                var factory = WindowFactory.Create();
                using (var win = factory.CreateWindow(() => new SkiaWindow(), "Hello"))
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

    public sealed class SkiaWindow : SkiaWindowBase
    {
        protected override void OnSkiaPaint(SKSurface surface)
        {
            var canvas = surface.Canvas;
            canvas.Clear(new SKColor(70, 120, 110, 200));
            base.OnSkiaPaint(surface);
        }
    }
}