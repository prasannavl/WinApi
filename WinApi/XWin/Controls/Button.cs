using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApi.User32;

namespace WinApi.XWin.Controls
{
    public class Button : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory = new Lazy<WindowFactory>(() => WindowFactory.CreateForExistingClass("button"));
        protected Button() { }

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new ButtonConstructionParams();

        public static Button Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new Button(), text, styles, exStyles, x, y, width,
                height, hParent, hMenu);
        }

        public class ButtonConstructionParams : VisibleChildConstructionParams
        {
            public override int Width => 200;
            public override int Height => 35;
            public override WindowExStyles ExStyles => 0;
        }
    }
}
