using System;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.Windows
{
    public interface IWindowMessageProcessor
    {
        void ProcessMessage(ref WindowMessage message);
    }

    /// <summary>
    ///     Derives from the WindowCore, and provides all the life cycle, and input events.
    ///     It doesn't really handle them in any way but just provides the events with the
    ///     correct decoded parameters. All the processing and decoding are done
    ///     transparently inside the `MessageDecoder` class.
    ///     If in certain high-performance requirements, you need only a few events, this
    ///     can be manually implemented only for the events required using the `MessageDecoder`
    ///     class. All of the parameter decoding are transparent.
    /// </summary>
    public abstract class EventedWindowCore : WindowCore, IWindowMessageProcessor
    {
        void IWindowMessageProcessor.ProcessMessage(ref WindowMessage msg)
        {
            this.OnMessage(ref msg);
        }

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
                    MessageDecoder.ProcessLostFocus(ref msg, OnLostFocus);
                    break;
                }
                case WM.SETFOCUS:
                {
                    MessageDecoder.ProcessGotFocus(ref msg, OnGotFocus);
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
//                default:
//                {
//                    base.OnMessage(ref msg);
//                    return;
//                }
            }

            // TODO: Uncomment the above default case and remove this after default processing code path
            // has been implemented for all the cases above.
            base.OnMessage(ref msg);
        }

        protected virtual void OnClose(ref WindowMessage msg) {}
        protected virtual EraseBackgroundResult OnEraseBkgnd(ref WindowMessage msg, IntPtr cHdc) => 0;
        protected virtual void OnMinMaxInfo(ref WindowMessage msg, ref MinMaxInfo minmaxinfo) {}
        protected virtual void OnDestroy(ref WindowMessage msg) {}
        protected virtual void OnSystemTimeChange(ref WindowMessage msg) {}
        protected virtual void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size) {}
        protected virtual void OnMove(ref WindowMessage msg, ref Point size) {}
        protected virtual CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct) => 0;

        protected virtual void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeHwnd) {}

        protected virtual void OnPaint(ref WindowMessage msg, IntPtr cHdc) {}
        protected virtual void OnDisplayChange(ref WindowMessage msg, uint imageDepthBitsPerPixel, ref Size size) {}
        protected virtual void OnActivateApp(ref WindowMessage msg, bool isActive, uint oppositeThreadId) {}
        protected virtual void OnMouseMove(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) {}
        protected virtual HitTestResult OnHitTest(ref WindowMessage msg, ref Point point) => 0;

        protected virtual MouseActivationResult OnMouseActivate(ref WindowMessage msg, IntPtr activeTopLevelParentHwnd,
            ushort messageId, HitTestResult hitTestResult) => 0;

        protected virtual void OnMouseWheel(ref WindowMessage msg, ref Point point, short wheelDelta,
            bool isWheelHorizontal, MouseInputKeyStateFlags flags) {}

        protected virtual void OnMouseLeave(ref WindowMessage msg) {}
        protected virtual void OnMouseHover(ref WindowMessage msg, ref Point point, MouseInputKeyStateFlags flags) {}
        protected virtual void OnInputCaptureChanged(ref WindowMessage msg, IntPtr handleOfWindowReceivingCapture) {}
        protected virtual void OnGotFocus(ref WindowMessage msg, IntPtr oppositeHwnd) {}
        protected virtual void OnLostFocus(ref WindowMessage msg, IntPtr oppositeHwnd) {}
        protected virtual void OnMenuCommand(ref WindowMessage msg, int menuIndex, IntPtr menuHandle) {}

        protected virtual void OnSysCommand(ref WindowMessage msg, SysCommand cmd, short mouseCursorX,
            short mouseCursorYOrKeyMnemonic) {}

        protected virtual void OnCommand(ref WindowMessage msg, CommandSource cmdSource, short id, IntPtr hWnd) {}

        protected virtual void OnKey(ref WindowMessage msg, VirtualKey key, bool isKeyUp,
            KeyboardInputState inputState, bool isSystemContext) {}

        protected virtual void OnKeyChar(ref WindowMessage msg, char inputChar, KeyboardInputState inputState,
            bool isSystemContext, bool isDeadChar) {}

        protected virtual void OnHotKey(ref WindowMessage msg, VirtualKey key, HotKeyInputState keyState,
            ScreenshotHotKey screenshotHotKey) {}

        protected virtual AppCommandResult OnAppCommand(ref WindowMessage msg, AppCommand cmd, AppCommandDevice device,
            KeyboardInputState keyState, IntPtr hwnd) => 0;

        protected virtual void OnMouseButton(ref WindowMessage msg, ref Point point, MouseButton button,
            bool inputKeyState, MouseInputKeyStateFlags mouseInputKeyState) {}

        protected virtual WindowViewRegionFlags OnNcCalcSize(ref WindowMessage msg, bool shouldCalcValidRects,
            ref NcCalcSizeParams ncCalcSizeParams) => 0;

        protected virtual void OnShow(ref WindowMessage msg, bool isShown, ShowWindowStatusFlags flags) {}
        protected virtual void OnQuit(ref WindowMessage msg, int code) {}

        protected virtual NcActivationResult OnNcActivate(ref WindowMessage msg, bool isShown,
                IntPtr updateRegion)
            => new NcActivationResult();

        protected virtual void OnNcDestroy(ref WindowMessage msg) {}
        protected virtual void OnNcPaint(ref WindowMessage msg, IntPtr updateregion) {}

        protected virtual void OnMouseDoubleClick(ref WindowMessage msg, ref Point point, MouseButton button,
            MouseInputKeyStateFlags mouseInputKeyState) {}
    }

    public sealed class EventedWindow : EventedWindowCore {}
}