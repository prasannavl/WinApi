using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.XWin;
using WinApi.XWin.Controls;
using WinApi.XWin.Helpers;

namespace Sample.Simple
{
    class Program
    {
        [HandleProcessCorruptedStateExceptions]
        static int Main(string[] args)
        {
            ApplicationHelpers.SetupDefaultExceptionHandlers();
            try
            {
                // Use window color brush to emulate Win Forms like behaviour
                var factory = WindowFactory.Create(hBgBrush: new IntPtr((int) SystemColor.COLOR_WINDOW));
                using (var win = Window.Create<SampleWindow>(factory: factory, text: "Hello"))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                ApplicationHelpers.ShowCriticalError(ex);
            }
            return 0;
        }

        public sealed class SampleWindow : Window
        {
            private NativeWindow m_textBox;
            private Rectangle m_textBoxPadding;

            protected override bool OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
            {
                var containerRect = GetClientRect();
                m_textBoxPadding = new Rectangle(10, 10, 10, 10);
                var textBoxRect = GetRectWithPadding(ref containerRect, ref m_textBoxPadding);

                m_textBox = StaticBox.Create(
                    styles: WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD,
                    exStyles: WindowExStyles.WS_EX_STATICEDGE,
                    hParent: Handle,
                    width: textBoxRect.Width, height: textBoxRect.Height, x: textBoxRect.Left, y: textBoxRect.Top);

                return base.OnCreate(ref msg, ref createStruct);
            }

            private static Rectangle GetRectWithPadding(ref Rectangle containerRect, ref Rectangle controlRect)
            {
                return RectangleHelpers.CreateFrom(ref containerRect, ref controlRect,
                    (l, r) => l + r,
                    (l, r) => l - r);
            }

            protected override void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags)
            {
                m_textBox.SetText($"{{ X: {point.X}, Y: {point.Y}, Flags: {flags} }}");
                base.OnMouseMove(ref msg, ref point, flags);
            }

            protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
            {
                var containerRect = GetClientRect();
                var textBoxRect = GetRectWithPadding(ref containerRect, ref m_textBoxPadding);
                var textBoxSize = textBoxRect.GetSize();
                m_textBox.SetSize(ref textBoxSize);
                base.OnSize(ref msg, flag, ref size);
            }
        }
    }
}