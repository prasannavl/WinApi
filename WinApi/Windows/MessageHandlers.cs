using System;
using WinApi.Core;
using WinApi.Extensions;
using WinApi.User32;

namespace WinApi.Windows
{
    public class MessageHandlers
    {
        public static void ProcessShowWindow(ref WindowMessage msg, ShowWindowHandler handler)
        {
            var isShown = msg.WParam.ToSafeUInt32() > 0;
            var flags = (ShowWindowStatusFlags) msg.LParam.ToSafeInt32();
            handler(ref msg, isShown, flags);
        }

        public static void ProcessQuit(ref WindowMessage msg, QuitHandler handler)
        {
            var quitCode = msg.WParam.ToSafeInt32();
            handler(ref msg, quitCode);
        }

        public static unsafe void ProcessCreate(ref WindowMessage msg, CreateHandler handler)
        {
            msg.Result = new IntPtr((int) handler(ref msg, ref *(CreateStruct*) msg.LParam));
            // Return 0 to continue creation. -1 to destroy and prevent
        }

        public static unsafe void ProcessGetMinMaxInfo(ref WindowMessage msg, GetMinMaxInfoHandler handler)
        {
            var lPtr = (MinMaxInfo*)msg.LParam;
            var minMaxInfo = *lPtr;
            handler(ref msg, ref minMaxInfo);
            *lPtr = minMaxInfo;
        }

        public static void ProcessSize(ref WindowMessage msg, SizeHandler handler)
        {
            Size size;
            var flag = (WindowSizeFlag) msg.WParam.ToSafeInt32();
            msg.LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
            handler(ref msg, flag, ref size);
            // Standard return. 0 if already processed
        }

        public static void ProcessMove(ref WindowMessage msg, MoveHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            handler(ref msg, ref point);
            // Standard return. 0 if already processed
        }

        public static void ProcessPaint(ref WindowMessage msg, PaintHandler handler)
        {
            handler(ref msg, msg.WParam);
        }

        public static void ProcessEraseBkgnd(ref WindowMessage msg, EraseBkgndHandler handler)
        {
            msg.Result =
                new IntPtr((int) handler(ref msg, msg.WParam));
            // 1 - prevent default erase.
            // 0 - Let DefWndProc erase the background with the window class's brush.
        }

        public static void ProcessActivate(ref WindowMessage msg, ActivateHandler handler)
        {
            int high, low;
            msg.WParam.BreakSafeInt32To16Signed(out high, out low);
            var flag = (WindowActivateFlag) low;
            var isMinimized = high != 0;
            var oppositeHwnd = msg.LParam;
            handler(ref msg, flag, isMinimized, oppositeHwnd);
            // Standard return. 0 if already processed
        }

        public static void ProcessActivateApp(ref WindowMessage msg, ActivateAppHandler handler)
        {
            var isActive = msg.WParam.ToSafeInt32() != 0;
            var oppositeThreadId = msg.LParam.ToSafeUInt32();
            handler(ref msg, isActive, oppositeThreadId);
            // Standard return. 0 if already processed
        }

        public static unsafe void ProcessDisplayChange(ref WindowMessage msg, DisplayChangeHandler handler)
        {
            var imageDepthBitsPerPixel = (uint) msg.WParam.ToPointer();
            Size size;
            msg.LParam.BreakSafeInt32To16(out size.Height, out size.Width);
            handler(ref msg, imageDepthBitsPerPixel, ref size);
            // Standard return. 0 if already processed
        }

        public static void ProcessMouseMove(ref WindowMessage msg, MouseMoveHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();
            handler(ref msg, ref point, flags);
            // Standard return. 0 if already processed
        }

        public static void ProcessMouseButton(ref WindowMessage msg, MouseButtonHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            var wParam = msg.WParam.ToSafeInt32();
            var mouseInputKeyState = (MouseInputKeyStateFlags) wParam.Low();

            var msgId = (int) msg.Id;
            var isButtonUp = false;
            MouseButton button;
            if ((msgId > 0x200) && (msgId < 0x204))
            {
                button = MouseButton.Left;
                if (msg.Id == WM.LBUTTONUP) isButtonUp = true;
            }
            else if ((msgId > 0x203) && (msgId < 0x207))
            {
                button = MouseButton.Right;
                if (msg.Id == WM.RBUTTONUP) isButtonUp = true;
            }
            else if ((msgId > 0x206) && (msgId < 0x210))
            {
                button = MouseButton.Middle;
                if (msg.Id == WM.MBUTTONUP) isButtonUp = true;
            }
            else
            {
                button = (MouseInputXButtonFlag) wParam.High() == MouseInputXButtonFlag.XBUTTON1
                    ? MouseButton.XButton1
                    : MouseButton.XButton2;
                if (msg.Id == WM.XBUTTONUP) isButtonUp = true;
            }
            handler(ref msg, ref point, button, isButtonUp, mouseInputKeyState);
            // Normal: Standard return. 0 if already processed
            // XButton: TRUE if processed, 0 if not
        }

