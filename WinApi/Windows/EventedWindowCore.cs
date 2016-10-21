using System;
using System.Runtime.CompilerServices;
using WinApi.Core;
using WinApi.Extensions;
using WinApi.User32;
using WinApi.Windows.Helpers;

namespace WinApi.Windows
{
    /// <summary>
    ///     Derives from the WindowCore, and provides all the life cycle, and input events.
    ///     It doesn't really handle them in any way but just provides the events with the
    ///     correct decoded parameters. All the processing and decoding are done
    ///     transparently inside the `MessageDecoder` class.
    ///     If in certain high-performance requirements, you need only a few events, this
    ///     can be manually implemented only for the events required using the `MessageDecoder`
    ///     class. All of the parameter decoding are transparent.
    /// </summary>
    public abstract class EventedWindowCore : WindowCore
    {
        protected override void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.PAINT:
                {
                    // The only specially handled message
                    var flag = false;
                    try
                    {
                        MessageDecoder.ProcessPaint(ref msg, OnPaint);
                        flag = true;
                    }
                    finally
                    {
                        // Validate window if an OnPaint handler throws. This is done to prevent a flood of WM_PAINT
                        // messages if the OnPaint errors are uncaught. For example, if a messagebox is shown with an 
                        // error that's unhandled from OnPaint, the flood of WM_PAINT to the thread's message loop 
                        // will prevent the MessageBox from being displayed, and the application ends up with 
                        // inconsistent state. This prevents that from happening. This is the ONLY non-standard
                        // behaviour that's applied - and it also happens only if the code in OnPaint throws an 
                        // exception. 
                        if (!flag)
                            Validate();
                    }
                    break;
                }
                case WM.NCDESTROY:
                {
                    OnNcDestroy(ref msg);
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
                case WM.DESTROY:
                {
                    OnDestroy(ref msg);
                    break;
                }
                case WM.MOUSELEAVE:
                {
                    OnMouseLeave(ref msg);
                    break;
                }
                case WM.NCACTIVATE:
                {
                    MessageDecoder.ProcessNcActivate(ref msg, OnNcActivate);
                    break;
                }
                case WM.NCCALCSIZE:
                {
                    MessageDecoder.ProcessNcCalcSize(ref msg, OnNcCalcSize);
                    break;
                }
                case WM.SHOWWINDOW:
                {
                    MessageDecoder.ProcessShowWindow(ref msg, OnShow);
                    break;
                }
                case WM.QUIT:
                {
                    MessageDecoder.ProcessQuit(ref msg, OnQuit);
                    break;
                }
                case WM.CREATE:
                {
                    MessageDecoder.ProcessCreate(ref msg, OnCreate);
                    break;
                }
                case WM.SIZE:
                {
                    MessageDecoder.ProcessSize(ref msg, OnSize);
                    break;
                }
                case WM.MOVE:
                {
                    MessageDecoder.ProcessMove(ref msg, OnMove);
                    break;
                }
                case WM.WINDOWPOSCHANGED:
                {
                    MessageDecoder.ProcessWindowPositionChange(ref msg, OnPositionChanged);
                    break;
                }
                case WM.WINDOWPOSCHANGING:
                {
                    MessageDecoder.ProcessWindowPositionChange(ref msg, OnPositionChanging);
                    break;
                }
                case WM.ACTIVATE:
                {
                    MessageDecoder.ProcessActivate(ref msg, OnActivate);
                    break;
                }
                case WM.ERASEBKGND:
                {
                    MessageDecoder.ProcessEraseBkgnd(ref msg, OnEraseBkgnd);
                    break;
                }
                case WM.ACTIVATEAPP:
                {
                    MessageDecoder.ProcessActivateApp(ref msg, OnActivateApp);
                    break;
                }
                case WM.DISPLAYCHANGE:
                {
                    MessageDecoder.ProcessDisplayChange(ref msg, OnDisplayChange);
                    break;
                }
                case WM.MOUSEMOVE:
                {
                    MessageDecoder.ProcessMouseMove(ref msg, OnMouseMove);
                    break;
                }
                case WM.LBUTTONUP:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.LBUTTONDOWN:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.LBUTTONDBLCLK:
                {
                    MessageDecoder.ProcessMouseDoubleClick(ref msg, OnMouseDoubleClick);
                    break;
                }
                case WM.RBUTTONUP:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.RBUTTONDOWN:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.RBUTTONDBLCLK:
                {
                    MessageDecoder.ProcessMouseDoubleClick(ref msg, OnMouseDoubleClick);
                    break;
                }
                case WM.MBUTTONUP:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.MBUTTONDOWN:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.MBUTTONDBLCLK:
                {
                    MessageDecoder.ProcessMouseDoubleClick(ref msg, OnMouseDoubleClick);
                    break;
                }
                case WM.XBUTTONUP:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.XBUTTONDOWN:
                {
                    MessageDecoder.ProcessMouseButton(ref msg, OnMouseButton);
                    break;
                }
                case WM.XBUTTONDBLCLK:
                {
                    MessageDecoder.ProcessMouseDoubleClick(ref msg, OnMouseDoubleClick);
                    break;
                }
                case WM.MOUSEACTIVATE:
                {
                    MessageDecoder.ProcessMouseActivate(ref msg, OnMouseActivate);
                    break;
                }
                case WM.MOUSEHOVER:
                {
                    MessageDecoder.ProcessMouseHover(ref msg, OnMouseHover);
                    break;
                }
                case WM.MOUSEWHEEL:
                {
                    MessageDecoder.ProcessMouseWheel(ref msg, OnMouseWheel);
                    break;
                }
                case WM.MOUSEHWHEEL:
                {
                    MessageDecoder.ProcessMouseWheel(ref msg, OnMouseWheel);
                    break;
                }
                case WM.CHAR:
                {
                    MessageDecoder.ProcessKeyChar(ref msg, OnKeyChar);
                    break;
                }
                case WM.SYSCHAR:
                {
                    MessageDecoder.ProcessKeyChar(ref msg, OnKeyChar);
                    break;
                }
                case WM.DEADCHAR:
                {
                    MessageDecoder.ProcessKeyChar(ref msg, OnKeyChar);
                    break;
                }
                case WM.SYSDEADCHAR:
                {
                    MessageDecoder.ProcessKeyChar(ref msg, OnKeyChar);
                    break;
                }
                case WM.KEYUP:
                {
                    MessageDecoder.ProcessKey(ref msg, OnKey);
                    break;
                }
                case WM.KEYDOWN:
                {
                    MessageDecoder.ProcessKey(ref msg, OnKey);
                    break;
                }
                case WM.SYSKEYUP:
                {
                    MessageDecoder.ProcessKey(ref msg, OnKey);
                    break;
                }
                case WM.SYSKEYDOWN:
                {
                    MessageDecoder.ProcessKey(ref msg, OnKey);
                    break;
                }
                case WM.COMMAND:
                {
                    MessageDecoder.ProcessCommand(ref msg, OnCommand);
                    break;
                }
                case WM.SYSCOMMAND:
                {
                    MessageDecoder.ProcessSysCommand(ref msg, OnSysCommand);
                    break;
                }
                case WM.MENUCOMMAND:
                {
                    MessageDecoder.ProcessMenuCommand(ref msg, OnMenuCommand);
                    break;
                }
                case WM.APPCOMMAND:
                {
                    MessageDecoder.ProcessAppCommand(ref msg, OnAppCommand);
                    break;
                }
                case WM.KILLFOCUS:
                {
                    MessageDecoder.ProcessFocus(ref msg, OnLostFocus);
                    break;
                }
                case WM.SETFOCUS:
                {
                    MessageDecoder.ProcessFocus(ref msg, OnGotFocus);
                    break;
                }
                case WM.CAPTURECHANGED:
                {
                    MessageDecoder.ProcessCaptureChanged(ref msg, OnInputCaptureChanged);
                    break;
                }
                case WM.NCHITTEST:
                {
                    MessageDecoder.ProcessHitTest(ref msg, OnHitTest);
                    break;
                }
                case WM.HOTKEY:
                {
                    MessageDecoder.ProcessHotKey(ref msg, OnHotKey);
                    break;
                }
                case WM.GETMINMAXINFO:
                {
                    MessageDecoder.ProcessGetMinMaxInfo(ref msg, OnMinMaxInfo);
                    break;
                }
                case WM.NCPAINT:
                {
                    MessageDecoder.ProcessNcPaint(ref msg, OnNcPaint);
                    break;
                }
                default:
                {
                    OnMessageDefault(ref msg);
                    return;
                }
            }
        }

