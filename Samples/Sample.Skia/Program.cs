using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using SkiaSharp;
using WinApi.DwmApi;
using WinApi.User32;
using WinApi.UxTheme;
using System.Linq;
using WinApi.Desktop;
using WinApi.Gdi32;
using WinApi.User32.Experimental;
using WinApi.Windows;
using WinApi.Windows.Helpers;
using NetCoreEx.Geometry;

namespace Sample.Skia
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ApplicationHelpers.SetupDefaultExceptionHandlers();
                // Using it without a dependency on WinApi.Windows.Controls
                var factory = WindowFactory.Create(hBgBrush: IntPtr.Zero);
                using (var win = factory.CreateWindow(() => new SkiaWindow(), "Hello",
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

    public sealed class SkiaWindow : SkiaWindowBase
    {
        protected override void OnSkiaPaint(SKSurface surface)
        {
            var windowRect = this.GetWindowRect();
            var clientRect = new Rectangle(windowRect.Width, windowRect.Height);
            var canvas = surface.Canvas;
            canvas.Clear(new SKColor(120, 50, 70, 200));
            var textPainter = new SKPaint {TextSize = 35, IsAntialias = true};
            var str = "Hello there!";
            var textBounds = new SKRect();
            var m = textPainter.MeasureText(str, ref textBounds);
            canvas.DrawText(str, (clientRect.Width - textBounds.Width)/2, (clientRect.Height - textBounds.Height)/2,
                textPainter);

            base.OnSkiaPaint(surface);
        }
    }
}