        public static void ProcessMouseDoubleClick(ref WindowMessage msg, MouseDoubleClickHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            var wParam = msg.WParam.ToSafeInt32();
            var mouseInputKeyState = (MouseInputKeyStateFlags) wParam.Low();

            var msgId = msg.Id;
            MouseButton button;
            if (msgId == WM.LBUTTONDBLCLK)
                button = MouseButton.Left;
            else if (msgId == WM.RBUTTONDBLCLK)
                button = MouseButton.Right;
            else if (msgId == WM.MBUTTONDBLCLK)
                button = MouseButton.Middle;
            else
            {
                button = (MouseInputXButtonFlag) wParam.High() == MouseInputXButtonFlag.XBUTTON1
                    ? MouseButton.XButton1
                    : MouseButton.XButton2;
            }
            handler(ref msg, ref point, button, mouseInputKeyState);
        }

        public static void ProcessMouseActivate(ref WindowMessage msg, MouseActivateHandler handler)
        {
            var activeTopLevelParentHwnd = msg.WParam;
            var lParam = msg.LParam.ToSafeInt32();
            var hitTestResult = (HitTestResult) lParam.Low();
            var messageId = (ushort) lParam.High();

            var res = handler(ref msg, activeTopLevelParentHwnd, messageId, hitTestResult);
            msg.Result = new IntPtr((int) res);
            // Return activation result
        }

        public static void ProcessMouseHover(ref WindowMessage msg, MouseHoverHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            var flags = (MouseInputKeyStateFlags) msg.WParam.ToSafeInt32();
            handler(ref msg, ref point, flags);
            // Standard return. 0 if already processed
        }

        public static void ProcessMouseWheel(ref WindowMessage msg, MouseWheelHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            var wParam = msg.WParam.ToSafeInt32();
            // Multiple or divisons of (WHEEL_DELTA = 120)
            var wheelDelta = wParam.High();
            var flags = (MouseInputKeyStateFlags) wParam.Low();
            var isWheelDirectionHorizontal = msg.Id == WM.MOUSEHWHEEL;
            handler(ref msg, ref point, wheelDelta, isWheelDirectionHorizontal, flags);
            // Standard return. 0 if already processed
        }

        public static void ProcessKeyChar(ref WindowMessage msg, KeyCharHandler handler)
        {
            var isSystemContext = false;
            var isDeadChar = false;
            if (msg.Id == WM.CHAR) {}
            else if (msg.Id == WM.SYSCHAR)
                isSystemContext = true;
            else if (msg.Id == WM.DEADCHAR)
                isDeadChar = true;
            else
            {
                isSystemContext = true;
                isDeadChar = true;
            }
            var inputChar = (char) msg.WParam.ToSafeInt32();
            var lParam = msg.LParam.ToSafeUInt32();
            var inputState = new KeyboardInputState(lParam);
            handler(ref msg, inputChar, inputState, isSystemContext, isDeadChar);
            // Standard return. 0 if already processed
        }

        public static void ProcessKey(ref WindowMessage msg, KeyHandler handler)
        {
            var isSystemContext = (msg.Id == WM.SYSKEYDOWN) || (msg.Id == WM.SYSKEYUP);
            var isKeyUp = (msg.Id == WM.KEYUP) || (msg.Id == WM.SYSKEYUP);
            var key = (VirtualKey) msg.WParam.ToSafeInt32();
            var lParam = msg.LParam.ToSafeInt32();
            var inputState = new KeyboardInputState((uint) lParam);
            handler(ref msg, key, isKeyUp, inputState, isSystemContext);
            // Standard return. 0 if already processed
        }

