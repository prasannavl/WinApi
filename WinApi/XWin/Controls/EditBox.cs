using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApi.User32;

namespace WinApi.XWin.Controls
{
    public class EditBox : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory = new Lazy<WindowFactory>(() => WindowFactory.CreateForExistingClass("edit"));
        protected EditBox() { }

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new VisibleChildConstructionParams();

        public static EditBox Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new EditBox(), text, styles, exStyles, x, y, width,
                height, hParent, hMenu);
        }
    }
}
