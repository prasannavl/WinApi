using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using WinApi.User32.Experimental;
using WinApi.XWin;

namespace Sample.Skia
{
    class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create("MainWindow");
            using (var win = factory.CreateFrameWindow<SkiaAppWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
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
