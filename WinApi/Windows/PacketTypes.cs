using System;
using System.Runtime.InteropServices;
using WinApi.Core;
using WinApi.Extensions;
using WinApi.User32;

namespace WinApi.Windows
{
    public struct Packet
    {
        public unsafe WindowMessage* Message;
        public unsafe Packet(WindowMessage* message)
        {
            this.Message = message;
        }
    }
    public struct SizePacket
    {
        public unsafe WindowMessage* Message;
        public unsafe SizePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe WindowSizeFlag Flag
        {
            get { return (WindowSizeFlag)Message->WParam.ToSafeInt32(); }
            set { Message->WParam = new IntPtr((int)value); }
        }

        public unsafe Size Size
        {
            get
            {
                Size size;
                Message->LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                return size;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }
    }
    public struct MovePacket
    {
        public unsafe WindowMessage* Message;
        public unsafe MovePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }
    }
    public struct PaintPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe PaintPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd
        {
            get { return Message->Hwnd; }
            set { Message->Hwnd = value; }
        }
    
        public unsafe IntPtr Hdc
        {
            get
            {
                return this.Message->WParam;
            }
            set
            {
                this.Message->WParam = value;
            }
        }
    }
    public struct MinMaxInfoPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe MinMaxInfoPacket(WindowMessage* message)
        {
            this.Message = message;
        }
        public unsafe ref MinMaxInfo Info => ref ((MinMaxInfoWrapper*)Message->LParam)->Value;

    }
    public struct EraseBkgndPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe EraseBkgndPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hdc
        {
            get
            {
                return this.Message->WParam;
            }
            set
            {
                this.Message->WParam = value;
            }
        }

        public unsafe EraseBackgroundResult Result
        {
            get
            {
                return (EraseBackgroundResult)this.Message->Result.ToSafeInt32();
            }
            set
            {
                this.Message->Result = new IntPtr((int)value);
            }
        }
    }
    public struct WindowPositionPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe WindowPositionPacket(WindowMessage* message)
        {
            this.Message = message;
        }
        public unsafe ref WindowPosition Position => ref ((WindowPositionWrapper*)Message->LParam)->Value;
    }
    public struct ShowWindowPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe ShowWindowPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe ShowWindowStatusFlags Flags
        {
            get { return (ShowWindowStatusFlags)Message->LParam.ToSafeInt32(); }
            set { Message->LParam = new IntPtr((int)value); }
        }

        public unsafe bool IsShown
        {
            get
            {
                return this.Message->WParam.ToSafeInt32() > 0;
            }
            set
            {
                this.Message->WParam = (IntPtr)(value ? 1 : 0);
            }
        }
    }
    public struct QuitPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe QuitPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe int Code
        {
            get
            {
                return this.Message->WParam.ToSafeInt32();
            }
            set
            {
                this.Message->WParam = (IntPtr)(value);
            }
        }
    }
    public struct CreateWindowPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe CreateWindowPacket(WindowMessage* message)
        {
            this.Message = message;
        }
        public unsafe ref CreateStruct CreateStruct => ref ((CreateStructWrapper*)Message->LParam)->Value;

        // Return 0 to continue creation. -1 to destroy and prevent
        public unsafe CreateWindowResult Result
        {
            get { return (CreateWindowResult)Message->Result.ToSafeInt32(); }
            set { Message->Result = new IntPtr((int)value); }
        }
    }
    public struct ActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe ActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr ConverseHwnd
        {
            get
            {
                return this.Message->LParam;
            }
            set
            {
                this.Message->LParam = value;
            }
        }

        public unsafe bool IsMinimized
        {
            get
            {
                return GetWParamAsInt().High() != 0;
            }
            set
            {
                this.Message->WParam = new IntPtr(GetWParamAsInt().WithHigh((short)(value ? 1 : 0)));
            }
        }

        public unsafe WindowActivateFlag Flag
        {
            get { return (WindowActivateFlag)GetWParamAsInt().LowAsInt(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithLow((short)value)); }
        }


        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }
    }
    public struct ActivateAppPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe ActivateAppPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr ConverseHwnd
        {
            get
            {
                return this.Message->LParam;
            }
            set
            {
                this.Message->LParam = value;
            }
        }

        public unsafe bool IsActive
        {
            get
            {
                return this.Message->WParam.ToSafeInt32() != 0;
            }
            set
            {
                this.Message->WParam = new IntPtr(value ? 1 : 0);
            }
        }
    }
    public struct KeyCharPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe KeyCharPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe char Key
        {
            get { return (char)Message->WParam.ToSafeInt32(); }
            set { Message->WParam = new IntPtr((int)value); }
        }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState((uint)Message->LParam.ToSafeInt32()); }
            set { Message->LParam = new IntPtr(value.Value); }
        }

        public unsafe bool IsDeadChar
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.DEADCHAR) || (id == WM.SYSDEADCHAR);
            }
            set
            {
                this.Message->Id = value ?
                    (IsSystemContext ? WM.SYSDEADCHAR : WM.DEADCHAR) :
                    (IsSystemContext ? WM.SYSCHAR : WM.CHAR);
            }
        }

        public unsafe bool IsSystemContext
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.SYSCHAR) || (id == WM.SYSDEADCHAR);
            }
            set
            {
                this.Message->Id = value ?
                    (IsDeadChar ? WM.SYSDEADCHAR : WM.SYSCHAR) :
                    (IsDeadChar ? WM.DEADCHAR : WM.CHAR);
            }
        }
    }
    public struct KeyPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe KeyPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe VirtualKey Key
        {
            get { return (VirtualKey)Message->WParam.ToSafeInt32(); }
            set { Message->WParam = new IntPtr((int)value); }
        }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState((uint)Message->LParam.ToSafeInt32()); }
            set { Message->LParam = new IntPtr(value.Value); }
        }

        public unsafe bool IsKeyDown
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.KEYDOWN) || (id == WM.SYSKEYDOWN);
            }
            set
            {
                this.Message->Id = value ?
                    (IsSystemContext ? WM.SYSKEYDOWN : WM.KEYDOWN) :
                    (IsSystemContext ? WM.SYSKEYUP : WM.KEYUP);
            }
        }

        public unsafe bool IsSystemContext
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.SYSKEYDOWN) || (id == WM.SYSKEYUP);
            }
            set
            {
                this.Message->Id = value ?
                    (IsKeyDown ? WM.SYSKEYDOWN : WM.SYSKEYUP) :
                    (IsKeyDown ? WM.KEYDOWN : WM.KEYUP);
            }
        }
    }
    public struct MousePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MousePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }

        public unsafe MouseInputKeyStateFlags Flag
        {
            get { return (MouseInputKeyStateFlags)GetWParamAsInt(); }
            set { Message->WParam = new IntPtr((int)value); }
        }

        private unsafe int GetWParamAsInt()
        {
            return Message->WParam.ToSafeInt32();
        }
    }
    public struct MouseDoubleClickPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseDoubleClickPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetWParamAsInt()
        {
            return Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }

        public unsafe MouseButton Button
        {
            get { return GetButton(); }
            set { SetButton(value); }
        }

        public unsafe MouseInputKeyStateFlags Flag
        {
            get { return (MouseInputKeyStateFlags)GetWParamAsInt().LowAsInt(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithLow((short)value)); }
        }

        public unsafe MouseButtonResult Result
        {
            get { return (MouseButtonResult)Message->Result; }
            set { Message->Result = new IntPtr((int)value); }
        }

        private unsafe void SetButton(MouseButton value)
        {
            switch (value)
            {
                case MouseButton.Left:
                    Message->Id = WM.LBUTTONDBLCLK;
                    return;
                case MouseButton.Right:
                    Message->Id = WM.RBUTTONDBLCLK;
                    return;
                case MouseButton.Middle:
                    Message->Id = WM.MBUTTONDBLCLK;
                    return;
                default:
                    Message->Id = WM.XBUTTONDBLCLK;
                    Message->WParam = new IntPtr(GetWParamAsInt().WithHigh((short)value));
                    return;
            }
        }

        private unsafe MouseButton GetButton()
        {
            var id = Message->Id;
            switch (id)
            {
                case WM.LBUTTONDBLCLK:
                    return MouseButton.Left;
                case WM.RBUTTONDBLCLK:
                    return MouseButton.Right;
                case WM.MBUTTONDBLCLK:
                    return MouseButton.Middle;
                default:
                    {
                        return (MouseInputXButtonFlag)GetWParamAsInt().HighAsInt()
                            == MouseInputXButtonFlag.XBUTTON1
                                ? MouseButton.XButton1
                                : MouseButton.XButton2;
                    }
            }
        }
    }
    public struct MouseButtonPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseButtonPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetWParamAsInt()
        {
            return Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }

        public unsafe MouseButton Button
        {
            get { return GetButton(); }
            set { SetButton(value); }
        }

        public unsafe bool IsButtonDown
        {
            get
            {
                var id = this.Message->Id;
                // Unfortunately, there's no better way than to do a full check here, since the numerical
                // values don't have any valid pattern to do it in one-go.
                return id == WM.LBUTTONDOWN || id == WM.RBUTTONDOWN || id == WM.MBUTTONDOWN || id == WM.XBUTTONDOWN;
            }

            set
            {
                var button = GetButton();
                switch (button)
                {
                    case MouseButton.Left:
                        this.Message->Id = value ? WM.LBUTTONDOWN : WM.LBUTTONUP;
                        return;
                    case MouseButton.Right:
                        this.Message->Id = value ? WM.RBUTTONDOWN : WM.RBUTTONUP;
                        return;
                    case MouseButton.Middle:
                        this.Message->Id = value ? WM.MBUTTONDOWN : WM.MBUTTONUP;
                        return;
                    default:
                        this.Message->Id = value ? WM.XBUTTONDOWN : WM.XBUTTONUP;
                        return;
                }
            }
        }

        public unsafe MouseInputKeyStateFlags Flag
        {
            get { return (MouseInputKeyStateFlags)GetWParamAsInt().LowAsInt(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithLow((short)value)); }
        }

        public unsafe MouseButtonResult Result
        {
            get { return (MouseButtonResult)Message->Result; }
            set { Message->Result = new IntPtr((int)value); }
        }

        private unsafe void SetButton(MouseButton value)
        {
            switch (value)
            {
                case MouseButton.Left:
                    Message->Id = IsButtonDown ? WM.LBUTTONDOWN : WM.LBUTTONUP;
                    return;
                case MouseButton.Right:
                    Message->Id = IsButtonDown ? WM.RBUTTONDOWN : WM.RBUTTONUP;
                    return;
                case MouseButton.Middle:
                    Message->Id = IsButtonDown ? WM.MBUTTONDOWN : WM.MBUTTONUP;
                    return;
                default:
                    Message->Id = IsButtonDown ? WM.XBUTTONDOWN : WM.XBUTTONUP;
                    Message->WParam = new IntPtr(GetWParamAsInt().WithHigh((short)value));
                    return;
            }
        }
        private unsafe MouseButton GetButton()
        {
            var id = (int)Message->Id;
            // Unfortunately, there's no better way than to do a full check here, since the numerical
            // values don't have any valid pattern to do it in one-go.
            if ((id > 0x200) && (id < 0x204))
            {
                return MouseButton.Left;
            }
            else if ((id > 0x203) && (id < 0x207))
            {
                return MouseButton.Right;
            }
            else if ((id > 0x206) && (id < 0x210))
            {
                return MouseButton.Middle;
            }
            else
            {
                return (MouseInputXButtonFlag)GetWParamAsInt().HighAsInt() == MouseInputXButtonFlag.XBUTTON1
                    ? MouseButton.XButton1
                    : MouseButton.XButton2;
            }
        }
    }
    public struct MouseActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetLParamAsInt()
        {
            return Message->LParam.ToSafeInt32();
        }

        public unsafe IntPtr TopLevelActiveParentHwnd
        {
            get
            {
                return this.Message->WParam;
            }
            set
            {
                this.Message->WParam = value;
            }
        }

        public unsafe HitTestResult HitTestResult
        {
            get { return (HitTestResult)GetLParamAsInt().LowAsInt(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithLow(unchecked((short)value))); }
        }

        public unsafe ushort MouseMessageId
        {
            get { return (ushort)GetLParamAsInt().High(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithHigh(unchecked((short)value))); }
        }


        public unsafe MouseActivationResult Result
        {
            get
            {
                return (MouseActivationResult)this.Message->Result;
            }
            set
            {
                this.Message->Result = new IntPtr((int)value);
            }
        }
    }
    public struct MouseWheelPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseWheelPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetWParamAsInt()
        {
            return Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }

        public unsafe MouseInputKeyStateFlags Flag
        {
            get { return (MouseInputKeyStateFlags)GetWParamAsInt().LowAsInt(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithLow((short)value)); }
        }

        // Multiple or divisons of (WHEEL_DELTA = 120)
        public unsafe short WheelDelta
        {
            get { return (short)GetWParamAsInt().High(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithHigh(unchecked(value))); }
        }

        public unsafe bool IsVertical
        {
            get
            {
                return this.Message->Id == WM.MOUSEWHEEL;
            }
            set
            {
                this.Message->Id = value ? WM.MOUSEWHEEL : WM.MOUSEHWHEEL;
            }
        }

    }
    public struct DisplayChangePacket
    {
        public unsafe WindowMessage* Message;
        public unsafe DisplayChangePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe uint ImageDepthAsBitsPerPixel
        {
            get { return (uint)Message->WParam.ToPointer(); }
            set { Message->WParam = new IntPtr(unchecked((int)value)); }
        }

        public unsafe Size Size
        {
            get
            {
                Size size;
                Message->LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                return size;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }
    }
    public struct CommandPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe CommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd
        {
            get
            {
                return this.Message->LParam;
            }
            set
            {
                this.Message->LParam = value;
            }
        }
        public unsafe short Id
        {
            get { return GetWParamAsInt().Low(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithLow(value)); }
        }

        public unsafe CommandSource CommandSource
        {
            get { return (CommandSource)GetWParamAsInt().HighAsInt(); }
            set { Message->WParam = new IntPtr(GetWParamAsInt().WithHigh((short)value)); }
        }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }
    }
    public struct SysCommandPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe SysCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe short X
        {
            get { return GetLParamAsInt().Low(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithLow(value)); }
        }

        public unsafe short Y
        {
            get { return GetLParamAsInt().High(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithHigh(value)); }
        }

        public unsafe SysCommand Command
        {
            get { return (SysCommand)this.Message->WParam.ToSafeInt32(); }
            set { Message->WParam = new IntPtr((int)value); }
        }

        public unsafe bool IsAccelerator
        {
            get { return Y == -1; }
            set { Y = (short)(value ? -1 : 0); }
        }

        public unsafe bool IsMnemonic
        {
            get { return GetLParamAsInt() == 0; }
            set { Message->LParam = IntPtr.Zero; }
        }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }
    }
    public struct MenuCommandPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe MenuCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe int MenuIndex
        {
            get { return this.Message->WParam.ToSafeInt32(); }
            set { Message->WParam = new IntPtr(value); }
        }

        public unsafe IntPtr MenuHandle
        {
            get { return this.Message->LParam; }
            set { Message->LParam = value; }
        }
    }
    public struct AppCommandPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe AppCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd
        {
            get { return this.Message->WParam; }
            set { Message->WParam = value; }
        }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState((uint)GetLParamAsInt().LowAsInt()); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithLow((short)value.Value)); }
        }

        public unsafe AppCommand Command
        {
            //GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
            get { return (AppCommand)(GetLParamAsInt().HighAsInt() & ~(uint)AppCommandDevice.FAPPCOMMAND_MASK); }
            set
            {
                var high16 = GetLParamAsInt().High();
                var mask = ~(uint)AppCommandDevice.FAPPCOMMAND_MASK;
                var final = high16 & ~mask | (int)value & mask;
                Message->LParam = new IntPtr(final);
            }
        }

        public unsafe AppCommandDevice Device
        {
            //GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
            get { return (AppCommandDevice)(GetLParamAsInt().HighAsInt() & (uint)AppCommandDevice.FAPPCOMMAND_MASK); }
            set
            {
                var high16 = GetLParamAsInt().High();
                var mask = (uint)AppCommandDevice.FAPPCOMMAND_MASK;
                var final = high16 & ~mask | (int)value & mask;
                Message->LParam = new IntPtr(final);
            }
        }

        public unsafe AppCommandResult Result
        {
            get { return (AppCommandResult)Message->Result; }
            set { Message->Result = new IntPtr((int)value); }
        }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }
    }
    public struct CaptureChangedPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe CaptureChangedPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr HwndReceivingCapture
        {
            get { return Message->LParam; }
            set { Message->LParam = value; }
        }
    }
    public struct FocusPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe FocusPacket(WindowMessage* message)
        {
            this.Message = message;
        }
        public unsafe IntPtr ConverseHwnd
        {
            get { return Message->WParam; }
            set { Message->WParam = value; }
        }
    }
    public struct HotKeyPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe HotKeyPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }

        public unsafe ScreenshotHotKey ScreenshotHotKey
        {
            get { return (ScreenshotHotKey)Message->WParam.ToSafeInt32(); }
            set { Message->LParam = new IntPtr((int)value); }
        }

        public unsafe HotKeyInputState KeyState
        {
            get { return (HotKeyInputState)GetLParamAsInt().Low(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithLow((short)value)); }
        }

        public unsafe VirtualKey Key
        {
            get { return (VirtualKey)GetLParamAsInt().High(); }
            set { Message->LParam = new IntPtr(GetLParamAsInt().WithHigh((short)value)); }
        }
    }
    public struct HitTestPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe HitTestPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set
            {
                Message->LParam = new IntPtr(value.ToInt32());
            }
        }

        public unsafe HitTestResult Result
        {
            get { return (HitTestResult)Message->Result; }
            set { Message->Result = new IntPtr((int)value); }
        }
    }
    public struct NcPaintPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcPaintPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr UpdateRegion
        {
            get
            {
                return this.Message->WParam;
            }
            set
            {
                this.Message->WParam = value;
            }
        }
    }
    public struct NcActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe bool IsActive
        {
            get
            {
                return this.Message->WParam.ToSafeInt32() > 0;
            }
            set
            {
                this.Message->WParam = (IntPtr)(value ? 1 : 0);
            }
        }

        // lParam is used only if visual styles are disabled
        public unsafe IntPtr UpdateRegion
        {
            get
            {
                return this.Message->LParam;
            }
            set
            {
                this.Message->LParam = value;
            }
        }

        public void PreventRegionUpdate()
        {
            // To prevent Nc region update in DefWndProc, set (updateRegion)LParam = -1;
            UpdateRegion = new IntPtr(-1);
        }

        // When wParam == TRUE, result is ignored
        public bool CanPreventDefault => !this.IsActive;

        public unsafe NcActivateResult Result
        {
            get
            {
                return (NcActivateResult)this.Message->Result.ToSafeInt32();
            }
            set
            {
                this.Message->Result = new IntPtr((int)value);
            }
        }

        public enum NcActivateResult
        {
            // var result = TRUE // Default processing;
            // var result = FALSE // Prevent changes.
            Default = 1,
            PreventDefault = 0,
        }
    }
    public struct NcCalcSizePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcCalcSizePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe bool ShouldCalcValidRects
        {
            get { return Message->WParam.ToSafeInt32() > 0; }
            set { Message->WParam = (IntPtr)(value ? 1 : 0); }
        }

        public unsafe ref NcCalcSizeParams Params => ref ((NcCalcSizeParamsWrapper*)this.Message->LParam)->Value;

        public unsafe WindowViewRegionFlags Result
        {
            get { return (WindowViewRegionFlags)Message->Result.ToSafeInt32(); }
            set { Message->Result = new IntPtr((int)value); }
        }
    }

    #region Support Types

    public enum MouseButton
    {
        Left = 0x1,
        Right = 0x2,
        Middle = 0x4,
        Other = 0x8,
        XButton1 = 0x10 | Other,
        XButton2 = 0x20 | Other
    }
    public enum MouseButtonResult
    {
        Default = 0,
        // Required to be set when application that processes XButton in order help simulation softwares
        // determine if the result was processed by application or DefWndProc
        Handled = 1,
    }
    public enum EraseBackgroundResult
    {
        // 1 - prevent default erase.
        // 0 - Let DefWndProc erase the background with the window class's brush.
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

    #endregion

    #region Ref-Wrapper 

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowMessageWrapper
    {
        public WindowMessage Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPositionWrapper
    {
        public WindowPosition Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CreateStructWrapper
    {
        public CreateStruct Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MinMaxInfoWrapper
    {
        public MinMaxInfo Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NcCalcSizeParamsWrapper
    {
        public NcCalcSizeParams Value;
    }

    #endregion
}