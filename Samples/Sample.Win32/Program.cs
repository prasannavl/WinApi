using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinApi.Gdi32;
using WinApi.Kernel32;
using WinApi.User32;

namespace Sample.Win32
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var instanceHandle = Kernel32Methods.GetModuleHandle(IntPtr.Zero);

            var wc = new WindowClassEx
            {
                Size = (uint) Marshal.SizeOf<WindowClassEx>(),
                ClassName = "MainWindow",
                CursorHandle = User32Helpers.LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW),
                IconHandle = User32Helpers.LoadIcon(IntPtr.Zero, SystemIcon.IDI_APPLICATION),
                Styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                BackgroundBrushHandle = new IntPtr((int) StockObject.WHITE_BRUSH),
                WindowProc = WindowProc,
                InstanceHandle = instanceHandle
            };

            var resReg = User32Methods.RegisterClassEx(ref wc);
            if (resReg == 0)
            {
                Console.Error.WriteLine("registration failed");
                return -1;
            }

            var hwnd = User32Methods.CreateWindowEx(WindowExStyles.WS_EX_APPWINDOW,
                wc.ClassName, "Hello", WindowStyles.WS_OVERLAPPEDWINDOW,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                IntPtr.Zero, IntPtr.Zero, instanceHandle, IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                Console.Error.WriteLine("window creation failed");
                return -1;
            }

            User32Methods.ShowWindow(hwnd, ShowWindowCommands.SW_SHOWNORMAL);
            User32Methods.UpdateWindow(hwnd);

            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                User32Methods.TranslateMessage(ref msg);
                User32Methods.DispatchMessage(ref msg);
            }

            return res;
        }

        private static IntPtr WindowProc(IntPtr hwnd, uint umsg,
            IntPtr wParam, IntPtr lParam)
        {
            var msg = (WM) umsg;
            switch (msg)
            {
                case WM.ERASEBKGND:
                    return new IntPtr(1);
                case WM.CLOSE:
                {
                    User32Methods.PostQuitMessage(0);
                    break;
                }
                case WM.PAINT:
                {
                    PaintStruct ps;
                    var hdc = User32Methods.BeginPaint(hwnd, out ps);
                    User32Methods.FillRect(hdc, ref ps.PaintRect,
                        Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
                    User32Methods.EndPaint(hwnd, ref ps);
                    break;
                }
            }
            return User32Methods.DefWindowProc(hwnd, umsg, wParam, lParam);
        }
    }
}