        public static void ProcessCommand(ref WindowMessage msg, CommandHandler handler)
        {
            var wParam = msg.WParam.ToSafeInt32();
            var cmdSource = (CommandSource) wParam.High();
            var id = wParam.Low();
            var hWnd = msg.LParam;
            handler(ref msg, cmdSource, id, hWnd);
            // Standard return. 0 if already processed
        }

        public static void ProcessSysCommand(ref WindowMessage msg, SysCommandHandler handler)
        {
            var cmd = (SysCommand) msg.WParam.ToSafeInt32();
            var lParam = msg.LParam.ToSafeInt32();
            // Cursor position if the menu is chosen with the mouse, or unused.
            var mouseCursorXOrZero = lParam.Low();
            // If chosen with the keyboard, this highY is 0 if accelerator is used,
            // or 1 if mnemonic is used.
            var mouseCursorYOrKeyMnemonic = lParam.High();
            handler(ref msg, cmd, mouseCursorXOrZero, mouseCursorYOrKeyMnemonic);
            // Standard return. 0 if already processed
        }

        public static void ProcessMenuCommand(ref WindowMessage msg, MenuCommandHandler handler)
        {
            var menuIndex = msg.WParam.ToSafeInt32();
            var menuHandle = msg.LParam;
            handler(ref msg, menuIndex, menuHandle);
            // Standard return. 0 if already processed
        }

        public static void ProcessLostFocus(ref WindowMessage msg, FocusHandler handler)
        {
            var oppositeHwnd = msg.WParam;
            handler(ref msg, oppositeHwnd);
            // Standard return. 0 if already processed
        }

        public static void ProcessGotFocus(ref WindowMessage msg, FocusHandler handler)
        {
            var oppositeHwnd = msg.WParam;
            handler(ref msg, oppositeHwnd);
            // Standard return. 0 if already processed
        }

        public static void ProcessCaptureChanged(ref WindowMessage msg, CaptureChangedHandler handler)
        {
            var handleOfWindowReceivingCapture = msg.LParam;
            handler(ref msg, handleOfWindowReceivingCapture);
            // Standard return. 0 if already processed
        }

        public static void ProcessHitTest(ref WindowMessage msg, HitTestHandler handler)
        {
            Point point;
            msg.LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
            msg.Result = new IntPtr((int) handler(ref msg, ref point));
            // Return value is the HitTestResult
        }

