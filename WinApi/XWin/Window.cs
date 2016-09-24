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
    public class FrameWindowConstructionParams : ConstructionParams
    {
        public override WindowStyles Styles
            => WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS;

        public override WindowExStyles ExStyles
            => WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE;
    }

    public class Window : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> Factory = new Lazy<WindowFactory>(() => WindowFactory.Create());

        public static Window Create()
        {
            var factory = Factory.Value;
            return factory.CreateWindow(() => new Window());
        }

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new FrameWindowConstructionParams();
        protected Window() {}
    }
}
