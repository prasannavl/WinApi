using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WinApi.Core;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.XWin;
using WinApi.XWin.Helpers;

namespace Sample.Simple
{
    class Program
    {
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create(
                className: "MainWindow", 
                // Use window color brush to emulate Win Forms like behaviour. 
                hBgBrush: new IntPtr((int)SystemColor.COLOR_WINDOW));
            using (var win = factory.CreateWindow(() => new SampleWindow(), text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }

        public sealed class SampleWindow : Window
        {
            private NativeWindow m_textBox;
            private Rectangle m_textBoxMargin;

            protected override bool OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
            {
                var containerRect = GetClientRect();
                m_textBoxMargin = new Rectangle(10, 10,10, 10);
                var textBoxRect = GetRectWithMargin(ref containerRect, ref m_textBoxMargin);

                var hTextBox = User32Methods.CreateWindowEx(
                    lpClassName: "static",
                    dwStyle: WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD,
                    dwExStyle: WindowExStyles.WS_EX_STATICEDGE,
                    hwndParent: Handle,
                    lpWindowName: "Log info",
                    x: textBoxRect.Left, y: textBoxRect.Top, nWidth: textBoxRect.Width, nHeight: textBoxRect.Height,
                    hMenu: IntPtr.Zero, hInstance: WindowFactory.FactoryCache.Instance.ProcessHandle, lpParam: IntPtr.Zero);

                m_textBox = WindowFactory.CreateWindowFromHandle(hTextBox);

                return base.OnCreate(ref msg, ref createStruct);
            }

            private static Rectangle GetRectWithMargin(ref Rectangle containerRect, ref Rectangle controlRect)
            {
                return RectangleHelpers.CreateFrom(ref containerRect, ref controlRect,
                    (l, r) => l + r,
                    (l, r) => l - r);
            }

            protected override void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags)
            {
                m_textBox.SetText($"{{ X: {point.X}, Y: {point.Y}, Flags: {flags.ToString()} }}");
                base.OnMouseMove(ref msg, ref point, flags);
            }

            protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
            {
                var containerRect = GetClientRect();
                var textBoxRect = GetRectWithMargin(ref containerRect, ref m_textBoxMargin);
                var textBoxSize = textBoxRect.GetSize();
                m_textBox.SetSize(ref textBoxSize);
                base.OnSize(ref msg, flag, ref size);
            }
        }
    }
}