        public static void ProcessAppCommand(ref WindowMessage msg, AppCommandHandler handler)
        {
            //GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
            //GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
            var lParam = msg.LParam.ToSafeUInt32();
            var cmd = (AppCommand) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
            var device = (AppCommandDevice) (lParam.HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
            var keyState = new KeyboardInputState(lParam.LowAsInt());
            var hwnd = msg.WParam;
            msg.Result = new IntPtr((int) handler(ref msg, cmd, device, keyState, hwnd));
            // Return TRUE if handled.
        }

        public static void ProcessHotKey(ref WindowMessage msg, HotKeyHandler handler)
        {
            var screenshotHotKey = (ScreenshotHotKey) msg.WParam.ToSafeInt32();
            var lParam = msg.LParam.ToSafeInt32();
            var keyState = (HotKeyInputState) lParam.Low();
            var key = (VirtualKey) lParam.High();
            handler(ref msg, key, keyState, screenshotHotKey);
            // Standard return. 0 if already processed
        }

        #region Delegate Types

        public delegate void ActivateAppHandler(ref WindowMessage msg, bool isActive, uint oppositeThreadId);

        public delegate void ActivateHandler(
            ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized, IntPtr oppositeHwnd);

        public delegate AppCommandResult AppCommandHandler(
            ref WindowMessage msg, AppCommand cmd, AppCommandDevice device, KeyboardInputState keyState,
            IntPtr hwnd);

        public delegate void GetMinMaxInfoHandler(ref WindowMessage msg, ref MinMaxInfo minMaxInfo);

        public delegate void CaptureChangedHandler(ref WindowMessage msg, IntPtr handleOfWindowReceivingCapture);

        public delegate void CommandHandler(ref WindowMessage msg, CommandSource cmdSource, short id, IntPtr hwnd);

        public delegate CreateWindowResult CreateHandler(ref WindowMessage msg, ref CreateStruct createStruct);

        public delegate void DisplayChangeHandler(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size);

        public delegate EraseBackgroundResult EraseBkgndHandler(ref WindowMessage msg, IntPtr cHdc);

        public delegate void FocusHandler(ref WindowMessage msg, IntPtr oppositeHwnd);

        public delegate HitTestResult HitTestHandler(ref WindowMessage msg, ref Point point);

        public delegate void HotKeyHandler(
            ref WindowMessage msg, VirtualKey key, HotKeyInputState keyState, ScreenshotHotKey screenshotHotKey);

        public delegate void KeyCharHandler(
            ref WindowMessage msg, char inputChar, KeyboardInputState inputState, bool isSystemContext, bool isDeadChar);

        public delegate void KeyHandler(
            ref WindowMessage msg, VirtualKey key, bool isKeyUp, KeyboardInputState inputState, bool isSystemContext);

        public delegate void MenuCommandHandler(ref WindowMessage msg, int index, IntPtr menuHandle);

        public delegate MouseActivationResult MouseActivateHandler(
            ref WindowMessage msg, IntPtr activeTopLevelParentHwnd, ushort messageId, HitTestResult hitTestResult);

        public delegate void MouseButtonHandler(
            ref WindowMessage msg, ref Point point, MouseButton button, bool isButtonUp,
            MouseInputKeyStateFlags keyStateFlags);

        public delegate void MouseDoubleClickHandler(
            ref WindowMessage msg, ref Point point, MouseButton button, MouseInputKeyStateFlags keyStateFlags);

        public delegate void MouseHoverHandler(
            ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags keyStateFlags);

        public delegate void MouseMoveHandler(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags);

        public delegate void MouseWheelHandler(
            ref WindowMessage msg, ref Point point, short wheelDelta, bool isWheelDirectionHorizontal,
            MouseInputKeyStateFlags flags);

        public delegate void MoveHandler(ref WindowMessage msg, ref Point point);

        public delegate void PaintHandler(ref WindowMessage msg, IntPtr cHdc);

        public delegate void QuitHandler(ref WindowMessage msg, int code);

        public delegate void ShowWindowHandler(ref WindowMessage msg, bool isShown, ShowWindowStatusFlags flags);

        public delegate void SizeHandler(ref WindowMessage msg, WindowSizeFlag flag, ref Size size);

        public delegate void SysCommandHandler(
            ref WindowMessage msg, SysCommand cmd, short mouseCursorXOrZero, short mouseCursorYOrKeyMnemonic);

        #endregion

        #region Non-client Handling

        public delegate NcActivationResult NcActivateHandler(
            ref WindowMessage msg, bool isShown, IntPtr updateRegion);

        public delegate WindowViewRegionFlags NcCalcSizeHandler(
            ref WindowMessage msg, bool shouldCalcValidRects, ref NcCalcSizeParams ncCalcSizeParams);

        public static unsafe void ProcessNcCalcSize(ref WindowMessage msg,
            NcCalcSizeHandler handler)
        {
            var shouldCalcValidRects = msg.WParam.ToSafeUInt32() > 0;
            var lPtr = (NcCalcSizeParams*) msg.LParam;
            var ncCalcSizeParams = *lPtr;
            msg.Result = new IntPtr((int) handler(ref msg, shouldCalcValidRects, ref ncCalcSizeParams));
            *lPtr = ncCalcSizeParams;
            // If providing, 0 preserves previous area & align top-left
        }

        public static unsafe void ProcessNcActivate(ref WindowMessage msg, NcActivateHandler handler)
        {
            var isShown = msg.WParam.ToSafeInt32() > 0;
            // lParam is used only if visual styles are disabled.
            var updateRegion = msg.LParam;
            var res = handler(ref msg, isShown, updateRegion);
            if (res.PreventRegionUpdates)
                *((int*)msg.LParam) = -1;
            msg.Result = new IntPtr(res.PreventDeactivationChanges ? 0 : 1);
            // To prevent Nc region update in DefWndProc, set LParam = -1;
            // When wParam == TRUE, result is ignored.
            // var result = TRUE // Default processing;
            // var result = FALSE // Prevent changes.
        }

        #endregion
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

    public enum EraseBackgroundResult
    {
        Default = 0,
        DisableDefaultErase = 1
    }

    public enum CreateWindowResult
    {
        Default = 0,
        PreventCreation = -1
    }


    public enum AppCommandResult
    {
        Default = 0,
        Handled = 1
    }

    public struct NcActivationResult
    {
        public bool PreventRegionUpdates;
        public bool PreventDeactivationChanges;
    }
}