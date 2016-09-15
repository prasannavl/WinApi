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
                    OnDestroy();
                    break;
                }
                case WM.CLOSE:
                {
                    OnClose();
                    break;
                }
                case WM.TIMECHANGE:
                {
                    OnSystemTimeChange();
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

        protected virtual void OnClose() {}

        protected virtual int OnEraseBkgnd(IntPtr hdc)
        {
            return 0;
        }

        protected virtual void OnDestroy() {}
        protected virtual void OnSystemTimeChange() {}
        protected virtual void OnSize(WindowSizeFlag flag, ref Size size) {}
        protected virtual void OnMove(ref Point size) {}
        protected virtual void OnCreate(ref CreateStruct createStruct) {}
        protected virtual void OnActivate(WindowActivateFlag flag, bool isMinimized, IntPtr oppositeWindowHandle) {}
        protected virtual void OnPaint(IntPtr hdc) {}
        protected virtual void OnDisplayChange(uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(bool isActive, long oppositeThreadId) {}

        public static class MessageHandlers
        {
            public static void OnPaint(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnPaint(msg.WParam);
            }

            public static void OnEraseBkgnd(WindowBase windowBase, ref WindowMessage msg)
            {
                msg.Result = new IntPtr(windowBase.OnEraseBkgnd(msg.WParam));
            }

            public static void OnSize(WindowBase windowBase, ref WindowMessage msg)
            {
                WindowSizeFlag flag;
                Size size;
                if (IntPtr.Size > 4)
                {
                    flag = (WindowSizeFlag) msg.WParam.ToInt64();
                }
                else
                {
                    flag = (WindowSizeFlag) msg.WParam.ToInt32();
                }
                msg.LParam.BreakIntoSafeInt(out size.Height, out size.Width);
                windowBase.OnSize(flag, ref size);
            }

            public static void OnMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakIntoSafeInt(out point.Y, out point.X);
                windowBase.OnMove(ref point);
            }

            public static unsafe void OnCreate(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnCreate(ref *(CreateStruct*) msg.LParam);
            }

            public static void OnActivate(WindowBase windowBase, ref WindowMessage msg)
            {
                int high, low;
                msg.WParam.BreakIntoSafeInt(out high, out low);
                var flag = (WindowActivateFlag) low;
                // Note: wParam is unsigned
                var isMinimized = high != 0;
                var oppositeWindowHandle = msg.LParam;
                windowBase.OnActivate(flag, isMinimized, oppositeWindowHandle);
            }

            public static void OnActivateApp(WindowBase windowBase, ref WindowMessage msg)
            {
                bool isActive;
                long oppositeThreadId;
                if (IntPtr.Size > 4)
                {
                    isActive = msg.WParam.ToInt64() != 0;
                    oppositeThreadId = msg.LParam.ToInt64();
                }
                else
                {
                    isActive = msg.WParam.ToInt32() != 0;
                    oppositeThreadId = msg.LParam.ToInt32();
                }
                windowBase.OnActivateApp(isActive, oppositeThreadId);
            }

            public static unsafe void OnDisplayChange(WindowBase windowBase, ref WindowMessage msg)
            {
                var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
                Size size;
                msg.LParam.BreakIntoSafeInt(out size.Height, out size.Width);
                windowBase.OnDisplayChange(imageDepthBitsPerPixel, ref size);
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

        protected override void OnDestroy()
        {
            User32Methods.PostQuitMessage(0);
            base.OnDestroy();
        }
    }

    public sealed class MainWindow : MainWindowBase {}

    public sealed class Window : WindowBase {}
}