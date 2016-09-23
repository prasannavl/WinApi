using System;
using System.Runtime.InteropServices;
using System.Threading;
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
                    MessageHandlers.ProcessClose(this, ref msg);
                    break;
                }
                case WM.TIMECHANGE:
                {
                    MessageHandlers.ProcessTimeChange(this, ref msg);
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
                case WM.LBUTTONUP:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Left, MouseButtonEvent.Up);
                    break;
                }
                case WM.LBUTTONDOWN:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Left, MouseButtonEvent.Down);
                    break;
                }
                case WM.LBUTTONDBLCLK:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Left,
                        MouseButtonEvent.DoubleClick);
                    break;
                }
                case WM.RBUTTONUP:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Right, MouseButtonEvent.Up);
                    break;
                }
                case WM.RBUTTONDOWN:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Right, MouseButtonEvent.Down);
                    break;
                }
                case WM.RBUTTONDBLCLK:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Right,
                        MouseButtonEvent.DoubleClick);
                    break;
                }
                case WM.MBUTTONUP:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Middle, MouseButtonEvent.Up);
                    break;
                }
                case WM.MBUTTONDOWN:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Middle, MouseButtonEvent.Down);
                    break;
                }
                case WM.MBUTTONDBLCLK:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Middle,
                        MouseButtonEvent.DoubleClick);
                    break;
                }
                case WM.XBUTTONUP:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Other, MouseButtonEvent.Up);
                    break;
                }
                case WM.XBUTTONDOWN:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Other, MouseButtonEvent.Down);
                    break;
                }
                case WM.XBUTTONDBLCLK:
                {
                    MessageHandlers.ProcessMouseButtonEvent(this, ref msg, MouseButton.Other,
                        MouseButtonEvent.DoubleClick);
                    break;
                }
                case WM.MOUSEACTIVATE:
                {
                    MessageHandlers.ProcessMouseActivate(this, ref msg);
                    break;
                }
                case WM.MOUSEHOVER:
                {
                    MessageHandlers.ProcessMouseHover(this, ref msg);
                    break;
                }
                case WM.MOUSEWHEEL:
                {
                    MessageHandlers.ProcessMouseWheel(this, ref msg, Orientation.Vertical);
                    break;
                }
                case WM.MOUSEHWHEEL:
                {
                    MessageHandlers.ProcessMouseWheel(this, ref msg, Orientation.Horizontal);
                    break;
                }
                case WM.MOUSELEAVE:
                {
                    MessageHandlers.ProcessMouseLeave(this, ref msg);
                    break;
                }
                case WM.CHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg, false, false);
                    break;
                }
                case WM.SYSCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg, true, false);
                    break;
                }
                case WM.DEADCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg, false, true);
                    break;
                }
                case WM.SYSDEADCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg, true, true);
                    break;
                }
                case WM.KEYUP:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg, KeyEvent.Up, false);
                    break;
                }
                case WM.KEYDOWN:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg, KeyEvent.Down, false);
                    break;
                }
                case WM.SYSKEYUP:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg, KeyEvent.Up, true);
                    break;
                }
                case WM.SYSKEYDOWN:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg, KeyEvent.Down, true);
                    break;
                }
                case WM.COMMAND:
                {
                    MessageHandlers.ProcessCommand(this, ref msg);
                    break;
                }
                case WM.SYSCOMMAND:
                {
                    MessageHandlers.ProcessSysCommand(this, ref msg);
                    break;
                }
                case WM.MENUCOMMAND:
                {
                    MessageHandlers.ProcessMenuCommand(this, ref msg);
                    break;
                }
                case WM.APPCOMMAND:
                {
                    MessageHandlers.ProcessAppCommand(this, ref msg);
                    break;
                }
                case WM.KILLFOCUS:
                {
                    MessageHandlers.ProcessLostFocus(this, ref msg);
                    break;
                }
                case WM.SETFOCUS:
                {
                    MessageHandlers.ProcessGotFocus(this, ref msg);
                    break;
                }
                case WM.CAPTURECHANGED:
                {
                    MessageHandlers.ProcessCaptureChanged(this, ref msg);
                    break;
                }
                case WM.NCHITTEST:
                {
                    MessageHandlers.ProcessHitTest(this, ref msg);
                    break;
                }
                case WM.HOTKEY:
                {
                    MessageHandlers.ProcessHotKey(this, ref msg);
                        break;
                }
            }
            base.OnMessage(ref msg);
        }

        protected virtual void OnClose(ref WindowMessage msg) {}

        protected virtual bool OnEraseBkgnd(ref WindowMessage msg, IntPtr hdc)
        {
            return true;
        }

        protected virtual void OnDestroy(ref WindowMessage msg) {}
        protected virtual void OnSystemTimeChange(ref WindowMessage msg) {}
        protected virtual void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size) {}
        protected virtual void OnMove(ref WindowMessage msg, ref Point size) {}

        protected virtual bool OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            return true;
        }

        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeWindowHandle) {}

        protected virtual void OnPaint(ref WindowMessage msg, IntPtr hdc) {}
        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, long oppositeThreadId) {}
        protected virtual void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) {}

        public virtual HitTestResult OnHitTest(ref WindowMessage msg, ref Point point)
        {
            return 0;
        }

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
                // Standard return. 0 if already processed.
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
                // Standard return. 0 if already processed
            }

            public static void ProcessEraseBkgnd(WindowBase windowBase, ref WindowMessage msg)
            {
                msg.Result = new IntPtr(windowBase.OnEraseBkgnd(ref msg, msg.WParam) ? 0 : 1);
            }

            public static void ProcessSize(WindowBase windowBase, ref WindowMessage msg)
            {
                Size size;
                var flag = (WindowSizeFlag) msg.WParam.ToSafeInt32();
                msg.LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                windowBase.OnSize(ref msg, flag, ref size);
                // Standard return. 0 if already processed
            }

            public static void ProcessMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                windowBase.OnMove(ref msg, ref point);
                // Standard return. 0 if already processed
            }

            public static unsafe void ProcessCreate(WindowBase windowBase, ref WindowMessage msg)
            {
                msg.Result = new IntPtr(windowBase.OnCreate(ref msg, ref *(CreateStruct*) msg.LParam) ? 0 : -1);
                // Return 0 to continue creation. -1 to destroy and prevent
            }

            public static void ProcessActivate(WindowBase windowBase, ref WindowMessage msg)
            {
                int high, low;
                msg.WParam.BreakSafeInt32To16Signed(out high, out low);
                var flag = (WindowActivateFlag) low;
                var isMinimized = high != 0;
                var oppositeWindowHandle = msg.LParam;
                windowBase.OnActivate(ref msg, flag, isMinimized, oppositeWindowHandle);
                // Standard return. 0 if already processed
            }

            public static void ProcessActivateApp(WindowBase windowBase, ref WindowMessage msg)
            {
                var isActive = msg.WParam.ToSafeInt32() != 0;
                var oppositeThreadId = msg.LParam.ToSafeInt32();
                windowBase.OnActivateApp(ref msg, isActive, oppositeThreadId);
                // Standard return. 0 if already processed
            }

            public static unsafe void ProcessDisplayChange(WindowBase windowBase, ref WindowMessage msg)
            {
                var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
                Size size;
                msg.LParam.BreakSafeInt32To16(out size.Height, out size.Width);
                windowBase.OnDisplayChange(ref msg, imageDepthBitsPerPixel, ref size);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseMove(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();
                windowBase.OnMouseMove(ref msg, ref point, flags);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseButtonEvent(WindowBase windowBase, ref WindowMessage msg, MouseButton button,
                MouseButtonEvent mouseButtonEvent)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var wParam = msg.WParam.ToSafeInt32();
                var flags = (MouseInputKeyStateFlags) wParam.Low();
                if (button == MouseButton.Other)
                {
                    button = (MouseXButtonFlag) wParam.High() == MouseXButtonFlag.XBUTTON1
                        ? MouseButton.XButton1
                        : MouseButton.XButton2;
                }

                // Normal: Standard return. 0 if already processed
                // XButton: TRUE if processed, 0 if not
            }

            public static void ProcessMouseActivate(WindowBase windowBase, ref WindowMessage msg)
            {
                var activeTopLevelParentWindow = msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                var hitTestResult = (HitTestResult) lParam.Low();
                var messageId = lParam.High();
                MouseActivationResult result;

                // Return activation result
            }

            public static void ProcessMouseHover(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();

                // Standard return. 0 if already processed
            }

            public static void ProcessMouseLeave(WindowBase windowBase, ref WindowMessage msg)
            {
                // Nothing to do here
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseWheel(WindowBase windowBase, ref WindowMessage msg,
                Orientation wheelOrientation)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var wParam = msg.WParam.ToSafeInt32();
                // Multiple or divisons of (WHEEL_DELTA = 120)
                var wheelDelta = wParam.High();
                var flags = (MouseInputKeyStateFlags) wParam.Low();

                // Standard return. 0 if already processed
            }

            public static void ProcessKeyChar(WindowBase windowBase, ref WindowMessage msg, bool isSystemContext,
                bool isDeadChar)
            {
                var c = (char) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeUInt32();
                var input = new KeyboardInputState(lParam);

                // Standard return. 0 if already processed
            }

            public static void ProcessKeyEvent(WindowBase windowBase, ref WindowMessage msg, KeyEvent keyEvent,
                bool isSystemContext)
            {
                var key = (VirtualKey) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeUInt32();
                var input = new KeyboardInputState(lParam);

                // Standard return. 0 if already processed
            }

            public static void ProcessCommand(WindowBase windowBase, ref WindowMessage msg)
            {
                var wParam = msg.WParam.ToSafeInt32();
                var cmdSource = (CommandSource) wParam.High();
                var id = wParam.Low();
                var hWnd = msg.LParam;

                // Standard return. 0 if already processed
            }

            public static void ProcessSysCommand(WindowBase windowBase, ref WindowMessage msg)
            {
                var cmd = (SysCommand) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                // Cursor position if the menu is chosen with the mouse.
                var lowXParam = lParam.Low();
                // If chose with the keyboard, this highY is 0 if accelerator is used,
                // or 1 if mnemonic is used.
                var highYParam = lParam.High();

                // Standard return. 0 if already processed
            }

            public static void ProcessMenuCommand(WindowBase windowBase, ref WindowMessage msg)
            {
                var menuIndex = msg.WParam.ToSafeInt32();
                var menuHandle = msg.LParam;

                // Standard return. 0 if already processed
            }

            public static void ProcessLostFocus(WindowBase windowBase, ref WindowMessage msg)
            {
                var oppositeWindowHandle = msg.WParam;
                // Standard return. 0 if already processed
            }

            public static void ProcessGotFocus(WindowBase windowBase, ref WindowMessage msg)
            {
                var oppositeWindowHandle = msg.WParam;
                // Standard return. 0 if already processed
            }

            public static void ProcessCaptureChanged(WindowBase windowBase, ref WindowMessage msg)
            {
                var windowGettingCaptureHandle = msg.LParam;
                // Standard return. 0 if already processed
            }

            public static void ProcessClose(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnClose(ref msg);
                // Standard return. 0 if already processed
            }

            public static void ProcessTimeChange(WindowBase windowBase, ref WindowMessage msg)
            {
                windowBase.OnSystemTimeChange(ref msg);
                // Standard return. 0 if already processed
            }

            public static void ProcessHitTest(WindowBase windowBase, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                msg.Result = new IntPtr((int) windowBase.OnHitTest(ref msg, ref point));
                // Return value is the HitTestResult
            }

            public static void ProcessAppCommand(WindowBase windowBase, ref WindowMessage msg)
            {
                //GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
                //GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
                var lParam = msg.LParam.ToSafeUInt32();
                var cmd = (AppCommand) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
                var uDevice = (AppCommandDevice) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
                var dwKeys = new KeyboardInputState(lParam.LowAsInt());
                var windowHandle = msg.WParam;
                // Return TRUE if handled.
            }

            public static void ProcessHotKey(WindowBase windowBase, ref WindowMessage msg)
            {
                var wParam = (ScreenshotHotKey)msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                var keyState = (HotKeyInputState) lParam.Low();
                var key = (VirtualKey) lParam.High();
            }
        }
    }

    public enum MouseButton
    {
        Left = 0x1,
        Right = 0x2,
        Middle = 0x4,
        Other = 0x8,
        XButton1 = 0x10 | Other,
        XButton2 = 0x20 | Other
    }

    public enum MouseButtonEvent
    {
        Up,
        Down,
        DoubleClick
    }

    public enum KeyEvent
    {
        Up,
        Down
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
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