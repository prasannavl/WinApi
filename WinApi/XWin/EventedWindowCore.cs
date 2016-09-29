using System;
using System.Runtime.InteropServices;
using System.Threading;
using WinApi.Core;
using WinApi.User32;
using WinApi.Extensions;

namespace WinApi.XWin
{
    /// <summary>
    ///     Derives from the WindowCore, and provides all the life cycle, and input events.
    ///     It doesn't really handle them in any way but just provides the events with the
    ///     correct decoded parameters. All the processing and decoding are done
    ///     transparently inside the `MessageHandlers` class.
    ///     If in certain high-performance requirements, you need only a few events, this
    ///     can be manually implemented only for the events required using the `MessageHandlers`
    ///     class. All of the parameter decoding are transparent.
    /// </summary>
    public abstract class EventedWindowCore : WindowCore, IWindowMessageProcessor
    {
        void IWindowMessageProcessor.ProcessMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.NCACTIVATE:
                {
                    MessageHandlers.ProcessNonClientActivate(this, ref msg);
                    break;
                }
                case WM.NCCALCSIZE:
                {
                    MessageHandlers.ProcessNonClientCalcSize(this, ref msg);
                    break;
                }
                case WM.NCDESTROY:
                {
                    MessageHandlers.ProcessNonClientDestroy(this, ref msg);
                    break;
                }
                case WM.SHOWWINDOW:
                {
                    MessageHandlers.ProcessShowWindow(this, ref msg);
                    break;
                }
                case WM.QUIT:
                {
                    MessageHandlers.ProcessQuit(this, ref msg);
                    break;
                }
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
                    MessageHandlers.ProcessMouseWheel(this, ref msg, ScrollDirection.Vertical);
                    break;
                }
                case WM.MOUSEHWHEEL:
                {
                    MessageHandlers.ProcessMouseWheel(this, ref msg, ScrollDirection.Horizontal);
                    break;
                }
                case WM.MOUSELEAVE:
                {
                    MessageHandlers.ProcessMouseLeave(this, ref msg);
                    break;
                }
                case WM.CHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg);
                    break;
                }
                case WM.SYSCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg);
                    break;
                }
                case WM.DEADCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg);
                    break;
                }
                case WM.SYSDEADCHAR:
                {
                    MessageHandlers.ProcessKeyChar(this, ref msg);
                    break;
                }
                case WM.KEYUP:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg);
                    break;
                }
                case WM.KEYDOWN:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg);
                    break;
                }
                case WM.SYSKEYUP:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg);
                    break;
                }
                case WM.SYSKEYDOWN:
                {
                    MessageHandlers.ProcessKeyEvent(this, ref msg);
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
        }

        protected override void OnMessage(ref WindowMessage msg)
        {
            ((IWindowMessageProcessor) this).ProcessMessage(ref msg);
            base.OnMessage(ref msg);
        }

        protected virtual void OnClose(ref WindowMessage msg) {}
        protected virtual EraseBackgroundResult OnEraseBkgnd(ref WindowMessage msg, IntPtr cHdc) => 0;
        protected virtual void OnDestroy(ref WindowMessage msg) {}
        protected virtual void OnSystemTimeChange(ref WindowMessage msg) {}
        protected virtual void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size) {}
        protected virtual void OnMove(ref WindowMessage msg, ref Point size) {}
        protected virtual CreationResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct) => 0;

        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeWindowHandle) {}

        protected virtual void OnPaint(ref WindowMessage msg, IntPtr cHdc) {}
        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, long oppositeThreadId) {}
        protected virtual void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) {}
        protected virtual HitTestResult OnHitTest(ref WindowMessage msg, ref Point point) => 0;

        protected virtual MouseActivationResult OnMouseActivate(ref WindowMessage msg, IntPtr activeTopLevelParentHwnd,
            short messageId, HitTestResult hitTestResult) => 0;

        protected virtual void OnMouseWheel(ref WindowMessage msg, ref Point point, short wheelDelta,
            MouseInputKeyStateFlags flags) {}

        protected virtual void OnMouseLeave(ref WindowMessage msg) {}
        protected virtual void OnMouseHover(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) {}
        protected virtual void OnInputCaptureChanged(ref WindowMessage msg, IntPtr handleOfWindowReceivingCapture) {}
        protected virtual void OnGotFocus(ref WindowMessage msg, IntPtr oppositeWindowHandle) {}
        protected virtual void OnLostFocus(ref WindowMessage msg, IntPtr oppositeWindowHandle) {}
        protected virtual void OnMenuCommand(ref WindowMessage msg, int menuIndex, IntPtr menuHandle) {}

        protected virtual void OnSysCommand(ref WindowMessage msg, SysCommand cmd, short mouseCursorX,
            short mouseCursorYOrKeyMnemonic) {}

        protected virtual void OnCommand(ref WindowMessage msg, CommandSource cmdSource, short id, IntPtr hWnd) {}

        protected virtual void OnKeyEvent(ref WindowMessage msg, VirtualKey key, bool isKeyUp,
            KeyboardInputState inputState, bool isSystemContext) {}

        protected virtual void OnKeyChar(ref WindowMessage msg, char inputChar, KeyboardInputState inputState,
            bool isSystemContext, bool isDeadChar) {}

        protected virtual void OnHotKey(ref WindowMessage msg, VirtualKey key, HotKeyInputState keyState,
            ScreenshotHotKey screenshotHotKey) {}

        protected virtual AppCommandResult OnAppCommand(ref WindowMessage msg, AppCommand cmd, AppCommandDevice device,
            KeyboardInputState keyState, IntPtr windowHandle) => 0;

        protected virtual void OnMouseButtonEvent(ref WindowMessage msg, ref Point point, MouseButton button,
            MouseInputKeyStateFlags mouseInputKeyState) {}

        protected virtual unsafe WindowViewRegionFlags OnNonClientCalcSizeGetArea(ref WindowMessage msg, NonClientArea* nonClientArea) => 0;
        protected virtual unsafe void OnNonClientCalcSizeWithRect(ref WindowMessage msg, NonClientAreaRectangle* nonClientRect) {}

        protected virtual void OnShow(ref WindowMessage msg, bool isShown, ShowWindowStatusFlags flags) {}
        protected virtual void OnQuit(ref WindowMessage msg, int code) {}

        protected virtual NonClientActivationResult OnNonClientActivate(ref WindowMessage msg, bool isShown, IntPtr updateRegion)
            => new NonClientActivationResult();

        protected virtual void OnNonClientDestroy(ref WindowMessage msg) {}

        public static class MessageHandlers
        {
            public static void ProcessClose(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                windowCore.OnClose(ref msg);
                // Standard return. 0 if already processed
            }

            public static void ProcessTimeChange(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                windowCore.OnSystemTimeChange(ref msg);
                // Standard return. 0 if already processed
            }

            public static void ProcessDestroy(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                try
                {
                    windowCore.OnDestroy(ref msg);
                }
                finally
                {
                    // This is done to notify a owned window that it shouldn't try to 
                    // destroy the window when the finalizer is called again.
                    windowCore.IsSourceOwner = false;
                }
                // Standard return. 0 if already processed.
            }

            public static void ProcessPaint(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var flag = false;
                try
                {
                    windowCore.OnPaint(ref msg, msg.WParam);
                    flag = true;
                }
                finally
                {
                    // Validate window if an OnPaint handler throws. This is done to prevent a flood of WM_PAINT
                    // messages if the OnPaint errors are uncaught. For example, if a messagebox is shown with an 
                    // error that's unhandled from OnPaint, the flood of WM_PAINT to the thread's message loop 
                    // will prevent the MessageBox from being displayed, and the application ends up with 
                    // inconsistent state. This prevents that from happening. This is the ONLY non-standard
                    // behaviour that's applied - and its also happens only if the code in OnPaint throws an 
                    // exception. 
                    if (!flag)
                        windowCore.Validate();
                }
                // Standard return. 0 if already processed
            }

            public static void ProcessEraseBkgnd(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                msg.Result =
                    new IntPtr((int) windowCore.OnEraseBkgnd(ref msg, msg.WParam));
                // 1 - prevent default erase.
                // 0 - Let DefWndProc erase the background with the window class's brush.
            }

            public static void ProcessSize(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                Size size;
                var flag = (WindowSizeFlag) msg.WParam.ToSafeInt32();
                msg.LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                windowCore.OnSize(ref msg, flag, ref size);
                // Standard return. 0 if already processed
            }

            public static void ProcessMove(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                windowCore.OnMove(ref msg, ref point);
                // Standard return. 0 if already processed
            }

            public static unsafe void ProcessCreate(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                msg.Result = new IntPtr((int) windowCore.OnCreate(ref msg, ref *(CreateStruct*) msg.LParam));
                // Return 0 to continue creation. -1 to destroy and prevent
            }

            public static void ProcessActivate(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                int high, low;
                msg.WParam.BreakSafeInt32To16Signed(out high, out low);
                var flag = (WindowActivateFlag) low;
                var isMinimized = high != 0;
                var oppositeWindowHandle = msg.LParam;
                windowCore.OnActivate(ref msg, flag, isMinimized, oppositeWindowHandle);
                // Standard return. 0 if already processed
            }

            public static void ProcessActivateApp(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var isActive = msg.WParam.ToSafeInt32() != 0;
                var oppositeThreadId = msg.LParam.ToSafeInt32();
                windowCore.OnActivateApp(ref msg, isActive, oppositeThreadId);
                // Standard return. 0 if already processed
            }

            public static unsafe void ProcessDisplayChange(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
                Size size;
                msg.LParam.BreakSafeInt32To16(out size.Height, out size.Width);
                windowCore.OnDisplayChange(ref msg, imageDepthBitsPerPixel, ref size);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseMove(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();
                windowCore.OnMouseMove(ref msg, ref point, flags);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseButtonEvent(EventedWindowCore windowCore, ref WindowMessage msg,
                MouseButton button,
                MouseButtonEvent mouseButtonEvent)
            {
                //TODO: refine handling
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var wParam = msg.WParam.ToSafeInt32();
                var mouseInputKeyState = (MouseInputKeyStateFlags) wParam.Low();
                if (button == MouseButton.Other)
                {
                    button = (MouseInputXButtonFlag) wParam.High() == MouseInputXButtonFlag.XBUTTON1
                        ? MouseButton.XButton1
                        : MouseButton.XButton2;
                }
                windowCore.OnMouseButtonEvent(ref msg, ref point, button, mouseInputKeyState);
                // Normal: Standard return. 0 if already processed
                // XButton: TRUE if processed, 0 if not
            }

            public static void ProcessMouseActivate(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var activeTopLevelParentHwnd = msg.WParam;
                var lParam = msg.LParam.ToSafeInt32();
                var hitTestResult = (HitTestResult) lParam.Low();
                var messageId = lParam.High();

                var res = windowCore.OnMouseActivate(ref msg, activeTopLevelParentHwnd, messageId, hitTestResult);
                msg.Result = new IntPtr((int) res);
                // Return activation result
            }

            public static void ProcessMouseHover(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();
                windowCore.OnMouseHover(ref msg, ref point, flags);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseLeave(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                // Nothing to do here
                windowCore.OnMouseLeave(ref msg);
                // Standard return. 0 if already processed
            }

            public static void ProcessMouseWheel(EventedWindowCore windowCore, ref WindowMessage msg,
                ScrollDirection wheelScrollDirection)
            {
                //TODO: refine handling
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                var wParam = msg.WParam.ToSafeInt32();
                // Multiple or divisons of (WHEEL_DELTA = 120)
                var wheelDelta = wParam.High();
                var flags = (MouseInputKeyStateFlags) wParam.Low();

                windowCore.OnMouseWheel(ref msg, ref point, wheelDelta, flags);
                // Standard return. 0 if already processed
            }

            public static void ProcessKeyChar(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var isSystemContext = msg.Id == WM.SYSCHAR || msg.Id == WM.SYSDEADCHAR;
                var isDeadChar = msg.Id == WM.DEADCHAR || msg.Id == WM.SYSDEADCHAR;
                var inputChar = (char) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeUInt32();
                var inputState = new KeyboardInputState(lParam);
                windowCore.OnKeyChar(ref msg, inputChar, inputState, isSystemContext, isDeadChar);
                // Standard return. 0 if already processed
            }

            public static void ProcessKeyEvent(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var isSystemContext = msg.Id == WM.SYSKEYDOWN || msg.Id == WM.SYSKEYUP;
                var isKeyUp = msg.Id == WM.KEYUP || msg.Id == WM.SYSKEYUP;
                var key = (VirtualKey) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                var inputState = new KeyboardInputState((uint) lParam);
                windowCore.OnKeyEvent(ref msg, key, isKeyUp, inputState, isSystemContext);
                // Standard return. 0 if already processed
            }

            public static void ProcessCommand(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var wParam = msg.WParam.ToSafeInt32();
                var cmdSource = (CommandSource) wParam.High();
                var id = wParam.Low();
                var hWnd = msg.LParam;
                windowCore.OnCommand(ref msg, cmdSource, id, hWnd);
                // Standard return. 0 if already processed
            }

            public static void ProcessSysCommand(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var cmd = (SysCommand) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                // Cursor position if the menu is chosen with the mouse, or unused.
                var mouseCursorXOrZero = lParam.Low();
                // If chosen with the keyboard, this highY is 0 if accelerator is used,
                // or 1 if mnemonic is used.
                var mouseCursorYOrKeyMnemonic = lParam.High();
                windowCore.OnSysCommand(ref msg, cmd, mouseCursorXOrZero, mouseCursorYOrKeyMnemonic);
                // Standard return. 0 if already processed
            }

            public static void ProcessMenuCommand(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var menuIndex = msg.WParam.ToSafeInt32();
                var menuHandle = msg.LParam;
                windowCore.OnMenuCommand(ref msg, menuIndex, menuHandle);
                // Standard return. 0 if already processed
            }

            public static void ProcessLostFocus(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var oppositeWindowHandle = msg.WParam;
                windowCore.OnLostFocus(ref msg, oppositeWindowHandle);
                // Standard return. 0 if already processed
            }

            public static void ProcessGotFocus(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var oppositeWindowHandle = msg.WParam;
                windowCore.OnGotFocus(ref msg, oppositeWindowHandle);
                // Standard return. 0 if already processed
            }

            public static void ProcessCaptureChanged(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var handleOfWindowReceivingCapture = msg.LParam;
                windowCore.OnInputCaptureChanged(ref msg, handleOfWindowReceivingCapture);
                // Standard return. 0 if already processed
            }

            public static void ProcessHitTest(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                Point point;
                msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                msg.Result = new IntPtr((int) windowCore.OnHitTest(ref msg, ref point));
                // Return value is the HitTestResult
            }

            public static void ProcessAppCommand(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                //GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
                //GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
                var lParam = msg.LParam.ToSafeUInt32();
                var cmd = (AppCommand) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
                var device = (AppCommandDevice) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
                var keyState = new KeyboardInputState(lParam.LowAsInt());
                var windowHandle = msg.WParam;
                msg.Result = new IntPtr((int) windowCore.OnAppCommand(ref msg, cmd, device, keyState, windowHandle));
                // Return TRUE if handled.
            }

            public static void ProcessHotKey(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var screenshotHotKey = (ScreenshotHotKey) msg.WParam.ToSafeInt32();
                var lParam = msg.LParam.ToSafeInt32();
                var keyState = (HotKeyInputState) lParam.Low();
                var key = (VirtualKey) lParam.High();
                windowCore.OnHotKey(ref msg, key, keyState, screenshotHotKey);
                // Standard return. 0 if already processed
            }

            public static void ProcessShowWindow(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var isShown = msg.WParam.ToSafeUInt32() > 0;
                var flags = (ShowWindowStatusFlags) msg.LParam.ToSafeInt32();
                windowCore.OnShow(ref msg, isShown, flags);
            }

            public static void ProcessQuit(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var quitCode = msg.WParam.ToSafeInt32();
                windowCore.OnQuit(ref msg, quitCode);
            }

            public static void ProcessNonClientDestroy(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                // No parameters here
                windowCore.OnNonClientDestroy(ref msg);
            }

            public static void ProcessNonClientActivate(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var isShown = msg.WParam.ToSafeInt32() > 0;
                // lParam is used only if visual styles are disabled.
                var updateRegion = msg.LParam;
                var res = windowCore.OnNonClientActivate(ref msg, isShown, updateRegion);
                if (res.PreventRegionUpdates)
                    msg.LParam = new IntPtr(-1);
                msg.Result = new IntPtr(res.PreventDeactivationChanges ? 0 : 1);
                // To prevent Nc region update in DefWndProc, set LParam = -1;
                // When wParam == TRUE, result is ignored.
                // var result = TRUE // Default processing;
                // var result = FALSE // Prevent changes.
            }

            public static unsafe void ProcessNonClientCalcSize(EventedWindowCore windowCore, ref WindowMessage msg)
            {
                var shouldProvideClientArea = msg.WParam.ToSafeUInt32() > 0;
                if (shouldProvideClientArea)
                {
                    var nonClientArea = (NonClientArea*)msg.LParam.ToPointer();
                    msg.Result = new IntPtr((int)windowCore.OnNonClientCalcSizeGetArea(ref msg, nonClientArea));
                    // If providing, 0 preserves previous area & align top-left
                }
                else
                {
                    var nonClientRect = (NonClientAreaRectangle*)msg.LParam.ToPointer();
                    windowCore.OnNonClientCalcSizeWithRect(ref msg, nonClientRect);
                    // Implicit 0 return; 
                }
            }
        }
    }

    public sealed class EventedWindow : EventedWindowCore {}

    public interface IWindowMessageProcessor
    {
        void ProcessMessage(ref WindowMessage message);
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

    public enum ScrollDirection
    {
        Horizontal,
        Vertical
    }

    public enum EraseBackgroundResult
    {
        Default = 0,
        DisableDefaultErase = 1
    }

    public enum CreationResult
    {
        Default = 0,
        PreventCreation = -1
    }


    public enum AppCommandResult
    {
        Default = 0,
        Handled = 1
    }

    public struct NonClientActivationResult
    {
        public bool PreventRegionUpdates;
        public bool PreventDeactivationChanges;
    }
}