using System;
using WinApi.Core;
using WinApi.DwmApi;
using WinApi.User32;
using WinApi.Extensions;

namespace WinApi.XWin
{
    public abstract class WindowBase : WindowCoreBase
    {
        protected override void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.DESTROY:
                {
                    OnDestroy(ref msg);
                    break;
                }
                case WM.CLOSE:
                {
                    OnClose(ref msg);
                    break;
                }
                case WM.TIMECHANGE:
                {
                    OnSystemTimeChange(ref msg);
                    break;
                }
                case WM.CREATE:
                {
                    MessageHandlers.OnCreate(this, ref msg);
                    break;
                }
                case WM.SIZE:
                {
                    MessageHandlers.OnSize(this, ref msg);
                    break;
                }
                case WM.MOVE:
                {
                    MessageHandlers.OnMove(this, ref msg);
                    break;
                }
                case WM.ACTIVATE:
                {
                    MessageHandlers.OnActivate(this, ref msg);
                    break;
                }
                case WM.ERASEBKGND:
                {
                    MessageHandlers.OnEraseBkgnd(this, ref msg);
                    break;
                }
                case WM.PAINT:
                {
                    MessageHandlers.OnPaint(this, ref msg);
                    break;
                }
                case WM.ACTIVATEAPP:
                {
                    MessageHandlers.OnActivateApp(this, ref msg);
                    break;
                }
                case WM.DISPLAYCHANGE:
                {
                    MessageHandlers.OnDisplayChange(this, ref msg);
                    break;
                }
            }
            base.OnMessage(ref msg);
        }

        protected virtual void OnClose(ref WindowMessage msg) {}

        protected virtual int OnEraseBkgnd(ref WindowMessage msg, IntPtr hdc)
        {
            return 0;
        }

        protected virtual void OnDestroy(ref WindowMessage msg) {}
        protected virtual void OnSystemTimeChange(ref WindowMessage msg) {}
        protected virtual void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size) {}
        protected virtual void OnMove(ref WindowMessage msg, ref Point size) {}
        protected virtual void OnCreate(ref WindowMessage msg, ref CreateStruct createStruct) {}
        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized, IntPtr oppositeWindowHandle) {}
        protected virtual void OnPaint(ref WindowMessage msg, IntPtr hdc) {}
        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, long oppositeThreadId) {}

        public static class MessageHandlers
        {
            public static void OnPaint(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnPaint(ref msg, msg.WParam);
            }

            public static void OnEraseBkgnd(WindowBase windowBase, ref WindowMessage msg)
            {
                msg.Result = new IntPtr(windowBase.OnEraseBkgnd(ref msg, msg.WParam));
            }

            public static void OnSize(WindowBase windowBase, ref WindowMessage msg)
            {
                Size size;
                var flag = (WindowSizeFlag) msg.WParam.ToLowInt32();
                msg.LParam.BreakLowInt32To16(out size.Height, out size.Width);
                windowBase.OnSize(ref msg, flag, ref size);
            }

            public static void OnMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakLowInt32To16(out point.Y, out point.X);
                windowBase.OnMove(ref msg, ref point);
            }

            public static unsafe void OnCreate(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnCreate(ref msg, ref *(CreateStruct*) msg.LParam);
            }

            public static void OnActivate(WindowBase windowBase, ref WindowMessage msg)
            {
                int high, low;
                msg.WParam.BreakLowInt32To16(out high, out low);
                var flag = (WindowActivateFlag) low;
                // Note: wParam is unsigned
                var isMinimized = high != 0;
                var oppositeWindowHandle = msg.LParam;
                windowBase.OnActivate(ref msg, flag, isMinimized, oppositeWindowHandle);
            }

            public static void OnActivateApp(WindowBase windowBase, ref WindowMessage msg)
            {
                var isActive = msg.WParam.ToLowInt32() != 0;
                var oppositeThreadId = msg.LParam.ToLowInt32();
                windowBase.OnActivateApp(ref msg, isActive, oppositeThreadId);
            }

            public static unsafe void OnDisplayChange(WindowBase windowBase, ref WindowMessage msg)
            {
                var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
                Size size;
                msg.LParam.BreakLowInt32To16(out size.Height, out size.Width);
                windowBase.OnDisplayChange(ref msg, imageDepthBitsPerPixel, ref size);
            }
        }
    }

    public abstract class MainWindowBase : WindowBase
    {
        protected override void OnMessageProcessDefault(ref WindowMessage msg)
        {
            IntPtr res;
            if (DwmApiMethods.DwmDefWindowProc(Handle, (uint) msg.Id, msg.WParam, msg.LParam, out res) > 0)
            {
                msg.Result = res;
                msg.SetHandled();
            }
            base.OnMessageProcessDefault(ref msg);
        }

        protected override void OnDestroy(ref WindowMessage msg)
        {
            User32Methods.PostQuitMessage(0);
            base.OnDestroy(ref msg);
        }
    }

    public sealed class MainWindow : MainWindowBase {}

    public sealed class Window : WindowBase {}
}