using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApi.User32;

namespace WinApi.XWin.Controls
{
    public class StaticBox : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory = new Lazy<WindowFactory>(() => WindowFactory.CreateForExistingClass("static"));
        protected StaticBox() { }

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new VisibleChildConstructionParams();

        public static StaticBox Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new StaticBox(), text, styles, exStyles, x, y, width,
                height, hParent, hMenu);
        }
    }
}
