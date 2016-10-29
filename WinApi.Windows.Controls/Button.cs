using System;
using System.Diagnostics.CodeAnalysis;
using WinApi.User32;

namespace WinApi.Windows.Controls
{
    public class Button : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory =
            new Lazy<WindowFactory>(() => WindowFactory.CreateForExistingClass("button"));

        protected Button() {}

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new ButtonConstructionParams();

        public static Button Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null, ButtonStyles? controlStyles = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindowEx(() => new Button(), text, styles, exStyles, x, y,
                width,
                height, hParent, hMenu, (uint) (controlStyles ?? 0));
        }

        public class ButtonConstructionParams : VisibleChildConstructionParams
        {
            public override int Width => 200;
            public override int Height => 35;
            public override WindowExStyles ExStyles => 0;
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [Flags]
        public enum ButtonStyles
        {
            BS_PUSHBUTTON = 0x00000000,
            BS_DEFPUSHBUTTON = 0x00000001,
            BS_CHECKBOX = 0x00000002,
            BS_AUTOCHECKBOX = 0x00000003,
            BS_RADIOBUTTON = 0x00000004,
            BS_3STATE = 0x00000005,
            BS_AUTO3STATE = 0x00000006,
            BS_GROUPBOX = 0x00000007,
            BS_USERBUTTON = 0x00000008,
            BS_AUTORADIOBUTTON = 0x00000009,
            BS_PUSHBOX = 0x0000000A,
            BS_OWNERDRAW = 0x0000000B,
            BS_TYPEMASK = 0x0000000F,
            BS_LEFTTEXT = 0x00000020,
            BS_TEXT = 0x00000000,
            BS_ICON = 0x00000040,
            BS_BITMAP = 0x00000080,
            BS_LEFT = 0x00000100,
            BS_RIGHT = 0x00000200,
            BS_CENTER = 0x00000300,
            BS_TOP = 0x00000400,
            BS_BOTTOM = 0x00000800,
            BS_VCENTER = 0x00000C00,
            BS_PUSHLIKE = 0x00001000,
            BS_MULTILINE = 0x00002000,
            BS_NOTIFY = 0x00004000,
            BS_FLAT = 0x00008000,
            BS_RIGHTBUTTON = BS_LEFTTEXT
        }
    }
}