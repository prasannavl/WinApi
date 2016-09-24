using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinApi.Gdi32;
using WinApi.User32;

namespace WinApi.XWin
{
    public class Window : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory = new Lazy<WindowFactory>(() => WindowFactory.Create());
        protected Window() {}

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new FrameWindowConstructionParams();

        public static Window Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new Window(), text, styles, exStyles, x, y, width,
                height, hParent, hMenu);
        }

        public static TWindow Create<TWindow>(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
            where TWindow : WindowCore, new()
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new TWindow(), null, text, styles, exStyles, x, y,
                width, height, hParent,
                hMenu);
        }
    }
}