        private int OnMessageDefaultAndGetAsInt(ref WindowMessage msg)
        {
            OnMessageDefault(ref msg);
            return msg.Result.ToSafeInt32();
        }

        protected virtual void OnPositionChanged(ref WindowMessage msg, ref WindowPosition windowPosition)
            => OnMessageDefault(ref msg);

        protected virtual void OnPositionChanging(ref WindowMessage msg, ref WindowPosition windowPosition)
            => OnMessageDefault(ref msg);

        protected virtual void OnClose(ref WindowMessage msg) =>
            OnMessageDefault(ref msg);

        protected virtual EraseBackgroundResult OnEraseBkgnd(ref WindowMessage msg, IntPtr cHdc)
            => (EraseBackgroundResult) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual void OnMinMaxInfo(ref WindowMessage msg, ref MinMaxInfo minmaxinfo) =>
            OnMessageDefault(ref msg);

        protected virtual void OnDestroy(ref WindowMessage msg) =>
            OnMessageDefault(ref msg);

        protected virtual void OnSystemTimeChange(ref WindowMessage msg) =>
            OnMessageDefault(ref msg);

        protected virtual void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMove(ref WindowMessage msg, ref Point point) =>
            OnMessageDefault(ref msg);

        protected virtual CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
            => (CreateWindowResult) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
                IntPtr oppositeHwnd) =>
            OnMessageDefault(ref msg);

        protected virtual void OnPaint(ref WindowMessage msg, IntPtr cHdc) =>
            OnMessageDefault(ref msg);

        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) =>
            OnMessageDefault(ref msg);

        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, uint oppositeThreadId) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) =>
            OnMessageDefault(ref msg);

        protected virtual HitTestResult OnHitTest(ref WindowMessage msg, ref Point point)
            => (HitTestResult) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual MouseActivationResult OnMouseActivate(ref WindowMessage msg, IntPtr activeTopLevelParentHwnd,
                ushort messageId, HitTestResult hitTestResult)
            => (MouseActivationResult) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual void OnMouseWheel(ref WindowMessage msg, ref Point point, short wheelDelta,
                bool isWheelHorizontal, MouseInputKeyStateFlags flags) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMouseLeave(ref WindowMessage msg) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMouseHover(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) =>
            OnMessageDefault(ref msg);

        protected virtual void OnInputCaptureChanged(ref WindowMessage msg, IntPtr handleOfWindowReceivingCapture) =>
            OnMessageDefault(ref msg);

        protected virtual void OnGotFocus(ref WindowMessage msg, IntPtr oppositeHwnd) =>
            OnMessageDefault(ref msg);

        protected virtual void OnLostFocus(ref WindowMessage msg, IntPtr oppositeHwnd) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMenuCommand(ref WindowMessage msg, int menuIndex, IntPtr menuHandle) =>
            OnMessageDefault(ref msg);

        protected virtual void OnSysCommand(ref WindowMessage msg, SysCommand cmd, short mouseCursorX,
                short mouseCursorYOrKeyMnemonic) =>
            OnMessageDefault(ref msg);

        protected virtual void OnCommand(ref WindowMessage msg, CommandSource cmdSource, short id, IntPtr hWnd) =>
            OnMessageDefault(ref msg);

        protected virtual void OnKey(ref WindowMessage msg, VirtualKey key, bool isKeyUp,
                KeyboardInputState inputState, bool isSystemContext) =>
            OnMessageDefault(ref msg);

        protected virtual void OnKeyChar(ref WindowMessage msg, char inputChar, KeyboardInputState inputState,
                bool isSystemContext, bool isDeadChar) =>
            OnMessageDefault(ref msg);

        protected virtual void OnHotKey(ref WindowMessage msg, VirtualKey key, HotKeyInputState keyState,
                ScreenshotHotKey screenshotHotKey) =>
            OnMessageDefault(ref msg);

        protected virtual AppCommandResult OnAppCommand(ref WindowMessage msg, AppCommand cmd, AppCommandDevice device,
                KeyboardInputState keyState, IntPtr hwnd)
            => (AppCommandResult) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual void OnMouseButton(ref WindowMessage msg, ref Point point, MouseButton button,
                bool inputKeyState, MouseInputKeyStateFlags mouseInputKeyState) =>
            OnMessageDefault(ref msg);

        protected virtual WindowViewRegionFlags OnNcCalcSize(ref WindowMessage msg, bool shouldCalcValidRects,
                ref NcCalcSizeParams ncCalcSizeParams)
            => (WindowViewRegionFlags) OnMessageDefaultAndGetAsInt(ref msg);

        protected virtual void OnShow(ref WindowMessage msg, bool isShown, ShowWindowStatusFlags flags)
            => OnMessageDefault(ref msg);

        protected virtual void OnQuit(ref WindowMessage msg, int code) =>
            OnMessageDefault(ref msg);

        protected virtual void OnNcActivate(ref WindowMessage msg, bool isActive,
            ref IntPtr updateRegion) => OnMessageDefault(ref msg);

        protected virtual void OnNcDestroy(ref WindowMessage msg) =>
            OnMessageDefault(ref msg);

        protected virtual void OnNcPaint(ref WindowMessage msg, ref IntPtr updateregion) =>
            OnMessageDefault(ref msg);

        protected virtual void OnMouseDoubleClick(ref WindowMessage msg, ref Point point, MouseButton button,
                MouseInputKeyStateFlags mouseInputKeyState) =>
            OnMessageDefault(ref msg);
    }

    public sealed class EventedWindow : EventedWindowCore {}
}