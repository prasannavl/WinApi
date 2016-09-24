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

        public static Window Create()
        {
            var factory = ClassFactory.Value;
            return factory.CreateWindow(() => new Window());
        }

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new FrameWindowConstructionParams();
        protected Window() {}
    }
}