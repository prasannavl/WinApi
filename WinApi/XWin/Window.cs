using System;
using WinApi.Core;
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
                case WM.DESTROY:
                {
                    MessageHandlers.ProcessDestroy(this, ref msg);
                    break;
                }
                case WM.CREATE:
                {
                    MessageHandlers.ProcessCreate(this, ref msg);
                    break;
                }
                case WM.SIZE:
                {
                    MessageHandlers.ProcessSize(this, ref msg);
                    break;
                }
                case WM.MOVE:
                {
                    MessageHandlers.ProcessMove(this, ref msg);
                    break;
                }
                case WM.ACTIVATE:
                {
                    MessageHandlers.ProcessActivate(this, ref msg);
                    break;
                }
                case WM.ERASEBKGND:
                {
                    MessageHandlers.ProcessEraseBkgnd(this, ref msg);
                    break;
                }
                case WM.PAINT:
                {
                    MessageHandlers.ProcessPaint(this, ref msg);
                    break;
                }
                case WM.ACTIVATEAPP:
                {
                    MessageHandlers.ProcessActivateApp(this, ref msg);
                    break;
                }
                case WM.DISPLAYCHANGE:
                {
                    MessageHandlers.ProcessDisplayChange(this, ref msg);
                    break;
                }
                case WM.MOUSEMOVE:
                {
                    MessageHandlers.ProcessMouseMove(this, ref msg);
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

        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeWindowHandle) {}

        protected virtual void OnPaint(ref WindowMessage msg, IntPtr hdc) {}
        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, long oppositeThreadId) {}

        public static class MessageHandlers
        {
            public static void ProcessDestroy(WindowBase windowBase, ref WindowMessage msg)
            {
                try
                {
                    windowBase.OnDestroy(ref msg);
                }
                finally
                {
                    // This is done to notify a owned window that it shouldn't try to 
                    // destroy the window when the finalizer is called again.
                    windowBase.IsSourceOwner = false;
                }
            }

            public static void ProcessPaint(WindowBase windowBase, ref WindowMessage msg)
            {
                var flag = false;
                try
                {
                    windowBase.OnPaint(ref msg, msg.WParam);
                    flag = true;
                }
                finally
                {
                    // Validate window if an OnPaint handler throws. This is done to prevent a flood of WM_PAINT
                    // messages if the OnPaint errors are uncaught. For example, if a messagebox is shown with an 
                    // error that's unhandled from OnPaint, the flood of WM_PAINT to the thread's message loop 
                    // will prevent the MessageBox from being displayed, and the application ends up with 
                    // inconsistent state. This prevents that from happening.
                    if (!flag)
                        windowBase.Validate();
                }
            }

            public static void ProcessEraseBkgnd(WindowBase windowBase, ref WindowMessage msg)
            {
                msg.Result = new IntPtr(windowBase.OnEraseBkgnd(ref msg, msg.WParam));
            }

            public static void ProcessSize(WindowBase windowBase, ref WindowMessage msg)
            {
                Size size;
                var flag = (WindowSizeFlag) msg.WParam.ToSafeInt32();
                msg.LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                windowBase.OnSize(ref msg, flag, ref size);
            }

            public static void ProcessMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                windowBase.OnMove(ref msg, ref point);
            }

            public static unsafe void ProcessCreate(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnCreate(ref msg, ref *(CreateStruct*) msg.LParam);
            }

            public static void ProcessActivate(WindowBase windowBase, ref WindowMessage msg)
            {
                int high, low;
                msg.WParam.BreakSafeInt32To16Signed(out high, out low);
                var flag = (WindowActivateFlag) low;
                var isMinimized = high != 0;
                var oppositeWindowHandle = msg.LParam;
                windowBase.OnActivate(ref msg, flag, isMinimized, oppositeWindowHandle);
            }

            public static void ProcessActivateApp(WindowBase windowBase, ref WindowMessage msg)
            {
                var isActive = msg.WParam.ToSafeInt32() != 0;
                var oppositeThreadId = msg.LParam.ToSafeInt32();
                windowBase.OnActivateApp(ref msg, isActive, oppositeThreadId);
            }

            public static unsafe void ProcessDisplayChange(WindowBase windowBase, ref WindowMessage msg)
            {
                var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
                Size size;
                msg.LParam.BreakSafeInt32To16(out size.Height, out size.Width);
                windowBase.OnDisplayChange(ref msg, imageDepthBitsPerPixel, ref size);
            }

            public static void ProcessMouseMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakInt32Into16(out point.Y, out point.X);
                windowBase.OnMouseMove(ref msg, ref point, (MouseMoveFlags)msg.WParam.ToSafeInt32());
            }
        }

        protected virtual void OnMouseMove(ref WindowMessage msg, ref Point point, MouseMoveFlags flags)
        {
            
        }
    }

    public abstract class MainWindowBase : WindowBase
    {
        protected override void OnDestroy(ref WindowMessage msg)
        {
            MessageHelpers.PostQuitMessage();
            base.OnDestroy(ref msg);
        }
    }

    public abstract class DwmWindowBase : WindowBase
    {
        protected override void OnMessageProcessDefault(ref WindowMessage msg)
        {
            MessageHelpers.RunDwmDefWindowProc(ref msg, Handle);
            base.OnMessageProcessDefault(ref msg);
        }
    }

    public sealed class MainWindow : MainWindowBase {}

    public sealed class Window : WindowBase {}
}