using System;
using System.Diagnostics.CodeAnalysis;
using WinApi.User32;

namespace WinApi.Windows.Controls
{
    public class StaticBox : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory =
            new Lazy<WindowFactory>(() => WindowFactory.CreateForExistingClass("static"));

        protected StaticBox() {}

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new VisibleChildConstructionParams();

        public static StaticBox Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null, StaticStyles? controlStyles = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindowEx(() => new StaticBox(), text, styles, exStyles, x, y,
                width,
                height, hParent, hMenu, (uint) (controlStyles ?? 0));
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [Flags]
        public enum StaticStyles
        {
            SS_LEFT = 0x00000000,
            SS_CENTER = 0x00000001,
            SS_RIGHT = 0x00000002,
            SS_ICON = 0x00000003,
            SS_BLACKRECT = 0x00000004,
            SS_GRAYRECT = 0x00000005,
            SS_WHITERECT = 0x00000006,
            SS_BLACKFRAME = 0x00000007,
            SS_GRAYFRAME = 0x00000008,
            SS_WHITEFRAME = 0x00000009,
            SS_USERITEM = 0x0000000A,
            SS_SIMPLE = 0x0000000B,
            SS_LEFTNOWORDWRAP = 0x0000000C,
            SS_OWNERDRAW = 0x0000000D,
            SS_BITMAP = 0x0000000E,
            SS_ENHMETAFILE = 0x0000000F,
            SS_ETCHEDHORZ = 0x00000010,
            SS_ETCHEDVERT = 0x00000011,
            SS_ETCHEDFRAME = 0x00000012,
            SS_TYPEMASK = 0x0000001F,
            SS_REALSIZECONTROL = 0x00000040,
            SS_NOPREFIX = 0x00000080 /* Don't do "&" character translation */,
            SS_NOTIFY = 0x00000100,
            SS_CENTERIMAGE = 0x00000200,
            SS_RIGHTJUST = 0x00000400,
            SS_REALSIZEIMAGE = 0x00000800,
            SS_SUNKEN = 0x00001000,
            SS_EDITCONTROL = 0x00002000,
            SS_ENDELLIPSIS = 0x00004000,
            SS_PATHELLIPSIS = 0x00008000,
            SS_WORDELLIPSIS = 0x0000C000,
            SS_ELLIPSISMASK = 0x0000C000
        }
    }
}